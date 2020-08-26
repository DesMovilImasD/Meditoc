import React, { useState, Fragment } from "react";
import { Accordion, AccordionSummary, AccordionDetails, IconButton, Tooltip } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import ExpandMore from "@material-ui/icons/ExpandMore";
import DeleteIcon from "@material-ui/icons/Delete";
import AccountTreeIcon from "@material-ui/icons/AccountTree";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../../../configurations/themeConfig";
import SistemaSubmodulo from "./SistemaSubmodulo";
import EditIcon from "@material-ui/icons/Edit";
import FormModulo from "./FormModulo";
import EliminarModulo from "./EliminarModulo";
import FormSubmodulo from "./FormSubmodulo";

const useStyles = makeStyles({
    backColor: {
        backgroundColor: theme.palette.primary.main,
    },
    marginAcc: {
        margin: "0px !important",
        borderBottom: `1px solid ${theme.palette.primary.main}`,
    },
    hideLineAcc: {
        position: "initial",
    },
});

const SistemaModulo = (props) => {
    const { modulo } = props;

    const classes = useStyles();
    const colorClass = "color-1";

    const [modalEditarModuloOpen, setModalEditarModuloOpen] = useState(false);
    const [modalEliminarModuloOpen, setModalEliminarModuloOpen] = useState(false);
    const [modalNuevoSubmoduloOpen, setModalNuevoSubmoduloOpen] = useState(false);

    const entSubmoduloNuevo = {
        iIdModulo: modulo.iIdModulo,
        iIdSubmodulo: 0,
        sNombre: "",
    };

    const handleClickNuevoSubmodulo = (e) => {
        e.stopPropagation();
        setModalNuevoSubmoduloOpen(true);
    };

    const handleClickEditar = (e) => {
        e.stopPropagation();
        setModalEditarModuloOpen(true);
    };

    const handleClickEliminar = (e) => {
        e.stopPropagation();
        setModalEliminarModuloOpen(true);
    };

    return (
        <Fragment>
            <Accordion elevation={0} classes={{ root: classes.hideLineAcc }}>
                <AccordionSummary
                    expandIcon={
                        <Tooltip title={`Mostrar/Ocultar submódulos de ${modulo.sNombre}`} placement="top-end" arrow>
                            <ExpandMore className={colorClass} />
                        </Tooltip>
                    }
                    classes={{ content: classes.marginAcc }}
                >
                    <div className="align-self-center flx-grw-1">
                        <AccountTreeIcon className={colorClass + " vertical-align-middle"} />
                        <span className={colorClass + " size-15"}>
                            {modulo.sNombre} ({modulo.iIdModulo})
                        </span>
                    </div>
                    <Tooltip title={`Agregar un submódulo a ${modulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickNuevoSubmodulo}>
                            <AddIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                    <Tooltip title={`Editar el módulo ${modulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickEditar}>
                            <EditIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                    <Tooltip title={`Eliminar el módulo ${modulo.sNombre}`} placement="top" arrow>
                        <IconButton onClick={handleClickEliminar}>
                            <DeleteIcon className={colorClass} />
                        </IconButton>
                    </Tooltip>
                </AccordionSummary>
                <AccordionDetails>
                    <div className="acc-content">
                        {modulo.lstSubmodulos.map((submodulo) => (
                            <SistemaSubmodulo key={submodulo.iIdSubmodulo} submodulo={submodulo} />
                        ))}
                    </div>
                </AccordionDetails>
            </Accordion>
            <FormModulo entModulo={modulo} open={modalEditarModuloOpen} setOpen={setModalEditarModuloOpen} />
            <EliminarModulo entModulo={modulo} open={modalEliminarModuloOpen} setOpen={setModalEliminarModuloOpen} />
            <FormSubmodulo
                entSubmodulo={entSubmoduloNuevo}
                open={modalNuevoSubmoduloOpen}
                setOpen={setModalNuevoSubmoduloOpen}
            />
        </Fragment>
    );
};

export default SistemaModulo;
