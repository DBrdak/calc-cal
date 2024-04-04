import {isTouchDevice} from "../../utlis/layout/deviceInspector";
import MainContainer from "../../components/MainContainer";
import {AppName} from "../../components/AppName";
import {LoginPaper} from "../login/components/LoginPaper";
import DesktopOverlay from "../../components/overlay/desktop/DesktopOverlay";
import {Stack} from "@mui/material";
import {RegisterPaper} from "./components/RegisterPaper";

export const RegisterPage = () => {
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
    );
};