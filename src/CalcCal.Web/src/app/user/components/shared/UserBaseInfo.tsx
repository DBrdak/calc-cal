import {Grid, Typography} from "@mui/material"
import theme from "../../../theme"
import {useStore} from "../../../../stores/store";

export default function UserBaseInfo () {
    const {userStore} = useStore()

    return (
        <Grid item xs={12}>
            <Grid item xs={12} marginBottom={theme.spacing(5)}>
                <Typography>
                    Username
                </Typography>
                <Typography>

                </Typography>
            </Grid>
            <Grid item xs={12} marginBottom={theme.spacing(3)}>
            </Grid>
            <Grid item xs={12}>
            </Grid>
        </Grid>
    );
};