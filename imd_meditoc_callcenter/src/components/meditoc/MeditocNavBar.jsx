import { AppBar, Button, IconButton, ListItemIcon, Menu, MenuItem, Toolbar, makeStyles } from "@material-ui/core";
import React, { Fragment, useState } from "react";

import ExitToAppIcon from "@material-ui/icons/ExitToApp";
import FormCambiarPassword from "./configuracion/usuarios/FormCambiarPassword";
import MenuIcon from "@material-ui/icons/Menu";
import PersonIcon from "@material-ui/icons/Person";
import PropTypes from "prop-types";
import VpnKeyIcon from "@material-ui/icons/VpnKey";
import { imgLogoMeditoc } from "../../configurations/imgConfig";
import theme from "../../configurations/themeConfig";
import { useHistory } from "react-router-dom";

const useStyles = makeStyles({
    root: {
        backgroundColor: theme.palette.primary.main,
        color: "#fff",
    },
});

/*************************************************************
 * Descripcion: Contiene la estructura y diseño de la Barra principal de Meditoc en la parte superior del portal
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: ContentMain
 *************************************************************/
const NavBar = (props) => {
    const {
        toggleDrawer,
        setUsuarioSesion,
        setUsuarioActivo,
        setUsuarioPermisos,
        usuarioSesion,
        funcCerrarTodo,
        funcLoader,
        funcAlert,
    } = props;
    const classes = useStyles();

    const history = useHistory();

    const [anchorEl, setAnchorEl] = useState(null);
    const [modalCambiarPasswordOpen, setModalCambiarPasswordOpen] = useState(false);

    const isMenuOpen = Boolean(anchorEl);

    const handleProfileMenuOpen = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleMenuClose = () => {
        setAnchorEl(null);
    };

    const handleClickCambiarPassword = () => {
        setModalCambiarPasswordOpen(true);
    };

    const handleClickCerrarSesion = () => {
        sessionStorage.removeItem("MeditocTkn");
        sessionStorage.removeItem("MeditocKey");

        if (typeof funcCerrarTodo === "function") {
            funcCerrarTodo();
        }
        history.push("/");

        setUsuarioSesion(null);
        setUsuarioPermisos(null);
        setUsuarioActivo(false);
    };

    return (
        <Fragment>
            <div className="flx-grw-1">
                <AppBar position="relative" color="inherit" elevation={0}>
                    <Toolbar>
                        <IconButton className="size-50" onClick={toggleDrawer(true)}>
                            <MenuIcon className="color-1" />
                        </IconButton>
                        <div className="flx-grw-1">
                            <img className="navbar-img" src={imgLogoMeditoc} alt="logomeditoc" />
                        </div>
                        <PersonIcon className="color-1" />
                        <Button style={{ textTransform: "initial" }} onClick={handleProfileMenuOpen}>
                            <span className="rob-con bold color-1 size-20">
                                {usuarioSesion.sNombres + " " + usuarioSesion.sApellidoPaterno}
                            </span>
                        </Button>
                        <Menu
                            anchorEl={anchorEl}
                            id="menu-profile"
                            keepMounted
                            open={isMenuOpen}
                            onClose={handleMenuClose}
                            classes={{ paper: classes.root }}
                        >
                            <MenuItem onClick={handleClickCambiarPassword}>
                                <ListItemIcon>
                                    <VpnKeyIcon className="color-0" />
                                </ListItemIcon>
                                Cambiar contraseña
                            </MenuItem>
                            <MenuItem onClick={handleClickCerrarSesion}>
                                <ListItemIcon>
                                    <ExitToAppIcon className="color-0" />
                                </ListItemIcon>
                                Cerrar sesión
                            </MenuItem>
                        </Menu>
                    </Toolbar>
                </AppBar>
            </div>
            <FormCambiarPassword
                open={modalCambiarPasswordOpen}
                setOpen={setModalCambiarPasswordOpen}
                usuarioSesion={usuarioSesion}
                setAnchorEl={setAnchorEl}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

NavBar.propTypes = {
    funcAlert: PropTypes.any,
    funcCerrarTodo: PropTypes.func,
    funcLoader: PropTypes.any,
    setUsuarioActivo: PropTypes.func,
    setUsuarioSesion: PropTypes.func,
    toggleDrawer: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        sApellidoPaterno: PropTypes.any,
        sNombres: PropTypes.string,
    }),
};

export default NavBar;
