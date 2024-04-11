import {Box, Typography} from "@mui/material";
import theme from "../app/theme";
import React from "react";

export const AppName = () => {
    return (
        <Box sx={{
            textAlign: 'center',
            userSelect: 'none',
            marginBottom: theme.spacing(2)
        }}>
            <Typography variant="h1" height={'100%'} fontWeight={'bolder'} fontFamily={'Segoe UI'} sx={{
                fontSize: '10vh', letterSpacing: 4
            }}>
                <span style={{ color: theme.palette.primary.main }}>Calc</span>
                <span style={{ color: theme.palette.secondary.main }}>Cal</span>
            </Typography>
        </Box>
    );
};