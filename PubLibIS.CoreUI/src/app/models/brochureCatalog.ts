import { Brochure } from "./brochure";

export class BrochureCatalog {

  constructor(
    public brochures?: Brochure[],
    public skip?: number,
    public isSeeMore?: boolean,
    public hasNextPage?: boolean
  ) { }
}
