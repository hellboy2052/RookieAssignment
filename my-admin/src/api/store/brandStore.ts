import { makeAutoObservable, runInAction } from "mobx";
import consumer from "../consumer";
import { Brand, BrandFormValues } from "../models/brand";
import { selectOption } from "../models/option";

export default class BrandStore {
    brands: Brand[] = [];
    Boption: selectOption[] = [];
    loadingInitial = false;
    loading = false;

    constructor() {
        makeAutoObservable(this)
    }

    loadBrands = async () => {

        this.setLoadingInitial(true);
        try {

            const brands = await consumer.Brands.list();
            brands.forEach(brand => {
                this.setBrand(brand);
                this.setOption(brand);
                this.setLoadingInitial(false);
            })
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);

        }
    }

    loadBrand = async (id: string) => {
        let brand = this.getBrand(Number.parseInt(id));
        if (brand) {
            return brand;
        } else {
            this.setLoadingInitial(true);
            try {
                brand = await consumer.Brands.detail(id);
                this.setBrand(brand);
                this.setLoadingInitial(false);
                return brand;
            } catch (error) {
                console.log(error);
                this.setLoadingInitial(false);

            }
        }
    }

    createBrand = async (brand: BrandFormValues) => {
        try {
            await consumer.Brands.create(brand);
        } catch (error) {
            console.log(error);

        }
    }

    deleteBrand = async (id: string) => {
        this.setLoading(true);
        try {
            await consumer.Brands.delete(id)
            runInAction(() => {
                this.brands = [...this.brands.filter(x => x.id != Number.parseInt(id))];
                this.Boption = [...this.Boption.filter(x => x.value != id)]
                this.setLoading(false);
            })
        } catch (error) {
            console.log(error);
            this.setLoading(false);
        }
    }

    private getBrand = (id: number) => {
        return this.brands.find(x => x.id == id);
    }

    setBrand = (brand: Brand) => {
        this.brands = [...this.brands, brand]
    }

    setOption = (brand: Brand) => {
        this.Boption = [...this.Boption, { text: brand.name, value: brand.id }]
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    setLoading = (state: boolean) => {
        this.loading = state;
    }

    clearBrands = () => {
        if (this.brands.length > 0) {
            this.brands.splice(0, this.brands.length)
        }
        if (this.Boption.length > 0) {
            this.Boption.splice(0, this.Boption.length)
        }
    }


}