import { Grid } from "@material-ui/core";
import React from "react"; //imr
import { useState } from "react";
import MeditocModal from "../../utilidades/MeditocModal";
import MeditocTable from "../../utilidades/MeditocTable";

const DetalleDoctor = (props) => {
    //sfc
    const { entDoctor, open, setOpen } = props;

    const columnas = [
        { title: "ID", field: "iIdConsulta", align: "center" },
        { title: "Consulta inicio", field: "sFechaConsultaInicio", align: "center" },
        { title: "Consulta fin", field: "sFechaConsultaFin", align: "center" },
        { title: "Nombre", field: "name", align: "center" },
        { title: "Estatus de consulta", field: "sEstatusConsulta", align: "center" },
    ];

    const [consultaSeleccionada, setConsultaSeleccionada] = useState({});

    return (
        <MeditocModal title="Detalle de doctor" size="large" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item sm={6} xs={12}>
                    Nombre del doctor: {entDoctor.sNombre}
                    <br />
                    Total de consultas: {entDoctor.iTotalConsultas}
                </Grid>
                <Grid item xs={12}>
                    <MeditocTable
                        columns={columnas}
                        data={entDoctor.lstConsultas.map((consulta) => ({
                            ...consulta,
                            name: consulta.entPaciente.name,
                            sFechaConsultaInicio:
                                consulta.sEstatusConsulta === "Creado/Programado" ||
                                consulta.sEstatusConsulta === "Cancelado"
                                    ? consulta.sFechaProgramadaInicio
                                    : consulta.sFechaConsultaInicio,
                            sFechaConsultaFin:
                                consulta.sEstatusConsulta === "Creado/Programado" ||
                                consulta.sEstatusConsulta === "Cancelado"
                                    ? consulta.sFechaProgramadaFin
                                    : consulta.sEstatusConsulta === "En consulta"
                                    ? "Consultando"
                                    : consulta.sFechaConsultaFin,
                        }))}
                        rowSelected={consultaSeleccionada}
                        setRowSelected={setConsultaSeleccionada}
                        mainField="iIdConsulta"
                    />
                </Grid>
            </Grid>
        </MeditocModal>
    );
};

export default DetalleDoctor;
