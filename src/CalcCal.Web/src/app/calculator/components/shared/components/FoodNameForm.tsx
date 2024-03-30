import {DotLoader} from "../../../../../components/DotLoader";
import {Autocomplete, Button, keyframes, List, ListItem, Paper, TextField} from "@mui/material";
import theme from "../../../../theme";
import {FormEvent, ReactElement, ReactNode, useEffect, useState} from "react";
import {useStore} from "../../../../../stores/store";
import {observer} from "mobx-react-lite";
import './foodNameFormStyles.scss'

const dropDownAnimation = keyframes`
  from {
    transform: translateY(-10%);
    height: 0;
  }
  to {
    transform: translateY(0);
    opacity: 1;
  }
`;

export default observer ( function FoodNameForm () {
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
        <form onSubmit={handleFormSubmit} style={{width: '70%'}}>
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
                        label="What have you eaten?"
                        onChange={(e) => setSearchPhrase(e.target.value)}
                    />
                )}
            />
            {searchLoading && <DotLoader/>}
        </form>
    );
})