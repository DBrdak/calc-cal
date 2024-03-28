import { createContext, useContext } from "react";
import UserStore from "./userStore";
import LayoutStore from "./layoutStore";
import FoodStore from "./foodStore";

interface Store {
    foodStore: FoodStore
    layoutStore: LayoutStore
    userStore: UserStore
}

export const store: Store = {
    layoutStore: new LayoutStore(),
    userStore: new UserStore(),
    foodStore: new FoodStore(),
}

export const StoreContext = createContext(store);

export function useStore() {
    return useContext(StoreContext)
}