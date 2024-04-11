import MainContainer from "../../components/MainContainer";
import {Stack} from "@mui/material";
import {isTouchDevice} from "../../utlis/layout/deviceInspector";
import DesktopOverlay from "../../components/overlay/desktop/DesktopOverlay";
import {AppName} from "../../components/AppName";
import {LoginPaper} from "./components/LoginPaper";
import {useEffect} from "react";
import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/store";
import {useNavigate} from "react-router-dom";

export default observer(function LoginPage () {
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
                <LoginPaper />
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
                    <LoginPaper />
                </Stack>
            </DesktopOverlay>
    )
})