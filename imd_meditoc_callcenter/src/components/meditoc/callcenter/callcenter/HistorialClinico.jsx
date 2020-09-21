import { Button, Grid, IconButton } from "@material-ui/core";
import React, { Fragment } from "react";
import { useState } from "react";
import MeditocTable from "../../../utilidades/MeditocTable";
import VisibilityIcon from "@material-ui/icons/Visibility";
import HistorialClinicoDetalle from "./HistorialClinicoDetalle";

const HistorialClinico = (props) => {
    const { entCallCenter } = props;
    const columns = [
        { title: "ID", field: "iIdHistorialClinico", align: "center", hidden: true },
        // { title: "Síntomas", field: "sSintomas", align: "center" },
        // { title: "Diagnótico", field: "sDiagnostico", align: "center" },
        // { title: "Tratamiento", field: "sTratamiento", align: "center" },
        { title: "Fecha", field: "sFechaCreacion", align: "center" },
        { title: "Duración", field: "sDuracionConsulta", align: "center" },
        { title: "Detalle", field: "sDetalle", align: "center" },
    ];

    const [historialSeleccionado, setHistorialSeleccionado] = useState({ iIdHistorialClinico: 0 });

    const [historialClinicoDetalle, setHistorialClinicoDetalle] = useState({ iIdHistorialClinico: 0 });

    const [modalHistorialDetalleOpen, setModalHistorialDetalleOpen] = useState(false);

    const handleClickDetalle = (iIdHistorialClinico) => {
        setHistorialClinicoDetalle(
            entCallCenter.lstHistorialClinico.find((x) => x.iIdHistorialClinico === iIdHistorialClinico)
        );
        setModalHistorialDetalleOpen(true);
    };

    return (
        <Fragment>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <MeditocTable
                        columns={columns}
                        data={entCallCenter.lstHistorialClinico.map((historia) => ({
                            ...historia,
                            sDetalle: (
                                <Button
                                    variant="contained"
                                    color="primary"
                                    onClick={() => handleClickDetalle(historia.iIdHistorialClinico)}
                                >
                                    Ver detalle
                                </Button>
                                // <IconButton onClick={() => handleClickDetalle(historia.iIdHistorialClinico)}>
                                //     <VisibilityIcon className="color-2" />
                                // </IconButton>
                            ),
                        }))}
                        rowSelected={historialSeleccionado}
                        setRowSelected={setHistorialSeleccionado}
                        mainField="iIdHistorialClinico"
                    />
                </Grid>
            </Grid>
            <HistorialClinicoDetalle
                open={modalHistorialDetalleOpen}
                setOpen={setModalHistorialDetalleOpen}
                historial={historialClinicoDetalle}
            />
        </Fragment>
    );
};

export default HistorialClinico;
