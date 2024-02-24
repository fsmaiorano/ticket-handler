export class PaginatedList<T> {
  items: T[] = []
  pageNumber: number = 0
  totalPages: number = 0
  totalCount: number = 0
  previousPage: string = ''
  nextPage: string = ''
}
