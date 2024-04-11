import TableContainer from "@mui/material/TableContainer";
import Table from "@mui/material/Table";
import {TableFooter, TableHead, Typography} from "@mui/material";
import theme from "../../../../../../theme";
import TableRow from "@mui/material/TableRow";
import TableCell from "@mui/material/TableCell";
import TableBody from "@mui/material/TableBody";
import {DotLoader} from "../../../../../../../components/DotLoader";
import formatCalories from "../../../../../../../utlis/formatters/caloriesFormatter";
import {useStore} from "../../../../../../../stores/store";
import {observer} from "mobx-react-lite";
import {useEffect, useRef, useState} from "react";
import {EatenFood} from "../../../../../../../models/eatenFood";

export default observer (function CalorieTable() {
    const {foodStore, userStore} = useStore()
    const tableContainerRef = useRef<HTMLDivElement>(null)

    useEffect(() => {
        if (tableContainerRef.current) {
            tableContainerRef.current.scrollTop = tableContainerRef.current.scrollHeight;
        }
    }, [userStore.eatenFood, foodStore.selectedFood, tableContainerRef]);

    return (
        <TableContainer ref={tableContainerRef} sx={{
            minHeight: '300px',
            maxHeight: '400px',
            overflowY: 'auto'
        }}>
            <Table sx={{
                overflow: 'visible',
            }}>
                <TableHead sx={{
                    position: 'sticky',
                    top: 0,
                    backgroundColor: theme.palette.background.default
                }}>
                    <TableRow>
                        <TableCell align={'center'}>
                            Food name
                        </TableCell>
                        <TableCell align={'center'}>
                            Eaten quantity [g]
                        </TableCell>
                        <TableCell align={'center'}>
                            Eaten calories [kcal]
                        </TableCell>
                    </TableRow>
                </TableHead>
                <TableBody sx={{verticalAlign: 'top'}}>
                    {
                        (!userStore.eatenFood || userStore.eatenFood?.length < 1) && !foodStore.selectedFood &&
                        <TableRow>
                            <TableCell align={'center'} colSpan={3}>
                                <Typography variant={'subtitle2'} color={theme.palette.text.secondary}>
                                    No food yet
                                </Typography>
                            </TableCell>
                        </TableRow>
                    }
                    {userStore.eatenFood.map((food, i) => (
                        <TableRow key={i}>
                            <TableCell align={'center'}>
                                {food.foodName}
                            </TableCell>
                            <TableCell align={'center'}>
                                {food.gramsQuantity}
                            </TableCell>
                            <TableCell align={'center'}>
                                {food.caloriesEaten}
                            </TableCell>
                        </TableRow>
                    ))}
                    {
                        foodStore.selectedFood &&
                        <TableRow>
                            <TableCell align={'center'}>
                                {foodStore.selectedFood.name}
                            </TableCell>
                            <TableCell align={'center'}>
                                {foodStore.selectedFoodWeight}
                            </TableCell>
                            <TableCell align={'center'}>
                                {foodStore.selectedFoodWeight && foodStore.selectedFood && <DotLoader/>}
                            </TableCell>
                        </TableRow>
                    }
                </TableBody>
                {userStore.eatenCalories() > 0 &&
                    <TableFooter sx={{
                        position: 'sticky',
                        bottom: 0,
                        backgroundColor: theme.palette.background.default
                    }}>
                        <TableRow>
                            <TableCell colSpan={3} align={'center'} >
                                    <Typography variant={'subtitle1'} color={theme.palette.primary.dark}>
                                        You've eaten {formatCalories(userStore.eatenCalories())}
                                    </Typography>
                            </TableCell>
                        </TableRow>
                    </TableFooter>
                }
            </Table>
        </TableContainer>
    )
})