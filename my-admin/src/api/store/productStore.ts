import { makeAutoObservable, runInAction } from "mobx";
import consumer from "../consumer";
import { Product } from "../models/product";

export default class ProductStore {
    productRegistry = new Map<number, Product>();
    selectedProduct: Product | undefined = undefined;
    loading = false;
    loadingInitial = false;

    constructor() {
        makeAutoObservable(this)
    }

    get ProductsByDate() {
        return Array.from(this.productRegistry.values()).sort((a, b) =>
            a.createdDate!.getTime() - b.createdDate!.getTime())
    }

    loadProducts = async () => {
        this.setLoadingInitial(true);
        try {
            const products = await consumer.Products.list();
            products.forEach(product => {
                this.setProduct(product);
            })
            this.setLoadingInitial(false);
        } catch (error) {
            console.log(error);
            this.setLoadingInitial(false);

        }
    }

    loadProduct = async (id: string) => {
        let product = this.getProduct(Number.parseInt(id));
        if (product) {
            this.selectedProduct = product;
            return
        } else {
            this.setLoadingInitial(true);
            try {
                product = await consumer.Products.detail(id);
                this.setProduct(product);
                runInAction(() => this.selectedProduct = product);
                this.setLoadingInitial(false);
                return product;
            } catch (error) {
                console.log(error);
                this.setLoadingInitial(false);

            }

        }
    }

    private getProduct = (id: number) => {
        return this.productRegistry.get(id);
    }

    private setProduct = (product: Product) => {
        product.createdDate = new Date(product.createdDate!);
        product.updatedDate = new Date(product.updatedDate!);
        this.productRegistry.set(product.id, product);
    }

    setLoadingInitial = (state: boolean) => {
        this.loadingInitial = state;
    }

    setLoading = (state: boolean) => {
        this.loading = state;
    }

    clearSelectedProduct = () => {
        this.selectedProduct = undefined;
    }

}