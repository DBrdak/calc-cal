import {Button, ButtonGroup, Grid, SxProps, Theme, Typography} from "@mui/material"
import theme from "../../../theme"
import {useStore} from "../../../../stores/store";
import {useNavigate} from "react-router-dom";

export default function UserBaseInfo () {
    const {userStore} = useStore()
    const navigate = useNavigate()

    const userPropertyGridSx: SxProps<Theme> = {
        textAlign: 'center'
    }

    return (
        <Grid item xs={12}>
            <Grid item xs={12} marginBottom={theme.spacing(5)} sx={userPropertyGridSx}>
                <Typography variant={'h6'}>
                    Username
                </Typography>
                <Typography variant={'subtitle1'}>
                    {userStore.user.username}
                </Typography>
            </Grid>
            <Grid item xs={12} marginBottom={theme.spacing(3)} sx={userPropertyGridSx}>
                <Typography variant={'h6'} >
                    Phone Number
                </Typography>
                <Typography variant={'subtitle1'}>
                    {userStore.user.phoneNumber}
                </Typography>
            </Grid>
            <Grid item xs={12} marginBottom={theme.spacing(3)} sx={userPropertyGridSx}>
                <Typography variant={'h6'}>
                    Full Name
                </Typography>
                <Typography variant={'subtitle1'}>
                    {userStore.user.firstName} {userStore.user.lastName}
                </Typography>
            </Grid>
            <Grid item xs={12} marginBottom={theme.spacing(3)} textAlign={'center'} >
                <Button variant={'outlined'} onClick={() => navigate('/logout')}>
                    Logout
                </Button>
            </Grid>
        </Grid>
    );
};