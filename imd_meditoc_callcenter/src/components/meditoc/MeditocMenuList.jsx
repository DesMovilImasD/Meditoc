import { Collapse, List, ListItem, ListItemIcon, ListItemText, makeStyles } from "@material-ui/core";
import React, { Fragment, useState } from "react";

import AccountTreeIcon from "@material-ui/icons/AccountTree";
import AddIcCallIcon from "@material-ui/icons/AddIcCall";
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
import { Link } from "react-router-dom";
import LocalHospitalIcon from "@material-ui/icons/LocalHospital";
import LocalMallIcon from "@material-ui/icons/LocalMall";
import LoyaltyIcon from "@material-ui/icons/Loyalty";
import PrintIcon from "@material-ui/icons/Print";
import PropTypes from "prop-types";
import SettingsIcon from "@material-ui/icons/Settings";
import ShoppingCartIcon from "@material-ui/icons/ShoppingCart";
import VerifiedUserIcon from "@material-ui/icons/VerifiedUser";
import WorkIcon from "@material-ui/icons/Work";
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
    const { toggleDrawer, usuarioPermisos } = props;

    const classes = useStyles();

    const [openConfiguracion, setOpenConfiguracion] = useState(false);
    const [openAdministracion, setOpenAdministracion] = useState(false);
    const [openFolios, setOpenFolios] = useState(false);
    const [openCallCenter, setOpenCallCenter] = useState(false);
    const [openReportes, setOpenReportes] = useState(false);

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
                                <Link to={urlSystem.configuracion.usuarios} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <GroupIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["1"].Submodulos["1"].Nombre} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos["1"].Submodulos["2"] !== undefined && (
                                <Link to={urlSystem.configuracion.perfiles} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <VerifiedUserIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["1"].Submodulos["2"].Nombre} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos["1"].Submodulos["3"] !== undefined && (
                                <Link to={urlSystem.configuracion.sistema} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <AccountTreeIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["1"].Submodulos["3"].Nombre} />
                                    </ListItem>
                                </Link>
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
                                <Link to={urlSystem.administracion.colaboradores} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <AssignmentIndIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["2"].Submodulos["1"].Nombre} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos["2"].Submodulos["2"] !== undefined && (
                                <Link to={urlSystem.administracion.institucion} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <BusinessIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["2"].Submodulos["2"].Nombre} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos["2"].Submodulos["3"] !== undefined && (
                                <Link to={urlSystem.administracion.productos} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <ShoppingCartIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["2"].Submodulos["3"].Nombre} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos["2"].Submodulos["4"] !== undefined && (
                                <Link to={urlSystem.administracion.cupones} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <LoyaltyIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["2"].Submodulos["4"].Nombre} />
                                    </ListItem>
                                </Link>
                            )}
                            {usuarioPermisos["2"].Submodulos["5"] !== undefined && (
                                <Link to={urlSystem.administracion.especialidades} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <EmojiObjectsIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["2"].Submodulos["5"].Nombre} />
                                    </ListItem>
                                </Link>
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
                                <Link to={urlSystem.folios.folios} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <CardMembershipIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["3"].Submodulos["1"].Nombre} />
                                    </ListItem>
                                </Link>
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
                                <Link to={urlSystem.callcenter.consultas} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <ContactPhoneIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["4"].Submodulos["1"].Nombre} />
                                    </ListItem>
                                </Link>
                            )}

                            {usuarioPermisos["4"].Submodulos["2"] !== undefined && (
                                <Link to={urlSystem.callcenter.administrarConsultas} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <AddIcCallIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["4"].Submodulos["2"].Nombre} />
                                    </ListItem>
                                </Link>
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
                                <Link to={urlSystem.reportes.ordenes} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <LocalMallIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["5"].Submodulos["1"].Nombre} />
                                    </ListItem>
                                </Link>
                            )}

                            {usuarioPermisos["5"].Submodulos["2"] !== undefined && (
                                <Link to={urlSystem.reportes.doctores} className={classes.link}>
                                    <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                                        <ListItemIcon>
                                            <LocalHospitalIcon className="color-0" />
                                        </ListItemIcon>
                                        <ListItemText primary={usuarioPermisos["5"].Submodulos["2"].Nombre} />
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

MenuList.propTypes = {
    toggleDrawer: PropTypes.func,
    usuarioPermisos: PropTypes.shape({
        administracion: PropTypes.shape({
            colaboradores: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
            cupones: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
            empresa: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
            especialidades: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
            name: PropTypes.any,
            productos: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
            set: PropTypes.bool,
        }),
        callcenter: PropTypes.shape({
            administrarconsultas: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
            consultas: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
            name: PropTypes.any,
            set: PropTypes.bool,
        }),
        configuracion: PropTypes.shape({
            name: PropTypes.any,
            perfiles: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
            set: PropTypes.bool,
            sistema: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
            usuarios: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
        }),
        folios: PropTypes.shape({
            folios: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
            name: PropTypes.any,
            set: PropTypes.bool,
        }),
        reportes: PropTypes.shape({
            doctores: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
            name: PropTypes.any,
            set: PropTypes.bool,
            ventas: PropTypes.shape({
                name: PropTypes.any,
                set: PropTypes.bool,
            }),
        }),
    }),
};

export default MenuList;
