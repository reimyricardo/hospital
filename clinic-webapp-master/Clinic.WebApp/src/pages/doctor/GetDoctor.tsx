import { Doctor } from "../../models/doctor.model";
import { TableColumn } from "../../models/table.model";
import Table from "../../shared/components/Table";
import { enviroments } from "../../shared/enviroments/enviroments.dev";

function GetDoctor(): React.JSX.Element {
  const columns: TableColumn[] = [
    { label: "Id", key: "doctorId" },
    { label: "Name", key: "name" },
    { label: "Telephone", key: "telephone" },
    { label: "NIF", key: "nif" },
    { label: "Social Number", key: "socialNumber" },
    { label: "Collegue Number", key: "collegueNumber" },
    { label: "Start Date", key: "startDate" },
    { label: "End Date", key: "endDate" },
    { label: "Position", key: "positionName" },
  ];

  return (
    <>
      <Table<Doctor>
        title="Doctor Table"
        columns={columns}
        url={enviroments.getAllDoctor}
        initialPageSize="10"
      />
    </>
  );
}

export default GetDoctor;
