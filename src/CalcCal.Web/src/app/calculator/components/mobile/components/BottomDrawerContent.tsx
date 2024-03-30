import {Box, Typography} from "@mui/material";
import TableContainer from "@mui/material/TableContainer";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableRow from "@mui/material/TableRow";
import TableCell from "@mui/material/TableCell";
import React from "react";
import {EatenFood} from "../../../../../models/eatenFood";

interface BottomDrawerContentProps {
    eatenFood: EatenFood[]
}

export const BottomDrawerContent = ({eatenFood}: BottomDrawerContentProps) => {
    return (
        <Box sx={{ padding: 2 }}>
            <Typography variant="h6" textAlign={'center'}>{}</Typography>
            <TableContainer>
                <Table>
                    <TableBody>
                        {
                            eatenFood?.map((food) => (
                                <TableRow key={food.foodName}>
                                    <TableCell>{food.foodName}</TableCell>
                                    <TableCell>{food.calories}</TableCell>
                                    <TableCell>{food.gramsQuantity}</TableCell>
                                    <TableCell>{food.caloriesEaten}</TableCell>
                                </TableRow>
                            ))
                        }
                    </TableBody>
                </Table>
            </TableContainer>
        </Box>
    );
};