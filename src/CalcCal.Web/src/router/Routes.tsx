import { RouteObject } from "react-router"
import App from "../app/App";
import {createBrowserRouter} from "react-router-dom";
import {UserPage} from "../app/user/UserPage";
import RegisterPage from "../app/register/RegisterPage";
import LoginPage from "../app/login/LoginPage";
import LogoutPage from "../app/logout/LogoutPage";
import NotFoundPage from "../app/notFound/NotFoundPage";
import {ResetPasswordPage} from "../app/reset-password/ResetPasswordPage";
import {VerifyCodePage} from "../app/verify-code/VerifyCodePage";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            {path: '/user', element: <UserPage />},
            {path: '/login', element: <LoginPage />},
            {path: '/register', element: <RegisterPage />},
            {path: '/logout', element: <LogoutPage />},
            {path: '/verify-code/:type', element: <VerifyCodePage />},
            {path: '/reset-password', element: <ResetPasswordPage />},
            {path: '*', element: <NotFoundPage />},
        ]
    }
]

export const router = createBrowserRouter(routes);