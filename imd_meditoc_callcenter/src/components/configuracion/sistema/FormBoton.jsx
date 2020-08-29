import PropTypes from "prop-types";
import React, { useState } from "react";
import ModalForm from "../../ModalForm";
import { Grid, TextField, Button } from "@material-ui/core";
import CGUController from "../../../controllers/CGUController";
import { useEffect } from "react";

/*************************************************************
 * Descripcion: Modal del formulario para Agregar/Modificar un botón
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: SistemaBoton (Modificar), SistemaSubmodulo (Agregar)
 *************************************************************/
const FormBoton = (props) => {
    const { entBoton, open, setOpen, usuarioSesion, funcGetPermisosXPerfil, funcLoader, funcAlert } = props;

    //Servicios API
    const cguController = new CGUController();

    //Objecto para los inputs del formulario
    const [formBoton, setFormBoton] = useState({
        txtIdModulo: "",
        txtIdSubmodulo: "",
        txtIdBoton: "",
        txtNombre: "",
    });

    //Consumir servicio para guardar boton en la base
    const funcSaveBoton = async () => {
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
        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setOpen(false);
            funcAlert(response.Message, "success");
            funcGetPermisosXPerfil();
        }

        funcLoader();
    };

    //Funcion para cerrar el formulario
    const handleClose = () => {
        setOpen(false);
    };

    //Funcion para capturar los valores de los inputs
    const handleChangeForm = (e) => {
        setFormBoton({
            ...formBoton,
            [e.target.name]: e.target.value,
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
        <ModalForm
            title={entBoton.iIdBoton === 0 ? "Nuevo botón" : "Editar botón"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                {entBoton.iIdModulo > 0 ? (
                    <Grid item xs={12}>
                        <TextField
                            name="txtIdModulo"
                            label="ID de módulo:"
                            variant="outlined"
                            color="secondary"
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
                            color="secondary"
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
                            color="secondary"
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
                        color="secondary"
                        autoComplete="off"
                        fullWidth
                        autoFocus
                        value={formBoton.txtNombre}
                        onChange={handleChangeForm}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcSaveBoton}>
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
