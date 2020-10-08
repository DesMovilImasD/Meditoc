import { Grid, TextField } from "@material-ui/core";
import React, { useState } from "react";
import { blurPrevent, funcPrevent } from "../../../../configurations/preventConfig";

import CGUController from "../../../../controllers/CGUController";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";
import { useEffect } from "react";

/*************************************************************
 * Descripcion: Modal del formulario para Agregar/Modificar un botón
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: SistemaBoton (Modificar), SistemaSubmodulo (Agregar)
 *************************************************************/
const FormBoton = (props) => {
    const {
        entBoton,
        open,
        setOpen,
        usuarioSesion,
        funcGetPermisosXPerfil,
        funcUpdSystemInfo,
        funcLoader,
        funcAlert,
    } = props;

    //Servicios API
    const cguController = new CGUController();

    //Objecto para los inputs del formulario
    const [formBoton, setFormBoton] = useState({
        txtIdModulo: "",
        txtIdSubmodulo: "",
        txtIdBoton: "",
        txtNombre: "",
    });

    const [formBotonOK, setFormBotonOK] = useState({
        txtNombre: true,
    });

    //Consumir servicio para guardar boton en la base
    const funcSaveBoton = async (e) => {
        funcPrevent(e);
        let formBotonOKValidacion = {
            txtNombre: true,
        };

        let formError = false;

        if (formBoton.txtNombre === "") {
            formBotonOKValidacion.txtNombre = false;
            formError = true;
        }

        setFormBotonOK(formBotonOKValidacion);

        if (formError) {
            return;
        }

        funcLoader(true, "Guardando botón...");

        const entSaveBoton = {
            iIdModulo: entBoton.iIdModulo,
            iIdSubModulo: entBoton.iIdSubModulo,
            iIdBoton: entBoton.iIdBoton,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: formBoton.txtNombre,
            bActivo: true,
            bBaja: false,
        };

        const response = await cguController.funcSaveBoton(entSaveBoton);

        if (response.Code === 0) {
            setOpen(false);
            await funcGetPermisosXPerfil();
            await funcUpdSystemInfo();
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
                if (!formBotonOK.txtNombre) {
                    if (valorCampo !== "") {
                        setFormBotonOK({
                            ...formBotonOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            default:
                break;
        }
        setFormBoton({
            ...formBoton,
            [nombreCampo]: valorCampo,
        });
    };

    //Actualizar los valores de los inputs con los datos de entBoton al cargar el componente
    useEffect(() => {
        setFormBoton({
            txtIdModulo: entBoton.iIdModulo,
            txtIdSubmodulo: entBoton.iIdSubModulo,
            txtIdBoton: entBoton.iIdBoton,
            txtNombre: entBoton.sNombre,
        });
        // eslint-disable-next-line
    }, [entBoton]);

    return (
        <MeditocModal
            title={entBoton.iIdBoton === 0 ? "Nuevo botón" : "Editar botón"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <form id="form-boton" onSubmit={funcSaveBoton} noValidate>
                <Grid container spacing={3}>
                    {entBoton.iIdModulo > 0 ? (
                        <Grid item xs={12}>
                            <TextField
                                name="txtIdModulo"
                                label="ID de módulo:"
                                variant="outlined"
                                fullWidth
                                value={formBoton.txtIdModulo}
                                disabled
                            />
                        </Grid>
                    ) : null}

                    {entBoton.iIdSubModulo > 0 ? (
                        <Grid item xs={12}>
                            <TextField
                                name="txtIdSubmodulo"
                                label="ID de submódulo:"
                                variant="outlined"
                                fullWidth
                                value={formBoton.txtIdSubmodulo}
                                disabled
                            />
                        </Grid>
                    ) : null}

                    {entBoton.iIdBoton > 0 ? (
                        <Grid item xs={12}>
                            <TextField
                                name="txtIdBoton"
                                label="ID de botón:"
                                variant="outlined"
                                fullWidth
                                value={formBoton.txtIdBoton}
                                disabled
                            />
                        </Grid>
                    ) : null}

                    <Grid item xs={12}>
                        <TextField
                            name="txtNombre"
                            label="Nombre de botón:"
                            variant="outlined"
                            autoComplete="off"
                            fullWidth
                            autoFocus
                            value={formBoton.txtNombre}
                            onChange={handleChangeForm}
                            error={!formBotonOK.txtNombre}
                            helperText={!formBotonOK.txtNombre ? "El nombre del botón el requerido" : ""}
                        />
                    </Grid>
                    <MeditocModalBotones okMessage="Guardar botón" okFunc={funcSaveBoton} setOpen={setOpen} />
                </Grid>
            </form>
        </MeditocModal>
    );
};

FormBoton.propTypes = {
    entBoton: PropTypes.shape({
        iIdBoton: PropTypes.number,
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

export default FormBoton;
