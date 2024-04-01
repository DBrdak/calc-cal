import {Box, List, ListItem, ListItemText, Paper, Typography} from "@mui/material";
import TableContainer from "@mui/material/TableContainer";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableRow from "@mui/material/TableRow";
import TableCell from "@mui/material/TableCell";
import React from "react";
import {EatenFood} from "../../../../../../models/eatenFood";
import formatCalories from "../../../../../../utlis/formatters/caloriesFormatter";
import formatGrams from "../../../../../../utlis/formatters/gramFormatter";
import theme from "../../../../../theme";

interface BottomDrawerContentProps {
    eatenFood: EatenFood[]
}

export const BottomDrawerContent = ({eatenFood}: BottomDrawerContentProps) => {
    return (
        <Box marginTop={1}>
            <List>
                {
                    eatenFood?.map((food) => (
                        <ListItem key={food.foodName} component={Paper} sx={{
                            marginBottom: theme.spacing(1)
                        }}>
                            <ListItemText >
                                {food.foodName}
                            </ListItemText>
                            <ListItemText >
                                {formatGrams(food.gramsQuantity)}
                            </ListItemText>
                            <ListItemText >
                                {formatCalories(food.caloriesEaten)}
                            </ListItemText>
                        </ListItem>
                    ))
                }
            </List>
        </Box>
    );
};