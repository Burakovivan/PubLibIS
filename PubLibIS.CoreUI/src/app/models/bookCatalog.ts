import { BookSlim } from "./bookSlim";

export class BookCatalog {

  constructor(
    public books?: BookSlim[],
    public skip?: number,
    public isSeeMore?: boolean,
    public hasNextPage?: boolean
  ) { }
}
