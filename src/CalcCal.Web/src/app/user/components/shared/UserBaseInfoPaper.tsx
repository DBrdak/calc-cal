import {Grid, Paper} from "@mui/material";
import theme from "../../../theme";
import UserBaseInfo from "./UserBaseInfo";

export const UserBaseInfoPaper = () => {
    return (
        <Grid container component={Paper} sx={{
            maxWidth: '500px',
            width: '95vw',
            minWidth: '250px',
            maxHeight: '700px',
            minHeight: '460px',
            display: 'flex',
            alignItems: 'start',
            borderRadius: '20px',
            position: 'relative',
            paddingTop: theme.spacing(6)
        }}>
            <UserBaseInfo />
        </Grid>
    );
};