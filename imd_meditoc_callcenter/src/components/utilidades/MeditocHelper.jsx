import { Fab, Paper, Zoom } from "@material-ui/core";
import React, { useState } from "react";

import LiveHelpIcon from "@material-ui/icons/LiveHelp";
import MeditocSubtitulo from "./MeditocSubtitulo";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../../configurations/themeConfig";

const useStyles = makeStyles({
    absolute: {
        position: "absolute",
        top: theme.spacing(20),
        right: theme.spacing(3),
        zIndex: theme.zIndex.appBar + 1,
    },
    absoluteDiv: {
        padding: "10px 30px 20px",
        position: "fixed",
        top: theme.spacing(20),
        right: theme.spacing(12),
        lineHeight: 2.3,
        width: "auto",
        zIndex: theme.zIndex.appBar + 1,
    },
});
const MeditocHelper = (props) => {
    const { title, children } = props;

    const classes = useStyles();

    //Guardar state (Mostrar/Ocultar) de la simbología
    const [fabOpen, setFabOpen] = useState(false);

    return (
        <div style={{ position: "fixed", right: "10px", top: "0%" }}>
            <div>
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
                            <MeditocSubtitulo title={title} />
                        </div>
                        {children}
                    </Paper>
                </Zoom>
            </div>
        </div>
    );
};

export default MeditocHelper;
