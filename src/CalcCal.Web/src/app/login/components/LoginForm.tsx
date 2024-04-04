import {Button, Grid, TextField, Typography} from "@mui/material";
import {Form, Formik} from "formik";
import {LogInRequest} from "../../../api/requests/logInRequest";
import * as yup from 'yup'
import {string} from "yup";
import {USERNAME_PATTERN} from "../../../utlis/settings/constants";
import theme from "../../theme";
import {useState} from "react";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../stores/store";
import {DotLoader} from "../../../components/DotLoader";
import {useNavigate} from "react-router-dom";

export default observer(function LoginForm() {
    const [isUsernameFormFocused, setIsUsernameFormFocused] = useState(false)
    const [isPasswordFormFocused, setIsPasswordFormFocused] = useState(false)
    const [loading, setLoading] = useState(false)
    const navigate = useNavigate()
    const {userStore} = useStore()

    const validationSchema = yup.object({
        username: string()
            .required('Username is required')
            .matches(USERNAME_PATTERN, {message: 'Invalid username'}),
        password: string()
            .required('Password is required')
    })

    function handleFormSubmit(formValues: LogInRequest) {
        setLoading(true)
        userStore.logIn(formValues).then(isSuccessful => {
            setLoading(false)
            if(isSuccessful){
                navigate('/')
            }
        })
    }

    return (
        <Grid item xs={12}>
            <Formik initialValues={new LogInRequest()} onSubmit={handleFormSubmit} validationSchema={validationSchema} sx={{
                width: '100%'
            }}>
                {({values, handleSubmit, handleChange, dirty, errors}) => (
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
                                label={isUsernameFormFocused && !errors.username ? 'Username' : ''}
                                placeholder={'Username'}
                                fullWidth
                                onChange={handleChange}
                                onFocusCapture={() => setIsUsernameFormFocused(true)}
                                onBlurCapture={() => setIsUsernameFormFocused(false)}
                                focused={isUsernameFormFocused}
                                helperText={errors.username}
                                error={!!errors.username}
                                sx={{width: '300px'}}
                                InputLabelProps={{
                                    shrink: false,
                                    focused: isUsernameFormFocused,
                                }}
                                FormHelperTextProps={{sx:{
                                    display: 'flex',
                                    alignItems: 'start',
                                    justifyContent: 'center',
                                    transform: 'translateY(-50%)',
                                    fontSize: 16
                                }}}
                            />
                        </Grid>
                        <Grid item xs={12} marginBottom={theme.spacing(3)}>
                            <TextField
                                type={'password'}
                                name={'password'}
                                label={isPasswordFormFocused && !errors.username ? 'Password' : ''}
                                placeholder={'Password'}
                                fullWidth
                                onChange={handleChange}
                                onFocusCapture={() => setIsPasswordFormFocused(true)}
                                onBlurCapture={() => setIsPasswordFormFocused(false)}
                                focused={isPasswordFormFocused}
                                inputProps={{
                                    min: 1,
                                    max: 99999,
                                }}
                                InputLabelProps={{
                                    shrink: false,
                                    focused: isPasswordFormFocused
                                }}
                                helperText={errors.password}
                                error={!!errors.password}
                                sx={{width: '300px'}}
                                FormHelperTextProps={{sx:{
                                        display: 'flex',
                                        alignItems: 'start',
                                        justifyContent: 'center',
                                        transform: 'translateY(-50%)',
                                        fontSize: 16
                                    }}}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            {
                                loading ?
                                    <DotLoader />
                                    :
                                    <Button variant={'contained'} type={'submit'}
                                            disabled={!!errors.password || !!errors.username}>
                                        Login
                                    </Button>
                            }
                        </Grid>
                    </Form>
                )}
            </Formik>
        </Grid>
    )
})