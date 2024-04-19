import {SwipeableDrawer, Typography} from "@mui/material";
import theme from "../../../../app/theme";
import {TopDrawerContent} from "./TopDrawerContent";
import React, {useEffect} from "react";
import {DrawerBox} from "./DrawerBox";
import {Puller} from "./Puller";
import {User} from "../../../../models/user";
import {DotLoader} from "../../../DotLoader";

interface TopMobileDrawerProps {
    isAuthenticated: boolean
    user: User
    loading: boolean
}

export const TopMobileDrawer = ({isAuthenticated, user, loading}: TopMobileDrawerProps) => {
    const [topDrawerOpen, setTopDrawerOpen] = React.useState(false)
    const drawerBleeding = 56

    const handleTopDrawerOpen = () => {
        setTopDrawerOpen(true);
    }

    const handleTopDrawerClose = () => {
        setTopDrawerOpen(false);
    }

    return (
        <SwipeableDrawer
            className={'MuiDrawer-anchorTop'}
            anchor="top"
            open={topDrawerOpen}
            onOpen={handleTopDrawerOpen}
            onClose={handleTopDrawerClose}
            swipeAreaWidth={drawerBleeding}
            disableSwipeToOpen={false}
            ModalProps={{
                keepMounted: true,
            }}
            onTouchMove={(event: React.TouchEvent<HTMLDivElement>) => event.preventDefault()}
        >
            <DrawerBox
                sx={{
                    position: 'absolute',
                    bottom: -drawerBleeding,
                    borderBottomLeftRadius: 8,
                    borderBottomRightRadius: 8,
                    visibility: 'visible',
                    right: 0,
                    left: 0,
                    textAlign: 'center',
                    display: 'flex',
                    justifyContent: 'right'
                }}
            >
                <Puller variant={'top'} />
                <Typography sx={{ p: 2, color: theme.palette.text.secondary, width: '100%', textAlign: 'center' }}
                            fontWeight={'bolder'}
                            fontSize={'x-large'}>
                    {loading ? <DotLoader /> : user.username}
                </Typography>
            </DrawerBox>
            <DrawerBox
                sx={{
                    px: 2,
                    pb: 2,
                    height: '100%',
                    overflow: 'auto',
                }}
            >
                <TopDrawerContent isAuthenticated={isAuthenticated} />
            </DrawerBox>
        </SwipeableDrawer>
    );
};