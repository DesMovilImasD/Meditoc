import React from "react";
import { Grid, Button } from "@material-ui/core";

const MeditocModalBotones = (props) => {
    const { okMessage, cancelMessage, okFunc, cancelFunc, align } = props;

    const mtOkMessage = okMessage === undefined ? "Confirmar" : okMessage;
    const mtCancelMessage = cancelMessage === undefined ? "Cancelar" : cancelMessage;

    const mtOkFunc = okFunc === undefined ? () => {} : okFunc;
    const mtCancelFunc = cancelFunc === undefined ? () => {} : cancelFunc;

    const mtAlign = align === undefined ? "right" : align;

    return (
        <Grid item xs={12} className={mtAlign}>
            <Button variant="contained" style={{ color: "#555", marginRight: "10px" }} onClick={mtCancelFunc}>
                {mtCancelMessage}
            </Button>
            <Button variant="contained" color="primary" onClick={mtOkFunc}>
                {mtOkMessage}
            </Button>
        </Grid>
    );
};

export default MeditocModalBotones;
