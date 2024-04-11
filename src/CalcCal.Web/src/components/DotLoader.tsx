import './componentStyles.css'
import theme from "../app/theme";
import {keyframes, styled} from "@mui/material";

const dotAnimation = keyframes`
    0% {
        transform: scale(1);
        opacity: 0;
        background-color: ${theme.palette.secondary.main};
    }
    50% {
        transform: scale(1.5);
        opacity: 1;
        background-color: ${theme.palette.secondary.dark};
    }
    100% {
        transform: scale(1);
        opacity: 0;
        background-color: ${theme.palette.secondary.light};
    }
`;

const LoadingContainer = styled('div')`
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100%;
`;

const Dot = styled('div')`
    width: 10px;
    height: 10px;
    border-radius: 50%;
    margin: 0 3px;
    animation: ${dotAnimation} 1.5s infinite;
`;

export const DotLoader = () => {

    return (
        <LoadingContainer>
            <Dot sx={{animationDelay: '0s'}} />
            <Dot sx={{animationDelay: '0.15s'}} />
            <Dot sx={{animationDelay: '0.3s'}} />
            <Dot sx={{animationDelay: '0.45s'}} />
            <Dot sx={{animationDelay: '0.6s'}} />
            <Dot sx={{animationDelay: '0.75s'}} />
        </LoadingContainer>
    )
}