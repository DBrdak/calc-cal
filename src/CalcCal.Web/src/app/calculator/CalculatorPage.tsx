import MobileOverlay from "./components/mobile/MobileOverlay";
import React from "react";
import DesktopOverlay from "./components/desktop/DesktopOverlay";
import CalculatorForm from "./components/shared/Calculator";
import {AppName} from "../../components/AppName";

export const CalculatorPage = () => {

    function isTouchDevice() {
        return ('maxTouchPoints' in navigator && navigator.maxTouchPoints > 0) ||
            ('msMaxTouchPoints' in navigator && Number(navigator.msMaxTouchPoints) > 0);
    }

    return (
        isTouchDevice() ?
            <MobileOverlay>
                <CalculatorForm />
            </MobileOverlay>
            :
            <DesktopOverlay>
                <CalculatorForm />
            </DesktopOverlay>
    );
};