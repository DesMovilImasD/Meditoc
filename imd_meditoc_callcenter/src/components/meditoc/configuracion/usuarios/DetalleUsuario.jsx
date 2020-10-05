import { Grid } from "@material-ui/core";
import MeditocInfoField from "../../../utilidades/MeditocInfoField";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";
import React from "react";

const DetalleUsuario = (props) => {
    const { entUsuario, open, setOpen } = props;

    return (
        <MeditocModal title="Detalle de usuario" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="ID de usuario:" value={entUsuario.iIdUsuario} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Usuario Meditoc:" value={entUsuario.sUsuario} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocInfoField label="Nombre:" value={entUsuario.sNombres} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Apellido paterno:" value={entUsuario.sApellidoPaterno} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Apellido materno:" value={entUsuario.sApellidoMaterno} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Fecha de nacimiento:" value={entUsuario.dtFechaNacimiento} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Domicilio:" value={entUsuario.sDomicilio} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Correo electrónico:" value={entUsuario.sCorreo} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Teléfono:" value={entUsuario.sTelefono} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Perfil:" value={entUsuario.sPerfil} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Tipo de cuenta:" value={entUsuario.sTipoCuenta} />
                </Grid>
                <MeditocModalBotones hideCancel okMessage="Cerrar detalle" setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

DetalleUsuario.propTypes = {
    entUsuario: PropTypes.shape({
        dtFechaNacimiento: PropTypes.any,
        iIdUsuario: PropTypes.any,
        sApellidoMaterno: PropTypes.any,
        sApellidoPaterno: PropTypes.any,
        sCorreo: PropTypes.any,
        sDomicilio: PropTypes.any,
        sNombres: PropTypes.any,
        sPerfil: PropTypes.any,
        sTelefono: PropTypes.any,
        sTipoCuenta: PropTypes.any,
        sUsuario: PropTypes.any,
    }),
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default DetalleUsuario;
