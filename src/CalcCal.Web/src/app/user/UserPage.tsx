import {useStore} from "../../stores/store";
import {useNavigate} from "react-router-dom";
import React, {useEffect} from "react";
import {isTouchDevice} from "../../utlis/layout/deviceInspector";
import MobileOverlay from "../../components/overlay/mobile/MobileOverlay";
import DesktopOverlay from "../../components/overlay/desktop/DesktopOverlay";
import {MobileUserContainer} from "./components/mobile/MobileUserContainer";
import {DesktopUserContainer} from "./components/desktop/DesktopUserContainer";

export const UserPage = () => {
    const {userStore} = useStore()
    const navigate = useNavigate()

    useEffect(() => {
        if(!userStore.token || !userStore.isAuthenticated()) {
            navigate('/')
        }
    }, [])

    return (
        isTouchDevice() ?
            <MobileOverlay>
                <MobileUserContainer />
            </MobileOverlay>
            :
            <DesktopOverlay>
                <DesktopUserContainer />
            </DesktopOverlay>
    );
};