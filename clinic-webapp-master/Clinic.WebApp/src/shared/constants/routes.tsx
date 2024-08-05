import { Route, Routes } from "../../models/routes.model";
import CreateDoctorPosition from "../../pages/doctor-position/CreateDoctorPosition";
import GetDoctorPosition from "../../pages/doctor-position/GetDoctorPosition";
import CreateDoctor from "../../pages/doctor/CreateDoctor";
import DeleteDoctor from "../../pages/doctor/DeleteDoctor";
import GetDoctor from "../../pages/doctor/GetDoctor";
import UpdateDoctor from "../../pages/doctor/UpdateDoctor";
import Home from "../../pages/home/Home";
import CreatePatient from "../../pages/patient/CreatePatient";
import DeletePatient from "../../pages/patient/DeletePatient";
import GetPatient from "../../pages/patient/GetPatient";
import UpdatePatient from "../../pages/patient/UpdatePatient";
import NotFound from "../components/NotFound";
import CreateEmployee from "../../pages/employee/CreateEmployee";
import DeleteEmployee from "../../pages/employee/DeleteEmployee";
import CreateEmployeePosition from "../../pages/employee-position/CreateEmployeePosition";
import GetEmployeePosition from "../../pages/employee-position/GetEmployeePosition";
import CreatePersonAddress from "../../pages/person-address/CreatePersonAddress";
import DeletePersonAddress from "../../pages/person-address/DeletePersonAddress";
import UpdatePersonAddress from "../../pages/person-address/UpdatePersonAddress";
import GetPersonAddress from "../../pages/person-address/GetPersonAddress";

const router: Route[] = [
  { path: "/", element: <Home /> },
  { path: "/doctor/create", element: <CreateDoctor /> },
  { path: "/doctor/get", element: <GetDoctor /> },
  { path: "/doctor/update", element: <UpdateDoctor /> },
  { path: "/doctor/delete", element: <DeleteDoctor /> },
  { path: "/doctorposition/create", element: <CreateDoctorPosition /> },
  { path: "/doctorposition/get", element: <GetDoctorPosition /> },
  { path: "/patient/get", element: <GetPatient /> },
  { path: "/patient/create", element: <CreatePatient /> },
  { path: "/patient/update", element: <UpdatePatient /> },
  { path: "/patient/delete", element: <DeletePatient /> },
  { path: "/employee/create", element: <CreateEmployee /> },
  { path: "/employee/delete", element: <DeleteEmployee /> },
  { path: "/employeeposition/create", element: <CreateEmployeePosition /> },
  { path: "/employeeposition/get", element: <GetEmployeePosition /> },
  { path: "/personaddress/create", element: <CreatePersonAddress /> },
  { path: "/personaddress/delete", element: <DeletePersonAddress /> },
  { path: "/personaddress/update", element: <UpdatePersonAddress /> },
  { path: "/personaddress/get", element: <GetPersonAddress /> },
  { path: "*", element: <NotFound /> },
];

const routes: Routes = {
  Doctor: [
    {
      path: "/doctor/create",
      pathChildName: "Create",
    },
    {
      path: "/doctor/update",
      pathChildName: "Update",
    },
    {
      path: "/doctor/delete",
      pathChildName: "Delete",
    },
    {
      path: "/doctor/get",
      pathChildName: "Get",
    },
  ],
  DoctorPosition: [
    { path: "/doctorposition/create", pathChildName: "Create" },
    { path: "/doctorposition/get", pathChildName: "Get" },
  ],
  Patient: [
    {
      path: "/patient/create",
      pathChildName: "Create",
    },
    {
      path: "/patient/update",
      pathChildName: "Update",
    },
    {
      path: "/patient/delete",
      pathChildName: "Delete",
    },
    { path: "/patient/get", pathChildName: "Get" },
  ],
  Employee: [
    {
      path: "/employee/create",
      pathChildName: "Create",
    },
    {
      path: "/employee/delete",
      pathChildName: "Delete",
    },
  ],
  EmployeePosition: [
    {
      path: "/employeeposition/create",
      pathChildName: "Create",
    },
    {
      path: "/employeeposition/get",
      pathChildName: "Get",
    },
  ],
  PersonAddress: [
    {
      path: "/personaddress/create",
      pathChildName: "Create",
    },
    {
      path: "/personaddress/update",
      pathChildName: "Update",
    },
    /*{
      path: "/personaddress/delete",
      pathChildName: "Delete",
    }, */
    {
      path: "/personaddress/get",
      pathChildName: "Get",
    },
  ],
};

export { router, routes };
