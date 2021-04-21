import { createContext, useContext } from "react";
import BrandStore from "./brandStore";
import CategoryStore from "./categoryStore";
import CommonStore from "./commonStore";
import ProductStore from "./productStore";
import UserStore from "./userStore";

interface Store {
    commonStore: CommonStore;
    userStore: UserStore;
    productStore: ProductStore;
    brandStore: BrandStore;
    categoryStore: CategoryStore;
}

export const store: Store = {
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    productStore: new ProductStore(),
    brandStore: new BrandStore(),
    categoryStore: new CategoryStore()
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}