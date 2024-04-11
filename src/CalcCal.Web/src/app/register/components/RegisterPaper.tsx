import {Grid, Paper} from "@mui/material";
import theme from "../../theme";
import LoginForm from "../../login/components/LoginForm";
import {Options} from "../../login/components/options/Options";
import {LoginOption} from "./options/LoginOption";
import {RegisterForm} from "./RegisterForm";

export const RegisterPaper = () => {
    return (
        <Grid container component={Paper} sx={{
            maxWidth: '500px',
            width: '95vw',
            minWidth: '250px',
            maxHeight: '100px',
            minHeight: '750px',
            display: 'flex',
            alignItems: 'start',
            borderRadius: '20px',
            position: 'relative',
            paddingTop: theme.spacing(6)
        }}>
            <RegisterForm />
            <LoginOption />
        </Grid>
    );
};