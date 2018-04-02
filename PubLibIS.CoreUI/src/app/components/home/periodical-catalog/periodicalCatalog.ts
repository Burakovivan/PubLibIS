import { Periodical } from "../../../models/periodical";

export class PeriodicalCatalog {
  constructor(
    public periodicals?: Periodical[],
    public skip?: number,
    public isSeeMore?: boolean,
    public hasNextPage?: boolean
  ) { }
}
