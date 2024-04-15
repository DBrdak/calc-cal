import {useParams} from "react-router-dom";
import {isTouchDevice} from "../../utlis/layout/deviceInspector";
import MainContainer from "../../components/MainContainer";
import {AppName} from "../../components/AppName";
import DesktopOverlay from "../../components/overlay/desktop/DesktopOverlay";
import {Stack} from "@mui/material";
import VerifyCodePaper from "./components/VerifyCodePaper";

export const VerifyCodePage = () => {
    const {type} = useParams()

    return (
        isTouchDevice() ?
            <MainContainer>
                <AppName />
                <VerifyCodePaper />
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
                    <VerifyCodePaper />
                </Stack>
            </DesktopOverlay>
    )
}