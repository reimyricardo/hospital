import { PersonAddress } from "../../models/personAddress.model";
import { TableColumn } from "../../models/table.model";
import Table from "../../shared/components/Table";
import { enviroments } from "../../shared/enviroments/enviroments.dev";

function GetPersonAddress(): React.JSX.Element {
  const columns: TableColumn[] = [
    { label: "Address ID", key: "addressId" },
    { label: "Person Name", key: "personName" },
    { label: "Street Number", key: "streerNumber" },
    { label: "Address Line 1", key: "addressLine1" },
    { label: "Address Line 2", key: "addressLine2" },
    { label: "City", key: "city" },
    { label: "Population", key: "population" },
    { label: "Postal Code", key: "postalCode" },
    { label: "Province", key: "province" },
  ];

  return (
    <>
      <Table<PersonAddress>
        title="Person Address Table"
        columns={columns}
        url={enviroments.getAllPersonAddress}
        initialPageSize="10"
      />
    </>
  );
}

export default GetPersonAddress;
