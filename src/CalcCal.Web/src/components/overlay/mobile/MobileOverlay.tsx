import React from 'react';
import {observer} from "mobx-react-lite";
import {useStore} from "../../../stores/store";
import MainContainer from "../../MainContainer";
import {Global} from "@emotion/react";
import {MobileOverlayRoot} from "./components/MobileOverlayRoot";
import {TopMobileDrawer} from "./components/TopMobileDrawer";
import BottomMobileDrawer from "./components/BottomMobileDrawer";
import {CssBaseline} from "@mui/material";

interface MobileOverlayProps {
    children: React.ReactNode
}

const MobileOverlay = ({ children}: MobileOverlayProps) => {
    const {userStore, foodStore } = useStore()
    const drawerBleeding = 56;

    return (
        <MobileOverlayRoot>
            <CssBaseline />
            <Global
                styles={(theme) => ({
                    '.MuiDrawer-root.MuiDrawer-anchorTop > .MuiPaper-root': {
                        height: `calc(30% - ${drawerBleeding}px)`,
                        overflow: 'visible',
                    },
                    '.MuiDrawer-root.MuiDrawer-anchorBottom > .MuiPaper-root': {
                        height: `calc(70% - ${drawerBleeding}px)`,
                        overflow: 'visible',
                    },
                })}
            />
            <TopMobileDrawer
                isAuthenticated={userStore.isAuthenticated()}
                user={userStore.user}
                loading={userStore.getLoading}
            />
            <MainContainer>
                {children}
            </MainContainer>
            <BottomMobileDrawer eatenFood={userStore.eatenFood || []} eatLoading={foodStore.eatloading} />
        </MobileOverlayRoot>
    );
};

export default observer(MobileOverlay)