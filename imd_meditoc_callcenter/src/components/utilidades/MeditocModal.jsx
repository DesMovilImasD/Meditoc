import React from "react";
import { Modal, IconButton, Fade, Backdrop } from "@material-ui/core";
import CloseIcon from "@material-ui/icons/Close";

/*************************************************************
 * Descripcion: Contiene la estructura y diseño para los modales del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: EliminarBoton, EliminarModulo, EliminarSubmodulo, FormBoton, FormModulo, FormSubmodulo
 *************************************************************/
const ModalForm = (props) => {
    const { size, title, children, open, setOpen } = props;

    const handleCloseModel = () => {
        setOpen(false);
    };

    return (
        <Modal
            open={open}
            onClose={handleCloseModel}
            closeAfterTransition
            BackdropComponent={Backdrop}
            BackdropProps={{
                timeout: 300,
                style: { backgroundColor: "rgb(255 255 255 / 0.6)" },
            }}
            style={{ overflowY: "auto" }}
        >
            <Fade in={open}>
                <div className={`modal-form modal-${size}`}>
                    <div className="modal-form-header ">
                        <div className="flx-grw-1 align-self-center">
                            <span className="rob-nor size-20 pad-left-20 color-0">{title}</span>
                        </div>
                        <IconButton onClick={handleCloseModel}>
                            <CloseIcon className="color-0" />
                        </IconButton>
                    </div>
                    <div className="modal-from-content">{children}</div>
                </div>
            </Fade>
        </Modal>
    );
};

export default ModalForm;
