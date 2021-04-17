import axios, { AxiosResponse } from 'axios';
const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay);
    })
}

axios.defaults.baseURL = "http://localhost:5000/";

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
    
}

const consumer = {
    Products
}

export default consumer;