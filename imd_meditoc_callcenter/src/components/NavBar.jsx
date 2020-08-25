import React, { useState } from "react";
import MenuIcon from "@material-ui/icons/Menu";
import PersonIcon from "@material-ui/icons/Person";
import { Button, AppBar, Toolbar, IconButton, Menu, MenuItem, makeStyles, ListItemIcon } from "@material-ui/core";
import VpnKeyIcon from "@material-ui/icons/VpnKey";
import ExitToAppIcon from "@material-ui/icons/ExitToApp";
import theme from "../configurations/themeConfig";
import { imgLogoMeditoc } from "../configurations/imgConfig";

const useStyles = makeStyles({
    root: {
        backgroundColor: theme.palette.primary.main,
        color: "#fff",
    },
});

const NavBar = (props) => {
    const { toggleDrawer } = props;
    const classes = useStyles();

    const [anchorEl, setAnchorEl] = useState(null);

    const isMenuOpen = Boolean(anchorEl);

    const handleProfileMenuOpen = (event) => {
        setAnchorEl(event.currentTarget);
    };

    const handleMenuClose = () => {
        setAnchorEl(null);
    };

    return (
        <div className="flx-grw-1">
            <AppBar position="static" color="inherit" elevation={0}>
                <Toolbar>
                    <IconButton className="size-50" onClick={toggleDrawer(true)}>
                        <MenuIcon className="color-1" />
                    </IconButton>
                    <div className="flx-grw-1">
                        <img className="navbar-img" src={imgLogoMeditoc} alt="logomeditoc" />
                    </div>
                    <PersonIcon className="color-1" />
                    <Button style={{ textTransform: "initial" }} onClick={handleProfileMenuOpen}>
                        <span className="rob-con bold color-1 size-20">Santiago Muñoz</span>
                    </Button>
                    <Menu
                        anchorEl={anchorEl}
                        id="menu-profile"
                        keepMounted
                        open={isMenuOpen}
                        onClose={handleMenuClose}
                        classes={{ paper: classes.root }}
                    >
                        <MenuItem onClick={handleMenuClose}>
                            <ListItemIcon>
                                <VpnKeyIcon className="color-0" />
                            </ListItemIcon>
                            Cambiar contraseña
                        </MenuItem>
                        <MenuItem onClick={handleMenuClose}>
                            <ListItemIcon>
                                <ExitToAppIcon className="color-0" />
                            </ListItemIcon>
                            Cerrar sesión
                        </MenuItem>
                    </Menu>
                </Toolbar>
            </AppBar>
        </div>
    );
};

export default NavBar;
