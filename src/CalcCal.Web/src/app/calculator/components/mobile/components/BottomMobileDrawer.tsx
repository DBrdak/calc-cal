import {Typography} from "@mui/material";
import theme from "../../../../theme";
import formatCalories from "../../../../../utlis/formatters/caloriesFormatter";
import {BottomDrawerContent} from "./BottomDrawerContent";
import React from "react";
import {EatenFood} from "../../../../../models/eatenFood";
import {DrawerBox} from "./DrawerBox";
import {Puller} from "./Puller";
import {AccessibleSwipeableDrawer} from "../../../../../components/AccessibleSwipeableDrawer";

interface BottomMobileDrawerProps {
    eatenFood: EatenFood[]
}

export const BottomMobileDrawer = ({eatenFood}: BottomMobileDrawerProps) => {
    const [bottomDrawerOpen, setBottomDrawerOpen] = React.useState(false)
    const drawerBleeding = 56

    const handleBottomDrawerOpen = () => {
        setBottomDrawerOpen(true);
    }

    const handleBottomDrawerClose = () => {
        setBottomDrawerOpen(false);
    }

    function sumCaloriesEaten(foodList: EatenFood[]) {
        return foodList.reduce((totalCalories, food) => totalCalories + food.caloriesEaten, 0)
    }

    return (
        <AccessibleSwipeableDrawer
            className={'MuiDrawer-anchorBottom'}
            anchor="bottom"
            open={bottomDrawerOpen}
            onClose={handleBottomDrawerClose}
            onOpen={handleBottomDrawerOpen}
            swipeAreaWidth={drawerBleeding}
            disableSwipeToOpen={false}
            ModalProps={{
                keepMounted: true,
            }}
            orientation={'vertical'}
            setOpen={setBottomDrawerOpen}
        >
            <DrawerBox
                sx={{
                    position: 'absolute',
                    top: -drawerBleeding,
                    borderTopLeftRadius: 8,
                    borderTopRightRadius: 8,
                    visibility: 'visible',
                    right: 0,
                    left: 0,
                }}
            >
                <Puller variant={'bottom'} />
                <Typography sx={{ p: 2, color: theme.palette.text.secondary }} fontWeight={'bolder'} fontSize={'x-large'}>
                    {formatCalories(sumCaloriesEaten(eatenFood))}
                </Typography>
            </DrawerBox>
            <DrawerBox
                sx={{
                    px: 2,
                    pb: 2,
                    height: '100%',
                    overflow: 'auto',
                }}
            >
                <BottomDrawerContent eatenFood={eatenFood} />
            </DrawerBox>
        </AccessibleSwipeableDrawer>
    );
};