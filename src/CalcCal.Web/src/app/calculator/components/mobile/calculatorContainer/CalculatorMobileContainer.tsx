import {Stack} from "@mui/material";
import {AppName} from "../../../../../components/AppName";
import CalculatorForm from "../../shared/calculatorForm/CalculatorForm";

export const CalculatorMobileContainer = () => {
    return (
        <Stack direction={'column'} sx={{
            minWidth: '300px',
            maxWidth: '95svw',
            width: '95svw',
            maxHeight: '100%',
            overflow: 'visible',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center'
        }}>
            <AppName />
            <CalculatorForm />
        </Stack>
    );
};