import PropTypes from "prop-types";
import React from "react";
import { IconButton, Dialog, Paper, makeStyles } from "@material-ui/core";
import CloseIcon from "@material-ui/icons/Close";
import Draggable from "react-draggable";

/*************************************************************
 * Descripcion: Contiene la estructura y diseño para los modales del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: EliminarBoton, EliminarModulo, EliminarSubmodulo, FormBoton, FormModulo, FormSubmodulo
 *************************************************************/
function PaperComponent(propsPaper) {
    return (
        <Draggable
            handle="#meditoc-drag-1"
            cancel={'[class*="MuiDialogContent-root"]'}
            //bounds="parent"
        >
            <Paper {...propsPaper} style={{ borderRadius: 12 }} />
        </Draggable>
    );
}

function PaperComponent2(propsPaper) {
    return (
        <Draggable
            handle="#meditoc-drag-2"
            cancel={'[class*="MuiDialogContent-root"]'}
            //bounds="parent"
        >
            <Paper {...propsPaper} style={{ borderRadius: 12 }} />
        </Draggable>
    );
}

const useStyles = makeStyles({
    paperScrollBody: {
        verticalAlign: "top", // default center
    },
    dialogRoot: {
        zIndex: 100,
    },
});

const MeditocModal = (props) => {
    const { size, title, children, open, setOpen, level } = props;

    const classes = useStyles();

    const handleCloseModel = () => {
        setOpen(false);
    };

    const mmLevel = level === undefined ? 1 : level;

    return (
        // <Modal
        //     open={open}
        //     onClose={handleCloseModel}
        //     closeAfterTransition
        //     disableBackdropClick
        //     BackdropComponent={Backdrop}
        //     BackdropProps={{
        //         timeout: 300,
        //         style: { backgroundColor: "rgb(255 255 255 / 0.7)" },
        //     }}
        //     style={{ overflowY: "auto" }}
        // >
        //     <Fade in={open}>
        //         <div className={`modal-form modal-${size}`}>
        //             <div className="modal-form-header ">
        //                 <div className="flx-grw-1 align-self-center">
        //                     <span className="rob-nor size-20 pad-left-20 color-0">{title}</span>
        //                 </div>
        //                 <IconButton onClick={handleCloseModel}>
        //                     <CloseIcon className="color-0" />
        //                 </IconButton>
        //             </div>
        //             <div className="modal-from-content">{children}</div>
        //         </div>
        //     </Fade>
        // </Modal>
        <Dialog
            open={open}
            onClose={handleCloseModel}
            //BackdropComponent={Backdrop}
            BackdropProps={{
                timeout: 300,
                style: { backgroundColor: "rgb(255 255 255 / 0.7)" },
            }}
            scroll="body"
            //PaperComponent={(props) => <PaperComponent {...props} id="draggable-dialog-title" />}
            PaperComponent={mmLevel === 2 ? PaperComponent2 : PaperComponent}
            disableBackdropClick
            maxWidth={size === "small" ? "sm" : size === "normal" ? "md" : "lg"}
            classes={{ paperScrollBody: classes.paperScrollBody, root: classes.dialogRoot }}
        >
            <div className="modal-form-header " id={mmLevel === 2 ? "meditoc-drag-2" : "meditoc-drag-1"}>
                <div className="flx-grw-1 align-self-center">
                    <span className="rob-nor size-20 pad-left-20 color-0">{title}</span>
                </div>
                <IconButton onClick={handleCloseModel}>
                    <CloseIcon className="color-0" />
                </IconButton>
            </div>
            <div className="modal-from-content">{children}</div>
        </Dialog>
    );
};

MeditocModal.propTypes = {
    children: PropTypes.any,
    level: PropTypes.any,
    open: PropTypes.any,
    setOpen: PropTypes.func,
    size: PropTypes.string,
    title: PropTypes.any,
};

export default MeditocModal;
