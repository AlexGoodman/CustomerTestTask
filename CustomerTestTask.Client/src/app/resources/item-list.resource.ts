export class ItemListResource<TItem> {
    constructor(
        public totalCount: number,
        public itemList: TItem[]
    ) {}
}