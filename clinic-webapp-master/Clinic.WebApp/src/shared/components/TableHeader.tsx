import { TableHeaderProps } from "../../models/props/table.props";
import TableHeaderCell from "./TableHeaderCell";

function TableHeader({
  columns,
  setSorting,
  sorting,
}: TableHeaderProps): React.JSX.Element {
  return (
    <thead>
      <tr>
        {columns.map((column, index) => (
          <TableHeaderCell
            column={column}
            sorting={sorting}
            key={index}
            setSorting={setSorting}
          />
        ))}
      </tr>
    </thead>
  );
}

export default TableHeader;
