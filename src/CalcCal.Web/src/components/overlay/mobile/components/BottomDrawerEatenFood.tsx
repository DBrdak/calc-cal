import {Box, List, ListItem, ListItemText, Paper, Typography} from "@mui/material";
import React from "react";
import {EatenFood} from "../../../../models/eatenFood";
import formatCalories from "../../../../utlis/formatters/caloriesFormatter";
import formatGrams from "../../../../utlis/formatters/gramFormatter";
import theme from "../../../../app/theme";
import {useStore} from "../../../../stores/store";
import {observer} from "mobx-react-lite";

interface BottomDrawerContentProps {
    eatenFood: EatenFood[]
}

export default function BottomDrawerEatenFood ({eatenFood}: BottomDrawerContentProps) {

    return (
        <Box marginTop={1}>
            {
                eatenFood.length < 1 ?
                    <Typography variant={'h6'} color={theme.palette.text.secondary} textAlign={'center'}>
                        No food yet
                    </Typography>
                    :
                    <List>
                        {
                            eatenFood?.map((food) => (
                                <ListItem key={food.foodName} component={Paper} sx={{
                                    marginBottom: theme.spacing(1)
                                }}>
                                    <ListItemText>
                                        {food.foodName}
                                    </ListItemText>
                                    <ListItemText>
                                        {formatGrams(food.gramsQuantity)}
                                    </ListItemText>
                                    <ListItemText>
                                        {formatCalories(food.caloriesEaten)}
                                    </ListItemText>
                                </ListItem>
                            ))
                        }
                    </List>
            }
        </Box>
    )
}