import PropTypes from "prop-types";
import React from "react";
import { Backdrop, CircularProgress, Typography } from "@material-ui/core";

/*****************************************************
 * Descripción: Loader del sitio
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Loader = (props) => {
    const { entLoader } = props;

    return (
        <Backdrop className="loader-back" open={entLoader.open}>
            <div className="center">
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
