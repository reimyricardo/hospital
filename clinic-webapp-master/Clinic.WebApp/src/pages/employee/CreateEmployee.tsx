import React from "react";
import { useForm } from "react-hook-form";
import { Toaster, toast } from "sonner";
import { useHttpMethod } from "../../hooks/useHttpMethod";
import { CreateEmployeeForm } from "../../models/input.model";
import Form from "../../shared/components/Form";
import InputErrorMessage from "../../shared/components/InputErrorMessage";
import { enviroments } from "../../shared/enviroments/enviroments.dev";
import { isHttpMethodHookError } from "../../shared/utils";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useFetch } from "../../hooks/useFetch";
import { PagedList } from "../../models/pagedList.model";
import { EmployeePosition } from "../../models/employee.model";
import Loader from "../../shared/components/Loader";

function CreateEmployee(): React.JSX.Element {
  const {
    handleSubmit,
    register,
    reset,
    setValue,
    watch,
    formState: { errors },
  } = useForm<CreateEmployeeForm>();

  const [position, isLoading, fetchError] = useFetch<
    PagedList<EmployeePosition[]>
  >({
    url: enviroments.getAllEmployeePosition,
    params: {
      page: "1",
      pageSize: "999",
    },
  });

  const { postData } = useHttpMethod<CreateEmployeeForm>({
    url: enviroments.createEmployee,
  });

  const onSubmit = handleSubmit(async (data) => {
    const response = await postData({ body: data, method: "POST" });

    if (isHttpMethodHookError(response)) {
      toast.error(response.message);
      return;
    } else {
      toast.success("Success");
      reset();
      return;
    }
  });

  return (
    <>
      <Toaster position="bottom-center" />
      {fetchError && (
        <>
          {(() => {
            toast.error(fetchError.message);
            return;
          })()}
        </>
      )}
      {isLoading && <Loader isLoading={isLoading} size={65} />}
      {!isLoading && (
        <Form title="Create Employee Form" onSubmit={onSubmit}>
          <div className="mb-3">
            <label htmlFor="name" className="form-label">
              Name
            </label>
            <input
              className="form-control"
              id="name"
              type="text"
              {...register("name", {
                required: {
                  value: true,
                  message: "The name is required",
                },
              })}
            />
            {errors.name && <InputErrorMessage message={errors.name.message} />}
          </div>
          <div className="mb-3">
            <label htmlFor="telephone" className="form-label">
              Telephone
            </label>
            <input
              type="text"
              className="form-control"
              id="telephone"
              {...register("telephone", {
                required: {
                  value: true,
                  message: "The telephone is required",
                },
                pattern: {
                  value: /^\(?\d{3}\)?[-.\s]?\d{3}[-.\s]?\d{4}$/,
                  message: "The telephone is invalid",
                },
              })}
            />
            {errors.telephone && (
              <InputErrorMessage message={errors.telephone.message} />
            )}
          </div>
          <div className="mb-3">
            <label htmlFor="nif" className="form-label">
              NIF
            </label>
            <input
              type="text"
              className="form-control"
              id="nif"
              {...register("nif", {
                required: {
                  value: true,
                  message: "The NIF is required",
                },
                pattern: {
                  value: /^\d{9}$/,
                  message: "The NIF is invalid",
                },
              })}
            />
            {errors.nif && <InputErrorMessage message={errors.nif.message} />}
          </div>
          <div className="mb-3">
            <label htmlFor="socialNumber" className="form-label">
              Social Number
            </label>
            <input
              type="number"
              className="form-control"
              id="socialNumber"
              {...register("socialNumber", {
                required: {
                  value: true,
                  message: "The Social Number is required",
                },
                pattern: {
                  value: /^\d{9}$/,
                  message: "The Social Number is invalid",
                },
              })}
            />
            {errors.socialNumber && (
              <InputErrorMessage message={errors.socialNumber.message} />
            )}
          </div>
          <div className="mb-3">
            <label
              htmlFor="datepicker"
              style={{ display: "block", marginBottom: 10 }}
            >
              Start Date
            </label>
            <DatePicker
              id="datepicker"
              selected={watch("startDate")}
              onChange={(date) => setValue("startDate", date)}
              className="form-control"
              dateFormat="yyyy-MM-dd"
              isClearable
            />
          </div>
          <div className="mb-3">
            <label htmlFor="employeePosition" className="form-label">
              Employee Position
            </label>
            <select
              id="employeePosition"
              {...register("employeePosition", {
                required: {
                  value: true,
                  message: "The employee position is required",
                },
              })}
              className="form-select"
            >
              {position?.data.items.map(({ positionName }, index) => {
                return <option key={index}>{positionName}</option>;
              })}
            </select>
            {errors.employeePosition && (
              <InputErrorMessage message={errors.employeePosition.message} />
            )}
          </div>
        </Form>
      )}
    </>
  );
}

export default CreateEmployee;
