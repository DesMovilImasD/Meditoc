import { Grid } from "@material-ui/core";
import MeditocInfoField from "../../../utilidades/MeditocInfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";
import PropTypes from "prop-types";
import React from "react";

/*************************************************************
 * Descripcion: Modal para visualizar el detalle de un colaborador seleccionado
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: Colaboradores
 *************************************************************/
const DetalleColaborador = (props) => {
    //PROPS
    const { entColaborador, open, setOpen } = props;

    return (
        <MeditocModal title="Detalle de colaborador" size="normal" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DEL DIRECTORIO MÉDICO" />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Nombre:" value={entColaborador.sNombreDirectorio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Cédula profesional:" value={entColaborador.sCedulaProfecional} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="RFC:" value={entColaborador.sRFC} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Teléfono de contacto:" value={entColaborador.sTelefonoDirectorio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="WhatsApp:" value={entColaborador.sWhatsApp} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Correo electrónico:" value={entColaborador.sCorreoDirectorio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Especialidad:" value={entColaborador.sEspecialidad} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Número de sala IceLink:" value={entColaborador.iNumSala} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Dirección de consultorio:" value={entColaborador.sDireccionConsultorio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Nombre de consultorio:" value={entColaborador.sNombreConsultorio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Sitio Web:" value={entColaborador.sURL} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Mapa:" value={entColaborador.sMaps} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DE CUENTA" />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Nombres:" value={entColaborador.sNombresDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Apellido paterno:" value={entColaborador.sApellidoPaternoDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Apellido materno:" value={entColaborador.sApellidoMaternoDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Fecha de nacimiento:" value={entColaborador.sFechaNacimientoDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Teléfono:" value={entColaborador.sTelefonoDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Correo electrónico:" value={entColaborador.sCorreoDoctor} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Domicilio particular:" value={entColaborador.sDomicilioDoctor} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="USUARIOS" />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Titular:" value={entColaborador.sUsuarioTitular} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Administrativo:" value={entColaborador.sUsuarioAdministrativo} />
                </Grid>
                <MeditocModalBotones okMessage="Cerrar detalle" hideCancel setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

DetalleColaborador.propTypes = {
    entColaborador: PropTypes.shape({
        iNumSala: PropTypes.number,
        sApellidoMaternoDoctor: PropTypes.string,
        sApellidoPaternoDoctor: PropTypes.string,
        sCedulaProfecional: PropTypes.string,
        sCorreoDirectorio: PropTypes.string,
        sCorreoDoctor: PropTypes.string,
        sDireccionConsultorio: PropTypes.string,
        sDomicilioDoctor: PropTypes.string,
        sEspecialidad: PropTypes.string,
        sFechaNacimientoDoctor: PropTypes.string,
        sMaps: PropTypes.string,
        sNombreConsultorio: PropTypes.string,
        sNombreDirectorio: PropTypes.string,
        sNombresDoctor: PropTypes.string,
        sRFC: PropTypes.string,
        sTelefonoDirectorio: PropTypes.string,
        sTelefonoDoctor: PropTypes.string,
        sURL: PropTypes.string,
        sUsuarioAdministrativo: PropTypes.string,
        sUsuarioTitular: PropTypes.string,
        sWhatsApp: PropTypes.string,
    }),
    open: PropTypes.bool,
    setOpen: PropTypes.func,
};

export default DetalleColaborador;
