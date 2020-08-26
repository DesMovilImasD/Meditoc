import React, { useState, Fragment } from "react";
import { TableRow, TableCell, Tooltip, IconButton } from "@material-ui/core";
import ExtensionIcon from "@material-ui/icons/Extension";
import DeleteIcon from "@material-ui/icons/Delete";
import { makeStyles } from "@material-ui/core/styles";
import EditIcon from "@material-ui/icons/Edit";
import FormBoton from "./FormBoton";
import EliminarBoton from "./EliminarBoton";

const useStyles = makeStyles({
    cell: {
        padding: "0px 24px 0px 16px",
    },
});

const SistemaBoton = (props) => {
    const { boton } = props;

    const classes = useStyles();
    const colorClass = "color-3";

    const [modalEditarBotonOpen, setModalEditarBotonOpen] = useState(false);
    const [modalEliminarBotonOpen, setModalEliminarBotonOpen] = useState(false);

    const handleClickEditarBoton = (e) => {
        e.stopPropagation();
        setModalEditarBotonOpen(true);
    };

    const handleClickEliminarBoton = (e) => {
        e.stopPropagation();
        setModalEliminarBotonOpen(true);
    };

    return (
        <Fragment>
            <TableRow>
                <TableCell classes={{ sizeSmall: classes.cell }} align="left">
                    <ExtensionIcon className={colorClass + " vertical-align-middle"} />
                    <span className={colorClass + " size-15"}>
                        {boton.sNombre} ({boton.iIdBoton})
                    </span>
                </TableCell>
                <TableCell classes={{ sizeSmall: classes.cell }} align="right">
                    <Tooltip title={`Editar el botón ${boton.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickEditarBoton}>
                            <EditIcon />
                        </IconButton>
                    </Tooltip>
                    <Tooltip title={`Eliminar el botón ${boton.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickEliminarBoton}>
                            <DeleteIcon />
                        </IconButton>
                    </Tooltip>
                </TableCell>
            </TableRow>
            <FormBoton entBoton={boton} open={modalEditarBotonOpen} setOpen={setModalEditarBotonOpen} />
            <EliminarBoton entBoton={boton} open={modalEliminarBotonOpen} setOpen={setModalEliminarBotonOpen} />
        </Fragment>
    );
};

export default SistemaBoton;
