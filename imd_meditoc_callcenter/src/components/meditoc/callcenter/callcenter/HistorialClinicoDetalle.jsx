import { Grid } from "@material-ui/core";
import React from "react";
import InfoField from "../../../utilidades/InfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";

const HistorialClinicoDetalle = (props) => {
    const { open, setOpen, historial } = props;
    return (
        <MeditocModal title="Detalle de la consulta" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <InfoField label="Peso:" value={historial.fPeso} />
                </Grid>
                <Grid item xs={12}>
                    <InfoField label="Altura:" value={historial.fAltura} />
                </Grid>
                <Grid item xs={12}>
                    <InfoField label="Alergias:" value={historial.sAlergias} />
                </Grid>
                <Grid item xs={12}>
                    <InfoField label="Síntomas:" value={historial.sSintomas} />
                </Grid>
                <Grid item xs={12}>
                    <InfoField label="Diagnóstico:" value={historial.sDiagnostico} />
                </Grid>
                <Grid item xs={12}>
                    <InfoField label="Tratamiento:" value={historial.sTratamiento} />
                </Grid>
                <Grid item xs={12}>
                    <InfoField label="Observaciones y comentarios:" value={historial.sComentarios} />
                </Grid>

                <Grid item xs={12}>
                    <InfoField label="Inicio de consulta:" value={historial.sFechaConsultaInicio} />
                </Grid>
                <Grid item xs={12}>
                    <InfoField label="Fin de consulta:" value={historial.sFechaConsultaFin} />
                </Grid>
                <Grid item xs={12}>
                    <InfoField label="Duración de consulta:" value={historial.sDuracionConsulta} />
                </Grid>
                <MeditocModalBotones hideCancel okMessage="Cerrar detalle" open={open} setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

export default HistorialClinicoDetalle;
