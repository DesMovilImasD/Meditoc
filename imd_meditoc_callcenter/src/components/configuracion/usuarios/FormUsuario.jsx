import React from "react";
import ModalForm from "../../ModalForm";
import { Grid, TextField, FormControl, InputLabel, Select, MenuItem, Button } from "@material-ui/core";
import { DatePicker } from "@material-ui/pickers";
import { useState } from "react";
import CGUController from "../../../controllers/CGUController";
import { useEffect } from "react";

const FormUsuario = (props) => {
    const {
        entUsuario,
        listaPerfiles,
        open,
        setOpen,
        setUsuarioSeleccionado,
        funcGetUsuarios,
        usuarioSesion,
        funcLoader,
        funcAlert,
    } = props;

    const cguController = new CGUController();

    const [formUsuario, setFormUsuario] = useState({
        txtNombres: "",
        txtApellidoPaterno: "",
        txtApellidoMaterno: "",
        txtPerfil: "",
        txtTipoCuenta: "",
        txtFechaNacimiento: "",
        txtTelefono: "",
        txtCorreoElectronico: "",
        txtDomicilio: "",
        txtUsuario: "",
        txtPassword: "",
    });

    const handleChangeFormulario = (e) => {
        setFormUsuario({
            ...formUsuario,
            [e.target.name]: e.target.value,
        });
    };

    const handleClickGuardarUsuario = async () => {
        if (formUsuario.txtNombres === "") {
            funcAlert("El nombre del usuario es requerido", "warning");
            return;
        }

        if (formUsuario.txtApellidoPaterno === "") {
            funcAlert("El apellido paterno del usuario es requerido", "warning");
            return;
        }

        if (parseInt(formUsuario.txtPerfil) <= 0) {
            funcAlert("Seleccione un perfil para el usuario", "warning");
            return;
        }

        if (parseInt(formUsuario.txtTipoCuenta) <= 0) {
            funcAlert("Seleccione un tipo de cuenta para el usuario", "warning");
            return;
        }

        if (formUsuario.txtFechaNacimiento === "") {
            funcAlert("La fecha de nacimiento del usuario es requerido", "warning");
            return;
        }

        if (formUsuario.txtTelefono === "") {
            funcAlert("El teléfono del usuario es requerido", "warning");
            return;
        }

        if (formUsuario.txtCorreoElectronico === "") {
            funcAlert("El correo electrónico del usuario es requerido", "warning");
            return;
        }

        if (formUsuario.txtUsuario === "") {
            funcAlert("El nombre de usuario es requerido", "warning");
            return;
        }

        if (entUsuario.iIdUsuario === 0 && (formUsuario.txtPassword === "" || formUsuario.txtPassword.length < 6)) {
            funcAlert("Ingrese una contraseña válida de al menos 6 caracteres", "warning");
            return;
        }

        const entSaveUsuario = {
            iIdUsuario: entUsuario.iIdUsuario,
            iIdTipoCuenta: entUsuario.iIdTipoCuenta,
            iIdPerfil: parseInt(formUsuario.txtPerfil),
            sUsuario: formUsuario.txtUsuario,
            sPassword: formUsuario.txtPassword === "" ? null : formUsuario.txtPassword,
            sNombres: formUsuario.txtNombres,
            sApellidoPaterno: formUsuario.txtApellidoPaterno,
            sApellidoMaterno: formUsuario.txtApellidoMaterno === "" ? null : formUsuario.txtApellidoMaterno,
            dtFechaNacimiento: formUsuario.txtFechaNacimiento,
            sTelefono: formUsuario.txtTelefono,
            sCorreo: formUsuario.txtCorreoElectronico,
            sDomicilio: formUsuario.txtDomicilio === "" ? null : formUsuario.txtDomicilio,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: true,
            bBaja: false,
        };

        funcLoader(true, "Guardando usuario...");

        const response = await cguController.funcSaveUsuario(entSaveUsuario);
        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setOpen(false);
            funcAlert(response.Message, "success");
            setUsuarioSeleccionado({
                ...entSaveUsuario,
                sPassword: "",
            });
            funcGetUsuarios();
        }

        funcLoader();
    };

    const handleClose = () => {
        setOpen(false);
    };

    useEffect(() => {
        setFormUsuario({
            txtNombres: entUsuario.sNombres,
            txtApellidoPaterno: entUsuario.sApellidoPaterno,
            txtApellidoMaterno: entUsuario.sApellidoMaterno,
            txtPerfil: entUsuario.iIdPerfil,
            txtTipoCuenta: entUsuario.iIdTipoCuenta,
            txtFechaNacimiento: entUsuario.dtFechaNacimiento,
            txtTelefono: entUsuario.sTelefono,
            txtCorreoElectronico: entUsuario.sCorreo,
            txtDomicilio: entUsuario.sDomicilio,
            txtUsuario: entUsuario.sUsuario,
            txtPassword: "",
        });
    }, [entUsuario]);

    return (
        <ModalForm
            title={entUsuario.iIdUsuario === 0 ? "Nuevo usuario" : "Editar usuario"}
            size="normal"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                <Grid item sm={12} xs={12}>
                    <TextField
                        variant="outlined"
                        label="Nombres:"
                        fullWidth
                        name="txtNombres"
                        value={formUsuario.txtNombres}
                        onChange={handleChangeFormulario}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        variant="outlined"
                        label="Apellido Paterno:"
                        fullWidth
                        name="txtApellidoPaterno"
                        value={formUsuario.txtApellidoPaterno}
                        onChange={handleChangeFormulario}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        variant="outlined"
                        label="Apellido Materno:"
                        fullWidth
                        name="txtApellidoMaterno"
                        value={formUsuario.txtApellidoMaterno}
                        onChange={handleChangeFormulario}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <FormControl fullWidth variant="outlined">
                        <InputLabel id="lblPerfil">Perfil:</InputLabel>
                        <Select
                            id="slcPerfil"
                            labelId="lblPerfil"
                            label="Perfil:"
                            name="txtPerfil"
                            value={formUsuario.txtPerfil}
                            onChange={handleChangeFormulario}
                        >
                            {listaPerfiles.map((perfil) => (
                                <MenuItem key={perfil.iIdPerfil} value={perfil.iIdPerfil}>
                                    {perfil.sNombre}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <FormControl fullWidth variant="outlined" disabled>
                        <InputLabel id="lblTipoCuenta">Tipo de cuenta:</InputLabel>
                        <Select
                            id="slcTipoCuenta"
                            labelId="lblTipoCuenta"
                            label="Tipo de cuenta:"
                            name="txtTipoCuenta"
                            value={formUsuario.txtTipoCuenta}
                            onChange={handleChangeFormulario}
                        >
                            <MenuItem value={1}>Titular</MenuItem>
                            <MenuItem value={2}>Subcuenta</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <DatePicker
                        disableFuture
                        label="Fecha de nacimiento"
                        inputVariant="outlined"
                        openTo="year"
                        format="dd/MM/yyyy"
                        views={["year", "month", "date"]}
                        InputAdornmentProps={{ position: "end" }}
                        fullWidth
                        name="txtFechaNacimiento"
                        value={formUsuario.txtFechaNacimiento}
                        onChange={(date) =>
                            setFormUsuario({
                                ...formUsuario,
                                txtFechaNacimiento: date.toISOString(),
                            })
                        }
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        variant="outlined"
                        label="Teléfono:"
                        fullWidth
                        name="txtTelefono"
                        value={formUsuario.txtTelefono}
                        onChange={handleChangeFormulario}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        variant="outlined"
                        label="Correo electrónico:"
                        fullWidth
                        name="txtCorreoElectronico"
                        value={formUsuario.txtCorreoElectronico}
                        onChange={handleChangeFormulario}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        variant="outlined"
                        label="Domicilio:"
                        fullWidth
                        name="txtDomicilio"
                        value={formUsuario.txtDomicilio}
                        onChange={handleChangeFormulario}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        variant="outlined"
                        label="Usuario:"
                        autoComplete="new-password"
                        fullWidth
                        name="txtUsuario"
                        value={formUsuario.txtUsuario}
                        onChange={handleChangeFormulario}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        variant="outlined"
                        label="Contraseña:"
                        type="password"
                        autoComplete="new-password"
                        fullWidth
                        name="txtPassword"
                        value={formUsuario.txtPassword}
                        onChange={handleChangeFormulario}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={handleClickGuardarUsuario}>
                        Guardar
                    </Button>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="secondary" fullWidth onClick={handleClose}>
                        Cancelar
                    </Button>
                </Grid>
            </Grid>
        </ModalForm>
    );
};

export default FormUsuario;
