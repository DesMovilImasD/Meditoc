import React from "react";
import { TableRow, TableCell, Tooltip, IconButton } from "@material-ui/core";
import ExtensionIcon from "@material-ui/icons/Extension";
import DeleteIcon from "@material-ui/icons/Delete";
import { makeStyles } from "@material-ui/core/styles";

const useStyles = makeStyles({
    cell: {
        padding: "0px 24px 0px 16px",
    },
});

const SistemaBoton = (props) => {
    const { boton } = props;

    const classes = useStyles();

    const colorClass = "color-3";

    return (
        <TableRow>
            <TableCell classes={{ sizeSmall: classes.cell }} align="left">
                <ExtensionIcon className={colorClass + " vertical-align-middle"} />
                <span className={colorClass + " size-15"}>{boton.sNombre}</span>
            </TableCell>
            <TableCell classes={{ sizeSmall: classes.cell }} align="right">
                <Tooltip title={`Eliminar el botón ${boton.sNombre}`} placement="top" arrow>
                    <IconButton>
                        <DeleteIcon />
                    </IconButton>
                </Tooltip>
            </TableCell>
        </TableRow>
    );
};

export default SistemaBoton;
