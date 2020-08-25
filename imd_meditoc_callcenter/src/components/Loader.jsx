import PropTypes from "prop-types";
import React from "react";
import { Backdrop, CircularProgress, Typography } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../configurations/themeConfig";

const useStyles = makeStyles(() => ({
    backdrop: {
        zIndex: theme.zIndex.drawer + 1,
        color: "#fff",
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
        <Backdrop
            className={classes.backdrop}
            open={entLoader.open}
            style={{ backgroundColor: "rgb(255 255 255 / 0.6)" }}
        >
            <div className="center">
                <CircularProgress color="primary" />
                <br />
                <Typography variant="subtitle2" align="center" color="textSecondary">
                    {entLoader.message}
                </Typography>
            </div>
        </Backdrop>
    );
};

Loader.propTypes = {
    entLoader: PropTypes.shape({
        message: PropTypes.string.isRequired,
        open: PropTypes.bool.isRequired,
    }),
};

export default Loader;
