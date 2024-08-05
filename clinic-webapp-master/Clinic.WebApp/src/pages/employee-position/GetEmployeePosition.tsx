import { EmployeePosition } from "../../models/employee.model";
import { TableColumn } from "../../models/table.model";
import Table from "../../shared/components/Table";
import { enviroments } from "../../shared/enviroments/enviroments.dev";

function GetEmployeePosition(): React.JSX.Element {
  const columns: TableColumn[] = [
    { label: "Position Name", key: "positionName" },
  ];

  return (
    <>
      <Table<EmployeePosition>
        title="Employee Position Table"
        columns={columns}
        url={enviroments.getAllEmployeePosition}
        initialPageSize="10"
      />
    </>
  );
}

export default GetEmployeePosition;
