import React, { useState, useEffect } from "react";
import ModalForm from "../../ModalForm";
import { Grid, TextField, Button } from "@material-ui/core";
import CGUController from "../../../controllers/CGUController";

/*************************************************************
 * Descripcion: Formulario para Agregar/Modificar un perfil
 * Creado: Cristopher Noh
 * Fecha: 27/08/2020
 * Invocado desde: Perfiles
 *************************************************************/
const FormPerfil = (props) => {
    const { entPerfil, open, setOpen, usuarioSesion, funcLoader, funcAlert } = props;

    const cguController = new CGUController();

    const [formPerfil, setFormPerfil] = useState({
        txtIdPerfil: entPerfil.iIdPerfil,
        txtNombre: entPerfil.sNombre,
    });

    const funcSavePerfil = async () => {
        funcLoader(true, "Guardando perfil...");

        const entSavePerfil = {
            iIdPerfil: entPerfil.iIdPerfil,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: formPerfil.txtNombre,
            bActivo: true,
            bBaja: false,
        };

        const response = await cguController.funcSavePerfil(entSavePerfil);
        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setOpen(false);
            funcAlert(response.Message, "success");
        }

        funcLoader();
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleChangeForm = (e) => {
        setFormPerfil({
            ...formPerfil,
            [e.target.name]: e.target.value,
        });
    };

    useEffect(() => {
        setFormPerfil({
            txtIdPerfil: entPerfil.iIdPerfil,
            txtNombre: entPerfil.sNombre,
        });
    }, [entPerfil]);

    return (
        <ModalForm
            title={entPerfil.iIdPerfil === 0 ? "Nuevo perfil" : "Editar perfil"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                {entPerfil.iIdPerfil > 0 ? (
                    <Grid item xs={12}>
                        <TextField
                            name="txtIdPerfil"
                            label="ID de perfil:"
                            variant="outlined"
                            color="secondary"
                            fullWidth
                            value={formPerfil.txtIdPerfil}
                            disabled
                        />
                    </Grid>
                ) : null}

                <Grid item xs={12}>
                    <TextField
                        name="txtNombre"
                        label="Nombre de perfil:"
                        variant="outlined"
                        color="secondary"
                        fullWidth
                        autoFocus
                        value={formPerfil.txtNombre}
                        onChange={handleChangeForm}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcSavePerfil}>
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

export default FormPerfil;
