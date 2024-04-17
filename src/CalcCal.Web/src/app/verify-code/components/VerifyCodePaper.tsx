import VerificationCodeForm from "./VerificationCodeForm";
import PhoneNumberForm from "./PhoneNumberForm";
import {useStore} from "../../../stores/store";
import {useNavigate, useParams} from "react-router-dom";
import {observer} from "mobx-react-lite";
import {Button, Grid, Paper, Stack, Typography} from "@mui/material";
import NewPasswordForm from "./NewPasswordForm";
import theme from "../../theme";

const VerifyCodePaper = () => {
    const {userStore} = useStore()
    const {type} = useParams()
    const navigate = useNavigate()

    return (
        <Grid container component={Paper} sx={{
            maxWidth: '500px',
            width: '95vw',
            minWidth: '250px',
            maxHeight: '600px',
            minHeight: '400px',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center',
            borderRadius: '20px',
            position: 'relative',
            textAlign: 'center'
        }}>
            {
                type === 'phone' && !userStore.verificationCountryCode && !userStore.verificationPhoneNumber &&
                    <Grid item xs={12} marginBottom={theme.spacing(3)} textAlign={'center'} sx={{
                        flexDirection: 'column',
                        display: 'flex', justifyContent: 'center', alignItems: 'center',
                        gap: theme.spacing(2)
                    }} >
                        <Typography variant={'h6'} sx={{minWidth: '200px', maxWidth: '400px'}}>
                            Sorry, the verification code failed to send. Please try again later, or logout
                        </Typography>
                        <Button variant={'outlined'} onClick={() => navigate('/logout')} sx={{maxWidth: '150px'}}>
                            Logout
                        </Button>
                    </Grid>
            }
            {type !== 'phone' && !userStore.verificationCountryCode && !userStore.verificationPhoneNumber && <PhoneNumberForm />}
            {type !== 'phone' && userStore.verificationCountryCode && userStore.verificationPhoneNumber && <VerificationCodeForm />}
            {type === 'password' && userStore.verificationCountryCode && userStore.verificationPhoneNumber && userStore.verificationCode && <NewPasswordForm />}
        </Grid>
    );
};

export default observer(VerifyCodePaper)