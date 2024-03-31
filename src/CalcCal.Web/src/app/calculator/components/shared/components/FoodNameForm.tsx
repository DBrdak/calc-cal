import {DotLoader} from "../../../../../components/DotLoader";
import {Autocomplete, Grid, TextField} from "@mui/material";
import theme from "../../../../theme";
import {FormEvent, useEffect, useState} from "react";
import {useStore} from "../../../../../stores/store";
import {observer} from "mobx-react-lite";
import './foodNameFormStyles.scss'

export default observer ( function FoodNameForm () {
    const [searchPhrase, setSearchPhrase] = useState<string>('')
    const [submittedValue, setSubmittedValue] = useState<string>('')
    const [searchLoading, setSearchLoading] = useState(false)
    const [isFormFocused, setIsFormFocused] = useState(false)
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

    const isFormComplete = () => (
        searchPhrase.length > 0 && !searchLoading && !!submittedValue && searchPhrase === submittedValue
    )

    const getLabel = (): string => {
        const isAnyInput = searchPhrase.length > 0
        const isInputProcessed = !searchLoading && !!submittedValue && searchPhrase === submittedValue
        return isFormComplete() ? "" : "What have you eaten?"
    }

    return (
        <Grid item xs={12} sx={{
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center'
        }}>
            <form onSubmit={handleFormSubmit} style={{width: '70%', minWidth: '200px'}}>
                <Autocomplete
                    freeSolo
                    value={searchPhrase}
                    onChange={(event, newValue) => handleFoodChange(newValue)}
                    onBlur={() => handleBlur()}
                    options={foodStore.query.map(f => f.name)}
                    sx={{marginBottom: theme.spacing(2)}}
                    PaperComponent={({ children }) => (
                        <div style={{
                            overflow: 'visible',
                            backgroundColor: 'transparent',
                        }}>
                            {children}
                        </div>
                    )}
                    ListboxProps={{sx: {
                            overflow: 'visible',
                            textAlign: 'center',
                            display: 'flex',
                            justifyContent: 'center',
                            alignItems: 'center',
                            flexDirection: 'column',
                        }}}
                    renderInput={(params) => (
                        <TextField
                            {...params}
                            sx={{
                                '&.MuiOutlinedInput-root': {
                                    '& fieldset': {
                                        borderColor: isFormComplete() ? theme.palette.success : 'inherit'
                                    },
                                    '&:hover fieldset': {
                                        borderColor: isFormComplete() ? theme.palette.success : 'inherit'
                                    },
                                    '&.Mui-focused fieldset': {
                                        borderColor: isFormComplete() ? theme.palette.success : 'inherit'
                                    },
                                }
                            }}
                            label={getLabel()}
                            onFocusCapture={e => setIsFormFocused(true)}
                            onBlurCapture={e => setIsFormFocused(false)}
                            onChange={(e) => setSearchPhrase(e.target.value)}
                            InputLabelProps={{ shrink: false, focused: isFormFocused || searchPhrase.length > 0 }}
                        />
                    )}
                    inputMode={'none'}
                />
                {searchLoading && <DotLoader/>}
            </form>
        </Grid>
    );
})