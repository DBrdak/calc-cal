import {Button, Typography} from "@mui/material";
import theme from "../../../theme";
import {useNavigate} from "react-router-dom";

export const ForgotPassowrdOption = () => {
    const navigate = useNavigate()

    return (
        <>
            <Typography>
                Forgot password?
            </Typography>
            <Button onClick={() => navigate('/verify-code/password')} sx={{ padding: theme.spacing(1)}}>
                Reset password
            </Button>
        </>
    );
};