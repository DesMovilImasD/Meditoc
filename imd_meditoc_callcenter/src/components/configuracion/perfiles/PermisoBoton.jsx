import React, { Fragment, useState } from "react";
import { makeStyles } from "@material-ui/core/styles";
import { TableRow, TableCell, Tooltip, IconButton } from "@material-ui/core";
import ExtensionIcon from "@material-ui/icons/Extension";
import BlockIcon from "@material-ui/icons/Block";
import EliminarPermisoBoton from "./EliminarPermisoBoton";

const useStyles = makeStyles({
    cell: {
        padding: "0px 24px 0px 16px",
    },
});

const PermisoBoton = (props) => {
    const { entPerfil, entBoton, usuarioSesion, funcGetPermisosXPerfil, funcLoader, funcAlert } = props;

    const classes = useStyles();
    const colorClass = "color-3";

    const [modalEliminarBotonOpen, setModalEliminarBotonOpen] = useState(false);

    const handleClickEliminarBoton = () => {
        setModalEliminarBotonOpen(true);
    };

    return (
        <Fragment>
            <TableRow>
                <TableCell classes={{ sizeSmall: classes.cell }} align="left">
                    <ExtensionIcon className={colorClass + " vertical-align-middle"} />
                    <span className={colorClass + " size-15"}>
                        {entBoton.sNombre} ({entBoton.iIdBoton})
                    </span>
                </TableCell>
                <TableCell classes={{ sizeSmall: classes.cell }} align="right">
                    <Tooltip title={`Quitar permiso para el botón ${entBoton.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickEliminarBoton}>
                            <BlockIcon />
                        </IconButton>
                    </Tooltip>
                </TableCell>
            </TableRow>
            <EliminarPermisoBoton
                entPerfil={entPerfil}
                entBoton={entBoton}
                open={modalEliminarBotonOpen}
                setOpen={setModalEliminarBotonOpen}
                funcGetPermisosXPerfil={funcGetPermisosXPerfil}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

export default PermisoBoton;
