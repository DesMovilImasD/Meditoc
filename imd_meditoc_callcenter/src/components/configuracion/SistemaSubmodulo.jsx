import React from "react";
import {
    Accordion,
    AccordionSummary,
    AccordionDetails,
    IconButton,
    Tooltip,
    Table,
    TableRow,
    TableCell,
    TableBody,
    Paper,
} from "@material-ui/core";
import AddIcon from "@material-ui/icons/Add";
import ExpandMore from "@material-ui/icons/ExpandMore";
import DeleteIcon from "@material-ui/icons/Delete";
import WebIcon from "@material-ui/icons/Web";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../../configurations/themeConfig";
import SistemaBoton from "./SistemaBoton";

const useStyles = makeStyles({
    backColor: {
        backgroundColor: theme.palette.secondary.main,
    },
    marginAcc: {
        margin: "0px !important",
    },
});

const SistemaSubmodulo = (props) => {
    const { submodulo } = props;

    const classes = useStyles();

    const handleClickAdd = (e) => {
        e.stopPropagation();
    };

    const colorClass = "color-2";

    return (
        <Accordion elevation={0}>
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
                    <span className={colorClass + " size-15"}>{submodulo.sNombre}</span>
                </div>
                <Tooltip title={`Agregar un botón a ${submodulo.sNombre}`} placement="top" arrow>
                    <IconButton onClick={handleClickAdd}>
                        <AddIcon className={colorClass} />
                    </IconButton>
                </Tooltip>
                <Tooltip title={`Eliminar el submódulo ${submodulo.sNombre}`} placement="top" arrow>
                    <IconButton onClick={handleClickAdd}>
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
    );
};

export default SistemaSubmodulo;
