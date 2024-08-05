import { ReactNode } from "react";

export interface Routes {
  [key: string]: Route[];
}

export interface Route {
  path: string;
  element?: ReactNode;
  pathChildName?: string;
}
