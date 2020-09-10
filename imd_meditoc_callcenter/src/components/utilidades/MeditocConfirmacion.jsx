import PropTypes from "prop-types";
import React from "react";
import MeditocModal from "./MeditocModal";
import { Grid, Button } from "@material-ui/core";
import MeditocModalBotones from "./MeditocModalBotones";

/*************************************************************
 * Descripcion: Representa una alerta de confirmación genérica
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: -
 *************************************************************/
const MeditocConfirmacion = (props) => {
    const { open, setOpen, title, children, okMessage, cancelMessage, okFunc, cancelFunc } = props;

    //Titulo de la alerta
    const fTitle = title === undefined ? "Confirmación" : title;

    //Funcion al presionar OK
    const fOKFunc =
        okFunc === undefined
            ? () => {
                  setOpen(false);
              }
            : okFunc;

    //Funcion al presionar Cancelar
    const fCancelFunc =
        cancelFunc === undefined
            ? () => {
                  setOpen(false);
              }
            : cancelFunc;

    return (
        <MeditocModal title={fTitle} size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12} className="center">
                    {children}
                </Grid>
                <MeditocModalBotones
                    okMessage={okMessage}
                    cancelMessage={cancelMessage}
                    okFunc={fOKFunc}
                    cancelFunc={fCancelFunc}
                    align="center"
                />
            </Grid>
        </MeditocModal>
    );
};

MeditocConfirmacion.propTypes = {
    cancelFunc: PropTypes.any,
    cancelMessage: PropTypes.any,
    children: PropTypes.any,
    okFunc: PropTypes.any,
    okMessage: PropTypes.any,
    open: PropTypes.bool,
    setOpen: PropTypes.func,
    title: PropTypes.any,
};

export default MeditocConfirmacion;
