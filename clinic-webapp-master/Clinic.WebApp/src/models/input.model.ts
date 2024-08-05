export interface CreateDoctorForm {
  name: string;
  telephone: string;
  nif: string;
  socialNumber: string;
  collegueNumber: string;
  startDate: Date | null;
  doctorPosition: string;
}
export interface CreateEmployeeForm {
  name: string;
  telephone: string;
  nif: string;
  socialNumber: string;
  collegueNumber: string;
  startDate: Date | null;
  employeeNumber: string;
  employeePosition: string;
}
export interface DeleteEmployeeForm {
  employeeName: string;
  employeeNif: number;
}

export interface CreateEmployeePositionForm {
  positionName: string;
}

export interface CreatePersonAddressForm {
  streetNumber: number;
  addressLine1: string;
  addressLine2: string;
  city: string;
  population: number;
  postalCode: number;
  province: string;
  personName: string;
  personNif: string;
}

export interface UpdatePersonAddressForm {
  addressId: number;
  personName: string;
  personNif: string;
  streetNumber: number;
  addressLine1: string;
  addressLine2: string;
  city: string;
  population: number;
  postalCode: number;
  province: string;
}

export interface DeletePersonAddressForm {
  addressId: number;
}

export interface DeleteDoctorForm {
  doctorName: string;
  collegueNumber: string;
}

export interface UpdateDoctorForm {
  doctorName: string;
  telephone: string;
  collegueNumber: string;
  startDate: Date | null;
  doctorPosition: string;
}

export interface CreateDoctorPositionForm {
  positionName: string;
}

export interface CreatePatientForm {
  name: string;
  telephone: string;
  nif: string;
  socialNumber: number;
}

export interface UpdatePatientForm {
  name: string;
  telephone: string;
  nif: string;
}

export interface DeletePatientForm {
  name: string;
  nif: string;
}
