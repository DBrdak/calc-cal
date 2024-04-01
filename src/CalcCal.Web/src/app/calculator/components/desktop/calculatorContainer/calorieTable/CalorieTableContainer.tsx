import {Paper} from "@mui/material";
import theme from "../../../../../theme";
import CalorieTable from "../../../shared/calorieTable/CalorieTable";
export default function CalorieTableContainer() {
    return (
        <Paper variant={'elevation'} elevation={0} sx={{
            width: '100%',
            minHeight: '400px',
            borderRadius: '0px 0px 20px 20px',
            boxShadow: `inset 0px 0px 10px 0px rgba(0,0,0,0.4)`,
            backgroundColor: theme.palette.background.default,
            padding: theme.spacing(3),
            display: 'flex', justifyContent: 'center', alignItems: 'top'
        }}>
            <CalorieTable />
        </Paper>
    )
}