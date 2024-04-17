import {makeAutoObservable} from "mobx";
import {User} from "../models/user";
import {RegisterRequest} from "../api/requests/registerRequest";
import agent from "../api/agent";
import {LogInRequest} from "../api/requests/logInRequest";
import {EatenFood} from "../models/eatenFood";
import {toast} from "react-toastify";

export default class UserStore {
    private guestUser: User = User.guest()
    private authenticatedUser?: User = undefined
    token: string | null = localStorage.getItem('jwt')
    verificationCountryCode?: string
    verificationPhoneNumber?: string
    verificationCode?: string
    getLoading: boolean = false
    loginLoading: boolean = false
    registerLoading: boolean = false
    changePasswordLoading = false
    verifyPhoneNumberLoading = false
    sendVerificationCodeLoading = false

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
            this.guestUser.eatenFood
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

    private setChangePasswordLoading(state: boolean) {
        this.changePasswordLoading = state
    }

    private setSendVerificationCodeLoading(state: boolean) {
        this.sendVerificationCodeLoading = state
    }

    private setVerifyPhoneNumberLoading(state: boolean) {
        this.verifyPhoneNumberLoading = state
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
            this.setVerificationPhoneNumber(registerRequest.countryCode, registerRequest.phoneNumber)
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
            return true
        } catch (e) {
            this.logout()
            return false
        } finally {
            this.setGetLoading(false)
        }
    }

    async changePassword(newPassword: string){
        this.setChangePasswordLoading(true)

        try {
            if(this.verificationCountryCode && this.verificationPhoneNumber && this.verificationCode){
                await agent.users.changePassword({
                    newPassword: newPassword,
                    countryCode: this.verificationCountryCode,
                    phoneNumber: this.verificationPhoneNumber,
                    verificationCode: this.verificationCode
                })
                this.setVerificationPhoneNumber(undefined, undefined)
                this.setVerificationCode(undefined)
            }
        } catch (e) {
        } finally {
            this.setChangePasswordLoading(false)
        }
    }

    async verifyPhoneNumber(verificationCode: string) {
        this.setVerifyPhoneNumberLoading(true)

        try {
            if(this.verificationCountryCode && this.verificationPhoneNumber){

                await agent.users.verifyPhone({
                        countryCode: this.verificationCountryCode,
                        phoneNumber: this.verificationPhoneNumber,
                        code: verificationCode
                })
                this.setVerificationPhoneNumber(undefined, undefined)
                this.setVerificationCode(undefined)
            }
        } catch (e) {
        } finally {
            this.setVerifyPhoneNumberLoading(false)
        }
    }

    async sendVerificationCode(countryCode: string, phoneNumber: string) {
        this.setSendVerificationCodeLoading(true)

        try {
            await agent.users.sendVerificationCode({
                countryCode: countryCode,
                phoneNumber: phoneNumber,
            })
            this.setVerificationPhoneNumber(countryCode, phoneNumber)
        } catch (e) {
        } finally {
            this.setSendVerificationCodeLoading(false)
        }
    }

    logout() {
        this.setAuthenticatedUser(undefined)
        this.setToken(null)
        localStorage.removeItem('jwt')
    }

    eat(food: EatenFood){
        if(this.isAuthenticated() && this.authenticatedUser) {

            this.authenticatedUser.eatenFood.push(food)
        } else {
            this.guestUser.eatenFood.push(food)
        }
    }

    private setVerificationPhoneNumber(countryCode: string | undefined, phoneNumber: string | undefined){
        
        this.verificationPhoneNumber = phoneNumber
        this.verificationCountryCode = countryCode
    }

    setVerificationCode(code: string | undefined) {
        this.verificationCode = code
    }
}