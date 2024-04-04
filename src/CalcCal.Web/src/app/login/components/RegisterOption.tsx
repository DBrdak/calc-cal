import {Box, Button, Typography} from "@mui/material";
import theme from "../../theme";
import {useNavigate} from "react-router-dom";

export const RegisterOption = () => {
    const navigate = useNavigate()

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
            <Typography>
                Don't have an account yet?
            </Typography>
            <Button onClick={() => navigate('/register')} sx={{ padding: theme.spacing(1)}}>
                Register
            </Button>
        </Box>
    );
};