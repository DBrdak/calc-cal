import {Button, Grid} from "@mui/material";

interface UndoButtonProps {
    onUndo: () => void
}

export const UndoButton = ({onUndo}: UndoButtonProps) => {
    return (
        <Grid item xs={12} sx={{
            display: 'flex',
            justifyContent: 'center',
            align: 'center'
        }}>
            <Button variant={'outlined'} color={'inherit'} onClick={() => onUndo()} sx={{
                minWidth: '100px',
                maxHeight: '50px',
                borderRadius: '20px'
            }}>
                Go back
            </Button>
        </Grid>
    );
};