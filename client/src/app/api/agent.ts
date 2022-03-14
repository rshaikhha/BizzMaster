import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { history } from "../..";
import { PaginatedResponse } from "../models/pagination";
import { store } from "../store/configureStore";

const sleep = () => new Promise(resolve => setTimeout(resolve,1000));

axios.defaults.baseURL = "http://localhost:5000/api"
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use(config => {
    const token = store.getState().account.user?.token;
    if (token) config.headers!.Authorization = `Bearer ${token}`;
    return config;
})

axios.interceptors.response.use(async response => {
    await sleep();
    const pagination = response.headers['pagination'];
    if (pagination) {
        response.data = new PaginatedResponse(response.data, JSON.parse(pagination));
    }
    return response;
}, (error: AxiosError) => {
    const {data, status} = error.response!;
    switch (status) {
        case 400:
            if(data.errors) {
                const modelStateErrors: string[] = [];
                for(const key in data.errors){
                    if(data.errors[key]) {
                        modelStateErrors.push(data.errors[key])
                    }
                }
                throw modelStateErrors.flat();
            }
            toast.error(data.title);
            break;
        case 401:
            toast.error(data.title);
            break; 
        case 500:
            history.push({
                pathname:'/server-error',
                state: {error: data}
            })
            break;
        default:
            break;
    }

    return Promise.reject(error.response);
})

const requests = {
    get: (url: string, params?: URLSearchParams) => axios.get(url, {params}).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
    put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
    delete: (url: string) => axios.delete(url).then(responseBody),
}


const basics = {
    countries: () => requests.get('basics/countries'),
    brands: () => requests.get('basics/brands'),
    usageTypes: ()=> requests.get('basics/usageTypes'),
    masterSystems: ()=> requests.get('basics/masterSystems'),
    categories : ()=> requests.get('basics/categories'),
    details: (id: number) => requests.get(`country/${id}`)
}
const Cars = {
    brands: () => requests.get('Car/Brands'),
    platforms: () => requests.get(`Car/Platforms`),
    brandPlatforms: (brandTitle: string = ' ') => requests.get(`Car/BrandPlatforms/${brandTitle}`),
    cars: () => requests.get(`Car/Cars`),
    platformCars: (platformTitle: string = ' ') => requests.get(`Car/PlatformCars/${platformTitle}`),

}

const Catalog = {
    list: (params: URLSearchParams) => requests.get('products', params),
    details: (id: number) => requests.get(`products/${id}`),
    fetchFilters: () => requests.get('products/filters')
}

const Suppliers = {
    list: () => requests.get('suppliers'),
    details: (id: number) => requests.get(`suppliers/${id}`),
    lines: () => requests.get('suppliers/lines'),
    lineDetails: (id: number) => requests.get(`suppliers/lines/${id}`),
    LeadTimes: (id: number) => requests.get(`suppliers/Leadtimes/${id}`),
    LeadTimeHistory: (id: number) => requests.get(`suppliers/LeadtimeHistory/${id}`),
    postLeadTime: (values: any) => requests.post('suppliers/Leadtime', values),
    activeProducts: (id: number) => requests.get(`suppliers/ActiveProducts/${id}`),
    
}

const Commercial = {
    Get: (id: number) => requests.get(`Commercial/${id}`),
    list: () => requests.get('Commercial'),
    Card: (id: number) => requests.get(`Commercial/Cards/${id}`),
    Cards: () => requests.get('Commercial/Cards'),

}



const SalesForecast = {
    post: (values: any) => requests.post('SalesForecast', values),
    list: (id: number) => requests.get(`SalesForecast/${id}`),
    history: (id: number, year: number, month: number) => requests.get(`SalesForecast/history/${id}/${year}/${month}`),
    get: (id: number, year: number, month: number) => requests.get(`SalesForecast/${id}/${year}/${month}`),
}
const Stock = {
    post: (values: any) => requests.post('Stock', values),
    list: (id: number) => requests.get(`Stock/${id}`),
    history: (id: number, year: number, month: number) => requests.get(`Stock/history/${id}/${year}/${month}`),
    get: (id: number, year: number, month: number) => requests.get(`Stock/${id}/${year}/${month}`),
    calulate: (id: number, year: number, month: number) => requests.get(`Stock/calculate/${id}/${year}/${month}`),
}
const Order = {
    post: (values: any) => requests.post('Order', values),
    list: (id: number) => requests.get(`Order/${id}`),
    history: (id: number, year: number, month: number) => requests.get(`Order/history/${id}/${year}/${month}`),
    get: (id: number, year: number, month: number) => requests.get(`Order/${id}/${year}/${month}`),
    calulate: (id: number, year: number, month: number) => requests.get(`Order/calculate/${id}/${year}/${month}`),
}
const Audit = {
    get: (id: number, year: number, month: number) => requests.get(`Audit/${id}/${year}/${month}`),
}

const Projects = {
    Get: (id: number) => requests.get(`Project/${id}`),
    list: () => requests.get('Project'),
    post: (values: any) => requests.post('Project', values),


}
const TestErrors = {
    get400Error: () => requests.get('buggy/bad-request'),
    get401Error: () => requests.get('buggy/unauthorized'),
    get404Error: () => requests.get('buggy/not-found'),
    get500Error: () => requests.get('buggy/server-error'),
    getValidationError: () => requests.get('buggy/validation-error'),

}

const Basket = {
    get: () => requests.get('basket'),
    addItem: (productId : number , quantity = 1) => requests.post(`basket?ProductId=${productId}&quantity=${quantity}`,{}),  
    removeItem: (productId : number , quantity = 1) => requests.delete(`basket?ProductId=${productId}&quantity=${quantity}`),
}

const Account = {
    login: (values: any) => requests.post('account/login', values),
    register: (values: any) => requests.post('account/register', values),
    currentUser: () => requests.get('account/currentUser'),
}

const agent = {
    basics,
    Cars,
    Catalog,
    Suppliers,
    Commercial,
    SalesForecast,
    Stock,
    Order,
    Audit,
    Projects,
    TestErrors,
    Basket,
    Account
}

export default agent;