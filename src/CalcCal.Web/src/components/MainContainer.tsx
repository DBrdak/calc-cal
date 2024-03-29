import React from 'react';
import { Box } from '@mui/material';
import theme from "../app/theme";

interface MainContainerProps {
    children: React.ReactNode
}

const MainContainer = ({children}: MainContainerProps) => {
    return (
        <Box
            sx={{
                display: 'flex',
                minHeight: '100svh',
                width: '100svw',
                backgroundColor: theme.palette.background.default,
                alignItems: 'center',
                justifyContent: 'center',
                flexDirection: 'column'
            }}
        >
            {children}
        </Box>
    );
};

export default MainContainer;