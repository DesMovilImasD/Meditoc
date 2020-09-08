import React, { Fragment } from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, TextField, MenuItem, IconButton, Tooltip, Divider, Typography } from "@material-ui/core";
import { useState } from "react";
import MeditocTabHeader from "../../../utilidades/MeditocTabHeader";
import MeditocTabBody from "../../../utilidades/MeditocTabBody";
import MeditocTabPanel from "../../../utilidades/MeditocTabPanel";
import theme from "../../../../configurations/themeConfig";
import BackupIcon from "@material-ui/icons/Backup";
import InputTelefono from "../../../utilidades/InputTelefono";
import { DatePicker } from "@material-ui/pickers";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import { rxCorreo } from "../../../../configurations/regexConfig";

const FormColaborador = (props) => {
    const { entColaborador, open, setOpen } = props;

    const [formColaborador, setFormColaborador] = useState({
        txtNombreDirectorio: "",
        txtCedulaProfesional: "",
        txtRFC: "",
        txtTelefonoContacto: "",
        txtCorreoElectronicoContacto: "",
        txtEspecialidad: "",
        txtNumeroSala: "",
        txtDireccionConsultorio: "",
        txtUrlDoctor: "",
        txtNombreDoctor: "",
        txtApellidoPaterno: "",
        txtApellidoMaterno: "",
        txtFechaNacimiento: null,
        txtTelefono: "",
        txtCorreoElectronico: "",
        txtDomicilio: "",
        txtUsuarioTitular: "",
        txtPasswordTitular: "",
        txtUsuarioAdministrativo: "",
        txtPasswordAdministrativo: "",
    });

    const [formColaboradorOK, setFormColaboradorOK] = useState({
        txtNombreDirectorio: true,
        txtCedulaProfesional: true,
        txtRFC: true,
        txtTelefonoContacto: true,
        txtCorreoElectronicoContacto: true,
        txtEspecialidad: true,
        txtNumeroSala: true,
        txtDireccionConsultorio: true,
        txtUrlDoctor: true,
        txtNombreDoctor: true,
        txtApellidoPaterno: true,
        txtApellidoMaterno: true,
        txtFechaNacimiento: true,
        txtTelefono: true,
        txtCorreoElectronico: true,
        txtDomicilio: true,
        txtUsuarioTitular: true,
        txtPasswordTitular: true,
        txtUsuarioAdministrativo: true,
        txtPasswordAdministrativo: true,
    });

    const [tabIndex, setTabIndex] = useState(0);

    const [sclTipoColaborador, setSclTipoColaborador] = useState("1");

    const handleChangeTipoColaborador = (e) => {
        setSclTipoColaborador(e.target.value);
    };

    const handleChangeFormColaborador = (e) => {
        const campoNombre = e.target.name;
        const campoValor = e.target.value;

        switch (campoNombre) {
            case "txtNombreDirectorio":
                if (!formColaboradorOK.txtNombreDirectorio) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtCedulaProfesional":
                if (!formColaboradorOK.txtCedulaProfesional) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtRFC":
                if (!formColaboradorOK.txtRFC) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtTelefonoContacto":
                if (!formColaboradorOK.txtTelefonoContacto) {
                    const telefonoValidacion = campoValor.replace(/ /g, "");
                    if (telefonoValidacion !== "" && telefonoValidacion.length === 10) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtCorreoElectronicoContacto":
                if (!formColaboradorOK.txtCorreoElectronicoContacto) {
                    if (campoValor !== "" && rxCorreo.test(campoValor)) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtEspecialidad":
                if (!formColaboradorOK.txtEspecialidad) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtNumeroSala":
                if (!formColaboradorOK.txtNumeroSala) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtNombreDoctor":
                if (!formColaboradorOK.txtNombreDoctor) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtApellidoPaterno":
                if (!formColaboradorOK.txtApellidoPaterno) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtFechaNacimiento":
                if (!formColaboradorOK.txtFechaNacimiento) {
                    if (campoValor !== "" && campoValor !== null) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }

                break;

            case "txtTelefono":
                if (!formColaboradorOK.txtTelefono) {
                    const telefonoValidacion = campoValor.replace(/ /g, "");
                    if (telefonoValidacion !== "" && telefonoValidacion.length === 10) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtCorreoElectronico":
                if (!formColaboradorOK.txtCorreoElectronico) {
                    if (campoValor !== "" && rxCorreo.test(campoValor)) {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtTipoColaborador":
                if (!formColaboradorOK.txtTipoColaborador) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtUsuarioTitular":
                if (!formColaboradorOK.txtUsuarioTitular) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtPasswordTitular":
                if (!formColaboradorOK.txtPasswordTitular) {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;
            case "txtUsuarioAdministrativo":
                if (!formColaboradorOK.txtUsuarioAdministrativo && sclTipoColaborador === "2") {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            case "txtPasswordAdministrativo":
                if (!formColaboradorOK.txtPasswordAdministrativo && sclTipoColaborador === "2") {
                    if (campoValor !== "") {
                        setFormColaboradorOK({ ...formColaboradorOK, [campoNombre]: true });
                    }
                }
                break;

            default:
                break;
        }

        setFormColaborador({
            ...formColaborador,
            [e.target.name]: e.target.value,
        });
    };

    const handleChangeFechaNacimiento = (date) => {
        if (!formColaboradorOK.txtFechaNacimiento) {
            if (date !== "" && date !== null) {
                setFormColaboradorOK({ ...formColaboradorOK, txtFechaNacimiento: true });
            }
        }

        setFormColaborador({ ...formColaborador, txtFechaNacimiento: date });
    };

    const handleClickGuardarColaborador = () => {
        let formColaboradorOKValidacion = {
            txtNombreDirectorio: true,
            txtCedulaProfesional: true,
            txtRFC: true,
            txtTelefonoContacto: true,
            txtCorreoElectronicoContacto: true,
            txtEspecialidad: true,
            txtNumeroSala: true,
            txtDireccionConsultorio: true,
            txtUrlDoctor: true,
            txtNombreDoctor: true,
            txtApellidoPaterno: true,
            txtApellidoMaterno: true,
            txtFechaNacimiento: true,
            txtTelefono: true,
            txtCorreoElectronico: true,
            txtDomicilio: true,
            txtUsuarioTitular: true,
            txtPasswordTitular: true,
            txtUsuarioAdministrativo: true,
            txtPasswordAdministrativo: true,
        };

        let errorDatosDoctor = false;
        let errorDatosCuenta = false;
        let errorUsuarioPassword = false;

        if (formColaborador.txtNombreDirectorio === "") {
            formColaboradorOKValidacion.txtNombreDirectorio = false;
            errorDatosDoctor = true;
        }

        if (formColaborador.txtCedulaProfesional === "") {
            formColaboradorOKValidacion.txtCedulaProfesional = false;
            errorDatosDoctor = true;
        }
        if (formColaborador.txtRFC === "") {
            formColaboradorOKValidacion.txtRFC = false;
            errorDatosDoctor = true;
        }

        const telefonoValidacion = formColaborador.txtTelefonoContacto.replace(/ /g, "");

        if (telefonoValidacion === "" || telefonoValidacion.length !== 10) {
            formColaboradorOKValidacion.txtTelefonoContacto = false;
            errorDatosDoctor = true;
        }

        if (
            formColaborador.txtCorreoElectronicoContacto === "" ||
            !rxCorreo.test(formColaborador.txtCorreoElectronicoContacto)
        ) {
            formColaboradorOKValidacion.txtCorreoElectronicoContacto = false;
            errorDatosDoctor = true;
        }

        if (formColaborador.txtEspecialidad === "") {
            formColaboradorOKValidacion.txtEspecialidad = false;
            errorDatosDoctor = true;
        }

        if (formColaborador.txtNumeroSala === "") {
            formColaboradorOKValidacion.txtNumeroSala = false;
            errorDatosDoctor = true;
        }

        if (formColaborador.txtNombreDoctor === "") {
            formColaboradorOKValidacion.txtNombreDoctor = false;
            errorDatosCuenta = true;
        }

        if (formColaborador.txtApellidoPaterno === "") {
            formColaboradorOKValidacion.txtApellidoPaterno = false;
            errorDatosCuenta = true;
        }

        if (formColaborador.txtFechaNacimiento === "" || formColaborador.txtFechaNacimiento === null) {
            formColaboradorOKValidacion.txtFechaNacimiento = false;
            errorDatosCuenta = true;
        }

        const telefonoValidacionUsuario = formColaborador.txtTelefono.replace(/ /g, "");

        if (telefonoValidacionUsuario === "" || telefonoValidacionUsuario.length !== 10) {
            formColaboradorOKValidacion.txtTelefono = false;
            errorDatosCuenta = true;
        }

        if (formColaborador.txtCorreoElectronico === "" || !rxCorreo.test(formColaborador.txtCorreoElectronico)) {
            formColaboradorOKValidacion.txtCorreoElectronico = false;
            errorDatosCuenta = true;
        }

        if (formColaborador.txtTipoColaborador === "") {
            formColaboradorOKValidacion.txtTipoColaborador = false;
            errorUsuarioPassword = true;
        }

        if (formColaborador.txtUsuarioTitular === "") {
            formColaboradorOKValidacion.txtUsuarioTitular = false;
            errorUsuarioPassword = true;
        }

        if (formColaborador.txtPasswordTitular === "") {
            formColaboradorOKValidacion.txtPasswordTitular = false;
            errorUsuarioPassword = true;
        }

        if (sclTipoColaborador === "2") {
            if (formColaborador.txtUsuarioAdministrativo === "") {
                formColaboradorOKValidacion.txtUsuarioAdministrativo = false;
                errorUsuarioPassword = true;
            }
            if (formColaborador.txtPasswordAdministrativo === "") {
                formColaboradorOKValidacion.txtPasswordAdministrativo = false;
                errorUsuarioPassword = true;
            }
        }

        setFormColaboradorOK(formColaboradorOKValidacion);

        if (errorUsuarioPassword || errorDatosCuenta || errorDatosDoctor) {
            let regresarTab = 2;
            if (errorUsuarioPassword) {
                regresarTab = 2;
            }

            if (errorDatosCuenta) {
                regresarTab = 1;
            }

            if (errorDatosDoctor) {
                regresarTab = 0;
            }
            setTabIndex(regresarTab);
            return;
        }
    };

    return (
        <MeditocModal size="normal" title="Nuevo colaborador" open={open} setOpen={setOpen}>
            <MeditocTabHeader
                tabs={["DATOS DEL DOCTOR", "DATOS DE CUENTA", "USUARIO Y CONTRASEÑA"]}
                index={tabIndex}
                setIndex={setTabIndex}
            />
            <Grid container spacing={0}>
                <Grid item xs={12}>
                    <MeditocTabBody index={tabIndex} setIndex={setTabIndex}>
                        <MeditocTabPanel id={0} index={tabIndex}>
                            <Grid container spacing={3}>
                                <Grid item xs={12}>
                                    <Typography variant="caption">
                                        Los datos ingresados seran mostrados en el directorio de Médicos de Meditoc.
                                    </Typography>
                                </Grid>
                                <Grid item xs={12}>
                                    <TextField
                                        name="txtNombreDirectorio"
                                        label="Nombre para mostrar en directorio:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtNombreDirectorio}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtNombreDirectorio}
                                        helperText={
                                            !formColaboradorOK.txtNombreDirectorio
                                                ? "El nombre para el directorio médico es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtCedulaProfesional"
                                        label="Cédula profesional:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtCedulaProfesional}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtCedulaProfesional}
                                        helperText={
                                            !formColaboradorOK.txtCedulaProfesional
                                                ? "La cédula profesional es requerida"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtRFC"
                                        label="RFC:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtRFC}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtRFC}
                                        helperText={!formColaboradorOK.txtRFC ? "El RFC es requerido" : ""}
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtTelefonoContacto"
                                        label="Teléfono de contacto:"
                                        variant="outlined"
                                        fullWidth
                                        InputProps={{
                                            inputComponent: InputTelefono,
                                        }}
                                        required
                                        value={formColaborador.txtTelefonoContacto}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtTelefonoContacto}
                                        helperText={
                                            !formColaboradorOK.txtTelefonoContacto
                                                ? "El teléfono de contacto es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtCorreoElectronicoContacto"
                                        label="Correo electrónico de contacto:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtCorreoElectronicoContacto}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtCorreoElectronicoContacto}
                                        helperText={
                                            !formColaboradorOK.txtCorreoElectronicoContacto
                                                ? "El correo electrónico de contacto es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>

                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtEspecialidad"
                                        label="Especialidad:"
                                        variant="outlined"
                                        fullWidth
                                        select
                                        required
                                        value={formColaborador.txtEspecialidad}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtEspecialidad}
                                        helperText={
                                            !formColaboradorOK.txtEspecialidad ? "La especialidad es requerida" : ""
                                        }
                                    >
                                        <MenuItem value="0">Médico general</MenuItem>
                                    </TextField>
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtNumeroSala"
                                        label="Asignar número de sala:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtNumeroSala}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtNumeroSala}
                                        helperText={
                                            !formColaboradorOK.txtNumeroSala
                                                ? "El número de asignación de sala es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextField
                                        name="txtDireccionConsultorio"
                                        label="Dirección del consultorio:"
                                        variant="outlined"
                                        fullWidth
                                        value={formColaborador.txtDireccionConsultorio}
                                        onChange={handleChangeFormColaborador}
                                    />
                                </Grid>
                                <Grid item xs={12}>
                                    <TextField
                                        name="txtUrlDoctor"
                                        label="Sitio web del doctor:"
                                        variant="outlined"
                                        fullWidth
                                        value={formColaborador.txtUrlDoctor}
                                        onChange={handleChangeFormColaborador}
                                    />
                                </Grid>
                                {/* <Grid item xs={12}>
                                    <TextField
                                        name="txtFoto"
                                        label="Foto para mostrar en directorio:"
                                        variant="outlined"
                                        fullWidth
                                        InputProps={{
                                            readOnly: true,
                                            endAdornment: (
                                                <Tooltip title="Subir foto" arrow placement="top">
                                                    <IconButton edge="end">
                                                        <BackupIcon />
                                                    </IconButton>
                                                </Tooltip>
                                            ),
                                        }}
                                    />
                                </Grid> */}
                            </Grid>
                        </MeditocTabPanel>
                        <MeditocTabPanel id={1} index={tabIndex}>
                            <Grid container spacing={3}>
                                <Grid item xs={12}>
                                    <TextField
                                        name="txtNombreDoctor"
                                        label="Nombres:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtNombreDoctor}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtNombreDoctor}
                                        helperText={
                                            !formColaboradorOK.txtNombreDoctor
                                                ? "El nombre del doctor es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtApellidoPaterno"
                                        label="Apellido paterno:"
                                        variant="outlined"
                                        fullWidth
                                        required
                                        value={formColaborador.txtApellidoPaterno}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtApellidoPaterno}
                                        helperText={
                                            !formColaboradorOK.txtApellidoPaterno
                                                ? "El apellido paterno del doctor es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtApellidoMaterno"
                                        label="Apellido materno:"
                                        variant="outlined"
                                        fullWidth
                                        value={formColaborador.txtApellidoMaterno}
                                        onChange={handleChangeFormColaborador}
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <DatePicker
                                        name="txtFechaNacimiento"
                                        disableFuture
                                        label="Fecha de nacimiento:"
                                        inputVariant="outlined"
                                        openTo="year"
                                        format="DD/MM/YYYY"
                                        views={["year", "month", "date"]}
                                        InputAdornmentProps={{ position: "end" }}
                                        fullWidth
                                        required
                                        value={formColaborador.txtFechaNacimiento}
                                        onChange={handleChangeFechaNacimiento}
                                        error={!formColaboradorOK.txtFechaNacimiento}
                                        helperText={
                                            !formColaboradorOK.txtFechaNacimiento
                                                ? "La fecha de nacimiento es requerida"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtTelefono"
                                        label="Teléfono personal:"
                                        variant="outlined"
                                        fullWidth
                                        InputProps={{
                                            inputComponent: InputTelefono,
                                        }}
                                        required
                                        value={formColaborador.txtTelefono}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtTelefono}
                                        helperText={
                                            !formColaboradorOK.txtTelefono ? "El teléfono personal es requerido" : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtCorreoElectronico"
                                        variant="outlined"
                                        label="Correo electrónico personal:"
                                        fullWidth
                                        required
                                        value={formColaborador.txtCorreoElectronico}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtCorreoElectronico}
                                        helperText={
                                            !formColaboradorOK.txtCorreoElectronico
                                                ? "El correo electrónico personal es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        name="txtDomicilio"
                                        variant="outlined"
                                        label="Domicilio particular:"
                                        fullWidth
                                        value={formColaborador.txtDomicilio}
                                        onChange={handleChangeFormColaborador}
                                    />
                                </Grid>
                            </Grid>
                        </MeditocTabPanel>
                        <MeditocTabPanel id={2} index={tabIndex}>
                            <Grid container spacing={3}>
                                <Grid item xs={12}>
                                    <TextField
                                        name="txtTipoColaborador"
                                        label="Tipo de colaborador:"
                                        variant="outlined"
                                        fullWidth
                                        select
                                        required
                                        value={sclTipoColaborador}
                                        onChange={handleChangeTipoColaborador}
                                        error={sclTipoColaborador === ""}
                                        helperText={
                                            sclTipoColaborador === "" ? "El tipo de colaborador es requerido" : ""
                                        }
                                    >
                                        <MenuItem value="1">Médico CallCenter</MenuItem>
                                        <MenuItem value="2">Médico Especialista</MenuItem>
                                    </TextField>
                                </Grid>
                                <Grid item xs={12}>
                                    <span className="rob-nor bold size-15 color-4">USUARIO TITULAR</span>
                                    <Divider />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        variant="outlined"
                                        label="Usuario:"
                                        autoComplete="new-password"
                                        fullWidth
                                        name="txtUsuarioTitular"
                                        required
                                        value={formColaborador.txtUsuarioTitular}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtUsuarioTitular}
                                        helperText={
                                            !formColaboradorOK.txtUsuarioTitular
                                                ? "El usuario titular es requerido"
                                                : ""
                                        }
                                    />
                                </Grid>
                                <Grid item sm={6} xs={12}>
                                    <TextField
                                        variant="outlined"
                                        label="Contraseña:"
                                        type="password"
                                        autoComplete="new-password"
                                        fullWidth
                                        name="txtPasswordTitular"
                                        required
                                        value={formColaborador.txtPasswordTitular}
                                        onChange={handleChangeFormColaborador}
                                        error={!formColaboradorOK.txtPasswordTitular}
                                        helperText={
                                            !formColaboradorOK.txtPasswordTitular
                                                ? "La contraseña titular es requerida"
                                                : ""
                                        }
                                    />
                                </Grid>
                                {sclTipoColaborador === "2" ? (
                                    <Fragment>
                                        <Grid item xs={12}>
                                            <span className="rob-nor bold size-15 color-4">USUARIO ADMINISTRATIVO</span>
                                            <Divider />
                                        </Grid>
                                        <Grid item sm={6} xs={12}>
                                            <TextField
                                                variant="outlined"
                                                label="Usuario:"
                                                autoComplete="new-password"
                                                fullWidth
                                                name="txtUsuarioAdministrativo"
                                                required
                                                value={formColaborador.txtUsuarioAdministrativo}
                                                onChange={handleChangeFormColaborador}
                                                error={!formColaboradorOK.txtUsuarioAdministrativo}
                                                helperText={
                                                    !formColaboradorOK.txtUsuarioAdministrativo
                                                        ? "El usuario administrativo es requerido"
                                                        : ""
                                                }
                                            />
                                        </Grid>
                                        <Grid item sm={6} xs={12}>
                                            <TextField
                                                variant="outlined"
                                                label="Contraseña:"
                                                type="password"
                                                autoComplete="new-password"
                                                fullWidth
                                                name="txtPasswordAdministrativo"
                                                required
                                                value={formColaborador.txtPasswordAdministrativo}
                                                onChange={handleChangeFormColaborador}
                                                error={!formColaboradorOK.txtPasswordAdministrativo}
                                                helperText={
                                                    !formColaboradorOK.txtPasswordAdministrativo
                                                        ? "La contraseña administrativa es requerida"
                                                        : ""
                                                }
                                            />
                                        </Grid>
                                    </Fragment>
                                ) : null}
                            </Grid>
                        </MeditocTabPanel>
                    </MeditocTabBody>
                </Grid>
                <MeditocModalBotones okMessage="Guardar" okFunc={handleClickGuardarColaborador} setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

export default FormColaborador;
