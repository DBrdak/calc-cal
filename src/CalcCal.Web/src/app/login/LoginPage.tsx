import MainContainer from "../../components/MainContainer";
import {Stack} from "@mui/material";
import {isTouchDevice} from "../../utlis/layout/deviceInspector";
import DesktopOverlay from "../../components/overlay/desktop/DesktopOverlay";
import {AppName} from "../../components/AppName";
import {LoginPaper} from "./components/LoginPaper";

export const LoginPage = () => {

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
}