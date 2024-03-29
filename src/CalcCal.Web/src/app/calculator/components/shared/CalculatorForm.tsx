import {Autocomplete, CircularProgress, IconButton, Paper, TextField} from "@mui/material";
import * as yup from 'yup'
import {FormEvent, useEffect, useState} from "react";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../../stores/store";
import {Food} from "../../../../models/food";
import {Search} from "@mui/icons-material";
import {DotLoader} from "../../../../components/DotLoader";
import theme from "../../../theme";

export default observer (function CalculatorForm () {
    const [searchPhrase, setSearchPhrase] = useState<string>('')
    const [selectedFood, setSelectedFood] = useState<Food>()
    const [submittedValue, setSubmittedValue] = useState<Food | string>('')
    const [getLoading, setGetLoading] = useState(false)
    const {foodStore} = useStore()

    useEffect(() => {
        setGetLoading(true)
        foodStore.getFood(searchPhrase as string || '').then(() => {
            setGetLoading(false)
        })
    }, [searchPhrase])

    useEffect(()=>{
        console.log(foodStore.loading.get)
    },[foodStore.loading.get])

    function handleFormSubmit(event: FormEvent<HTMLFormElement>) {
        event.preventDefault()
        setSubmittedValue(searchPhrase)
    }

    const handleBlur = () => {
        if (searchPhrase) {
            setSubmittedValue(searchPhrase)
        }
    }

    return (
        <Paper variant={'elevation'} elevation={4} sx={{
            width: '400px',
            height: '400px',
            display: 'flex', justifyContent: 'center', alignItems: 'center',
            borderRadius: '20px'
        }}>
            <form onSubmit={handleFormSubmit} style={{width: '70%'}}>
                {getLoading && <DotLoader/>}
                <Autocomplete
                    freeSolo
                    value={searchPhrase}
                    onChange={(event, newValue) => {
                        console.log(newValue)
                    }}
                    onBlur={() => handleBlur()}
                    options={foodStore.query.map(f => f.name)}
                    sx={{marginTop: theme.spacing(2)}}
                    renderInput={(params) => (
                        <TextField
                            {...params}
                            label="What have you eaten?"
                            onChange={(e) => setSearchPhrase(e.target.value)}
                        />
                    )}
                />
            </form>
        </Paper>
    )
})