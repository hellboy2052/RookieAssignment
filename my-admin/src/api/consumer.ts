import axios, { AxiosResponse } from 'axios';
import { User } from './models/user';
const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = "https://localhost:5002/";

// axios.interceptors.request.use(config =>)
axios.interceptors.response.use(async res => {
    await sleep(1000);
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

}

const Account = {
    current: () => request.get<User>("api/Account"),
}

const consumer = {
    Products,
    Account
}

export default consumer;