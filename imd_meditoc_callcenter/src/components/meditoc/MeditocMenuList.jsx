import { Collapse, List, ListItem, ListItemIcon, ListItemText, makeStyles } from "@material-ui/core";
import { Link, useHistory } from "react-router-dom";
import React, { Fragment, useState } from "react";

import AccountTreeIcon from "@material-ui/icons/AccountTree";
import AddIcCallIcon from "@material-ui/icons/AddIcCall";
import AssignmentIcon from "@material-ui/icons/Assignment";
import AssignmentIndIcon from "@material-ui/icons/AssignmentInd";
import BusinessIcon from "@material-ui/icons/Business";
import CallIcon from "@material-ui/icons/Call";
import CardMembershipIcon from "@material-ui/icons/CardMembership";
import ContactPhoneIcon from "@material-ui/icons/ContactPhone";
import EmojiObjectsIcon from "@material-ui/icons/EmojiObjects";
import ExpandLess from "@material-ui/icons/ExpandLess";
import ExpandMore from "@material-ui/icons/ExpandMore";
import GradeIcon from "@material-ui/icons/Grade";
import GroupIcon from "@material-ui/icons/Group";
import LocalHospitalIcon from "@material-ui/icons/LocalHospital";
import LocalMallIcon from "@material-ui/icons/LocalMall";
import LoyaltyIcon from "@material-ui/icons/Loyalty";
import PrintIcon from "@material-ui/icons/Print";
import PropTypes from "prop-types";
import SettingsIcon from "@material-ui/icons/Settings";
import ShoppingCartIcon from "@material-ui/icons/ShoppingCart";
import VerifiedUserIcon from "@material-ui/icons/VerifiedUser";
import WorkIcon from "@material-ui/icons/Work";
import { tiempoSalirCallCenter } from "../../configurations/systemConfig";
import { urlSystem } from "../../configurations/urlConfig";

const useStyles = makeStyles({
    nested: {
        paddingLeft: 50,
    },
    link: {
        textDecoration: "none",
        color: "inherit",
    },
});

/*************************************************************
 * Descripcion: Contiene todo el menú del portal con todos los módulos y submódulos activos del portal de Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: DrawerMenu
 *************************************************************/
