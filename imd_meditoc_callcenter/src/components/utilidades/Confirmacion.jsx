import PropTypes from "prop-types";
import React from "react";
import ModalForm from "./ModalForm";
import { Grid, Button } from "@material-ui/core";

/*************************************************************
 * Descripcion: Representa una alerta de confirmación genérica
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: -
 *************************************************************/
const Confirmacion = (props) => {
    const { open, setOpen, title, children, okMessage, cancelMessage, okFunc, cancelFunc } = props;

    //Titulo de la alerta
    const fTitle = title === undefined ? "Confirmación" : title;

    //Texto para boton OK
    const fOkMessage = okMessage === undefined ? "Aceptar" : okMessage;

    //Texto para boton Cancelar
    const fCancelMessage = cancelMessage === undefined ? "Cancelar" : cancelMessage;

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
        <ModalForm title={fTitle} size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12} className="center">
                    {children}
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={fOKFunc}>
                        {fOkMessage}
                    </Button>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="secondary" fullWidth onClick={fCancelFunc}>
                        {fCancelMessage}
                    </Button>
                </Grid>
            </Grid>
        </ModalForm>
    );
};

Confirmacion.propTypes = {
    cancelFunc: PropTypes.any,
    cancelMessage: PropTypes.any,
    children: PropTypes.any,
    okFunc: PropTypes.any,
    okMessage: PropTypes.any,
    open: PropTypes.bool,
    setOpen: PropTypes.func,
    title: PropTypes.any,
};

export default Confirmacion;
