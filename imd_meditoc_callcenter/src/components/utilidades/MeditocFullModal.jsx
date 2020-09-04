import React from "react";
import { forwardRef } from "react";
import { Slide, Dialog } from "@material-ui/core";

const Transition = forwardRef(function Transition(props, ref) {
    return <Slide direction="up" ref={ref} {...props} />;
});

const MeditocFullModal = (props) => {
    const { open, setOpen, children } = props;

    //Funcion para esta ventana de administrarció de permisos
    const handleClose = () => {
        setOpen(false);
    };

    return (
        <Dialog fullScreen open={open} onClose={handleClose} TransitionComponent={Transition}>
            {children}
        </Dialog>
    );
};

export default MeditocFullModal;
