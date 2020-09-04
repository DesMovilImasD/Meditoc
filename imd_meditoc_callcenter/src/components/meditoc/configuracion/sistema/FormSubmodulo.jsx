import PropTypes from "prop-types";
import React, { useState } from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, TextField, Button } from "@material-ui/core";
import CGUController from "../../../../controllers/CGUController";
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

    //Consumir servicio para guardar el submodulo en la base
    const funcSaveSubmodulo = async () => {
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
        setFormSubmodulo({
            ...formSubmodulo,
            [e.target.name]: e.target.value,
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
            <Grid container spacing={3}>
                {entSubmodulo.iIdModulo > 0 ? (
                    <Grid item xs={12}>
                        <TextField
                            name="txtIdModulo"
                            label="ID de módulo:"
                            variant="outlined"
                            color="secondary"
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
                            color="secondary"
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
                        color="secondary"
                        autoComplete="off"
                        fullWidth
                        autoFocus
                        value={formSubmodulo.txtNombre}
                        onChange={handleChangeForm}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcSaveSubmodulo}>
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
