export interface RegisterRequest {
    username: string
    firstName: string
    lastName: string
    countryCode: string
    phoneNumber: string
    password: string
}

export class RegisterRequest implements RegisterRequest {
    constructor(){
        this.username = ''
        this.countryCode = ''
        this.firstName = ''
        this.lastName = ''
        this.phoneNumber = ''
        this.password = ''
    }
}