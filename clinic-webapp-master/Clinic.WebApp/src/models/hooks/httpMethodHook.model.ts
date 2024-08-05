import { Error } from "../problem-details.model";

export interface PostHookOptions<TRequest> {
  url: string;
  params?: Record<string, string>;
  headers?: Record<string, string>;
  body?: TRequest;
}

export interface PostHookError {
  code: string;
  status: number;
  message?: string;
  params?: string;
  ok: boolean;
  url: string;
  errors?: PostError[];
}

export interface PostHookDefaultResponse {
  status: number;
  url: string;
  ok: boolean;
  params?: string;
}

type PostError = Omit<Error, "errorType">;
