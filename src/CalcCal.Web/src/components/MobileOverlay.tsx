import React from 'react';
import {
    Box,
    CssBaseline,
    Divider,
    Drawer,
    IconButton,
    styled,
    SwipeableDrawer,
    Typography,
    useMediaQuery
} from '@mui/material';
import { List, ListItem, ListItemIcon, ListItemText } from '@mui/material';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import MenuIcon from '@mui/icons-material/Menu';
import {observer} from "mobx-react-lite";
import {useStore} from "../stores/store";
import {useLocation} from "react-router-dom";
import {grey} from "@mui/material/colors";
import MainContainer from "./MainContainer";
import theme from "../app/theme";
import {KeyboardArrowUp} from "@mui/icons-material";
import {Global} from "@emotion/react";

interface AppOverlayProps {
    children: React.ReactNode
}

const MobileOverlay = ({ children}: AppOverlayProps) => {
    const [topDrawerOpen, setTopDrawerOpen] = React.useState(false)
    const [bottomDrawerOpen, setBottomDrawerOpen] = React.useState(false)
    const isMobile = useMediaQuery(theme.breakpoints.down('lg'))
    const {userStore} = useStore()
    const location = useLocation()

    const handleTopDrawerOpen = () => {
        setTopDrawerOpen(true);
    };

    const handleTopDrawerClose = () => {
        setTopDrawerOpen(false);
    };

    const handleBottomDrawerOpen = () => {
        setBottomDrawerOpen(true);
    };

    const handleBottomDrawerClose = () => {
        setBottomDrawerOpen(false);
    };

    const tableData = [
        { id: 1, name: 'Item 1', value: 'Data 1' },
        { id: 2, name: 'Item 2', value: 'Data 2' },
    ];

    const topDrawerContent = (
        <Box sx={{ padding: 2 }}>
            <Typography variant="h6">Navigation</Typography>
            <List>
                {
                    !userStore.isAuthenticated() &&
                        <ListItem button onClick={() => console.log()}>
                            <ListItemIcon>
                                <MenuIcon />
                            </ListItemIcon>
                            <ListItemText primary="Login" />
                        </ListItem>
                }
                {
                    !userStore.isAuthenticated() &&
                        <ListItem button onClick={() => console.log()}>
                            <ListItemIcon>
                                <MenuIcon />
                            </ListItemIcon>
                            <ListItemText primary="Register" />
                        </ListItem>
                }
                {
                    userStore.isAuthenticated() &&
                        <ListItem button onClick={() => console.log()}>
                            <ListItemIcon>
                                <MenuIcon />
                            </ListItemIcon>
                            <ListItemText primary="Profile" />
                        </ListItem>
                }
                {
                    userStore.isAuthenticated() &&
                    <ListItem button onClick={() => console.log()}>
                        <ListItemIcon>
                            <MenuIcon />
                        </ListItemIcon>
                        <ListItemText primary="Logout" />
                    </ListItem>
                }
                {
                    location.pathname !== '/' &&
                        <ListItem button onClick={() => console.log()}>
                            <ListItemIcon>
                                <MenuIcon />
                            </ListItemIcon>
                            <ListItemText primary="Calculator" />
                        </ListItem>
                }
            </List>
        </Box>
    );

    const bottomDrawerContent = (
        <Box sx={{ padding: 2 }}>
            <Typography variant="h6">Table Data</Typography>
            <TableContainer>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>Name</TableCell>
                            <TableCell>Value</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {tableData.map((row) => (
                            <TableRow key={row.id}>
                                <TableCell>{row.id}</TableCell>
                                <TableCell>{row.name}</TableCell>
                                <TableCell>{row.value}</TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>
    );

    const StyledBox = styled('div')(({ theme }) => ({
        backgroundColor: theme.palette.background.paper,
    }));
//TODO Add TopPuller or adjust this to be on top or bottom
    const Puller = styled('div')(({ theme }) => ({
        width: 30,
        height: 6,
        backgroundColor: theme.palette.text.secondary,
        borderRadius: 3,
        position: 'absolute',
        top: 8,
        left: 'calc(50% - 15px)',
    }));

    const Root = styled('div')(({ theme }) => ({
        height: '100%',
        backgroundColor: theme.palette.background.default,
    }));

    const drawerBleeding = 56;

    return (
        <Root>
            <Global
                styles={{
                    '.MuiDrawer-root > .MuiPaper-root': {
                        height: `calc(50% - ${drawerBleeding}px)`,
                        overflow: 'visible',
                    },
                }}
            />
            <SwipeableDrawer
                anchor="top"
                open={topDrawerOpen}
                onOpen={handleTopDrawerOpen}
                onClose={handleTopDrawerClose}
                swipeAreaWidth={drawerBleeding}
                disableSwipeToOpen={false}
                ModalProps={{
                    keepMounted: true,
                }}
            >
                <StyledBox
                    onClick={handleBottomDrawerOpen}
                    sx={{
                        position: 'absolute',
                        bottom: -drawerBleeding,
                        borderBottomLeftRadius: 8,
                        borderBottomRightRadius: 8,
                        visibility: 'visible',
                        right: 0,
                        left: 0,
                    }}
                >
                    <Puller />
                    <Typography sx={{ p: 2, color: theme.palette.text.secondary }}>
                        User
                    </Typography>
                </StyledBox>
                <StyledBox
                    sx={{
                        px: 2,
                        pb: 2,
                        height: '100%',
                        overflow: 'auto',
                    }}
                >
                    {topDrawerContent}
                </StyledBox>
            </SwipeableDrawer>
            <MainContainer>
                {children}
            </MainContainer>
                <SwipeableDrawer
                    anchor="bottom"
                    open={bottomDrawerOpen}
                    onClose={handleBottomDrawerClose}
                    onOpen={handleBottomDrawerOpen}
                    swipeAreaWidth={drawerBleeding}
                    disableSwipeToOpen={false}
                    ModalProps={{
                        keepMounted: true,
                    }}
                >
                    <StyledBox
                        onClick={handleBottomDrawerOpen}
                        sx={{
                            position: 'absolute',
                            top: -drawerBleeding,
                            borderTopLeftRadius: 8,
                            borderTopRightRadius: 8,
                            visibility: 'visible',
                            right: 0,
                            left: 0,
                        }}
                    >
                        <Puller />
                        <Typography sx={{ p: 2, color: theme.palette.text.secondary }}>
                            51 results
                        </Typography>
                    </StyledBox>
                    <StyledBox
                        sx={{
                            px: 2,
                            pb: 2,
                            height: '100%',
                            overflow: 'auto',
                        }}
                    >
                        {bottomDrawerContent}
                    </StyledBox>
                </SwipeableDrawer>
        </Root>
    );
};

export default observer(MobileOverlay)