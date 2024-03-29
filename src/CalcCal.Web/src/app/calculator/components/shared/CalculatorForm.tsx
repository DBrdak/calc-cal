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
    const [submittedValue, setSubmittedValue] = useState<string>('')
    const [searchLoading, setSearchLoading] = useState(false)
    const {foodStore} = useStore()

    useEffect(() => {
        setSearchLoading(true)
        foodStore.getFood(searchPhrase as string || '').then(() => {
            setSearchLoading(false)
        })

    }, [searchPhrase])

    useEffect(() => {
        if(submittedValue.length < 1){
            return
        }
        setSearchLoading(true)
        foodStore.selectFood(submittedValue).then(f => {
            setSearchLoading(false)
        })
    }, [submittedValue])


    function handleFormSubmit(event: FormEvent<HTMLFormElement>) {
        event.preventDefault()
        const foodName = (event.currentTarget.elements[0] as HTMLInputElement).value
        setSearchPhrase(foodName)
        setSubmittedValue(foodName)
    }

    function handleFoodChange(foodName: string | null) {
        if(!foodName) {
            setSearchPhrase('')
            return
        }
        setSearchPhrase(foodName)
        setSubmittedValue(foodName)
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
                {searchLoading && <DotLoader/>}
                <Autocomplete
                    freeSolo
                    value={searchPhrase}
                    onChange={(event, newValue) => handleFoodChange(newValue)}
                    onBlur={() => handleBlur()}
                    options={foodStore.query.map(f => f.name)}
                    sx={{marginTop: theme.spacing(2)}}
                    renderInput={(params) => (
                        <TextField
                            {...params}
                            label="What have you eaten?"
                            onChange={(e) => setSearchPhrase(e.target.value)}
                            sx={{borderRadius: '30px'}}
                        />
                    )}
                />
            </form>
        </Paper>
    )
})