import {Box, Typography} from "@mui/material";
import theme from "../app/theme";
import React from "react";

export const AppName = () => {
    return (
        <Box sx={{
            position: 'absolute', top: 0, left: 0, right: 0,
            textAlign: 'center'
        }}>
            <Typography variant="h1" fontWeight={'bolder'} fontFamily={'Segoe UI'} sx={{
                fontSize: '8vh'
            }}>
                <span style={{ color: theme.palette.primary.main }}>Calc</span>
                <span style={{ color: theme.palette.secondary.main }}>Cal</span>
            </Typography>
        </Box>
    );
};