import axios, { AxiosError, AxiosResponse } from 'axios';
import { toast } from 'react-toastify';
import { history } from '..';
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
}, (error: AxiosError) => {
    const { data, status, config } = error.response!;

    switch (status) {
        case 400:

            if (typeof data === 'string') {
                toast.error(data);
            }

            if (config.method === 'get' && data.errors.hasOwnProperty('id')) {
                history.push("/not-found")
            }
            if (data.errors) {
                const modalStateErrors = [];
                for (const key in data.errors) {
                    if (data.errors[key]) {
                        modalStateErrors.push(data.errors[key]);
                    }
                }

                throw modalStateErrors.flat();
            } else {
                toast.error(data);
            }
            break;
        case 401:
            toast.error('unauthorised');
            break;
        case 404:
            history.push("/not-found");
            break;
        case 500:
            store.commonStore.setServerError(data);
            history.push('/server-error');
            break;

        default:
            break;
    }

    return Promise.reject(error);
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