import {Button, Grid, TextField} from "@mui/material";
import {Form, Formik} from "formik";
import {LogInRequest} from "../../../api/requests/logInRequest";
import * as yup from 'yup'
import {string} from "yup";
import {USERNAME_PATTERN} from "../../../utlis/settings/constants";
import theme from "../../theme";
import {useState} from "react";

export function LoginForm() {
    const [isUsernameFormFocused, setIsUsernameFormFocused] = useState(false)
    const [isPasswordFormFocused, setIsPasswordFormFocused] = useState(false)
// TODO Fix submit handling
    function handleFormSubmit(formValues: LogInRequest) {
        console.log(formValues)
    }

    const validationSchema = yup.object({
        username: string()
            .required('Username is required')
            .matches(USERNAME_PATTERN, {message: 'Invalid username'}),
        password: string()
            .required('Password is required')
    })

    return (
        <Grid item xs={12}>
            <Formik initialValues={new LogInRequest()} onSubmit={handleFormSubmit} validationSchema={validationSchema} sx={{
                width: '100%'
            }}>
                {({values, handleSubmit}) => (
                    <Form autoComplete={'off'} style={{
                        width: '100%',
                        display: 'flex',
                        justifyContent: 'center',
                        alignItems: 'center',
                        flexDirection: 'column',
                        position: 'relative'
                    }}>
                        <Grid item xs={12} marginBottom={theme.spacing(5)}>
                            <TextField
                                type={'text'}
                                name={'username'}
                                label={isUsernameFormFocused ? 'Username' : ''}
                                placeholder={'Username'}
                                fullWidth
                                onFocusCapture={() => setIsUsernameFormFocused(true)}
                                onBlurCapture={() => setIsUsernameFormFocused(false)}
                                focused={isUsernameFormFocused || values.username.length > 0}
                                InputLabelProps={{
                                    shrink: false,
                                    focused: isUsernameFormFocused,
                                }}
                                sx={{minWidth: '300px'}}
                            />
                        </Grid>
                        <Grid item xs={12} marginBottom={theme.spacing(3)}>
                            <TextField
                                type={'password'}
                                name={'password'}
                                label={isPasswordFormFocused ? 'Password' : ''}
                                placeholder={'Password'}
                                fullWidth
                                onFocusCapture={() => setIsPasswordFormFocused(true)}
                                onBlurCapture={() => setIsPasswordFormFocused(false)}
                                focused={isPasswordFormFocused || values.password.length > 0}
                                inputProps={{
                                    min: 1,
                                    max: 99999,
                                }}
                                InputLabelProps={{
                                    shrink: false,
                                    focused: isPasswordFormFocused
                                }}
                                sx={{minWidth: '300px'}}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <Button variant={'contained'} type={'submit'}>
                                Login
                            </Button>
                        </Grid>
                    </Form>
                )}
            </Formik>
        </Grid>
    )
}