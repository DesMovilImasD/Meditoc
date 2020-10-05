import { Grid, TextField } from "@material-ui/core";
import React, { useState } from "react";
import { blurPrevent, funcPrevent } from "../../../../configurations/preventConfig";

import CGUController from "../../../../controllers/CGUController";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";
import { useEffect } from "react";

/*************************************************************
 * Descripcion: Modal del formulario para Agregar/Modificar un submódulo
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: SistemaSubmodulo (Modificar), SistemaModulo (Agregar)
 *************************************************************/
const FormSubmodulo = (props) => {
    const { entSubmodulo, open, setOpen, usuarioSesion, funcGetPermisosXPerfil, funcLoader, funcAlert } = props;

    //Servicios API
    const cguController = new CGUController();

    //Objeto para guardar los valores de los inputs
    const [formSubmodulo, setFormSubmodulo] = useState({
        txtIdModulo: "",
        txtIdSubmodulo: "",
        txtNombre: "",
    });

    const [formSubmoduloOK, setFormSubmoduloOK] = useState({
        txtNombre: true,
    });

    //Consumir servicio para guardar el submodulo en la base
    const funcSaveSubmodulo = async (e) => {
        funcPrevent(e);
        let formSubmoduloOKValidacion = {
            txtNombre: true,
        };

        let formError = false;

        if (formSubmodulo.txtNombre === "") {
            formSubmoduloOKValidacion.txtNombre = false;
            formError = true;
        }

        setFormSubmoduloOK(formSubmoduloOKValidacion);

        if (formError) {
            return;
        }

        funcLoader(true, "Guardando submódulo...");

        const entSaveSubmodulo = {
            iIdModulo: entSubmodulo.iIdModulo,
            iIdSubModulo: entSubmodulo.iIdSubModulo,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: formSubmodulo.txtNombre,
            bActivo: true,
            bBaja: false,
        };

        const response = await cguController.funcSaveSubmodulo(entSaveSubmodulo);

        if (response.Code === 0) {
            setOpen(false);
            await funcGetPermisosXPerfil();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
        blurPrevent();
    };

    //Funcion para capturar los valores de los inputs
    const handleChangeForm = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNombre":
                if (!formSubmoduloOK.txtNombre) {
                    if (valorCampo !== "") {
                        setFormSubmoduloOK({
                            ...formSubmoduloOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            default:
                break;
        }

        setFormSubmodulo({
            ...formSubmodulo,
            [nombreCampo]: valorCampo,
        });
    };

    //Actualizar los valores de los inputs con los datos de entSubmodulo al cargar el componente
    useEffect(() => {
        setFormSubmodulo({
            txtIdModulo: entSubmodulo.iIdModulo,
            txtIdSubmodulo: entSubmodulo.iIdSubModulo,
            txtNombre: entSubmodulo.sNombre,
        });
        // eslint-disable-next-line
    }, [entSubmodulo]);

    return (
        <MeditocModal
            title={entSubmodulo.iIdSubModulo === 0 ? "Nuevo submódulo" : "Editar submódulo"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <form id="form-submodulo" onSubmit={funcSaveSubmodulo} noValidate>
                <Grid container spacing={3}>
                    {entSubmodulo.iIdModulo > 0 ? (
                        <Grid item xs={12}>
                            <TextField
                                name="txtIdModulo"
                                label="ID de módulo:"
                                variant="outlined"
                                fullWidth
                                value={formSubmodulo.txtIdModulo}
                                disabled
                            />
                        </Grid>
                    ) : null}

                    {entSubmodulo.iIdSubModulo > 0 ? (
                        <Grid item xs={12}>
                            <TextField
                                name="txtIdSubmodulo"
                                label="ID de submódulo:"
                                variant="outlined"
                                fullWidth
                                value={formSubmodulo.txtIdSubmodulo}
                                disabled
                            />
                        </Grid>
                    ) : null}

                    <Grid item xs={12}>
                        <TextField
                            name="txtNombre"
                            label="Nombre de submódulo:"
                            variant="outlined"
                            autoComplete="off"
                            fullWidth
                            autoFocus
                            value={formSubmodulo.txtNombre}
                            onChange={handleChangeForm}
                            error={!formSubmoduloOK.txtNombre}
                            helperText={!formSubmoduloOK.txtNombre ? "El nombre del submódulo es requerido" : ""}
                        />
                    </Grid>
                    <MeditocModalBotones setOpen={setOpen} okMessage="Guardar submódulo" okFunc={funcSaveSubmodulo} />
                </Grid>
            </form>
        </MeditocModal>
    );
};

FormSubmodulo.propTypes = {
    entSubmodulo: PropTypes.shape({
        iIdModulo: PropTypes.number,
        iIdSubModulo: PropTypes.number,
        sNombre: PropTypes.string,
    }),
    funcAlert: PropTypes.func,
    funcGetPermisosXPerfil: PropTypes.func,
    funcLoader: PropTypes.func,
    open: PropTypes.bool,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.number,
    }),
};

export default FormSubmodulo;
