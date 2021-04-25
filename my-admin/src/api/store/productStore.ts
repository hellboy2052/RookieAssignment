import { makeAutoObservable, runInAction } from "mobx";
import consumer from "../consumer";
import { Product, ProductFormValues } from "../models/product";

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
            return product;
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


    createProduct = async (product: ProductFormValues) => {

        try {
            await consumer.Products.create(product);
        } catch (error) {
            console.log(error);

        }
    }

    updateProduct = async (product: ProductFormValues) => {

        try {
            await consumer.Products.update(product);
            runInAction(() => {
                if (product.id) {
                    console.log(product.id);
                    
                    this.loadProducts()
                    this.selectedProduct = this.getProduct(product.id);
                }
            })
        } catch (error) {
            console.log(error);

        }
    }

    deleteProduct = async (id: string) => {
        this.setLoading(true);
        try {
            await consumer.Products.delete(id)
            runInAction(() => {
                this.productRegistry.delete(Number.parseInt(id));
                this.setLoading(false);
            })
        } catch (error) {
            console.log(error);
            this.setLoading(false);
        }
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

    clearProducts = () => {
        this.productRegistry.clear();
    }

}