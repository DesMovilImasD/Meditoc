import { Dialog, Slide } from "@material-ui/core";

import MeditocFooter from "../meditoc/MeditocFooter";
import PropTypes from "prop-types";
import React from "react";
import { forwardRef } from "react";

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
            <div className="flx-grw-1 pos-rel">
                {children}
                <div style={{ height: 30 }}></div>
            </div>
            <MeditocFooter />
        </Dialog>
    );
};

MeditocFullModal.propTypes = {
    children: PropTypes.any,
    open: PropTypes.any,
    setOpen: PropTypes.func,
};

export default MeditocFullModal;
