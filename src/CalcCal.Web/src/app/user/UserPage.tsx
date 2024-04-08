import {useStore} from "../../stores/store";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";

export const UserPage = () => {
    const {userStore} = useStore()
    const navigate = useNavigate()

    useEffect(() => {
        if(!userStore.token || !userStore.isAuthenticated()) {
            navigate('/')
        }
    }, [])

    return (
        <></>
    );
};