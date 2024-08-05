import { TableContentProps } from "../../models/props/table.props";

function TableContent<T extends object>({
  items,
  columns,
}: TableContentProps<T>): React.JSX.Element {
  return (
    <tbody className="table-group-divider">
      {items.map((item, index) => (
        <tr key={index}>
          {columns.map(({ key }, index) => (
            <td key={index}>{item[key as keyof object]}</td>
          ))}
        </tr>
      ))}
    </tbody>
  );
}

export default TableContent;
