import React from "react";
import { Accordion, AccordionSummary, AccordionDetails, IconButton, Tooltip } from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import ExpandMore from "@material-ui/icons/ExpandMore";
import DeleteIcon from "@material-ui/icons/Delete";
import AccountTreeIcon from "@material-ui/icons/AccountTree";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../../configurations/themeConfig";
import SistemaSubmodulo from "./SistemaSubmodulo";

const useStyles = makeStyles({
    backColor: {
        backgroundColor: theme.palette.primary.main,
    },
    marginAcc: {
        margin: "0px !important",
    },
});

const SistemaModulo = (props) => {
    const { modulo } = props;

    const classes = useStyles();

    const handleClickAdd = (e) => {
        e.stopPropagation();
    };

    const colorClass = "color-1";

    return (
        <Accordion elevation={0}>
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
                    <span className={colorClass + " size-15"}>{modulo.sNombre}</span>
                </div>
                <Tooltip title={`Agregar un submódulo a ${modulo.sNombre}`} placement="top" arrow>
                    <IconButton onClick={handleClickAdd}>
                        <AddIcon className={colorClass} />
                    </IconButton>
                </Tooltip>
                <Tooltip title={`Eliminar el módulo ${modulo.sNombre}`} placement="top" arrow>
                    <IconButton onClick={handleClickAdd}>
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
    );
};

export default SistemaModulo;
