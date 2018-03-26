import { PublishingHouse } from "./publishingHouse";
import { PeriodicalType } from "./periodicalType";

export class Periodical {
  constructor(
    public id?: number,
    public issn?: string,
    public name?: string,
    public type?: PeriodicalType,
    public foundation?: string,
    public publishingHouse_Id?: number[],
    public publicationsCount?: number,
    public publishingHouse?: PublishingHouse) { }
}
