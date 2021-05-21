export class FilterItem {
    public static readonly OPERATOR_LIST: string[] = [
        ">", "<", "==", "!=", ">=", "<=", "contains"
    ];

    private _name: string = '';
    private _customOperator: string = '';
    private _value: string = '';

    constructor(
        name: string, 
        customOperator: string, 
        value: string
    ) { 
        this.name = name;
        this.customOperator = customOperator;
        this.value = value;
    }

    public get name(): string {
        return this._name;
    }

    public set name(name: string) {        
        this._name = name;
    }

    public get customOperator(): string {
        return this._customOperator;
    }

    public set customOperator(customOperator: string) {
        if (FilterItem.OPERATOR_LIST.includes(customOperator) == false) {
            throw Error(`invalid operator - ${customOperator}`);
        }
        
        this._customOperator = customOperator;
    }

    public get value(): string {
        return this._value;
    }

    public set value(value: string) {        
        this._value = value;
    }

    public json(): string {
        return JSON.stringify({
            name: this.name,
            customOperator: this.customOperator,
            value: this.value
        });
    }

    public static fromJson(stringValue: string): FilterItem {
        const object: any = JSON.parse(stringValue);
        return new FilterItem(object['name'], object['customOperator'], object['value']); 
    }
}