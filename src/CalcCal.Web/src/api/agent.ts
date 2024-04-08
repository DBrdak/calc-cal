import axios, {AxiosResponse} from "axios"
import {toast} from "react-toastify"
import {router} from "../router/Routes"
import {AddFoodRequest} from "./requests/addFoodRequest";
import {EatFoodRequest} from "./requests/eatFoodRequest";
import {LogInRequest} from "./requests/logInRequest";
import {RegisterRequest} from "./requests/registerRequest";
import AccessToken from "./responses/accessToken";
import {User} from "../models/user";
import {Food} from "../models/food";
import {EatenFood} from "../models/eatenFood";
import {store} from "../stores/store";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = process.env.REACT_APP_API_URL

axios.interceptors.request.use(config => {
    const token = store.userStore.token

    config.headers.Authorization = `Bearer ${token}`

    return config
})

const responseBody = <T> (response: AxiosResponse<T>) => response.data;

axios.interceptors.response.use(async(response) => {
        if(process.env.NODE_ENV === "development") {
            await sleep(1000)
        }

        return response
    }, (error) => {
        if (error.response.data.name) {
            const errorMessage = error.response.data.name
            const errorMessages = errorMessage.split('\n')
            switch(error.response.status) {
                case 400:
                    errorMessages.forEach(toast.error)
                    return Promise.reject();
                case 403:
                    errorMessages.forEach(toast.error)
                    return Promise.reject();
                case 404:
                    if(error.response.config.method === 'get' && error.response.data.errors.hasOwnProperty('id')){
                        router.navigate('/not-found');
                    }
                    errorMessages.forEach(toast.error)
                    return Promise.reject();
                default:
                    errorMessages.forEach(toast.error)
            }
        }
        switch(error.response.status) {
            case 401:
                //toast.error('Unauthorized')
                toast.clearWaitingQueue()
                break
            case 403:
                toast.error(error.response.data.error.name)
                toast.clearWaitingQueue()
                break
            case 429:
                if(!toast.isActive(1)) toast.error('Too many requests')
                break
            case 500:
                toast.error('Server error')
                //router.navigate('/server-error');
                break;
        }
        return Promise.reject(error);
    }
);

const food = {
    getFood: (foodName?: string) => axios.get<Food[]>('/food', { params: { foodName } }).then(responseBody),
    addFood: (request: AddFoodRequest) => axios.post<Food[]>('/food', request).then(responseBody),
    eatFood: (request: EatFoodRequest) => axios.put<EatenFood>('/food', request).then(responseBody),
}

const users = {
    getCurrentUser: () => axios.get<User>('/users/current').then(responseBody),
    logInUser: (request: LogInRequest) => axios.post<AccessToken>('/users/login', request).then(responseBody),
    registerUser: (request: RegisterRequest) => axios.post('/users/register', request),
}

const agent = {
    food,
    users,
}

export default agent;

