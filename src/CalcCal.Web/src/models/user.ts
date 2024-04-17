import {EatenFood} from "./eatenFood";

export interface User {
    userId: string
    firstName: string
    lastName: string
    username: string
    countryCode: string
    phoneNumber: string
    isPhoneNumberVerified: boolean
    eatenFood: EatenFood[]
}

export class User implements User {

    constructor(userId: string, firstName: string, lastName: string, username: string, countryCode: string, phoneNumber: string, isPhoneNumberVerified: boolean, eatenFood?: EatenFood[]) {
        this.userId = userId
        this.firstName = firstName
        this.lastName = lastName
        this.username = username
        this.phoneNumber = phoneNumber
        this.isPhoneNumberVerified = isPhoneNumberVerified
        this.eatenFood = eatenFood || []
    }

    static guest() {
        return new User('', 'Guest', 'Guest', 'Guest', '','', true, []);
    }
}