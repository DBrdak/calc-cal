import { RouteObject } from "react-router"
import App from "../app/App";
import {createBrowserRouter} from "react-router-dom";
import {UserPage} from "../app/user/UserPage";
import {RegisterPage} from "../app/register/RegisterPage";
import {LoginPage} from "../app/login/LoginPage";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            {path: '/user', element: <UserPage />},
            {path: '/login', element: <LoginPage />},
            {path: '/register', element: <RegisterPage />}
        ]
    }
]

export const router = createBrowserRouter(routes);