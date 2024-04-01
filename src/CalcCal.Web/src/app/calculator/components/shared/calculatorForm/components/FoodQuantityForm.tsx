import {observer} from "mobx-react-lite";
import {ChangeEvent, FormEvent, useState} from "react";
import {Grid, InputAdornment, TextField} from "@mui/material";
import {isDigit} from "../../../../../../utlis/extensions/stringExtensions";
import './foodQuantityFormStyles.css'
import {MAX_WEIGHT, MIN_WEIGHT} from "../../../../../../utlis/settings/constants";
import {useStore} from "../../../../../../stores/store";

export default observer(function FoodQuantityForm() {
    const [value, setValue] = useState('100')
    const [isFormFocused, setIsFormFocused] = useState(true)
    const [isInitialState, setIsInitialState] = useState(true)
    const {foodStore} = useStore()

    const handleFormSubmit = (event: FormEvent<HTMLFormElement>) => {
        event.preventDefault()
        const inputValue = (event.currentTarget.elements[0] as HTMLInputElement).value
        const quantity = Number(inputValue.slice(0, inputValue.indexOf('g')))

        foodStore.setSelectedFoodWeight(quantity)
    }

    const handleValueChange = (e: ChangeEvent<HTMLInputElement>) => {
        let inputValue

        if(isInitialState){
            setIsInitialState(false)

            inputValue = e.target.value.length <= value.length ?
                e.target.value.slice(0, e.target.value.length - 1) :
                e.target.value.replace('g', '').slice(3)
        } else {
            inputValue = e.target.value.length <= value.length ?
                e.target.value.slice(0, e.target.value.length - 1) :
                e.target.value.replace('g', '')
        }

        const lastInputValueCharacter = inputValue[inputValue.length - 1]

        if(inputValue.length > 0 && !isDigit(lastInputValueCharacter)){
            return
        }

        if(inputValue.startsWith('0') && lastInputValueCharacter === '0'){
            return
        }

        if (/^\d*$/.test(inputValue) && Number(inputValue) >= MIN_WEIGHT && Number(inputValue) < MAX_WEIGHT) {
            setValue(inputValue)
        }
    }

    const isValid = () => !!Number(value)

    const getLabel = (): string => {
        return !isInitialState && isValid() ? '' : "How much have you eaten?"
    }

    const handleBlur = () => {

    }

    return (
        <Grid item xs={12} sx={{
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'top'
        }}>
            <form onSubmit={handleFormSubmit} style={{width: '70%', minWidth: '200px'}}>
                <TextField
                    value={value.length > 0 ? value + 'g' : value}
                    fullWidth
                    label={getLabel()}
                    onChange={handleValueChange}
                    onBlur={handleBlur}
                    onFocusCapture={e => setIsFormFocused(true)}
                    onBlurCapture={e => setIsFormFocused(false)}
                    focused={isFormFocused || value.length > 0}
                    autoFocus
                    inputProps={{
                        min: 1,
                        max: 99999,
                    }}
                    InputLabelProps={{
                        shrink: false,
                        focused: isFormFocused || value.length > 0
                    }}
                />
            </form>
        </Grid>
    )
})