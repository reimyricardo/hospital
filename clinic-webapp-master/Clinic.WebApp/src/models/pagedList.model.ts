export interface PagedList<TItems> {
  page: number;
  pageSize: number;
  totalCount: number;
  hasNexPage: boolean;
  hasPreviousPage: boolean;
  items: TItems;
}
