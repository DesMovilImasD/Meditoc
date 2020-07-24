import PropTypes from "prop-types";
import React from "react";
import { Backdrop, CircularProgress, Typography } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles((theme) => ({
    backdrop: {
        zIndex: theme.zIndex.modal + 100,
        color: "#fff",
    },
    centrar: {
        textAlign: "center",
    },
}));

/*****************************************************
 * Descripción: Loader del sitio
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Loader = (props) => {
    const { entLoader } = props;

    const classes = useStyles();

    return (
        <Backdrop className={classes.backdrop} open={entLoader.open}>
            <div className={classes.centrar}>
                <CircularProgress color="inherit" />
                <br />
                <Typography variant="body1" align="center">
                    {entLoader.message}
                </Typography>
            </div>
        </Backdrop>
    );
};

Loader.propTypes = {
    entLoader: PropTypes.shape({
        message: PropTypes.string,
        open: PropTypes.bool,
    }),
};

export default Loader;
