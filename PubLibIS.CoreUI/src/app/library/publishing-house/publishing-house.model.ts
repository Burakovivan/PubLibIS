export class PublishingHouse {
    constructor(
        public id?: number,
        public name?: string,
        public country?: string,
        public city?: string,
        public postalCode?: string,
        public phone?: string,
        public foundationDate?: Date,
        public shortInfo?: string,
        public fullAddressFormatted?: string, ) { }
}