import {observer} from "mobx-react-lite";
import {useStore} from "../../../../../stores/store";
import {ReactNode} from "react";
import MainContainer from "../../../../../components/MainContainer";
import {Box, Button} from "@mui/material";
import theme from "../../../../theme";
import {PersonOutlined} from "@mui/icons-material";

interface DesktopOverlayProps {
    children: ReactNode
}

export default observer (function DesktopOverlay({children}: DesktopOverlayProps) {
    const {userStore} = useStore()

    return (
        <MainContainer>
            <Box sx={{
                position: 'absolute',
                top: 0, right: 0,
                padding: theme.spacing(1)
            }}>
                <Button variant={'contained'} sx={{width: '36px', aspectRatio: 1}}>
                    <PersonOutlined fontSize={'large'} />
                </Button>
            </Box>
            {children}
        </MainContainer>
    )
})