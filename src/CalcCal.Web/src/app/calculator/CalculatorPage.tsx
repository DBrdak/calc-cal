import MobileOverlay from "./components/mobile/overlay/MobileOverlay";
import React from "react";
import DesktopOverlay from "./components/desktop/overlay/DesktopOverlay";
import {CalculatorDesktopContainer} from "./components/desktop/calculatorContainer/CalculatorDesktopContainer";
import {CalculatorMobileContainer} from "./components/mobile/calculatorContainer/CalculatorMobileContainer";
import {isTouchDevice} from "../../utlis/layout/deviceInspector";

export const CalculatorPage = () => {

    return (
        isTouchDevice() ?
            <MobileOverlay>
                <CalculatorMobileContainer />
            </MobileOverlay>
            :
            <DesktopOverlay>
                <CalculatorDesktopContainer />
            </DesktopOverlay>
    );
};