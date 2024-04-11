import {makeAutoObservable} from "mobx";

export default class LayoutStore {

    constructor() {
        makeAutoObservable(this);
    }
}