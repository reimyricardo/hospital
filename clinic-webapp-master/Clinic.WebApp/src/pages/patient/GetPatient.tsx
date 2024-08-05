import { Patient } from "../../models/patient.model";
import { TableColumn } from "../../models/table.model";
import Table from "../../shared/components/Table";
import { enviroments } from "../../shared/enviroments/enviroments.dev";

function GetPatient(): React.JSX.Element {
  const columns: TableColumn[] = [
    { label: "Id", key: "patientId" },
    { label: "Name", key: "name" },
    { label: "Telephone", key: "telephone" },
    { label: "NIF", key: "nif" },
    { label: "Social Number", key: "socialNumber" },
  ];

  return (
    <>
      <Table<Patient>
        title="Patient Table"
        columns={columns}
        url={enviroments.getPatient}
        initialPageSize="10"
      />
    </>
  );
}

export default GetPatient;
