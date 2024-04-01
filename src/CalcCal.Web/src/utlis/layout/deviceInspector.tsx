import {useMediaQuery} from "@mui/material";
import theme from "../../app/theme";

export function isTouchDevice() {
    return ('maxTouchPoints' in navigator && navigator.maxTouchPoints > 0) ||
        ('msMaxTouchPoints' in navigator && Number(navigator.msMaxTouchPoints) > 0);
}

export function useSmallScreen(){
    return useMediaQuery(theme.breakpoints.down('md'))
}