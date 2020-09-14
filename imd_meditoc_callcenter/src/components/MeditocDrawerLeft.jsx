import PropTypes from "prop-types";
import React from "react";
import { makeStyles } from "@material-ui/core/styles";
import { Drawer } from "@material-ui/core";
import theme from "../configurations/themeConfig";
import MeditocMenuList from "./MeditocMenuList";

const useStyles = makeStyles({
    drawerColor: {
        backgroundColor: "rgb(254 254 254 / 0.6)",
    },
    paperColor: {
        backgroundColor: theme.palette.primary.main,
    },
    list: {
        width: 300,
    },
});

/*************************************************************
 * Descripcion: Representa el conteneder desplegable del Menú lateral izquierdo
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: ContentMain
 *************************************************************/
const DrawerMenu = (props) => {
    const { drawerOpen, toggleDrawer } = props;

    const classes = useStyles();

    return (
        <Drawer
            BackdropProps={{ className: classes.drawerColor }}
            PaperProps={{ className: classes.paperColor }}
            anchor="left"
            open={drawerOpen}
            onClose={toggleDrawer(false)}
        >
            <div
                className={classes.list + " bg-color-1 color-0"}
                role="presentation"
                //onClick={toggleDrawer(false)}
                onKeyDown={toggleDrawer(false)}
            >
                <MeditocMenuList toggleDrawer={toggleDrawer} />
            </div>
        </Drawer>
    );
};

DrawerMenu.propTypes = {
    drawerOpen: PropTypes.bool,
    toggleDrawer: PropTypes.func,
};

export default DrawerMenu;