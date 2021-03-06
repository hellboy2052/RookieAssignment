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

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

axios.interceptors.request.use(config => {
    const token = store.commonStore.token;
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
})

axios.interceptors.response.use(async res => {
    if (process.env.NODE_ENV === "development") await sleep(1000);
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
// request.post<void>("Products", product)
const Products = {
    list: () => request.get<Product[]>("Products"),
    detail: (id: string) => request.get<Product>(`Products/${id}`),
    create: (product: ProductFormValues) => {
        let formData = new FormData();
        formData.append('name', product.name);
        formData.append('price', product.price.toString());
        formData.append('description', product.description);
        formData.append('brandId', product.brandId!.toString());

        product.categoryName!.forEach(cate => {
            formData.append('categoryName[]', cate);
        });
        product.pictures?.forEach((pic: any, index) => {
            formData.append(`pictures`, pic);
        })
        return axios.post<void>('Products', formData, {
            headers: { 'Content-type': 'multipart/form-data' }
        })
    },
    update: (product: ProductFormValues) => {
        let formData = new FormData();
        formData.append('name', product.name);
        formData.append('price', product.price.toString());
        formData.append('description', product.description);
        formData.append('brandId', product.brandId!.toString());

        product.categoryName!.forEach(cate => {
            formData.append('categoryName[]', cate);
        });
        product.pictures?.forEach((pic: any, index) => {
            formData.append(`pictures`, pic);
        })
        return axios.put<void>(`Products/${product.id}`, formData, {
            headers: { 'Content-type': 'multipart/form-data' }
        })
    },
    delete: (id: string) => request.del<void>(`Products/${id}`),
    setMain: (id: string, proId: string) => request.post<void>(`Photo/${id}/setMain?proId=${proId}`, {}),
    deletePhoto: (id: string, proId: string) => request.del<void>(`Photo/${id}?proId=${proId}`),
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