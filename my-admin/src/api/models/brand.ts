export interface Brand{
    id: number;
    name: string;
}

export class Brand implements Brand {
    constructor(init?: BrandFormValues){
        Object.assign(this, init);
    }
}

export class BrandFormValues{
    id?: number = 0;
    name: string = "";

    constructor(brand?: BrandFormValues){
        // For edit brand
        if(brand){
            this.id = brand.id;
            this.name = brand.name;
        }
    }
}