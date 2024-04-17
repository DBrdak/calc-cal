import {useStore} from "../../stores/store";
import {useNavigate} from "react-router-dom";
import React, {useEffect, useState} from "react";
import {isTouchDevice} from "../../utlis/layout/deviceInspector";
import MobileOverlay from "../../components/overlay/mobile/MobileOverlay";
import DesktopOverlay from "../../components/overlay/desktop/DesktopOverlay";
import {MobileUserContainer} from "./components/mobile/MobileUserContainer";
import {DesktopUserContainer} from "./components/desktop/DesktopUserContainer";
import {DotLoader} from "../../components/DotLoader";
import {observer} from "mobx-react-lite";

const UserPage = () => {
    const {userStore} = useStore()
    const navigate = useNavigate()
    const [loading, setLoading] = useState(false)

    useEffect(() => {
        setLoading(true)
        userStore.loadCurrentUser().then(async () => {

            if(!userStore.token || !userStore.isAuthenticated() || !localStorage.getItem('jwt')) {
                navigate('/login')
                return
            }

            if(!userStore.user.isPhoneNumberVerified) {
                await userStore.sendVerificationCode(userStore.user.countryCode, userStore.user.phoneNumber).then(() => {
                    navigate('/verify-code/phone')
                })
            }

            setLoading(false)
        })
    }, [])

    return (
        isTouchDevice() ?
            <MobileOverlay>
                {
                    loading ?
                        <DotLoader />
                        :
                        <MobileUserContainer />
                }
            </MobileOverlay>
            :
            <DesktopOverlay>
                {
                    loading ?
                        <DotLoader />
                        :
                       <DesktopUserContainer/>
                }
            </DesktopOverlay>
    );
};

export default observer(UserPage)