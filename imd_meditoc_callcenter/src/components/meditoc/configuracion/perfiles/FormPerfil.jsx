import { Grid, TextField } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import { blurPrevent, funcPrevent } from "../../../../configurations/preventConfig";

import CGUController from "../../../../controllers/CGUController";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";

/*************************************************************
 * Descripcion: Formulario para Agregar/Modificar un perfil
 * Creado: Cristopher Noh
 * Fecha: 27/08/2020
 * Invocado desde: Perfiles
 *************************************************************/
const FormPerfil = (props) => {
    const { entPerfil, open, setOpen, funcGetPerfiles, usuarioSesion, funcLoader, funcAlert } = props;

    //Objeto para guardar los valores de los inputs de formulario
    const [formPerfil, setFormPerfil] = useState({ txtIdPerfil: "", txtNombre: "" });

    const [formPerfilOK, setFormPerfilOK] = useState({ txtIdPerfil: true, txtNombre: true });

    //Actualizar los inputs del formulario con lo datos de entPerfil al cargar el componente
    useEffect(() => {
        setFormPerfil({
            txtIdPerfil: entPerfil.iIdPerfil,
            txtNombre: entPerfil.sNombre,
        });
        // eslint-disable-next-line
    }, [entPerfil]);

    //Funcion para capturar los valores de los inputs
    const handleChangeForm = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNombre":
                if (!formPerfilOK.txtNombre) {
                    if (valorCampo !== "") {
                        setFormPerfilOK({ ...formPerfilOK, [nombreCampo]: true });
                    }
                }
                break;

            default:
                break;
        }
        setFormPerfil({
            ...formPerfil,
            [e.target.name]: e.target.value,
        });
    };

    //Consumir servicio para guardar el perfil nuevo en la base
    const funcSavePerfil = async (e) => {
        funcPrevent(e);
        let formPerfilOKValidacion = { txtIdPerfil: true, txtNombre: true };

        let formError = false;

        if (formPerfil.txtNombre === "") {
            formPerfilOKValidacion.txtNombre = false;
            formError = true;
        }

        setFormPerfilOK(formPerfilOKValidacion);

        if (formError) {
            return;
        }

        funcLoader(true, "Guardando perfil...");

        const entSavePerfil = {
            iIdPerfil: entPerfil.iIdPerfil,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: formPerfil.txtNombre,
            bActivo: true,
            bBaja: false,
        };

        const cguController = new CGUController();
        const response = await cguController.funcSavePerfil(entSavePerfil);

        if (response.Code === 0) {
            setOpen(false);
            await funcGetPerfiles();

            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
        blurPrevent();
    };

    return (
        <MeditocModal
            title={entPerfil.iIdPerfil === 0 ? "Nuevo perfil" : "Editar perfil"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <form id="form-perfil" onSubmit={funcSavePerfil} noValidate>
                <Grid container spacing={3}>
                    {entPerfil.iIdPerfil > 0 ? (
                        <Grid item xs={12}>
                            <TextField
                                name="txtIdPerfil"
                                label="ID de perfil:"
                                variant="outlined"
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
                            fullWidth
                            autoFocus
                            value={formPerfil.txtNombre}
                            onChange={handleChangeForm}
                            error={!formPerfilOK.txtNombre}
                            helperText={!formPerfilOK.txtNombre ? "Ingrese un nombre para el perfil" : ""}
                        />
                    </Grid>
                    <MeditocModalBotones okMessage="Guardar" okFunc={funcSavePerfil} setOpen={setOpen} />
                </Grid>
            </form>
        </MeditocModal>
    );
};

FormPerfil.propTypes = {
    entPerfil: PropTypes.shape({
        iIdPerfil: PropTypes.number,
        sNombre: PropTypes.string,
    }),
    funcAlert: PropTypes.func,
    funcGetPerfiles: PropTypes.func,
    funcLoader: PropTypes.func,
    open: PropTypes.bool,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default FormPerfil;
