import {VerificationCodeForm} from "./VerificationCodeForm";
import PhoneNumberForm from "./PhoneNumberForm";
import {useStore} from "../../../stores/store";
import {useParams} from "react-router-dom";
import {observer} from "mobx-react-lite";
import {Grid, Paper} from "@mui/material";
import theme from "../../theme";

const VerifyCodePaper = () => {
    const {userStore} = useStore()
    const {type} = useParams()

    return (
        <Grid container component={Paper} sx={{
            maxWidth: '500px',
            width: '95vw',
            minWidth: '250px',
            maxHeight: '600px',
            minHeight: '400px',
            display: 'flex',
            alignItems: 'center',
            borderRadius: '20px',
            position: 'relative',
            paddingTop: theme.spacing(6)
        }}>
            {
                userStore.verificationCountryCode && userStore.verificationPhoneNumber ?
                    <VerificationCodeForm />
                    :
                    <PhoneNumberForm />
            }
        </Grid>
    );
};

export default observer(VerifyCodePaper)