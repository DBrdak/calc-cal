import {EatenFood} from "./eatenFood";

export interface User {
    userId: string
    firstName: string
    lastName: string
    username: string
    phoneNumber: string
    eatenFood?: EatenFood[]
}

export class User implements User {

    constructor(userId: string, firstName: string, lastName: string, username: string, phoneNumber: string, eatenFood?: EatenFood[]) {
        this.userId = userId
        this.firstName = firstName
        this.lastName = lastName
        this.username = username
        this.phoneNumber = phoneNumber
        this.eatenFood = eatenFood || []
    }

    eat(food: EatenFood) {
        this.eatenFood?.push(food)
    }

    static guest() {
        return new User('', 'Guest', 'Guest', 'Guest', '', []);
    }
}