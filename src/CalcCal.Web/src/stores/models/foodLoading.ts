
export interface FoodLoading {
    get: boolean
    add: boolean
    eat: boolean
}

export class FoodLoading implements FoodLoading {
    constructor() {
        this.eat = false
        this.add = false
        this.get = false
    }

    startEat(){
        this.eat = true
    }
    stopEat(){
        this.eat = false
    }

    startGet(){
        this.get = true
    }
    stopGet(){
        this.get = false
    }

    startAdd(){
        this.add = true
    }
    stopAdd(){
        this.add = false
    }
}