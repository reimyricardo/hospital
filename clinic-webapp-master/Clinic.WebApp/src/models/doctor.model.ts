export interface Doctor {
  doctorId: number;
  name: string;
  telephone: string;
  nif: string;
  socialNumber: number;
  collegueNumber: number;
  startDate: string;
  endDate: string;
  positionName: string;
}

export interface DoctorPosition {
  positionName: string;
}
