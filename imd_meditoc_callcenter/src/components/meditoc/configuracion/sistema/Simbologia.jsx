import { Fab, Paper, Zoom } from "@material-ui/core";
import React, { Fragment, useState } from "react";

import AccountTreeIcon from "@material-ui/icons/AccountTree";
import ExtensionIcon from "@material-ui/icons/Extension";
import LiveHelpIcon from "@material-ui/icons/LiveHelp";
import WebIcon from "@material-ui/icons/Web";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../../../../configurations/themeConfig";

const useStyles = makeStyles({
    absolute: {
        position: "absolute",
        top: theme.spacing(20),
        right: theme.spacing(3),
        zIndex: theme.zIndex.appBar + 1,
    },
    absoluteDiv: {
        padding: "10px 30px",
        position: "absolute",
        top: theme.spacing(20),
        right: theme.spacing(12),
        lineHeight: 2,
        zIndex: theme.zIndex.appBar + 1,
    },
});

/*************************************************************
 * Descripcion: Boton de ayuda para mostrar la simbología de los
 * componentes (Módulo, Submódulo y Botón) para el adminsitrador de CGU
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: Sistema, Permisos
 *************************************************************/
const Simbologia = () => {
    const classes = useStyles();

    //Guardar state (Mostrar/Ocultar) de la simbología
    const [fabOpen, setFabOpen] = useState(false);

    return (
        <Fragment>
            <Fab
                color="secondary"
                className={classes.absolute}
                onMouseEnter={() => setFabOpen(true)}
                onMouseLeave={() => setFabOpen(false)}
                onClick={() => setFabOpen(true)}
            >
                <LiveHelpIcon />
            </Fab>
            <Zoom in={fabOpen}>
                <Paper className={classes.absoluteDiv} elevation={10}>
                    <div>
                        <span className="rob-nor bold size-15 color-3">Simbología:</span>
                    </div>
                    <div>
                        <AccountTreeIcon className={"color-1 vertical-align-middle"} />
                        <span className={"color-1 size-15"}>Módulos</span>
                    </div>
                    <div>
                        <WebIcon className={"color-2 vertical-align-middle"} />
                        <span className={"color-2 size-15"}>Submódulos</span>
                    </div>
                    <div>
                        <ExtensionIcon className={"color-3 vertical-align-middle"} />
                        <span className={"color-3 size-15"}>Botones</span>
                    </div>
                </Paper>
            </Zoom>
        </Fragment>
    );
};

export default Simbologia;
