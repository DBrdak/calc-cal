import {makeAutoObservable} from "mobx";
import {Food} from "../models/food";
import agent from "../api/agent";
import {AddFoodRequest} from "../api/requests/addFoodRequest";
import {EatFoodRequest} from "../api/requests/eatFoodRequest";
import UserStore from "./userStore";
import {store} from "./store";

export default class FoodStore {
    private food: Map<string, Food> = new Map<string, Food>()
    private searchPhrase?: string = undefined
    private usedSearchPhrases: string[] = []
    getloading: boolean = false
    addloading: boolean = false
    eatloading: boolean = false
    selectedFood: Food | null = null
    selectedFoodWeight: number | null = null

    constructor() {
        makeAutoObservable(this);
    }

    get query(){
        const food = Array.from(this.food.values())

        return this.searchPhrase ?
            food.filter(f => f.name.toLowerCase().includes(this.searchPhrase!)).slice(0,5) :
            []
    }

    setSelectedFoodWeight(weight: number | null) {
        this.selectedFoodWeight = weight
    }

    undoFoodSelection() {
        this.setSelectedFood(null)
    }

    private setEatLoading(state: boolean) {
        this.eatloading = state
    }

    private setGetLoading(state: boolean) {
        this.getloading = state
    }

    private setAddLoading(state: boolean) {
        this.addloading = state
    }

    private setFood(food: Food){
        this.food.set(food.name, food)
    }

    private setSelectedFood(food: Food | null) {
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

        this.setGetLoading(true)

        try {
            this.setSearchPhrase(foodName)
            const food = isApiRequired && await agent.food.getFood(foodName)
            food && food.forEach(f => this.setFood(f))
        } catch(e) {

        } finally {
            this.setGetLoading(false)
        }
    }

    async selectFood(foodName: string) {
        const food = this.food.get(foodName)
        console.log(foodName)

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
        this.setAddLoading(true)

        try {
            const addedFood = await agent.food.addFood(request)
            addedFood.forEach(f => this.setFood(f))
            return addedFood
        } catch (e) {

        } finally {
            this.setAddLoading(false)
        }
    }

    async eatFood(){
        if(!this.selectedFood || !this.selectedFoodWeight) {
            return
        }
        this.setEatLoading(true)

        const request: EatFoodRequest = {
            foodId: this.selectedFood.foodId,
            foodWeight: this.selectedFoodWeight
        }

        try {
            const eatenFood = await agent.food.eatFood(request)
            this.setSelectedFood(null)
            this.setSelectedFoodWeight(null)
            store.userStore.eat(eatenFood)
            return eatenFood
        } catch (e) {

        } finally {
            this.setEatLoading(false)
        }
    }
}