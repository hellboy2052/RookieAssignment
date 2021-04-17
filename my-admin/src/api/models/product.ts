import { Category } from "./category";
import { Rate } from "./rate";

export interface Product{
    id: number;
    name: string;
    price: number;
    description: string;
    image: string;
    brandName: string;
    ratingCount: number;
    rating: number;
    currentRate: number;
    isRate: boolean;
    rate: Rate[];
    productCategories: Category[];


}