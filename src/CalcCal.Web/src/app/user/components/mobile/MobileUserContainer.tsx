import {Stack} from "@mui/material";
import {AppName} from "../../../../components/AppName";
import {UserBaseInfoPaper} from "../shared/UserBaseInfoPaper";

export const MobileUserContainer = () => {
    return (
        <Stack direction={'column'} sx={{
            minWidth: '300px',
            maxWidth: '95svw',
            width: '95svw',
            maxHeight: '100%',
            overflow: 'visible',
            display: 'flex',
            alignItems: 'center',
            justifyContent: 'center'
        }}>
            <AppName />
            <UserBaseInfoPaper />
        </Stack>
    );
};