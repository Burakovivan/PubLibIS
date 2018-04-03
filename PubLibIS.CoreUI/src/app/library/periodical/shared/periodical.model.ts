import { PublishingHouse } from "../../publishing-house/publishing-house.model";
import { PeriodicalType } from "./periodical-type.model";

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
