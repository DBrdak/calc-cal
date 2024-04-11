import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {Box, LinearProgress, Typography} from "@mui/material";

export default function NotFoundPage () {
    const [progress, setProgress] = useState(0)
    const navigate = useNavigate()

    useEffect(() => {
        const timer = setInterval(() => {
            setProgress((oldProgress) => {
                if (oldProgress === 100) {
                    clearInterval(timer)
                    return 100
                }
                return Math.min(oldProgress + 1, 100)
            });
        }, 25)

        return () => {
            clearInterval(timer)
        }
    }, [])

    useEffect(() => {
        if(progress === 100) {
            navigate('/')
        }
    }, [progress])
    return (
        <Box
            display="flex"
            flexDirection="column"
            alignItems="center"
            justifyContent="center"
            height="100svh"
            width={'100svw'}
        >
            <Typography variant="h6" marginBottom={2}>
                Content not found
            </Typography>
            <LinearProgress variant='determinate' color='primary' value={progress} style={{ width: '500px' }} />
        </Box>
    )
}