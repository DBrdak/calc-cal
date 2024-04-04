import {Box, List, ListItem, ListItemIcon, ListItemText} from "@mui/material";
import {LoginOutlined, LogoutOutlined, PersonAddAltOutlined, PersonOutlined} from "@mui/icons-material";
import MenuIcon from "@mui/icons-material/Menu";
import React from "react";
import {useLocation, useNavigate} from "react-router-dom";

interface TopDrawerContentProps {
    isAuthenticated: boolean
}

export const TopDrawerContent = ({isAuthenticated}: TopDrawerContentProps) => {
    const location = useLocation()
    const navigate = useNavigate()

    return (
        <Box sx={{
            padding: 2,
            height: '100%',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
        }}>
            <List sx={{
                minWidth: '200px',
                maxWidth: '600px'
            }}>
                {
                    !isAuthenticated &&
                    <ListItem button onClick={() => navigate('/login')} sx={{}}>
                        <ListItemIcon>
                            <LoginOutlined />
                        </ListItemIcon>
                        <ListItemText primary="Login"  />
                    </ListItem>
                }
                {
                    !isAuthenticated &&
                    <ListItem button onClick={() => navigate('/register')}>
                        <ListItemIcon>
                            <PersonAddAltOutlined />
                        </ListItemIcon>
                        <ListItemText primary="Register" />
                    </ListItem>
                }
                {
                    isAuthenticated &&
                    <ListItem button onClick={() => navigate('/user')}>
                        <ListItemIcon>
                            <PersonOutlined />
                        </ListItemIcon>
                        <ListItemText primary="Profile" />
                    </ListItem>
                }
                {
                    isAuthenticated &&
                    <ListItem button onClick={() => navigate('/logout')}>
                        <ListItemIcon>
                            <LogoutOutlined />
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
};