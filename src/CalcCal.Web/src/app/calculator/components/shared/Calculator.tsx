import {Grid, Paper, Stack} from "@mui/material";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";
import FoodNameForm from "./components/FoodNameForm";
import {AppName} from "../../../../components/AppName";
import FoodQuantityForm from "./components/FoodQuantityForm";

export default observer (function Calculator () {
    const {foodStore} = useStore()

    return (
        <Stack direction={'column'}>
            <AppName />
            <Grid container component={Paper} variant={'elevation'} elevation={4} sx={{
                width: '400px',
                height: '400px',
                borderRadius: '20px',
            }}>
                <FoodNameForm />
                <FoodQuantityForm />
            </Grid>
        </Stack>
    )
})