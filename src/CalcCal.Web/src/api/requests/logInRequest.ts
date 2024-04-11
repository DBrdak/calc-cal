export interface LogInRequest {
    username: string
    password: string
}

export class LogInRequest implements LogInRequest {
    constructor() {
        this.username = ''
        this. password = ''
    }
}