import { TableColumn, TableSorting } from "../table.model";

export interface TableProps {
  columns: TableColumn[];
  title: string;
  url: string;
  initialPageSize: string;
}

export interface TableContentProps<T extends object> {
  columns: TableColumn[];
  items: T[];
}

export interface TableHeaderProps {
  columns: TableColumn[];
  setSorting: (value: TableSorting) => void;
  sorting: TableSorting | null;
}

export interface TableHeaderCellProps {
  column: TableColumn;
  setSorting: (value: TableSorting) => void;
  sorting: TableSorting | null;
}
