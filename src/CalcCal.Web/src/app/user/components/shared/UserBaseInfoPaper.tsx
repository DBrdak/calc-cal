import {Grid, Paper} from "@mui/material";
import theme from "../../../theme";
import UserBaseInfo from "./UserBaseInfo";
import {isTouchDevice} from "../../../../utlis/layout/deviceInspector";

export const UserBaseInfoPaper = () => {
    return (
        <Grid container component={Paper} sx={{
            maxWidth: '600px',
            width: '95vw',
            minWidth: '250px',
            maxHeight: '700px',
            zIndex: 1000,
            borderRadius: isTouchDevice() ? '20px' : '20px 20px 0px 0px',
            minHeight: '350px',
            display: 'flex',
            alignItems: 'start',
            position: 'relative',
            paddingTop: theme.spacing(2)
        }}>
            <UserBaseInfo />
        </Grid>
    );
};