const MenuList = (props) => {
    const { toggleDrawer, usuarioPermisos, funcCerrarTodo, rutaActual, setRutaActual } = props;

    const classes = useStyles();

    const history = useHistory();

    const [openConfiguracion, setOpenConfiguracion] = useState(false);
    const [openAdministracion, setOpenAdministracion] = useState(false);
    const [openFolios, setOpenFolios] = useState(false);
    const [openCallCenter, setOpenCallCenter] = useState(false);
    const [openReportes, setOpenReportes] = useState(false);

    const handleClickRuta = async (e, ruta) => {
        const funcCloseDrawer = toggleDrawer(false);
        funcCloseDrawer(e);

        if (rutaActual === urlSystem.callcenter.consultas) {
            if (funcCerrarTodo) {
                if (typeof funcCerrarTodo.e === "function") {
                    await funcCerrarTodo.e();
                }
            }

            //Esperar para reiniciar el chat
            setTimeout(() => {
                setRutaActual(ruta);
                history.push(ruta);
            }, tiempoSalirCallCenter);
        } else {
            setRutaActual(ruta);
            history.push(ruta);
        }
    };

    return (
        <List component="div">
            {usuarioPermisos["1"] !== undefined && (
                <Fragment>
                    <ListItem button onClick={() => setOpenConfiguracion(!openConfiguracion)}>
                        <ListItemIcon>
                            <SettingsIcon className="color-0" />
                        </ListItemIcon>
                        <ListItemText primary={usuarioPermisos["1"].Nombre} />
                        {openConfiguracion ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={openConfiguracion} unmountOnExit>
                        <List component="div">
                            {usuarioPermisos["1"].Submodulos["1"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.configuracion.usuarios)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <GroupIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["1"].Submodulos["1"].Nombre} />
                                </ListItem>
                            )}
                            {usuarioPermisos["1"].Submodulos["2"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.configuracion.perfiles)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <VerifiedUserIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["1"].Submodulos["2"].Nombre} />
                                </ListItem>
                            )}
                            {usuarioPermisos["1"].Submodulos["3"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.configuracion.sistema)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <AccountTreeIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["1"].Submodulos["3"].Nombre} />
                                </ListItem>
                            )}
                        </List>
                    </Collapse>
                </Fragment>
            )}
            {usuarioPermisos["2"] !== undefined && (
                <Fragment>
                    <ListItem button onClick={() => setOpenAdministracion(!openAdministracion)}>
                        <ListItemIcon>
                            <WorkIcon className="color-0" />
                        </ListItemIcon>
                        <ListItemText primary={usuarioPermisos["2"].Nombre} />
                        {openAdministracion ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={openAdministracion} unmountOnExit>
                        <List component="div">
                            {usuarioPermisos["2"].Submodulos["1"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.administracion.colaboradores)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <AssignmentIndIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["2"].Submodulos["1"].Nombre} />
                                </ListItem>
                            )}
                            {usuarioPermisos["2"].Submodulos["2"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.administracion.institucion)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <BusinessIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["2"].Submodulos["2"].Nombre} />
                                </ListItem>
                            )}
                            {usuarioPermisos["2"].Submodulos["3"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.administracion.productos)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <ShoppingCartIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["2"].Submodulos["3"].Nombre} />
                                </ListItem>
                            )}
                            {usuarioPermisos["2"].Submodulos["4"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.administracion.cupones)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <LoyaltyIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["2"].Submodulos["4"].Nombre} />
                                </ListItem>
                            )}
                            {usuarioPermisos["2"].Submodulos["5"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.administracion.especialidades)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <EmojiObjectsIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["2"].Submodulos["5"].Nombre} />
                                </ListItem>
                            )}
                        </List>
                    </Collapse>
                </Fragment>
            )}

            {usuarioPermisos["3"] !== undefined && (
                <Fragment>
                    <ListItem button onClick={() => setOpenFolios(!openFolios)}>
                        <ListItemIcon>
                            <GradeIcon className="color-0" />
                        </ListItemIcon>
                        <ListItemText primary={usuarioPermisos["3"].Nombre} />
                        {openFolios ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={openFolios} unmountOnExit>
                        <List component="div">
                            {usuarioPermisos["3"].Submodulos["1"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.folios.folios)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <CardMembershipIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["3"].Submodulos["1"].Nombre} />
                                </ListItem>
                            )}
                        </List>
                    </Collapse>
                </Fragment>
            )}

            {usuarioPermisos["4"] !== undefined && (
                <Fragment>
                    <ListItem button onClick={() => setOpenCallCenter(!openCallCenter)}>
                        <ListItemIcon>
                            <CallIcon className="color-0" />
                        </ListItemIcon>
                        <ListItemText primary={usuarioPermisos["4"].Nombre} />
                        {openCallCenter ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={openCallCenter} unmountOnExit>
                        <List component="div">
                            {usuarioPermisos["4"].Submodulos["1"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.callcenter.consultas)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <ContactPhoneIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["4"].Submodulos["1"].Nombre} />
                                </ListItem>
                            )}

                            {usuarioPermisos["4"].Submodulos["2"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.callcenter.administrarConsultas)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <AddIcCallIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["4"].Submodulos["2"].Nombre} />
                                </ListItem>
                            )}
                            {usuarioPermisos["4"].Submodulos["3"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.callcenter.misconsultas)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <AssignmentIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["4"].Submodulos["3"].Nombre} />
                                </ListItem>
                            )}
                        </List>
                    </Collapse>
                </Fragment>
            )}

            {usuarioPermisos["5"] !== undefined && (
                <Fragment>
                    <ListItem button onClick={() => setOpenReportes(!openReportes)}>
                        <ListItemIcon>
                            <PrintIcon className="color-0" />
                        </ListItemIcon>
                        <ListItemText primary={usuarioPermisos["5"].Nombre} />
                        {openReportes ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={openReportes} unmountOnExit>
                        <List component="div">
                            {usuarioPermisos["5"].Submodulos["1"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.reportes.ordenes)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <LocalMallIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["5"].Submodulos["1"].Nombre} />
                                </ListItem>
                            )}

                            {usuarioPermisos["5"].Submodulos["2"] !== undefined && (
                                <ListItem
                                    button
                                    onClick={(e) => handleClickRuta(e, urlSystem.reportes.doctores)}
                                    className={classes.nested}
                                >
                                    <ListItemIcon>
                                        <LocalHospitalIcon className="color-0" />
                                    </ListItemIcon>
                                    <ListItemText primary={usuarioPermisos["5"].Submodulos["2"].Nombre} />
                                </ListItem>
                            )}
                        </List>
                    </Collapse>
                </Fragment>
            )}
        </List>
    );
};

MenuList.propTypes = {
    toggleDrawer: PropTypes.func,
    usuarioPermisos: PropTypes.any,
};

export default MenuList;
