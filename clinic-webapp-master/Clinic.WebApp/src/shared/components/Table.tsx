import { useState } from "react";
import { useForm } from "react-hook-form";
import { Toaster, toast } from "sonner";
import { useFetch } from "../../hooks/useFetch";
import { PagedList } from "../../models/pagedList.model";
import { TableProps } from "../../models/props/table.props";
import { TableSorting } from "../../models/table.model";
import InputErrorMessage from "./InputErrorMessage";
import Loader from "./Loader";
import TableContent from "./TableContent";
import TableHeader from "./TableHeader";

function Table<T extends object>({
  title,
  columns,
  url,
  initialPageSize,
}: TableProps): React.JSX.Element {
  const [sorting, setSorting] = useState<TableSorting>({
    column: columns[0].key,
    direction: "asc",
  });

  const [page, setPage] = useState<number>(1);

  const [searchValue, setSearchValue] = useState<string>("");

  const {
    handleSubmit,
    register,
    formState: { errors },
    reset,
  } = useForm<{ name: string }>();

  const [items, isLoading, error] = useFetch<PagedList<T[]>>({
    url,
    sorting,
    page,
    searchValue,
    params: {
      page: `${page}`,
      pageSize: initialPageSize,
      sortColumn: sorting.column,
      sortOrder: sorting.direction,
    },
  });

  const handleNextPage = () => {
    if (!items?.data.hasNexPage) {
      toast.error("There is not more items left");
      return;
    }

    setPage((current) => current + 1);
  };

  const handlePreviousPage = () => {
    if (!items?.data.hasPreviousPage) {
      toast.error("There is not more previous items left");
      return;
    }

    setPage((current) => current - 1);
  };

  const onSubmit = handleSubmit((data) => {
    setSearchValue(data.name);
    reset();
    return;
  });

  const handleRefresh = () => {
    window.location.reload();
  };

  return (
    <>
      {isLoading && <Loader isLoading={isLoading} size={65} />}
      {!isLoading && (
        <div className="container d-flex flex-column justify-content-center align-items-center">
          <h1 className="display-2 mb-4">{title}</h1>
          <div className="col-lg-10">
            <form onSubmit={onSubmit}>
              <div className="mb-3">
                <input
                  type="text"
                  className="form-control"
                  placeholder="Search"
                  id="name"
                  {...register("name", {
                    required: {
                      value: true,
                      message: "The search value is required",
                    },
                  })}
                />
                {errors.name && (
                  <InputErrorMessage message={errors.name.message} />
                )}
              </div>
            </form>
            <table className="table table-striped table-hover table-bordered mt-4">
              <TableHeader
                columns={columns}
                setSorting={setSorting}
                sorting={sorting}
              />
              <TableContent columns={columns} items={items?.data.items ?? []} />
            </table>
            <button
              type="button"
              className="btn btn-outline-dark me-3"
              onClick={handleNextPage}
            >
              Next Page
            </button>
            <button
              type="button"
              className="btn btn-outline-dark me-3"
              onClick={handlePreviousPage}
            >
              Previous Page
            </button>
            <button
              type="button"
              className="btn btn-outline-dark"
              onClick={handleRefresh}
            >
              Reload
            </button>
          </div>
        </div>
      )}
      {error && (
        <>
          {(() => {
            toast.error(error.message);
            return;
          })()}
        </>
      )}
      <Toaster />
    </>
  );
}

export default Table;
