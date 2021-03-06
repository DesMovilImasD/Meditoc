import {
    Checkbox,
    FormControlLabel,
    Grid,
    IconButton,
    InputAdornment,
    MenuItem,
    TextField,
    Tooltip,
} from "@material-ui/core";
import { blurPrevent, funcPrevent } from "../../../../configurations/preventConfig";

import CGUController from "../../../../controllers/CGUController";
import { DatePicker } from "@material-ui/pickers";
import DateRangeIcon from "@material-ui/icons/DateRange";
import { EnumListPerfilesColaboradores } from "../../../../configurations/enumConfig";
import InfoIcon from "@material-ui/icons/Info";
import MeditocInputPhone from "../../../utilidades/MeditocInputPhone";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";
import React from "react";
import VisibilityIcon from "@material-ui/icons/Visibility";
import VisibilityOffIcon from "@material-ui/icons/VisibilityOff";
import { rxCorreo } from "../../../../configurations/regexConfig";
import { useEffect } from "react";
import { useState } from "react";

/*************************************************************
 * Descripcion: Formulario para registrar o editar un usuario
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: ContentMain
 *************************************************************/
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

    //Objeto para guardar los valores de los inputs del formulario
    const [formUsuario, setFormUsuario] = useState({
        txtNombres: "",
        txtApellidoPaterno: "",
        txtApellidoMaterno: "",
        txtPerfil: "",
        txtTipoCuenta: "",
        txtFechaNacimiento: null,
        txtTelefono: "",
        txtCorreoElectronico: "",
        txtDomicilio: "",
        txtUsuario: "",
        txtPassword: "",
        txtAcceso: true,
    });

    const validacionFormulario = {
        txtNombres: true,
        txtApellidoPaterno: true,
        txtApellidoMaterno: true,
        txtPerfil: true,
        txtTipoCuenta: true,
        txtFechaNacimiento: true,
        txtTelefono: true,
        txtCorreoElectronico: true,
        txtDomicilio: true,
        txtUsuario: true,
        txtPassword: true,
    };

    const [formUsuarioOK, setFormUsuarioOK] = useState(validacionFormulario);

    //Función para capturar los valores de los inputs
    const handleChangeFormulario = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNombres":
                if (!formUsuarioOK.txtNombres) {
                    if (valorCampo !== "") {
                        setFormUsuarioOK({
                            ...formUsuarioOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            case "txtApellidoPaterno":
                if (!formUsuarioOK.txtApellidoPaterno) {
                    if (valorCampo !== "") {
                        setFormUsuarioOK({
                            ...formUsuarioOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            case "txtPerfil":
                if (!formUsuarioOK.txtPerfil) {
                    if (valorCampo !== "" && parseInt(valorCampo) > 0) {
                        setFormUsuarioOK({
                            ...formUsuarioOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            case "txtTelefono":
                if (!formUsuarioOK.txtTelefono) {
                    const telefonoValidacionUsuario = valorCampo.replace(/ /g, "");
                    if (telefonoValidacionUsuario !== "" && telefonoValidacionUsuario.length === 10) {
                        setFormUsuarioOK({
                            ...formUsuarioOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            case "txtCorreoElectronico":
                if (!formUsuarioOK.txtCorreoElectronico) {
                    if (valorCampo !== "" && rxCorreo.test(valorCampo)) {
                        setFormUsuarioOK({
                            ...formUsuarioOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            case "txtUsuario":
                if (!formUsuarioOK.txtUsuario) {
                    if (valorCampo !== "") {
                        setFormUsuarioOK({
                            ...formUsuarioOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            case "txtPassword":
                if (!formUsuarioOK.txtPassword) {
                    if (valorCampo !== "" && entUsuario.iIdUsuario === 0) {
                        setFormUsuarioOK({
                            ...formUsuarioOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            default:
                break;
        }
        setFormUsuario({
            ...formUsuario,
            [nombreCampo]: valorCampo,
        });
    };

    const handleChangeDate = (date) => {
        if (!formUsuarioOK.txtFechaNacimiento) {
            if (date !== "" && date !== null) {
                setFormUsuarioOK({
                    ...formUsuarioOK,
                    txtFechaNacimiento: true,
                });
            }
        }
        setFormUsuario({
            ...formUsuario,
            txtFechaNacimiento: date,
        });
    };

    //Consumir servicio para registrar/editar los datos del usuario en le base
    const funcSaveUsuario = async (e) => {
        funcPrevent(e);
        let formUsuarioOKValidacion = { ...validacionFormulario };

        let formError = false;

        if (formUsuario.txtNombres === "") {
            formUsuarioOKValidacion.txtNombres = false;
            formError = true;
        }

        if (formUsuario.txtApellidoPaterno === "") {
            formUsuarioOKValidacion.txtApellidoPaterno = false;
            formError = true;
        }

        if (formUsuario.txtPerfil === "" || parseInt(formUsuario.txtPerfil) <= 0) {
            formUsuarioOKValidacion.txtPerfil = false;
            formError = true;
        }

        if (formUsuario.txtFechaNacimiento === "" || formUsuario.txtFechaNacimiento === null) {
            formUsuarioOKValidacion.txtFechaNacimiento = false;
            formError = true;
        }

        const telefonoValidacionUsuario = formUsuario.txtTelefono.replace(/ /g, "");

        if (telefonoValidacionUsuario === "" || telefonoValidacionUsuario.length !== 10) {
            formUsuarioOKValidacion.txtTelefono = false;
            formError = true;
        }

        if (formUsuario.txtCorreoElectronico === "" || !rxCorreo.test(formUsuario.txtCorreoElectronico)) {
            formUsuarioOKValidacion.txtCorreoElectronico = false;
            formError = true;
        }

        if (formUsuario.txtUsuario === "") {
            formUsuarioOKValidacion.txtUsuario = false;
            formError = true;
        }

        if (entUsuario.iIdUsuario === 0 && formUsuario.txtPassword === "") {
            formUsuarioOKValidacion.txtPassword = false;
            formError = true;
        }

        setFormUsuarioOK(formUsuarioOKValidacion);

        if (formError === true) {
            return;
        }

        const entSaveUsuario = {
            iIdUsuario: entUsuario.iIdUsuario,
            iIdTipoCuenta: entUsuario.iIdTipoCuenta,
            iIdPerfil: parseInt(formUsuario.txtPerfil),
            sTipoCuenta: entUsuario.sTipoCuenta,
            sPerfil: entUsuario.sPerfil,
            sUsuario: formUsuario.txtUsuario,
            sPassword: formUsuario.txtPassword === "" ? null : formUsuario.txtPassword,
            sNombres: formUsuario.txtNombres,
            sApellidoPaterno: formUsuario.txtApellidoPaterno,
            sApellidoMaterno: formUsuario.txtApellidoMaterno === "" ? null : formUsuario.txtApellidoMaterno,
            dtFechaNacimiento: formUsuario.txtFechaNacimiento.toLocaleDateString(),
            sTelefono: formUsuario.txtTelefono,
            sCorreo: formUsuario.txtCorreoElectronico,
            sDomicilio: formUsuario.txtDomicilio === "" ? null : formUsuario.txtDomicilio,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bAcceso: formUsuario.txtAcceso,
            bActivo: true,
            bBaja: false,
        };

        funcLoader(true, "Guardando usuario...");

        const cguController = new CGUController();
        const response = await cguController.funcSaveUsuario(entSaveUsuario);

        if (response.Code === 0) {
            setOpen(false);
            setUsuarioSeleccionado({
                ...entSaveUsuario,
                sPassword: "",
            });
            await funcGetUsuarios();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
        blurPrevent();
    };

    useEffect(() => {
        setFormUsuario({
            txtNombres: entUsuario.sNombres,
            txtApellidoPaterno: entUsuario.sApellidoPaterno,
            txtApellidoMaterno: entUsuario.sApellidoMaterno,
            txtPerfil: entUsuario.iIdPerfil === 0 ? "" : entUsuario.iIdPerfil,
            txtTipoCuenta: entUsuario.iIdTipoCuenta,
            txtFechaNacimiento: entUsuario.dtFechaNacimiento === null ? null : new Date(entUsuario.dtFechaNacimiento),
            txtTelefono: entUsuario.sTelefono,
            txtCorreoElectronico: entUsuario.sCorreo,
            txtDomicilio: entUsuario.sDomicilio,
            txtUsuario: entUsuario.sUsuario,
            txtPassword: "",
            txtAcceso: entUsuario.bAcceso,
        });
        setFormUsuarioOK(validacionFormulario);
        // eslint-disable-next-line
    }, [entUsuario]);

    const [verPassword, setVerPassword] = useState(false);

    return (
        <MeditocModal
            title={entUsuario.iIdUsuario === 0 ? "Nuevo usuario" : "Editar usuario"}
            size="normal"
            open={open}
            setOpen={setOpen}
        >
            <form id="form-usuario" onSubmit={funcSaveUsuario} noValidate>
                <Grid container spacing={3}>
                    <Grid item sm={12} xs={12}>
                        <TextField
                            variant="outlined"
                            label="Nombres:"
                            fullWidth
                            autoFocus
                            name="txtNombres"
                            value={formUsuario.txtNombres}
                            onChange={handleChangeFormulario}
                            required
                            error={!formUsuarioOK.txtNombres}
                            helperText={!formUsuarioOK.txtNombres ? "El nombre del usuario es requerido" : null}
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
                            required
                            error={!formUsuarioOK.txtApellidoPaterno}
                            helperText={
                                !formUsuarioOK.txtApellidoPaterno
                                    ? "El apellido paterno del usuario es requerido"
                                    : null
                            }
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
                        <TextField
                            id="slcPerfil"
                            labelId="lblPerfil"
                            label="Perfil:"
                            name="txtPerfil"
                            variant="outlined"
                            select
                            fullWidth
                            value={formUsuario.txtPerfil}
                            onChange={handleChangeFormulario}
                            required
                            error={!formUsuarioOK.txtPerfil}
                            helperText={!formUsuarioOK.txtPerfil ? "Seleccione un perfil para el usuario" : null}
                        >
                            {listaPerfiles
                                .filter((perfil) => !EnumListPerfilesColaboradores.includes(perfil.iIdPerfil))
                                .map((perfil) => (
                                    <MenuItem key={perfil.iIdPerfil} value={perfil.iIdPerfil}>
                                        {perfil.sNombre}
                                    </MenuItem>
                                ))}
                        </TextField>
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <DatePicker
                            disableFuture
                            variant="inline"
                            label="Fecha de nacimiento"
                            inputVariant="outlined"
                            openTo="year"
                            format="dd/MM/yyyy"
                            views={["year", "month", "date"]}
                            InputProps={{
                                endAdornment: (
                                    <InputAdornment position="end">
                                        <IconButton>
                                            <DateRangeIcon />
                                        </IconButton>
                                    </InputAdornment>
                                ),
                            }}
                            fullWidth
                            clearable
                            clearLabel="Limpiar"
                            cancelLabel="Cancelar"
                            name="txtFechaNacimiento"
                            value={formUsuario.txtFechaNacimiento}
                            onChange={handleChangeDate}
                            required
                            error={!formUsuarioOK.txtFechaNacimiento}
                            helperText={
                                !formUsuarioOK.txtFechaNacimiento ? "La fecha de nacimiento es requerido" : null
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
                            InputProps={{
                                inputComponent: MeditocInputPhone,
                            }}
                            onChange={handleChangeFormulario}
                            required
                            error={!formUsuarioOK.txtTelefono}
                            helperText={!formUsuarioOK.txtTelefono ? "El teléfono del usuario es requerido" : null}
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
                            required
                            InputProps={{
                                endAdornment: (
                                    <Tooltip
                                        title="El correo proporcionado será usado para la recuperación de las credenciales."
                                        arrow
                                        placement="top"
                                    >
                                        <IconButton>
                                            <InfoIcon />
                                        </IconButton>
                                    </Tooltip>
                                ),
                            }}
                            error={!formUsuarioOK.txtCorreoElectronico}
                            helperText={
                                !formUsuarioOK.txtCorreoElectronico
                                    ? "El correo electrónico del usuario es requerido"
                                    : null
                            }
                        />
                    </Grid>
                    <Grid item xs={12}>
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
                            required
                            error={!formUsuarioOK.txtUsuario}
                            helperText={!formUsuarioOK.txtUsuario ? "El nombre usuario es requerido" : null}
                        />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <TextField
                            variant="outlined"
                            label="Contraseña:"
                            type={verPassword ? "text" : "password"}
                            autoComplete="new-password"
                            fullWidth
                            name="txtPassword"
                            value={formUsuario.txtPassword}
                            onChange={handleChangeFormulario}
                            required
                            error={!formUsuarioOK.txtPassword}
                            helperText={!formUsuarioOK.txtPassword ? "La contrasañe de usuario es requerido" : null}
                            InputProps={{
                                endAdornment: (
                                    <Tooltip
                                        title={verPassword ? "Ocultar contraseña" : "Ver contraseña"}
                                        arrow
                                        placement="top"
                                    >
                                        <IconButton
                                            onMouseDown={() => setVerPassword(true)}
                                            onMouseUp={() => setVerPassword(false)}
                                        >
                                            {verPassword ? <VisibilityIcon /> : <VisibilityOffIcon />}
                                        </IconButton>
                                    </Tooltip>
                                ),
                            }}
                        />
                    </Grid>
                    <Grid item xs={12} className="center">
                        <FormControlLabel
                            control={
                                <Checkbox
                                    checked={formUsuario.txtAcceso}
                                    onChange={(e) => {
                                        setFormUsuario({ ...formUsuario, txtAcceso: e.target.checked });
                                    }}
                                />
                            }
                            label="Dar permisos de acceso al Portal Meditoc CallCenter"
                        />
                    </Grid>
                    <MeditocModalBotones open={open} setOpen={setOpen} okMessage="Guardar" okFunc={funcSaveUsuario} />
                </Grid>
            </form>
        </MeditocModal>
    );
};

FormUsuario.propTypes = {
    entUsuario: PropTypes.shape({
        dtFechaNacimiento: PropTypes.string,
        iIdPerfil: PropTypes.number,
        iIdTipoCuenta: PropTypes.number,
        iIdUsuario: PropTypes.number,
        sApellidoMaterno: PropTypes.string,
        sApellidoPaterno: PropTypes.string,
        sCorreo: PropTypes.string,
        sDomicilio: PropTypes.string,
        sNombres: PropTypes.string,
        sPerfil: PropTypes.string,
        sTelefono: PropTypes.string,
        sTipoCuenta: PropTypes.string,
        sUsuario: PropTypes.string,
    }),
    funcAlert: PropTypes.func,
    funcGetUsuarios: PropTypes.func,
    funcLoader: PropTypes.func,
    listaPerfiles: PropTypes.array,
    open: PropTypes.bool,
    setOpen: PropTypes.func,
    setUsuarioSeleccionado: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default FormUsuario;
