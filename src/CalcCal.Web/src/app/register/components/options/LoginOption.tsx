import {Box, Button, Typography} from "@mui/material";
import theme from "../../../theme";
import {useNavigate} from "react-router-dom";

export const LoginOption = () => {
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
                Already have an account?
            </Typography>
            <Button onClick={() => navigate('/login')} sx={{ padding: theme.spacing(1)}}>
                Login
            </Button>
        </Box>
    );
};