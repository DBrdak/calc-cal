import React, {useState} from 'react';
import {
    TextField,
    FormControl,
    Tooltip,
    FilledInputProps,
    InputProps,
    OutlinedInputProps, TextFieldProps
} from '@mui/material';
import { useField } from 'formik';
import { observer } from 'mobx-react-lite';

interface Props {
    name: string
    type: string
    label: string
    errors?: string
    onChange: (e: React.ChangeEvent<HTMLInputElement>) => void
    props?: TextFieldProps
}

function MyTextInput({name, props, type, label, errors, onChange}: Props) {
    const [field] = useField(name);
    const [isFormFocused, setIsFormFocused] = useState(false)

    return (
        <TextField
            {...field}
            {...props}
            onChange={onChange}
            type={type}
            name={name}
            label={isFormFocused && !errors ? label : ''}
            placeholder={label}
            fullWidth
            onFocusCapture={() => setIsFormFocused(true)}
            onBlurCapture={() => setIsFormFocused(false)}
            focused={isFormFocused}
            helperText={errors}
            error={!!errors}
            InputLabelProps={{
                shrink: false,
                focused: isFormFocused,
            }}
            FormHelperTextProps={{sx:{
                    display: 'flex',
                    alignItems: 'start',
                    justifyContent: 'center',
                    transform: 'translateY(-50%)',
                    fontSize: 16
                }}}
        />
    )
}

export default observer(MyTextInput);
