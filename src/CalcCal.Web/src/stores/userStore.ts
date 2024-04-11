import {makeAutoObservable} from "mobx";
import {User} from "../models/user";
import {RegisterRequest} from "../api/requests/registerRequest";
import agent from "../api/agent";
import {LogInRequest} from "../api/requests/logInRequest";
import {EatenFood} from "../models/eatenFood";

export default class UserStore {
    private guestUser: User = User.guest()
    private authenticatedUser?: User = undefined
    token: string | null = localStorage.getItem('jwt')
    getLoading: boolean = false
    loginLoading: boolean = false
    registerLoading: boolean = false

    constructor() {
        makeAutoObservable(this);

        if(localStorage.getItem('jwt')) {
            this.token = localStorage.getItem('jwt')

            !this.isAuthenticated() && this.loadCurrentUser()
        }
    }

    get eatenFood() {
        return this.authenticatedUser ?
            this.authenticatedUser.eatenFood.filter(f => new Date(f.eatenDateTime).toISOString().split('T')[0] === new Date().toISOString().split('T')[0]) :
            this.guestUser.eatenFood.filter(f => new Date(f.eatenDateTime).toISOString().split('T')[0] === new Date().toISOString().split('T')[0])
    }

    get user(){
        return this.authenticatedUser || this.guestUser
    }

    eatenCalories() {
        if(!this.eatenFood){
            return 0
        }

        return this.eatenFood?.reduce((sum, food) => {
            return sum + food.caloriesEaten;
        }, 0)
    }

    private setLoginLoading(state: boolean) {
        this.loginLoading = state
    }

    private setRegisterLoading(state: boolean) {
        this.registerLoading = state
    }

    private setGetLoading(state: boolean) {
        this.getLoading = state
    }

    private setAuthenticatedUser(user: User | undefined) {
        this.authenticatedUser = user
    }

    setToken = (token: string | null) => {
        this.token = token;
    }

    isAuthenticated() {
        return !!this.authenticatedUser
    }

    async register(registerRequest: RegisterRequest) {
        this.setRegisterLoading(true)

        try {
            await agent.users.registerUser(registerRequest)
            await this.logIn({username: registerRequest.username, password: registerRequest.password})
            return true
        } catch (e) {
            return false
        } finally {
            this.setRegisterLoading(false)
        }
    }

    async logIn(logInRequest: LogInRequest) {
        this.setLoginLoading(true)

        try {
            const accessToken = await agent.users.logInUser(logInRequest)

            if(accessToken.value.length > 0) {
                localStorage.setItem('jwt', accessToken.value)
                this.setToken(accessToken.value)
                await this.loadCurrentUser()
            } else {
                return false
            }

            return true
        } catch (e) {
            return false
        } finally {
            this.setLoginLoading(false)
        }
    }

    async loadCurrentUser() {
        this.setGetLoading(true)

        try {
            const user = await agent.users.getCurrentUser()
            this.setAuthenticatedUser(user)
        } catch (e) {
        } finally {
            this.setGetLoading(false)
        }
    }

    logout() {
        this.setAuthenticatedUser(undefined)
        this.setToken(null)
        localStorage.removeItem('jwt')
    }

    eat(food: EatenFood){
        if(this.isAuthenticated()) {
            this.authenticatedUser!.eat(food)
        } else {
            this.guestUser.eat(food)
        }
    }
}