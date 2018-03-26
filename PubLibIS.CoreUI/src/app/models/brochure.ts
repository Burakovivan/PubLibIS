﻿import { PublishingHouse } from "./publishingHouse";

export class Brochure {
    constructor(
        public id?: number,
        public capation?: string,
        public volume?: number,
        public releaseDate?: Date,
        public circulation?: number,
        public publishingHouse_Id?: number,
        public publishingHouse?: PublishingHouse) { }
}