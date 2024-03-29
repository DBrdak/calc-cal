import {makeAutoObservable} from "mobx";
import {FoodLoading} from "./models/foodLoading";
import {Food} from "../models/food";
import agent from "../api/agent";
import {AddFoodRequest} from "../api/requests/addFoodRequest";
import {EatFoodRequest} from "../api/requests/eatFoodRequest";
import UserStore from "./userStore";

export default class FoodStore {
    private food: Map<string, Food> = new Map<string, Food>()
    private searchPhrase?: string = undefined
    private usedSearchPhrases: string[] = []
    loading: FoodLoading = new FoodLoading()

    constructor() {
        makeAutoObservable(this);
    }

    get query(){
        const food = Array.from(this.food.values())

        return this.searchPhrase ?
            food.filter(f => f.name.toLowerCase().includes(this.searchPhrase!)) :
            []
    }

    private setFood(food: Food){
        this.food.set(food.name, food)
    }

    private setUsedSearchPhrase(searchPhrase: string){
        this.usedSearchPhrases.push(searchPhrase)
    }

    private setSearchPhrase(foodName: string){
        this.setUsedSearchPhrase(foodName)
        this.searchPhrase = foodName
    }

    async getFood(foodName: string) {
        foodName = foodName.toLowerCase()

        if(this.usedSearchPhrases.some(n => n.toLowerCase() === foodName)
            || this.usedSearchPhrases.some(n => n.toLowerCase().includes(foodName))
            || foodName.length > 4){
            this.setSearchPhrase(foodName)
            this.loading.stopGet()
            return
        }

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