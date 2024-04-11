import MobileOverlay from "../../components/overlay/mobile/MobileOverlay";
import React, {useEffect, useState} from "react";
import DesktopOverlay from "../../components/overlay/desktop/DesktopOverlay";
import {CalculatorDesktopContainer} from "./components/desktop/calculatorContainer/CalculatorDesktopContainer";
import {CalculatorMobileContainer} from "./components/mobile/calculatorContainer/CalculatorMobileContainer";
import {isTouchDevice} from "../../utlis/layout/deviceInspector";
import {observer} from "mobx-react-lite";
import {useStore} from "../../stores/store";
import {DotLoader} from "../../components/DotLoader";

const CalculatorPage = () => {
    const [loading, setLoading] = useState(false)
    const {userStore} = useStore()

    useEffect(() => {
        setLoading(true)
        userStore.loadCurrentUser().then(() => {
            setLoading(false)
        })
    }, [])

    return (
        isTouchDevice() ?
            <MobileOverlay>
                {loading ? <DotLoader /> : <CalculatorMobileContainer />}
            </MobileOverlay>
            :
            <DesktopOverlay>
                {loading ? <DotLoader /> : <CalculatorDesktopContainer />}
            </DesktopOverlay>
    )
}

export default observer(CalculatorPage)