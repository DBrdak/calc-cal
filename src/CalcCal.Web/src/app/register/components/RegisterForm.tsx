import {Button, Grid, TextField} from "@mui/material";
import {Form, Formik} from "formik";
import {LogInRequest} from "../../../api/requests/logInRequest";
import theme from "../../theme";
import {DotLoader} from "../../../components/DotLoader";
import {useState} from "react";
import {useStore} from "../../../stores/store";
import * as yup from "yup";
import {string} from "yup";
import {
    COUNTRY_CODE_PATTERN,
    NAME_PATTERN,
    PASSWORD_PATTERN, PHONE_NUMBER_PATTERN,
    USERNAME_PATTERN
} from "../../../utlis/settings/constants";
import MyTextInput from "../../../components/MyTextInput";
import {RegisterRequest} from "../../../api/requests/registerRequest";
import CountrySelect from "../../../components/CountrySelect";
import {useNavigate} from "react-router-dom";

export const RegisterForm = () => {
    const {userStore} = useStore()
    const navigate = useNavigate()
    const [loading, setLoading] = useState(false)

    const validationSchema = yup.object({
        username: string()
            .required('Username is required')
            .matches(USERNAME_PATTERN, {message: 'Invalid username'}),
        password: string()
            .required('Password is required')
            .matches(PASSWORD_PATTERN, {message: 'Password too weak'}),
        firstName: string()
            .required('First name is required')
            .matches(NAME_PATTERN, {message: 'Invalid first name'}),
        lastName: string()
            .required('Last name is required')
            .matches(NAME_PATTERN, {message: 'Invalid last name'}),
        countryCode: string()
            .required('Country code is required')
            .matches(COUNTRY_CODE_PATTERN, {message: 'Invalid country code'}),
        phoneNumber: string()
            .required('Phone number is required')
            .matches(PHONE_NUMBER_PATTERN, {message: 'Invalid phone number'})
    })

    function handleFormSubmit(formValues: RegisterRequest) {
        setLoading(true)
        userStore.register(formValues).then(isSuccessful => {
            setLoading(false)

            if(isSuccessful){
                navigate('/verify-code/phone')
            }
        })
    }

    return (
        <Grid item xs={12}>
            <Formik initialValues={new RegisterRequest()} onSubmit={handleFormSubmit} validationSchema={validationSchema} sx={{
                width: '100%'
            }}>
                {({values, handleSubmit, handleChange, setValues, errors}) => (
                    <Form autoComplete={'off'} style={{
                        width: '100%',
                        display: 'flex',
                        justifyContent: 'center',
                        alignItems: 'center',
                        flexDirection: 'column',
                        position: 'relative'
                    }}>
                        <Grid item xs={12} marginBottom={theme.spacing(5)}>
                            <MyTextInput
                                type={'text'}
                                name={'firstName'}
                                label={'First Name'}
                                onChange={handleChange}
                                errors={errors.firstName}
                            />
                        </Grid>
                        <Grid item xs={12} marginBottom={theme.spacing(5)}>
                            <MyTextInput
                                type={'text'}
                                name={'lastName'}
                                label={'Last Name'}
                                onChange={handleChange}
                                errors={errors.lastName}
                            />
                        </Grid>
                        <Grid item xs={12} marginBottom={theme.spacing(5)}>
                            <MyTextInput
                                type={'text'}
                                name={'username'}
                                label={'Username'}
                                onChange={handleChange}
                                errors={errors.username}
                            />
                        </Grid>
                        <Grid item xs={12} marginBottom={theme.spacing(5)}>
                            <CountrySelect
                                onChange={countryCode => setValues({...values, countryCode: countryCode})}
                            />
                        </Grid>
                        <Grid item xs={12} marginBottom={theme.spacing(5)}>
                            <MyTextInput
                                type={'phoneNumber'}
                                name={'phoneNumber'}
                                label={'Phone number'}
                                onChange={handleChange}
                                errors={errors.phoneNumber}
                            />
                        </Grid>
                        <Grid item xs={12} marginBottom={theme.spacing(5)}>
                            <MyTextInput
                                type={'password'}
                                name={'password'}
                                label={'Password'}
                                onChange={handleChange}
                                errors={errors.password}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            {
                                loading ?
                                    <DotLoader />
                                    :
                                    <Button variant={'contained'} type={'submit'}
                                            disabled={
                                                !!errors.password
                                                || !!errors.username
                                                || !!errors.firstName
                                                || !!errors.lastName
                                                || !!errors.countryCode
                                                || !!errors.phoneNumber
                                    }>
                                        Register
                                    </Button>
                            }
                        </Grid>
                    </Form>
                )}
            </Formik>
        </Grid>
    );
};