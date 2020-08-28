import React from "react";
import ModalForm from "./ModalForm";
import { Grid, Button } from "@material-ui/core";

const Confirmacion = (props) => {
    const { open, setOpen, title, children, okMessage, cancelMessage, okFunc, cancelFunc } = props;

    const fTitle = title === undefined ? "Confirmación" : title;

    const fOkMessage = okMessage === undefined ? "Aceptar" : okMessage;

    const fCancelMessage = cancelMessage === undefined ? "Cancelar" : cancelMessage;

    const fOKFunc =
        okFunc === undefined
            ? () => {
                  setOpen(false);
              }
            : okFunc;

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

export default Confirmacion;
