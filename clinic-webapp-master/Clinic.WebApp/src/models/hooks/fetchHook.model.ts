import { TableSorting } from "../table.model";

export interface FetchHookOptions {
  url: string;
  params?: Record<string, string>;
  headers?: Record<string, string>;
  sorting?: TableSorting;
  page?: number;
  searchValue?: string;
}

export interface FetchHookResponse<TResponse> {
  status: number;
  data: TResponse;
  url: string;
  ok: boolean;
  params?: string;
}

export type FetchHookReturn<TResponse> = [
  FetchHookResponse<TResponse> | null,
  boolean,
  Error | null,
  (err: Error | null) => void
];
