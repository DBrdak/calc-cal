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
import {ChangePasswordRequest} from "./requests/changePasswordRequest";

const sleep = (delay: number) => {
    return new Promise((resolve) => {
        setTimeout(resolve, delay)
    })
}

axios.defaults.baseURL = process.env.REACT_APP_API_URL

axios.interceptors.request.use(config => {
    const token = store.userStore.token || localStorage.getItem('jwt')

    config.headers.Authorization = token && `Bearer ${token}`

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
                    toast.clearWaitingQueue()
                    return Promise.reject();
                case 404:
                    if(error.response.config.method === 'get' && error.response.data.errors.hasOwnProperty('id')){
                        router.navigate('/not-found');
                    }
                    errorMessages.forEach(toast.error)
                    return Promise.reject();
                case 401:
                    //toast.error('Unauthorized')
                    toast.clearWaitingQueue()
                    break
                case 419:
                    toast.error('Session expired - please login')
                    store.userStore.logout()
                    break
                case 429:
                    errorMessages.forEach(toast.error)
                    break
                case 500:
                    toast.error('Server error')
                    //router.navigate('/server-error');
                    break;
            }
        }

        if(error.response.status === 419) {
            !toast.isActive(1) && toast.error('Session expired - please login')
            store.userStore.logout()
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
    sendVerificationCode: (request: SendVerificationCodeRequest) => axios.put('/users/verification/send', request),
    verifyPhone: (request: VerifyCodeRequest) => axios.put('/users/verification/verify-phone', request),
    verifyCode: (request: VerifyCodeRequest) => axios.put('/users/verification/verify-code', request),
    changePassword: (request: ChangePasswordRequest) => axios.put('/users/newPassword', request),
}

const agent = {
    food,
    users,
}

export default agent;

