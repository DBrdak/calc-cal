import {Grid, Paper, useMediaQuery} from "@mui/material";
import {LoginForm} from "./LoginForm";
import theme from "../../theme";
import {RegisterOption} from "./RegisterOption";

export const LoginPaper = () => {
    const isMobile = useMediaQuery(theme.breakpoints.down('md'))

    return (
        <Grid container component={Paper} sx={{
            maxWidth: '500px',
            width: '95vw',
            minWidth: '250px',
            maxHeight: '700px',
            minHeight: '420px',
            display: 'flex',
            alignItems: 'start',
            borderRadius: '20px',
            position: 'relative',
            paddingTop: theme.spacing(6)
        }}>
            <LoginForm />
            <RegisterOption />
        </Grid>
    );
};