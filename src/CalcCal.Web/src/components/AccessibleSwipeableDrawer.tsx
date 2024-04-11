import {SwipeableDrawer, SwipeableDrawerProps} from "@mui/material";
import {ReactNode, useState} from "react";

interface AccessibleSwipeableDrawerProps extends SwipeableDrawerProps {
    children?: ReactNode;
    setOpen: (state: boolean) => void
    orientation: 'vertical' | 'horizontal'
}

export const AccessibleSwipeableDrawer = ({ children, setOpen, orientation, ...props }: AccessibleSwipeableDrawerProps) => {
    const [startX, setStartX] = useState(0);
    const [startY, setStartY] = useState(0);

    const handleMouseDown = (event: React.MouseEvent) => {
        orientation === 'vertical' ? setStartY(event.clientY) : setStartX(event.clientX)
    }

    const handleMouseUp = () => {
        orientation === 'vertical' ? setStartY(0) : setStartX(0)
    }

    const handleMouseMove = (event: React.MouseEvent) => {
        const deltaX = event.clientX - startX
        const deltaY = event.clientY - startY
console.log('hi')
        if (orientation === 'vertical' && deltaX > 50) {
            setOpen(true)
        } else if (orientation === 'vertical' && deltaX < -50) {
            setOpen(false)
        }

        if (orientation === 'vertical' && deltaY > 50) {
            setOpen(true)
        } else if (orientation === 'vertical' && deltaY < -50) {
            setOpen(false)
        }
    }

    return (
        <SwipeableDrawer
            onMouseDown={handleMouseDown}
            onMouseMove={handleMouseMove}
            onMouseUp={handleMouseUp}
            {...props}
        >
            {children}
        </SwipeableDrawer>
    );
}