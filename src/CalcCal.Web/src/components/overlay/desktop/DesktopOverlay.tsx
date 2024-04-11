import {observer} from "mobx-react-lite";
import {useStore} from "../../../stores/store";
import {ReactNode} from "react";
import MainContainer from "../../MainContainer";
import {Box, Button} from "@mui/material";
import theme from "../../../app/theme";
import {CalculateOutlined, PersonOutlined} from "@mui/icons-material";
import {useLocation, useNavigate} from "react-router-dom";

interface DesktopOverlayProps {
    children: ReactNode
}

export default observer (function DesktopOverlay({children}: DesktopOverlayProps) {
    const {userStore} = useStore()
    const location = useLocation()
    const navigate = useNavigate()

    function handleClick() {
        const navigatePath = location.pathname === '/' ?
            userStore.isAuthenticated() || userStore.token ?
                '/user' :
                '/login' :
            '/'

            navigate(navigatePath)
    }

    return (
        <MainContainer>
            <Box sx={{
                position: 'absolute',
                top: 0, right: 0,
                padding: theme.spacing(1)
            }}>
                <Button variant={'contained'} sx={{width: '36px', aspectRatio: 1}} onClick={handleClick}>
                    {
                        location.pathname === '/' ?
                            <PersonOutlined fontSize={'large'}/>
                            :
                            <CalculateOutlined fontSize={'large'} />
                    }
                </Button>
            </Box>
            {children}
        </MainContainer>
    )
})