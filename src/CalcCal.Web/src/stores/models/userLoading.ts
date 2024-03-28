export interface UserLoading {
    register: boolean
    login: boolean
    get: boolean
}

export class UserLoading implements UserLoading {
    constructor() {
        this.register = false
        this.login = false
        this.get = false
    }

    startRegister(){
        this.register = true
    }
    stopRegister(){
        this.register = false
    }

    startLogin(){
        this.login = true
    }
    stopLogin(){
        this.login = false
    }

    startGet(){
        this.get = true
    }
    stopGet(){
        this.get = false
    }
}