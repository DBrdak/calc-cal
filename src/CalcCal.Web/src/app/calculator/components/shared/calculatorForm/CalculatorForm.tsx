/* eslint-disable react-hooks/exhaustive-deps */
import {Grid, Paper } from "@mui/material";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../../stores/store";
import FoodNameForm from "./components/FoodNameForm";
import FoodQuantityForm from "./components/FoodQuantityForm";
import {useEffect, useState} from "react";
import theme from "../../../../theme";
import {UndoButton} from "./components/UndoButton";
import {DotLoader} from "../../../../../components/DotLoader";
import {isTouchDevice} from "../../../../../utlis/layout/deviceInspector";

export default observer (function CalculatorForm () {
    const {foodStore, userStore} = useStore()
    const [eatLoading, setEatLoading] = useState(false)

    useEffect(() => {
        setEatLoading(true)
        foodStore.eatFood().then(() => setEatLoading(false))
    }, [foodStore.selectedFood, foodStore.selectedFoodWeight])

    const handleUndo = () => {
        foodStore.undoFoodSelection()
    }

    return (
        <Grid container component={Paper} variant={'elevation'} elevation={4} sx={{
            width: '100%',
            minHeight: '300px',
            zIndex: 1000,
            borderRadius: isTouchDevice() ? '20px' : '20px 20px 0px 0px',
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'top',
            padding: theme.spacing(5)
        }}>
            {!foodStore.selectedFood && <FoodNameForm/>}
            {foodStore.selectedFood && !foodStore.selectedFoodWeight && <FoodQuantityForm/>}
            {foodStore.selectedFood && !foodStore.selectedFoodWeight && <UndoButton onUndo={handleUndo} />}
            {eatLoading && <DotLoader />}
        </Grid>
    )
})