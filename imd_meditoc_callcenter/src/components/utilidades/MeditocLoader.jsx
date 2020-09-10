import PropTypes from "prop-types";
import React from "react";
import { Backdrop, CircularProgress, Typography } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../../configurations/themeConfig";

const useStyles = makeStyles(() => ({
    backdrop: {
        zIndex: theme.zIndex.drawer + 1000,
        color: "#fff",
    },
}));

/*************************************************************
 * Descripcion: Representa el loader para todos los eventos de "loading..." del portal de Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: App
 *************************************************************/
const MeditocLoader = (props) => {
    const { entLoader } = props;

    const classes = useStyles();

    return (
        <Backdrop
            className={classes.backdrop}
            open={entLoader.open}
            style={{ backgroundColor: "rgb(255 255 255 / 0.7)" }}
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

MeditocLoader.propTypes = {
    entLoader: PropTypes.shape({
        message: PropTypes.string.isRequired,
        open: PropTypes.bool.isRequired,
    }),
};

export default MeditocLoader;
