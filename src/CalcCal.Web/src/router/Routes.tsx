import { RouteObject } from "react-router"
import App from "../app/App";
import {createBrowserRouter} from "react-router-dom";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
        ]
    }
]

export const router = createBrowserRouter(routes);