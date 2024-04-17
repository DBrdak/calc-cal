import {Button, Grid} from "@mui/material";
import {Form, Formik} from "formik";
import theme from "../../theme";
import MyTextInput from "../../../components/MyTextInput";
import CountrySelect from "../../../components/CountrySelect";
import {DotLoader} from "../../../components/DotLoader";
import {useNavigate} from "react-router-dom";
import {useState} from "react";
import * as yup from "yup";
import {string} from "yup";
import {
    COUNTRY_CODE_PATTERN,
    PHONE_NUMBER_PATTERN
} from "../../../utlis/settings/constants";
import {observer} from "mobx-react-lite";
import {useStore} from "../../../stores/store";

const PhoneNumberForm = () => {
    const {userStore} = useStore()
    const [loading, setLoading] = useState(false)

    const validationSchema = yup.object({
        countryCode: string()
            .required('Country code is required')
            .matches(COUNTRY_CODE_PATTERN, {message: 'Invalid country code'}),
        phoneNumber: string()
            .required('Phone number is required')
            .matches(PHONE_NUMBER_PATTERN, {message: 'Invalid phone number'})
    })

    function handleFormSubmit(phoneNumber: string, countryCode: string) {
        setLoading(true)
        userStore.sendVerificationCode(countryCode, phoneNumber).then(() => {
            setLoading(false)
        })
    }
    return (
        <Grid item xs={12}>
            <Formik
                initialValues={{phoneNumber: '', countryCode: ''}}
                onSubmit={values =>
                    handleFormSubmit(values.phoneNumber, values.countryCode)} validationSchema={validationSchema}>
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
                        <Grid item xs={12}>
                            {
                                loading ?
                                    <DotLoader />
                                    :
                                    <Button variant={'contained'} type={'submit'}
                                            disabled={!!errors.countryCode || !!errors.phoneNumber}>
                                        Send message
                                    </Button>
                            }
                        </Grid>
                    </Form>
                )}
            </Formik>
        </Grid>
    );
};

export default observer(PhoneNumberForm)