import {makeAutoObservable, observable} from "mobx";
import {FoodLoading} from "./models/foodLoading";
import {Food} from "../models/food";
import agent from "../api/agent";
import {AddFoodRequest} from "../api/requests/addFoodRequest";
import {EatFoodRequest} from "../api/requests/eatFoodRequest";
import UserStore from "./userStore";
import {useStore} from "./store";

export default class FoodStore {
    private food: Map<string, Food> = new Map<string, Food>()
    private searchPhrase?: string = undefined
    loading: FoodLoading = new FoodLoading()

    constructor() {
        makeAutoObservable(this);
    }

    get query(){
        const food = Array.from(this.food.values())

        return this.searchPhrase ?
            food.map(f => f.name.toLowerCase().includes(this.searchPhrase!)) :
            []
    }

    private setFood(food: Food){
        this.food.set(food.name, food)
    }

    private setSearchPhrase(foodName: string){
        this.searchPhrase = foodName
    }

    async getFood(foodName: string) {
        foodName = foodName.toLowerCase()
        this.loading.startGet()

        try {
            this.setSearchPhrase(foodName)
            const food = await agent.food.getFood(foodName)
            food.forEach(f => this.setFood(f))
        } catch(e) {

        } finally {
            this.loading.stopGet()
        }
    }

    async addFood(request: AddFoodRequest) {
        this.loading.startAdd()

        try {
            const addedFood = await agent.food.addFood(request)
            this.setFood(addedFood)
            return addedFood
        } catch (e) {

        } finally {
            this.loading.stopAdd()
        }
    }

    async eatFood(request: EatFoodRequest, userStore: UserStore){
        this.loading.startEat()

        try {
            const eatenFood = await agent.food.eatFood(request)
            userStore.eat(eatenFood)
            return eatenFood
        } catch (e) {

        } finally {
            this.loading.stopEat()
        }
    }
}