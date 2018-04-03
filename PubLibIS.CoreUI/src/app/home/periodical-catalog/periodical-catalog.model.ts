import { Periodical } from "../../library/periodical/shared/periodical.model";

export class PeriodicalCatalog {
  constructor(
    public periodicals?: Periodical[],
    public skip?: number,
    public isSeeMore?: boolean,
    public hasNextPage?: boolean
  ) { }
}
