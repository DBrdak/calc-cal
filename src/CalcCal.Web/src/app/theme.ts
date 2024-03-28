import { createTheme } from '@mui/material/styles';

const theme = createTheme({
    palette: {
        mode: 'light',
        primary: {
            main: '#EF476F', // Cherry Red (Primary Color)
            light: '#F781A6', // Lighter tint of Primary Color
            dark: '#D32F4F', // Darker shade of Primary Color
        },
        secondary: {
            main: '#FCAF03', // Mustard Yellow (Secondary Color)
            light: '#FCF0C7', // Lighter tint of Secondary Color
            dark: '#DBA002', // Darker shade of Secondary Color
        },
        error: {
            main: '#C62828', // Reddish tone for error messages (same from previous example)
            dark: '#9D1F1F', // Darker shade for error emphasis (same from previous example)
            light: '#F9DEDC', // Lighter shade for error background (same from previous example)
        },
        info: {
            main: '#A9A9A9', // Light gray for informational elements
        },
        background: {
            default: '#f8f8f8', // Very light gray for background
            paper: '#FFFFFF', // White for content areas
        },
        text: {
            primary: '#212121', // Dark gray for primary text (same from previous example)
            secondary: '#757575', // Lighter gray for secondary text (same from previous example)
        },
        success: {
            main: '#43A047', // Green for success messages
            light: '#C8E6C9', // Lighter shade for success background (same from previous example)
            dark: '#388E3C', // Darker shade for success emphasis (same from previous example)
        },
    },
    breakpoints: {
        values: {
            xs: 0,
            sm: 600,
            md: 900,
            lg: 1500,
            xl: 2200,
        }
    },
    components: {
        MuiCssBaseline: {
            styleOverrides: {
                '*::-webkit-scrollbar': {
                    width: '10px',
                },
                '*::-webkit-scrollbar-track': {
                    background: 'transparent',
                },
                '*::-webkit-scrollbar-thumb': {
                    backgroundColor: '#888',
                    borderRadius: '15px',
                    '&:hover': {
                        backgroundColor: '#555',
                    },
                },
            },
        },
        MuiListItemButton:{
            styleOverrides: {
                root:{
                    "&.Mui-disabled": {
                        opacity: 1,
                    }
                }
            }
        },
        MuiButton: {
            styleOverrides: {
                root: {
                    borderRadius: 50
                },
            },
        },
    },
});

export default theme;