import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, TextField, Button, TextareaAutosize } from "@material-ui/core";
import { useState } from "react";
import { useEffect } from "react";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import { rxCorreo } from "../../../../configurations/regexConfig";

const FormEmpresa = (props) => {
    const { entEmpresa, open, setOpen } = props;

    const [formEmpresa, setFormEmpresa] = useState({
        txtFolioEmpresa: "",
        txtNombreEmpresa: "",
        txtCorreoEmpresa: "",
    });

    const [formEmpresaOK, setFormEmpresaOK] = useState({
        txtFolioEmpresa: true,
        txtNombreEmpresa: true,
        txtCorreoEmpresa: true,
    });

    useEffect(() => {
        setFormEmpresa({
            txtFolioEmpresa: entEmpresa.sFolioEmpresa,
            txtNombreEmpresa: entEmpresa.sNombre,
            txtCorreoEmpresa: entEmpresa.sCorreo,
        });
    }, [entEmpresa]);

    const handleChangeFormEmpresa = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNombreEmpresa":
                if (valorCampo != "" && !formEmpresaOK.txtNombreEmpresa) {
                    setFormEmpresaOK({ ...formEmpresaOK, [nombreCampo]: true });
                }
                break;

            case "txtCorreoEmpresa":
                if (valorCampo != "" && rxCorreo.test(valorCampo) && !formEmpresaOK.txtCorreoEmpresa) {
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

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

    const handleClickGuardarEmpresa = () => {
        let bFormError = false;
        let formEmpresaOKValidacion = {
            txtFolioEmpresa: true,
            txtNombreEmpresa: true,
            txtCorreoEmpresa: true,
        };
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
                <MeditocModalBotones okMessage="Guardar" okFunc={handleClickGuardarEmpresa} cancelFunc={handleClose} />
            </Grid>
        </MeditocModal>
    );
};

export default FormEmpresa;
