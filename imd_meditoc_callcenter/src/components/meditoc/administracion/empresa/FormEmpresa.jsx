import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, TextField, Button, TextareaAutosize } from "@material-ui/core";
import { useState } from "react";
import { useEffect } from "react";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import { rxCorreo } from "../../../../configurations/regexConfig";
import EmpresaController from "../../../../controllers/EmpresaController";

const FormEmpresa = (props) => {
    const { entEmpresa, open, setOpen, funcGetEmpresas, usuarioSesion, funcLoader, funcAlert } = props;

    const [formEmpresa, setFormEmpresa] = useState({
        txtFolioEmpresa: "",
        txtNombreEmpresa: "",
        txtCorreoEmpresa: "",
    });

    const formularioValidacion = {
        txtFolioEmpresa: true,
        txtNombreEmpresa: true,
        txtCorreoEmpresa: true,
    };

    const [formEmpresaOK, setFormEmpresaOK] = useState(formularioValidacion);

    useEffect(() => {
        setFormEmpresa({
            txtFolioEmpresa: entEmpresa.sFolioEmpresa,
            txtNombreEmpresa: entEmpresa.sNombre,
            txtCorreoEmpresa: entEmpresa.sCorreo,
        });
        setFormEmpresaOK(formularioValidacion);
    }, [entEmpresa]);

    const handleChangeFormEmpresa = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNombreEmpresa":
                if (valorCampo !== "" && !formEmpresaOK.txtNombreEmpresa) {
                    setFormEmpresaOK({ ...formEmpresaOK, [nombreCampo]: true });
                }
                break;

            case "txtCorreoEmpresa":
                if (valorCampo !== "" && rxCorreo.test(valorCampo) && !formEmpresaOK.txtCorreoEmpresa) {
                    setFormEmpresaOK({ ...formEmpresaOK, [nombreCampo]: true });
                }
                break;

            default:
                break;
        }

        setFormEmpresa({
            ...formEmpresa,
            [e.target.name]: e.target.value,
        });
    };

    const handleClickGuardarEmpresa = async () => {
        let bFormError = false;
        let formEmpresaOKValidacion = { ...formularioValidacion };
        if (formEmpresa.txtNombreEmpresa === "") {
            formEmpresaOKValidacion.txtNombreEmpresa = false;
            bFormError = true;
        }
        if (formEmpresa.txtCorreoEmpresa === "" || !rxCorreo.test(formEmpresa.txtCorreoEmpresa)) {
            formEmpresaOKValidacion.txtCorreoEmpresa = false;
            bFormError = true;
        }
        setFormEmpresaOK(formEmpresaOKValidacion);
        if (bFormError) {
            return;
        }

        const entEmpresaSubmit = {
            iIdEmpresa: entEmpresa.iIdEmpresa,
            sNombre: formEmpresa.txtNombreEmpresa,
            sCorreo: formEmpresa.txtCorreoEmpresa,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: true,
            bBaja: false,
        };

        funcLoader(true, "Guardando empresa...");

        const empresaController = new EmpresaController();
        const response = await empresaController.funcSaveEmpresa(entEmpresaSubmit);

        if (response.Code === 0) {
            setOpen(false);
            await funcGetEmpresas();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    return (
        <MeditocModal
            title={entEmpresa.iIdEmpresa === 0 ? "Nueva empresa" : "Editar empresa"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    {entEmpresa.iIdEmpresa > 0 ? (
                        <TextField
                            name="txtFolioEmpresa"
                            label="Folio de empresa:"
                            variant="outlined"
                            disabled
                            fullWidth
                            value={formEmpresa.txtFolioEmpresa}
                        />
                    ) : null}
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="txtNombreEmpresa"
                        label="Nombre de empresa:"
                        autoFocus
                        variant="outlined"
                        fullWidth
                        required
                        value={formEmpresa.txtNombreEmpresa}
                        onChange={handleChangeFormEmpresa}
                        error={!formEmpresaOK.txtNombreEmpresa}
                        helperText={!formEmpresaOK.txtNombreEmpresa ? "El nombre de la empresa es requerido" : ""}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="txtCorreoEmpresa"
                        label="Correo de empresa:"
                        variant="outlined"
                        fullWidth
                        required
                        value={formEmpresa.txtCorreoEmpresa}
                        onChange={handleChangeFormEmpresa}
                        error={!formEmpresaOK.txtCorreoEmpresa}
                        helperText={!formEmpresaOK.txtCorreoEmpresa ? "Ingrese un correo electrónico válido" : ""}
                    />
                </Grid>
                <MeditocModalBotones okMessage="Guardar" okFunc={handleClickGuardarEmpresa} setOpen={setOpen} />
            </Grid>
        </MeditocModal>
    );
};

export default FormEmpresa;
