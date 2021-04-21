import { makeAutoObservable } from "mobx";
import consumer from "../consumer";
import { Brand } from "../models/brand";
import { selectOption } from "../models/option";

export default class BrandStore {
    brands: Brand[] = [];
    Boption: selectOption[] = [];

    constructor() {
        makeAutoObservable(this)
    }

    loadBrands = async () => {
        try {
            const brands = await consumer.Brands.list();
            brands.forEach(brand => {
                this.setBrand(brand);
                this.setOption(brand);
            })
        } catch (error) {
            console.log(error);

        }
    }

    setBrand = (brand: Brand) => {
        this.brands = [...this.brands, brand]
    }

    setOption = (brand: Brand) => {
        this.Boption = [...this.Boption, {text: brand.name, value: brand.id}]
    }


}