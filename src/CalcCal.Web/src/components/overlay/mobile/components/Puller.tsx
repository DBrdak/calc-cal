import {styled} from "@mui/material";

interface PullerProps {
    variant: 'top' | 'bottom';
}

export const Puller = styled('div')<PullerProps>(({ theme, variant }) => ({
    width: 30,
    height: 6,
    backgroundColor: theme.palette.text.secondary,
    borderRadius: 3,
    position: 'absolute',
    [variant === 'top' ? 'bottom' : 'top']: 8,
    left: 'calc(50% - 15px)',
}))