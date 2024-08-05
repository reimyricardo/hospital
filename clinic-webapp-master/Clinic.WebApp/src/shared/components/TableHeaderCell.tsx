import { TableHeaderCellProps } from "../../models/props/table.props";

function TableHeaderCell({
  column: { label, key },
  sorting,
  setSorting,
}: TableHeaderCellProps): React.JSX.Element {
  const isDescSorting = sorting?.column === key && sorting.direction === "desc";

  const isAscSorting = sorting?.column === key && sorting.direction === "asc";

  const futureSortingDirection = isDescSorting ? "asc" : "desc";

  return (
    <th
      scope="col"
      onClick={() =>
        setSorting({ column: key, direction: futureSortingDirection })
      }
    >
      {label}
      {isDescSorting && <span>▼</span>}
      {isAscSorting && <span>▲</span>}
    </th>
  );
}

export default TableHeaderCell;
