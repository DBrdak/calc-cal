import { createTheme } from '@mui/material/styles';

const theme = createTheme({
    palette: {
        mode: 'light',
        primary: {
            main: '#EF476F',
            light: '#F781A6',
            dark: '#D32F4F',
        },
        secondary: {
            main: '#FCAF03',
            light: '#FCF0C7',
            dark: '#DBA002',
        },
        error: {
            main: '#C62828',
            dark: '#9D1F1F',
            light: '#F9DEDC',
        },
        info: {
            main: '#A9A9A9',
        },
        background: {
            default: '#f8f8f8',
            paper: '#FFFFFF',
        },
        text: {
            primary: '#212121',
            secondary: '#757575',
        },
        success: {
            main: '#43A047',
            light: '#C8E6C9',
            dark: '#388E3C',
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
        MuiOutlinedInput: {
            styleOverrides: {
                root: {
                    borderRadius: 30
                },
            },
        },
    },
});

export default theme;