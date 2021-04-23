import axios, { AxiosResponse } from 'axios';
import { Brand, BrandFormValues } from './models/brand';
import { Category } from './models/category';
import { Product, ProductFormValues } from './models/product';
import { User, UserFormValues, UserData } from './models/user';
import { store } from './store/store';
const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = "https://localhost:5002/";

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
})

axios.interceptors.response.use(async res => {
    await sleep(1000);
    console.log(res);

    return res;
});

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const request = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    del: <T>(url: string) => axios.delete<T>(url).then(responseBody)
};

const Products = {
    list: () => request.get<Product[]>("Products"),
    detail: (id: string) => request.get<Product>(`Products/${id}`),
    create: (product: ProductFormValues) => request.post<void>("Products", product),
    update: (product: ProductFormValues) => request.put<void>(`Products/${product.id}`, product),
    delete: (id: string) => request.del<void>(`Products/${id}`)
}

const Account = {
    current: () => request.get<User>("api/Account"),
    login: (user: UserFormValues) => request.post<User>("api/Account/login", user),
    list: () => request.get<UserData[]>("api/Account/List")
}

const Brands = {
    list: () => request.get<Brand[]>("Brand"),
    detail: (id: string) => request.get<Brand>(`Brand/${id}`),
    create: (brand: BrandFormValues) => request.post<void>("Brand", brand),
    delete: (id: string) => request.del<void>(`Brand/${id}`)
}

const Categories = {
    list: () => request.get<Category[]>("Categories"),
}

const consumer = {
    Products,
    Account,
    Brands,
    Categories
}

export default consumer;