import MobileOverlay from "../../components/overlay/mobile/MobileOverlay";
import React from "react";
import DesktopOverlay from "../../components/overlay/desktop/DesktopOverlay";
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