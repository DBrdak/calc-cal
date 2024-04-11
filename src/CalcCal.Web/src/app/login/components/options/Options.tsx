import {RegisterOption} from "./RegisterOption";
import {ForgotPassowrdOption} from "./ForgotPassowrdOption";
import theme from "../../../theme";
import {Box} from "@mui/material";

export const Options = () => {
    return (
        <Box sx={{
            position: 'absolute',
            bottom: 0, left: 0, right: 0,
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            padding: theme.spacing(2),
            flexDirection: 'column',
        }}>
            <RegisterOption />
            <ForgotPassowrdOption />
        </Box>
    );
};