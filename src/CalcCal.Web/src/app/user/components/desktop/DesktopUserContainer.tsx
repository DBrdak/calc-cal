import {Box, Button, Stack} from "@mui/material"
import {AppName} from "../../../../components/AppName"
import {UserBaseInfoPaper} from "../shared/UserBaseInfoPaper";
import {UserHistory} from "./UserHistory";

export const DesktopUserContainer = () => {
    return (
        <Stack direction={'column'} sx={{
            minWidth: '400px',
            maxWidth: '600px',
            width: '60vw',
            alignItems: 'center'
        }}>
            <AppName />
            <UserBaseInfoPaper />
            <UserHistory />
        </Stack>
    );
};