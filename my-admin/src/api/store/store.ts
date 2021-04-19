import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import ProductStore from "./productStore";
import UserStore from "./userStore";

interface Store {
    commonStore: CommonStore;
    userStore: UserStore;
    productStore: ProductStore;
}

export const store: Store = {
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    productStore: new ProductStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext);
}