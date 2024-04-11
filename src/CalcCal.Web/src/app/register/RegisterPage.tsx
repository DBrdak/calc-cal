import {isTouchDevice} from "../../utlis/layout/deviceInspector";
import MainContainer from "../../components/MainContainer";
import {AppName} from "../../components/AppName";
import DesktopOverlay from "../../components/overlay/desktop/DesktopOverlay";
import {Stack} from "@mui/material";
import {RegisterPaper} from "./components/RegisterPaper";
import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/store";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";

export default observer (function RegisterPage () {
    const {userStore} = useStore()
    const navigate = useNavigate()

    useEffect(() => {
        if(userStore.token || userStore.isAuthenticated()) {
            navigate('/')
        }
    }, [])
    
    return (
        isTouchDevice() ?
            <MainContainer>
                <AppName />
                <RegisterPaper />
            </MainContainer>
            :
            <DesktopOverlay>
                <Stack direction={'column'} sx={{
                    minWidth: '400px',
                    maxWidth: '600px',
                    width: '60vw',
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center'
                }}>
                    <AppName />
                    <RegisterPaper />
                </Stack>
            </DesktopOverlay>
    )
})