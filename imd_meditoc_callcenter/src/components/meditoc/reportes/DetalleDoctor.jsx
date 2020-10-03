import PropTypes from "prop-types";
import { Button, Grid } from "@material-ui/core";
import React, { useState } from "react";
import MeditocModal from "../../utilidades/MeditocModal";
import MeditocTable from "../../utilidades/MeditocTable";
import InfoField from "../../utilidades/InfoField";
import MeditocModalBotones from "../../utilidades/MeditocModalBotones";
import DetalleDoctorConsulta from "./DetalleDoctorConsulta";

const DetalleDoctor = (props) => {
    const { entDoctor, open, setOpen, funcLoader, funcAlert } = props;

    const columnas = [
        { title: "ID", field: "iIdConsulta", align: "center" },
        {
            title: "Consulta inicio",
            field: "sFechaConsultaInicio",
            align: "center",
        },
        { title: "Consulta fin", field: "sFechaConsultaFin", align: "center" },
        { title: "Nombre", field: "name", align: "center" },
        {
            title: "Estatus de consulta",
            field: "sEstatusConsulta",
            align: "center",
        },
        { title: "Detalle", field: "sDetalle", align: "center" },
    ];

    const [modalDetalleConsultaOpen, setModalDetalleConsultaOpen] = useState(false);
    const [iIdConsulta, setIIdConsulta] = useState(0);

    const handleClickDetalleConsulta = (id) => {
        setIIdConsulta(id);
        setModalDetalleConsultaOpen(true);
    };

    return (
        <MeditocModal title="Detalle de doctor" size="large" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item sm={4} xs={12}>
                    <InfoField label="Nombre del Doctor:" value={entDoctor.sNombre} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <InfoField label="Tipo de Doctor:" value={entDoctor.sTipoDoctor} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <InfoField label="Especialidad:" value={entDoctor.sEspecialidad} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <InfoField label="Teléfono:" value={entDoctor.sTelefono} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <InfoField label="Correo Electrónico:" value={entDoctor.sCorreo} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <InfoField label="Dirección de Consultorio:" value={entDoctor.sDireccionConsultorio} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <InfoField label="Número de Sala Icelink:" value={entDoctor.iNumSala} />
                </Grid>
                <Grid item sm={4} xs={12}>
                    <InfoField label="Total de consultas:" value={entDoctor.iTotalConsultas} />
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
                            sDetalle: (
                                <Button
                                    variant="contained"
                                    color="primary"
                                    onClick={() => handleClickDetalleConsulta(consulta.iIdConsulta)}
                                >
                                    DETALLE
                                </Button>
                            ),
                        }))}
                        rowClick={false}
                        mainField="iIdConsulta"
                    />
                </Grid>
                <MeditocModalBotones cancelMessage="Cerrar detalle de doctor" setOpen={setOpen} hideOk />
            </Grid>
            <DetalleDoctorConsulta
                iIdConsulta={iIdConsulta}
                open={modalDetalleConsultaOpen}
                setOpen={setModalDetalleConsultaOpen}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </MeditocModal>
    );
};

DetalleDoctor.propTypes = {
    entDoctor: PropTypes.shape({
        iNumSala: PropTypes.any,
        iTotalConsultas: PropTypes.any,
        lstConsultas: PropTypes.shape({
            map: PropTypes.func,
        }),
        sCorreo: PropTypes.any,
        sDireccionConsultorio: PropTypes.any,
        sEspecialidad: PropTypes.any,
        sNombre: PropTypes.any,
        sTelefono: PropTypes.any,
        sTipoDoctor: PropTypes.any,
    }),
    funcAlert: PropTypes.any,
    funcLoader: PropTypes.any,
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default DetalleDoctor;
