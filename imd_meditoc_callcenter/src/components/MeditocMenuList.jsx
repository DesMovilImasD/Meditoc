import React, { Fragment, useState } from "react";
import { List, ListItem, ListItemText, Collapse, makeStyles, ListItemIcon } from "@material-ui/core";
import ExpandLess from "@material-ui/icons/ExpandLess";
import ExpandMore from "@material-ui/icons/ExpandMore";
import { Link } from "react-router-dom";
import { urlSystem } from "../configurations/urlConfig";
import SettingsIcon from "@material-ui/icons/Settings";
import GroupIcon from "@material-ui/icons/Group";
import VerifiedUserIcon from "@material-ui/icons/VerifiedUser";
import AccountTreeIcon from "@material-ui/icons/AccountTree";
import WorkIcon from "@material-ui/icons/Work";
import AssignmentIndIcon from "@material-ui/icons/AssignmentInd";
import BusinessIcon from "@material-ui/icons/Business";
import ShoppingCartIcon from "@material-ui/icons/ShoppingCart";
import LoyaltyIcon from "@material-ui/icons/Loyalty";
import EmojiObjectsIcon from "@material-ui/icons/EmojiObjects";
import GradeIcon from "@material-ui/icons/Grade";
import CardMembershipIcon from "@material-ui/icons/CardMembership";
import CallIcon from "@material-ui/icons/Call";
import ContactPhoneIcon from "@material-ui/icons/ContactPhone";
import AddIcCallIcon from "@material-ui/icons/AddIcCall";
import PrintIcon from "@material-ui/icons/Print";
import LocalMallIcon from "@material-ui/icons/LocalMall";
import LocalHospitalIcon from "@material-ui/icons/LocalHospital";

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
    const { toggleDrawer, usuarioPermisos } = props;

    const classes = useStyles();

    const [openConfiguracion, setOpenConfiguracion] = useState(false);
    const [openAdministracion, setOpenAdministracion] = useState(false);
    const [openFolios, setOpenFolios] = useState(false);
    const [openCallCenter, setOpenCallCenter] = useState(false);
    const [openReportes, setOpenReportes] = useState(false);

    return (
        <List component="div">
            {usuarioPermisos.configuracion.set === true && (
                <Fragment>
                    <ListItem button onClick={() => setOpenConfiguracion(!openConfiguracion)}>
                        <ListItemIcon>
                            <SettingsIcon className="color-0" />
                        </ListItemIcon>
                        <ListItemText primary={usuarioPermisos.configuracion.name} />
                        {openConfiguracion ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={openConfiguracion} unmountOnExit>
                        <List component="div">
                            {usuarioPermisos.configuracion.usuarios.set === true && (
                                <Link to={urlSystem.configuracion.usuarios} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <GroupIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.configuracion.usuarios.name} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos.configuracion.perfiles.set === true && (
                                <Link to={urlSystem.configuracion.perfiles} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <VerifiedUserIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.configuracion.perfiles.name} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos.configuracion.sistema.set === true && (
                                <Link to={urlSystem.configuracion.sistema} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <AccountTreeIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.configuracion.sistema.name} />
                                    </ListItem>
                                </Link>
                            )}
                        </List>
                    </Collapse>
                </Fragment>
            )}
            {usuarioPermisos.administracion.set === true && (
                <Fragment>
                    <ListItem button onClick={() => setOpenAdministracion(!openAdministracion)}>
                        <ListItemIcon>
                            <WorkIcon className="color-0" />
                        </ListItemIcon>
                        <ListItemText primary={usuarioPermisos.administracion.name} />
                        {openAdministracion ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={openAdministracion} unmountOnExit>
                        <List component="div">
                            {usuarioPermisos.administracion.colaboradores.set === true && (
                                <Link to={urlSystem.administracion.colaboradores} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <AssignmentIndIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.administracion.colaboradores.name} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos.administracion.empresa.set === true && (
                                <Link to={urlSystem.administracion.institucion} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <BusinessIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.administracion.empresa.name} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos.administracion.productos.set === true && (
                                <Link to={urlSystem.administracion.productos} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <ShoppingCartIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.administracion.productos.name} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos.administracion.cupones.set === true && (
                                <Link to={urlSystem.administracion.cupones} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <LoyaltyIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.administracion.cupones.name} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos.administracion.especialidades.set === true && (
                                <Link to={urlSystem.administracion.especialidades} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <EmojiObjectsIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.administracion.especialidades.name} />
                                    </ListItem>
                                </Link>
                            )}
                        </List>
                    </Collapse>
                </Fragment>
            )}

            {usuarioPermisos.folios.set === true && (
                <Fragment>
                    <ListItem button onClick={() => setOpenFolios(!openFolios)}>
                        <ListItemIcon>
                            <GradeIcon className="color-0" />
                        </ListItemIcon>
                        <ListItemText primary={usuarioPermisos.folios.name} />
                        {openFolios ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={openFolios} unmountOnExit>
                        <List component="div">
                            {usuarioPermisos.folios.folios.set === true && (
                                <Link to={urlSystem.folios.folios} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <CardMembershipIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.folios.folios.name} />
                                    </ListItem>
                                </Link>
                            )}
                        </List>
                    </Collapse>
                </Fragment>
            )}

            {usuarioPermisos.callcenter.set === true && (
                <Fragment>
                    <ListItem button onClick={() => setOpenCallCenter(!openCallCenter)}>
                        <ListItemIcon>
                            <CallIcon className="color-0" />
                        </ListItemIcon>
                        <ListItemText primary={usuarioPermisos.callcenter.name} />
                        {openCallCenter ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={openCallCenter} unmountOnExit>
                        <List component="div">
                            {usuarioPermisos.callcenter.consultas.set === true && (
                                <Link to={urlSystem.callcenter.consultas} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <ContactPhoneIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.callcenter.consultas.name} />
                                    </ListItem>
                                </Link>
                            )}

                            {usuarioPermisos.callcenter.administrarconsultas.set === true && (
                                <Link to={urlSystem.callcenter.administrarConsultas} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <AddIcCallIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.callcenter.administrarconsultas.name} />
                                    </ListItem>
                                </Link>
                            )}
                        </List>
                    </Collapse>
                </Fragment>
            )}

            {usuarioPermisos.reportes.set === true && (
                <Fragment>
                    <ListItem button onClick={() => setOpenReportes(!openReportes)}>
                        <ListItemIcon>
                            <PrintIcon className="color-0" />
                        </ListItemIcon>
                        <ListItemText primary={usuarioPermisos.reportes.name} />
                        {openReportes ? <ExpandLess /> : <ExpandMore />}
                    </ListItem>
                    <Collapse in={openReportes} unmountOnExit>
                        <List component="div">
                            {usuarioPermisos.reportes.ventas.set === true && (
                                <Link to={urlSystem.reportes.ordenes} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <LocalMallIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.reportes.ventas.name} />
                                    </ListItem>
                                </Link>
                            )}

                            {usuarioPermisos.reportes.doctores.set === true && (
                                <Link to={urlSystem.reportes.doctores} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <LocalHospitalIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos.reportes.doctores.name} />
                                    </ListItem>
                                </Link>
                            )}
                        </List>
                    </Collapse>
                </Fragment>
            )}
        </List>
    );
};

export default MenuList;
