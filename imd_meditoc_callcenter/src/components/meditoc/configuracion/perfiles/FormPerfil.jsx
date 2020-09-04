import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, TextField, Button } from "@material-ui/core";
import CGUController from "../../../../controllers/CGUController";

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

    //Actualizar los inputs del formulario con lo datos de entPerfil al cargar el componente
    useEffect(() => {
        setFormPerfil({
            txtIdPerfil: entPerfil.iIdPerfil,
            txtNombre: entPerfil.sNombre,
        });
    }, [entPerfil]);

    //Funcion para cerrar el formulario
    const handleClose = () => {
        setOpen(false);
    };

    //Funcion para capturar los valores de los inputs
    const handleChangeForm = (e) => {
        setFormPerfil({
            ...formPerfil,
            [e.target.name]: e.target.value,
        });
    };

    //Consumir servicio para guardar el perfil nuevo en la base
    const funcSavePerfil = async () => {
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

        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setOpen(false);
            funcAlert(response.Message, "success");
            funcGetPerfiles();
        }

        funcLoader();
    };

    return (
        <MeditocModal
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
