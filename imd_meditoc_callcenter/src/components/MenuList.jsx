import React, { useState } from "react";
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
import GradeIcon from "@material-ui/icons/Grade";
import CardMembershipIcon from "@material-ui/icons/CardMembership";

const useStyles = makeStyles({
    nested: {
        paddingLeft: 50,
    },
    link: {
        textDecoration: "none",
        color: "inherit",
    },
});

const MenuList = (props) => {
    const { toggleDrawer } = props;

    const classes = useStyles();

    const [openConfiguracion, setOpenConfiguracion] = useState(false);
    const [openAdministracion, setOpenAdministracion] = useState(false);
    const [openFolios, setOpenFolios] = useState(false);

    return (
        <List component="div">
            <ListItem button onClick={() => setOpenConfiguracion(!openConfiguracion)}>
                <ListItemIcon>
                    <SettingsIcon className="color-0" />
                </ListItemIcon>
                <ListItemText primary="Configuración" />
                {openConfiguracion ? <ExpandLess /> : <ExpandMore />}
            </ListItem>
            <Collapse in={openConfiguracion} unmountOnExit>
                <List component="div">
                    <Link to={urlSystem.configuracion.usuarios} className={classes.link}>
                        <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                            <ListItemIcon>
                                <GroupIcon className="color-0" />
                            </ListItemIcon>
                            <ListItemText primary="Usuarios" />
                        </ListItem>
                    </Link>
                    <Link to={urlSystem.configuracion.perfiles} className={classes.link}>
                        <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                            <ListItemIcon>
                                <VerifiedUserIcon className="color-0" />
                            </ListItemIcon>
                            <ListItemText primary="Perfiles" />
                        </ListItem>
                    </Link>
                    <Link to={urlSystem.configuracion.sistema} className={classes.link}>
                        <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                            <ListItemIcon>
                                <AccountTreeIcon className="color-0" />
                            </ListItemIcon>
                            <ListItemText primary="Sistema" />
                        </ListItem>
                    </Link>
                </List>
            </Collapse>
            <ListItem button onClick={() => setOpenAdministracion(!openAdministracion)}>
                <ListItemIcon>
                    <WorkIcon className="color-0" />
                </ListItemIcon>
                <ListItemText primary="Administración" />
                {openAdministracion ? <ExpandLess /> : <ExpandMore />}
            </ListItem>
            <Collapse in={openAdministracion} unmountOnExit>
                <List component="div">
                    <Link to={urlSystem.administracion.colaboradores} className={classes.link}>
                        <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                            <ListItemIcon>
                                <AssignmentIndIcon className="color-0" />
                            </ListItemIcon>
                            <ListItemText primary="Colaboradores" />
                        </ListItem>
                    </Link>
                    <Link to={urlSystem.administracion.institucion} className={classes.link}>
                        <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                            <ListItemIcon>
                                <BusinessIcon className="color-0" />
                            </ListItemIcon>
                            <ListItemText primary="Institución" />
                        </ListItem>
                    </Link>
                    <Link to={urlSystem.administracion.productos} className={classes.link}>
                        <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                            <ListItemIcon>
                                <ShoppingCartIcon className="color-0" />
                            </ListItemIcon>
                            <ListItemText primary="Productos" />
                        </ListItem>
                    </Link>
                    <Link to={urlSystem.administracion.cupones} className={classes.link}>
                        <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                            <ListItemIcon>
                                <LoyaltyIcon className="color-0" />
                            </ListItemIcon>
                            <ListItemText primary="Cupones" />
                        </ListItem>
                    </Link>
                </List>
            </Collapse>
            <ListItem button onClick={() => setOpenFolios(!openFolios)}>
                <ListItemIcon>
                    <GradeIcon className="color-0" />
                </ListItemIcon>
                <ListItemText primary="Folios" />
                {openFolios ? <ExpandLess /> : <ExpandMore />}
            </ListItem>
            <Collapse in={openFolios} unmountOnExit>
                <List component="div">
                    <Link to={urlSystem.folios.folios} className={classes.link}>
                        <ListItem button onClick={toggleDrawer(false)} className={classes.nested}>
                            <ListItemIcon>
                                <CardMembershipIcon className="color-0" />
                            </ListItemIcon>
                            <ListItemText primary="Folios" />
                        </ListItem>
                    </Link>
                </List>
            </Collapse>
        </List>
    );
};

export default MenuList;
