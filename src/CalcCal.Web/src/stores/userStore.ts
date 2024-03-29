import {makeAutoObservable} from "mobx";
import {UserLoading} from "./models/userLoading";
import {User} from "../models/user";
import {RegisterRequest} from "../api/requests/registerRequest";
import agent from "../api/agent";
import {LogInRequest} from "../api/requests/logInRequest";
import {EatenFood} from "../models/eatenFood";

export default class UserStore {
    private guestUser: User = User.guest()
    private authenticatedUser?: User = undefined
    token: string | null = localStorage.getItem('jwt')
    loading: UserLoading = new UserLoading()

    constructor() {
        makeAutoObservable(this);

        if(localStorage.getItem('jwt')) {
            this.token = localStorage.getItem('jwt')

            !this.authenticatedUser && this.loadCurrentUser()
        }
    }

    get eatenFood() {
        return this.authenticatedUser ?
            this.authenticatedUser.eatenFood :
            this.guestUser.eatenFood
    }

    get user(){
        return this.authenticatedUser || this.guestUser
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
        this.loading.stopRegister()

        try {
            await agent.users.registerUser(registerRequest)
            await this.logIn({username: registerRequest.username, password: registerRequest.password})
            return true
        } catch (e) {
            return false
        } finally {
            this.loading.stopRegister()
        }
    }

    async logIn(logInRequest: LogInRequest) {
        this.loading.startLogin()

        try {
            const accessToken = await agent.users.logInUser(logInRequest)

            if(accessToken.value.length > 0) {
                localStorage.setItem('jwt', accessToken.value)
                this.setToken(accessToken.value)
                await this.loadCurrentUser()
            }

            return true
        } catch (e) {
            return false
        } finally {
            this.loading.stopLogin()
        }
    }

    async loadCurrentUser() {
        this.loading.startGet()

        try {
            const user = await agent.users.getCurrentUser()
            this.setAuthenticatedUser(user)
        } catch (e) {
        } finally {
            this.loading.stopGet()
        }
    }

    eat(food: EatenFood){
        if(this.isAuthenticated()) {
            this.authenticatedUser!.eat(food)
        } else {
            this.guestUser.eat(food)
        }
    }
}