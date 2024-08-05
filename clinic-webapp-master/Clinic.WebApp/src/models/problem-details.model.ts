export interface ProblemDetails {
  type: string;
  title: string;
  status: number;
  detail: string;
  error: Error;
  validationErrors?: Error[];
}

export interface Error {
  errorCode: string;
  errorDescription: string;
  errorType: number;
}
