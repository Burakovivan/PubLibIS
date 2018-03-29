import { Periodical } from "./periodical";

export class PeriodicalCatalog {
  constructor(
    public periodicals?: Periodical[],
    public skip?: number,
    public isSeeMore?: boolean,
    public hasNextPage?: boolean
  ) { }
}
