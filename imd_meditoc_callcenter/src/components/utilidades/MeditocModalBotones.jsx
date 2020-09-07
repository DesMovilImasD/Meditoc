import React from "react";
import { Grid, Button } from "@material-ui/core";

const MeditocModalBotones = (props) => {
    const { okMessage, cancelMessage, okFunc, cancelFunc, align, setOpen, hideCancel } = props;

    const mtOkMessage = okMessage === undefined ? "Confirmar" : okMessage;
    const mtCancelMessage = cancelMessage === undefined ? "Cancelar" : cancelMessage;

    const mtOkFunc =
        okFunc === undefined
            ? () => {
                  setOpen(false);
              }
            : okFunc;
    const mtCancelFunc =
        cancelFunc === undefined
            ? () => {
                  setOpen(false);
              }
            : cancelFunc;

    const mtAlign = align === undefined ? "right" : align;

    const mtHideCancel = hideCancel === undefined ? false : hideCancel;

    return (
        <Grid item xs={12} className={mtAlign}>
            {!mtHideCancel && (
                <Button variant="contained" style={{ color: "#555", marginRight: "10px" }} onClick={mtCancelFunc}>
                    {mtCancelMessage}
                </Button>
            )}
            <Button variant="contained" color="primary" onClick={mtOkFunc}>
                {mtOkMessage}
            </Button>
        </Grid>
    );
};

export default MeditocModalBotones;
