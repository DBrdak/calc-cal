import {Button, Grid, TextField} from "@mui/material";
import {Form, Formik} from "formik";
import * as yup from 'yup'
import theme from "../../theme";
import {DotLoader} from "../../../components/DotLoader";
import {useState} from "react";
import {useNavigate, useParams} from "react-router-dom";
import {VERIFICATION_CODE_PATTERN} from "../../../utlis/settings/constants";
import '../../calculator/components/shared/calculatorForm/components/foodQuantityFormStyles.css'
import {observer} from "mobx-react-lite";
import {useStore} from "../../../stores/store";

const VerificationCodeForm = () => {
    const [isFormFocused, setIsFormFocused] = useState(false)
    const [loading, setLoading] = useState(false)
    const navigate = useNavigate()
    const {type} = useParams()
    const {userStore} = useStore()

    function handleFormSubmit(code: string) {
        if(type === 'phone'){
            setLoading(true)
            userStore.verifyPhoneNumber(code).then(() => {
                setLoading(false)
                navigate('/')
            })
        }
        if(type === 'password') {
            userStore.setVerificationCode(code)
        }
    }

    const validationSchema = yup.object({
        code: yup.string()
            .required('Insert verification code')
            .matches(VERIFICATION_CODE_PATTERN, 'Invalid verification code')
    })

    return (
        <Grid item xs={12}>
            <Formik initialValues={{code: ''}} onSubmit={values => handleFormSubmit(values.code)} validationSchema={validationSchema}>
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
                                name={'code'}
                                label={isFormFocused && !errors.code ? 'Verification code' : ''}
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
                                helperText={errors.code}
                                error={!!errors.code}
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
                                            disabled={!!errors.code || !!errors.code}>
                                        Verify
                                    </Button>
                            }
                        </Grid>
                    </Form>
                )}
            </Formik>
        </Grid>
    );
};

export default observer(VerificationCodeForm)