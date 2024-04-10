import {Box, Typography} from "@mui/material";
import theme from "../../../../app/theme";
import formatCalories from "../../../../utlis/formatters/caloriesFormatter";
import BottomDrawerEatenFood from "./BottomDrawerEatenFood";
import React from "react";
import {EatenFood} from "../../../../models/eatenFood";
import {DrawerBox} from "./DrawerBox";
import {Puller} from "./Puller";
import {AccessibleSwipeableDrawer} from "../../../AccessibleSwipeableDrawer";
import {DotLoader} from "../../../DotLoader";
import {useLocation} from "react-router-dom";
import BottomDrawerUserHistory from "./BottomDrawerUserHistory";

interface BottomMobileDrawerProps {
    eatenFood: EatenFood[]
    eatLoading: boolean
}

export default function BottomMobileDrawer({eatenFood, eatLoading}: BottomMobileDrawerProps) {
    const [bottomDrawerOpen, setBottomDrawerOpen] = React.useState(false)
    const drawerBleeding = 56
    const location = useLocation()

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
                <Typography fontWeight={'bolder'} fontSize={'x-large'} sx={{
                    p: 2, color: theme.palette.text.secondary
                }}>
                    {location.pathname === '/user' ? 'History' : formatCalories(sumCaloriesEaten(eatenFood))}
                </Typography>
                <Box sx={{
                    position: 'absolute',
                    top: 0, right: 5,
                    height: '100%',
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                }}>
                    {eatLoading && <DotLoader/>}
                </Box>
            </DrawerBox>
            <DrawerBox
                sx={{
                    px: 2,
                    pb: 2,
                    height: '100%',
                    overflow: 'auto',
                }}
            >
                {location.pathname === '/user' ? <BottomDrawerUserHistory /> : <BottomDrawerEatenFood eatenFood={eatenFood} />}
            </DrawerBox>
        </AccessibleSwipeableDrawer>
    )
}