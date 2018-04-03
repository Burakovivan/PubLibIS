import { BookSlim } from "../../library/book/shared/book-slim.model";

export class BookCatalog {

  constructor(
    public books?: BookSlim[],
    public skip?: number,
    public isSeeMore?: boolean,
    public hasNextPage?: boolean
  ) { }
}
