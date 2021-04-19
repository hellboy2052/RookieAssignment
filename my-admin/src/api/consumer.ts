import axios, { AxiosResponse } from 'axios';
import { Product } from './models/product';
import { User, UserFormValues } from './models/user';
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
}

const Account = {
    current: () => request.get<User>("api/Account"),
    login: (user: UserFormValues) => request.post<User>("api/Account/login", user),
}

const consumer = {
    Products,
    Account
}

export default consumer;