import { PublishingHouse } from "./publishingHouse";


export class PublishedBook {
    constructor(
        public id?: number,
        public volume?: number,
        public dateOfPublication?: string,
        public book_Id?: number,
        public publishingHouse_Id?: number,
        public publishingHouse?: PublishingHouse) { }
}