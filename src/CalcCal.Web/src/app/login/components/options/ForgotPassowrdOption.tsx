import {Button, Typography} from "@mui/material";
import theme from "../../../theme";

export const ForgotPassowrdOption = () => {
    return (
        <>
            <Typography>
                Forgot password?
            </Typography>
            <Button onClick={() => console.log()} sx={{ padding: theme.spacing(1)}}>
                Reset password
            </Button>
        </>
    );
};