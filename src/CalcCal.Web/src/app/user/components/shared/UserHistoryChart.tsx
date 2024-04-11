import {EatenFood} from "../../../../models/eatenFood";
import {Box, Typography} from "@mui/material";
import {LineChart, LineSeriesType} from "@mui/x-charts";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";
import {MakeOptional} from "@mui/x-charts/models/helpers";
import theme from "../../../theme";

interface UserHistoryChartProps {
    mobile?: boolean
}

function groupByDate(eatenFood: EatenFood[]): Map<string, EatenFood[]> {
    const map = new Map<string, EatenFood[]>()

    eatenFood.forEach(item => {
        const dateKey = new Date(item.eatenDateTime).toISOString().split('T')[0]

        if (!map.has(dateKey)) {
            map.set(dateKey, [])
        }

        map.get(dateKey)!.push(item)
    });

    return map
}

function calculateTotalCalories(eatenFood: EatenFood[]): number {
    let totalCalories = 0;
    for (const food of eatenFood) {
        totalCalories += food.caloriesEaten;
    }
    return totalCalories;
}

const UserHistoryChart = ({mobile}: UserHistoryChartProps) => {
    const {userStore} = useStore()
    const eatenFoodRegistry = groupByDate(userStore.user.eatenFood ?? [])

    const getSeries = () => {
        let series: MakeOptional<LineSeriesType, "type">[] = [{
            data: Array.from(eatenFoodRegistry.values()).map(f => calculateTotalCalories(f)),
            area: false,
            color: theme.palette.primary.light,
            label: 'kcal',
        }]

        eatenFoodRegistry.forEach((food, date) => {
           series.push()
        })

        return series
    }

    return (
        <Box sx={{
            width: '100%',
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            flexDirection: 'column'
        }}>
            {
                !mobile &&
                    <Typography variant={'h5'}>
                        Calorie chart
                    </Typography>
            }
            <LineChart
                height={mobile ? 300 : 500}
                series={getSeries()}
                slotProps={{legend: {hidden: true}}}
                xAxis={[{data: Array.from(eatenFoodRegistry.keys()), scaleType: 'point', id: 'axis1'}]}
                grid={{vertical: true, horizontal: true}}
            />
        </Box>
    );
}

export default observer(UserHistoryChart)