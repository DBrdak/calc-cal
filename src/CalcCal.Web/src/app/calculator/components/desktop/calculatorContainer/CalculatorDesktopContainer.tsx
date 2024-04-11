import CalculatorForm from "../../shared/calculatorForm/CalculatorForm";
import {Stack} from "@mui/material";
import {AppName} from "../../../../../components/AppName";
import CalorieTableContainer from "./calorieTable/CalorieTableContainer";

export const CalculatorDesktopContainer = () => {
    return (
        <Stack direction={'column'} sx={{
            minWidth: '400px',
            maxWidth: '600px',
            width: '60vw'
        }}>
            <AppName />
            <CalculatorForm />
            <CalorieTableContainer />
        </Stack>
    );
};