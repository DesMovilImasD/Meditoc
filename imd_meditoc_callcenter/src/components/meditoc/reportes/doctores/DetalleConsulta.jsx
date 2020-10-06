import React, { useEffect } from "react";

import CallCenterController from "../../../../controllers/CallCenterController";
import { Grid } from "@material-ui/core";
import HistorialClinico from "../../callcenter/callcenter/HistorialClinico";
import MeditocInfoField from "../../../utilidades/MeditocInfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";
import PropTypes from "prop-types";
import { useState } from "react";

const DetalleConsulta = (props) => {
    const { iIdConsulta, open, setOpen, funcLoader, funcAlert } = props;

    const callcenterController = new CallCenterController();

    const [entConsulta, setEntConsulta] = useState(null);
    const [lstHistorialClinico, setLstHistorialClinico] = useState(null);

    const funcGetDetalle = async () => {
        funcLoader(true, "Consultando detalles...");

        const response = await callcenterController.funcGetConsulta(iIdConsulta);

        if (response.Code === 0) {
            setEntConsulta(response.Result[0]);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcGetHistorial = async () => {
        funcLoader(true, "Consultando historial clínico...");

        const response = await callcenterController.funcGetHistorial(null, iIdConsulta);

        if (response.Code === 0) {
            setLstHistorialClinico(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcGetData = async () => {
        await funcGetDetalle();
        await funcGetHistorial();
    };

    useEffect(() => {
        if (iIdConsulta !== 0) {
            funcGetData();
        }
        // eslint-disable-next-line
    }, [iIdConsulta]);

    return (
        <MeditocModal title="Detalle de consulta" size="normal" open={open} setOpen={setOpen} level={2}>
            {entConsulta !== null && lstHistorialClinico != null && (
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <MeditocSubtitulo title="DATOS DE CONSULTA" />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="ID de consulta:" value={entConsulta.iIdConsulta} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Estatus:" value={entConsulta.sEstatusConsulta} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField
                            label="Fecha programada de inicio:"
                            value={entConsulta.sFechaProgramadaInicio}
                        />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Fecha programada de fin:" value={entConsulta.sFechaProgramadaFin} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Inicio de consulta:" value={entConsulta.sFechaConsultaInicio} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Fin de consulta:" value={entConsulta.sFechaConsultaFin} />
                    </Grid>
                    <Grid item xs={12}>
                        <MeditocSubtitulo title="DATOS DE HISTORIAL" />
                    </Grid>
                    <Grid item xs={12}>
                        <HistorialClinico lstHistorialClinico={lstHistorialClinico} />
                    </Grid>
                    <Grid item xs={12}>
                        <MeditocSubtitulo title="DATOS DEL PACIENTE" />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Nombre:" value={entConsulta.sNombrePaciente} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Sexo:" value={entConsulta.sSexoPaciente} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Tipo de sangre:" value={entConsulta.sTipoSangrePaciente} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Fecha de nacimiento:" value={entConsulta.sFechaNacimientoPaciente} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Telefono:" value={entConsulta.sTelefonoPaciente} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Correo electrónico:" value={entConsulta.sCorreoPaciente} />
                    </Grid>
                    <Grid item xs={12}>
                        <MeditocSubtitulo title="DATOS DEL FOLIO" />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="ID de folio:" value={entConsulta.iIdFolio} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Folio:" value={entConsulta.sFolio} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Origen:" value={entConsulta.sOrigen} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Fecha de vencimiento:" value={entConsulta.sFechaVencimiento} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Número de orden:" value={entConsulta.sOrdenConekta} />
                    </Grid>
                    <Grid item xs={12}>
                        <MeditocSubtitulo title="DATOS DE EMPRESA/CLIENTE" />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Folio de empresa/cliente:" value={entConsulta.sFolioEmpresa} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Nombre:" value={entConsulta.sNombreEmpresa} />
                    </Grid>
                    <Grid item xs={12}>
                        <MeditocInfoField label="Correo electrónico:" value={entConsulta.sCorreoEmpresa} />
                    </Grid>
                    <Grid item xs={12}>
                        <MeditocSubtitulo title="DATOS DE PRODUCTO" />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Nombre de producto:" value={entConsulta.sNombreProducto} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Tipo de producto:" value={entConsulta.sTipoProducto} />
                    </Grid>
                    <Grid item xs={12}>
                        <MeditocSubtitulo title="DATOS DE COLABORADOR/DOCTOR" />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Nombre:" value={entConsulta.sNombreColaborador} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Correo electrónico:" value={entConsulta.sCorreoColaborador} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Tipo de doctor:" value={entConsulta.sTipoDoctor} />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <MeditocInfoField label="Especialidad:" value={entConsulta.sEspecialidad} />
                    </Grid>
                    <MeditocModalBotones hideCancel okMessage="Cerrar detalle" setOpen={setOpen} />
                </Grid>
            )}
        </MeditocModal>
    );
};

DetalleConsulta.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    iIdConsulta: PropTypes.number,
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default DetalleConsulta;
