import React, { useState, Fragment } from "react";
import {
    Accordion,
    AccordionSummary,
    AccordionDetails,
    IconButton,
    Tooltip,
    Table,
    TableBody,
    Paper,
} from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import ExpandMore from "@material-ui/icons/ExpandMore";
import DeleteIcon from "@material-ui/icons/Delete";
import WebIcon from "@material-ui/icons/Web";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../../../configurations/themeConfig";
import SistemaBoton from "./SistemaBoton";
import EditIcon from "@material-ui/icons/Edit";
import FormSubmodulo from "./FormSubmodulo";
import EliminarSubmodulo from "./EliminarSubmodulo";
import FormBoton from "./FormBoton";

const useStyles = makeStyles({
    backColor: {
        backgroundColor: theme.palette.secondary.main,
    },
    marginAcc: {
        margin: "0px !important",
        borderBottom: `1px solid ${theme.palette.secondary.main}`,
    },
    hideLineAcc: {
        position: "initial",
    },
});

const SistemaSubmodulo = (props) => {
    const { submodulo } = props;

    const classes = useStyles();
    const colorClass = "color-2";

    const entBotonNuevo = {
        iIdModulo: submodulo.iIdModulo,
        iIdSubmodulo: submodulo.iIdSubmodulo,
        iIdBoton: 0,
        sNombre: "",
    };

    const [modalEditarSubmoduloOpen, setModalEditarSubmoduloOpen] = useState(false);
    const [modalEliminarSubmoduloOpen, setModalEliminarSubmoduloOpen] = useState(false);
    const [modalNuevoBotonOpen, setModalNuevoBotonOpen] = useState(false);

    const handleClickEditarSubmodulo = (e) => {
        e.stopPropagation();
        setModalEditarSubmoduloOpen(true);
    };

    const handleClickEliminarSubmodulo = (e) => {
        e.stopPropagation();
        setModalEliminarSubmoduloOpen(true);
    };

    const handleClickNuevoBoton = (e) => {
        e.stopPropagation();
        setModalNuevoBotonOpen(true);
    };

    return (
        <Fragment>
            <Accordion elevation={0} classes={{ root: classes.hideLineAcc }}>
                <AccordionSummary
                    expandIcon={
                        <Tooltip title={`Mostrar/Ocultar botones de ${submodulo.sNombre}`} placement="top-end" arrow>
                            <ExpandMore className={colorClass} />
                        </Tooltip>
                    }
                    classes={{ content: classes.marginAcc }}
                >
                    <div className="align-self-center flx-grw-1">
                        <WebIcon className={colorClass + " vertical-align-middle"} />
                        <span className={colorClass + " size-15"}>
                            {submodulo.sNombre} ({submodulo.iIdSubmodulo})
                        </span>
                    </div>
                    <Tooltip title={`Agregar un botón a ${submodulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickNuevoBoton}>
                            <AddIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                    <Tooltip title={`Editar el submódulo ${submodulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickEditarSubmodulo}>
                            <EditIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                    <Tooltip title={`Eliminar el submódulo ${submodulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickEliminarSubmodulo}>
                            <DeleteIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                </AccordionSummary>
                <AccordionDetails>
                    <div className="acc-content">
                        <Paper elevation={0}>
                            <Table size="small">
                                <TableBody>
                                    {submodulo.lstBotones.map((boton) => (
                                        <SistemaBoton key={boton.iIdBoton} boton={boton} />
                                    ))}
                                </TableBody>
                            </Table>
                        </Paper>
                    </div>
                </AccordionDetails>
            </Accordion>
            <FormSubmodulo
                entSubmodulo={submodulo}
                open={modalEditarSubmoduloOpen}
                setOpen={setModalEditarSubmoduloOpen}
            />
            <EliminarSubmodulo
                entSubmodulo={submodulo}
                open={modalEliminarSubmoduloOpen}
                setOpen={setModalEliminarSubmoduloOpen}
            />
            <FormBoton entBoton={entBotonNuevo} open={modalNuevoBotonOpen} setOpen={setModalNuevoBotonOpen} />
        </Fragment>
    );
};

export default SistemaSubmodulo;
