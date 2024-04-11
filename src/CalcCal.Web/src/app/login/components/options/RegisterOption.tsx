import {Button, Typography} from "@mui/material";
import theme from "../../../theme";
import {useNavigate} from "react-router-dom";

export const RegisterOption = () => {
    const navigate = useNavigate()

    return (
        <>
            <Typography>
                Don't have an account yet?
            </Typography>
            <Button onClick={() => navigate('/register')} sx={{ padding: theme.spacing(1)}}>
                Register
            </Button>
        </>
    );
};