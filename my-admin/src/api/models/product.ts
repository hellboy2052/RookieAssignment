import { Category } from "./category";
import { Rate } from "./rate";

export interface Product{
    id: number;
    name: string;
    price: number;
    description: string;
    image: string | null;
    brandName: string;
    createdDate: Date | null;
    updatedDate: Date | null;
    ratingCount: number;
    rating: number;
    currentRate: number;
    isRate: boolean;
    rate: Rate[];
    productCategories: Category[];
    pictures?: Picture[];
}


export class Product implements Product {
    constructor(init?: ProductFormValues){
        Object.assign(this, init);
    }
}

export class ProductFormValues{
    id?: number = 0;
    name: string = '';
    price: number = 0;
    description: string = ''; //Nullable
    image: string | null  = ''; //Nullable
    brandId: number | undefined;
    categoryName: string[] = [];
    pictures?: Blob[] = [];

    constructor(product?: ProductFormValues){
        // For edit product
        if(product){
            this.id = product.id;
            this.name = product.name;
            this.price = product.price;
            this.description = product.description;
            this.image = product.image;
            this.brandId = product.brandId;
            this.categoryName = product.categoryName;
        }
    }

    
}

export interface Picture {
    id: string;
    url: string;
    isMain: boolean;
}