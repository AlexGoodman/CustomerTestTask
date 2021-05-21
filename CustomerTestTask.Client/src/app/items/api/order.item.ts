export class OrderItem {
    public static readonly OPERATOR_LIST: string[] = [
        "desc", "asc"
    ];

    private _name: string = '';
    private _customOperator: string = '';

    constructor(
        name: string, 
        customOperator: string
    ) { 
        this.name = name;
        this.customOperator = customOperator;
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
        if (OrderItem.OPERATOR_LIST.includes(customOperator) == false) {            
            throw Error(`invalid operator - ${customOperator}`);
        }
        
        this._customOperator = customOperator;
    }
    
    public json(): string {
        return JSON.stringify({
            name: this.name,
            customOperator: this.customOperator
        });
    }

    public static fromJson(stringValue: string): OrderItem {
        const object: any = JSON.parse(stringValue);
        return new OrderItem(object['name'], object['customOperator']); 
    }
}