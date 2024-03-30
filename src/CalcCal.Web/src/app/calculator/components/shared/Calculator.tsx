import {Autocomplete, CircularProgress, IconButton, Paper, Stack, TextField} from "@mui/material";
import * as yup from 'yup'
import {FormEvent, useEffect, useState} from "react";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";
import {Food} from "../../../../models/food";
import {Search} from "@mui/icons-material";
import {DotLoader} from "../../../../components/DotLoader";
import theme from "../../../theme";
import FoodNameForm from "./components/FoodNameForm";
import {AppName} from "../../../../components/AppName";

export default observer (function Calculator () {
    const {foodStore} = useStore()

    return (
        <Stack direction={'column'}>
            <AppName />
            <Paper variant={'elevation'} elevation={4} sx={{
                width: '400px',
                height: '400px',
                display: 'flex', justifyContent: 'center', alignItems: 'center',
                borderRadius: '20px'
            }}>
                <FoodNameForm />
                <form>

                </form>
            </Paper>
        </Stack>
    )
})