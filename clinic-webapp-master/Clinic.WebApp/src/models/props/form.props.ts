import { ReactNode } from "react";

export interface FormProps {
  children: ReactNode;
  title: string;
  onSubmit: (
    e: React.BaseSyntheticEvent<object, object, object> | undefined
  ) => Promise<void>;
}
