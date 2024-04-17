import {Button, Grid, TextField} from "@mui/material";
import {Form, Formik} from "formik";
import * as yup from 'yup'
import theme from "../../theme";
import {DotLoader} from "../../../components/DotLoader";
import {useState} from "react";
import {useNavigate, useParams} from "react-router-dom";
import {PASSWORD_PATTERN, VERIFICATION_CODE_PATTERN} from "../../../utlis/settings/constants";
import '../../calculator/components/shared/calculatorForm/components/foodQuantityFormStyles.css'
import {observer} from "mobx-react-lite";
import {useStore} from "../../../stores/store";

const NewPasswordForm = () => {
    const [isFormFocused, setIsFormFocused] = useState(false)
    const [loading, setLoading] = useState(false)
    const navigate = useNavigate()
    const {type} = useParams()
    const {userStore} = useStore()

    function handleFormSubmit(password: string) {
        setLoading(true)
        userStore.changePassword(password).then(() => {
            setLoading(false)
            navigate('/login')
        })
    }

    const validationSchema = yup.object({
        password: yup.string()
            .required('Insert password')
            .matches(PASSWORD_PATTERN, 'Password too weak')
    })

    return (
        <Grid item xs={12}>
            <Formik initialValues={{password: ''}} onSubmit={values => handleFormSubmit(values.password)} validationSchema={validationSchema}>
                {({handleChange, errors}) => (
                    <Form autoComplete={'off'} style={{
                        width: '100%',
                        display: 'flex',
                        justifyContent: 'center',
                        alignItems: 'center',
                        flexDirection: 'column',
                        position: 'relative'
                    }}>
                        <Grid item xs={12} marginBottom={theme.spacing(3)}>
                            <TextField
                                name={'password'}
                                type={'password'}
                                label={isFormFocused && !errors.password ? 'Verification code' : ''}
                                placeholder={'Verification code'}
                                fullWidth
                                onChange={handleChange}
                                onFocusCapture={() => setIsFormFocused(true)}
                                onBlurCapture={() => setIsFormFocused(false)}
                                focused={isFormFocused}
                                inputProps={{
                                    min: 1,
                                    max: 999999,
                                }}
                                inputMode={'none'}
                                InputLabelProps={{
                                    shrink: false,
                                    focused: isFormFocused
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
                                            disabled={!!errors.password}>
                                        Change Password
                                    </Button>
                            }
                        </Grid>
                    </Form>
                )}
            </Formik>
        </Grid>
    );
};

export default observer(NewPasswordForm)