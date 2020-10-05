import { Button, Grid } from "@material-ui/core";

import PropTypes from "prop-types";
import React from "react";

const MeditocModalBotones = (props) => {
    const {
        okMessage,
        cancelMessage,
        okFunc,
        cancelFunc,
        okDisabled,
        align,
        setOpen,
        hideCancel,
        hideOk,
        xs,
        sm,
    } = props;

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
    const mtHideOk = hideOk === undefined ? false : hideOk;

    const mtXs = xs === undefined ? 12 : xs;
    const mtSm = sm === undefined ? 12 : sm;

    const mtOkDisabled = okDisabled === undefined ? false : okDisabled;

    return (
        <Grid item sm={mtSm} xs={mtXs} className={mtAlign}>
            {!mtHideCancel && (
                <Button
                    variant="contained"
                    type="button"
                    style={{ color: "#555", marginRight: "10px" }}
                    onClick={mtCancelFunc}
                >
                    {mtCancelMessage}
                </Button>
            )}
            {!mtHideOk && (
                <Button variant="contained" type="button" disabled={mtOkDisabled} color="primary" onClick={mtOkFunc}>
                    {mtOkMessage}
                </Button>
            )}
            <button type="submit" hidden>
                SUBMIT
            </button>
        </Grid>
    );
};

MeditocModalBotones.propTypes = {
    align: PropTypes.any,
    cancelFunc: PropTypes.any,
    cancelMessage: PropTypes.any,
    hideCancel: PropTypes.any,
    hideOk: PropTypes.any,
    okDisabled: PropTypes.any,
    okFunc: PropTypes.any,
    okMessage: PropTypes.any,
    setOpen: PropTypes.func,
    sm: PropTypes.any,
    xs: PropTypes.any,
};

export default MeditocModalBotones;
