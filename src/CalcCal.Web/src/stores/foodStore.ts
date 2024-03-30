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
    private selectedFood?: Food

    constructor() {
        makeAutoObservable(this);
    }

    get query(){
        const food = Array.from(this.food.values())

        return this.searchPhrase ?
            food.filter(f => f.name.toLowerCase().includes(this.searchPhrase!)).slice(0,10) :
            []
    }

    private setFood(food: Food){
        this.food.set(food.name, food)
    }

    private setSelectedFood(food: Food) {
        this.selectedFood = food
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

        const isApiRequired = !(this.usedSearchPhrases.some(n => n.toLowerCase() === foodName)
            || this.usedSearchPhrases.some(n => n.toLowerCase().includes(foodName))
            || foodName.length > 4
            || foodName.length < 3)

        this.loading.startGet()

        try {
            this.setSearchPhrase(foodName)
            const food = isApiRequired && await agent.food.getFood(foodName)
            food && food.forEach(f => this.setFood(f))
        } catch(e) {

        } finally {
            this.loading.stopGet()
        }
    }

    async selectFood(foodName: string) {
        const food = this.food.get(foodName)

        if(food) {
            this.setSelectedFood(food)
            return food
        }

        const newFood = await this.addFood({foodName})

        if(newFood){
            newFood.forEach(f => this.setFood(f))
            return newFood
        }
    }

    private async addFood(request: AddFoodRequest) {
        this.loading.startAdd()

        try {
            const addedFood = await agent.food.addFood(request)
            addedFood.forEach(f => this.setFood(f))
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