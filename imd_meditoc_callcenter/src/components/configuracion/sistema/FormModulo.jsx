import PropTypes from "prop-types";
import React, { useState } from "react";
import ModalForm from "../../ModalForm";
import { Grid, TextField, Button } from "@material-ui/core";
import CGUController from "../../../controllers/CGUController";
import { useEffect } from "react";

/*************************************************************
 * Descripcion: Modal del formulario para Agregar/Modificar un módulo
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: SistemaModulo (Modificar), Sistema (Agregar)
 *************************************************************/
const FormModulo = (props) => {
    const { entModulo, open, setOpen, usuarioSesion, funcGetPermisosXPerfil, funcLoader, funcAlert } = props;

    //Servicios API
    const cguController = new CGUController();

    //Objeto para guardar los valore de los inputs
    const [formModulo, setFormModulo] = useState({
        txtIdModulo: "",
        txtNombre: "",
    });

    //Consumir servicio para guardar el modulo en la base
    const funcSaveModulo = async () => {
        funcLoader(true, "Guardando módulo...");

        const entSaveModulo = {
            iIdModulo: entModulo.iIdModulo,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: formModulo.txtNombre,
            bActivo: true,
            bBaja: false,
        };

        const response = await cguController.funcSaveModulo(entSaveModulo);
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
        setFormModulo({
            ...formModulo,
            [e.target.name]: e.target.value,
        });
    };

    //Actualizar los valores de los inputs con los datos de entModulo al cargar el componente
    useEffect(() => {
        setFormModulo({
            txtIdModulo: entModulo.iIdModulo,
            txtNombre: entModulo.sNombre,
        });
        // eslint-disable-next-line
    }, [entModulo]);

    return (
        <ModalForm
            title={entModulo.iIdModulo === 0 ? "Nuevo módulo" : "Editar módulo"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                {entModulo.iIdModulo > 0 ? (
                    <Grid item xs={12}>
                        <TextField
                            name="txtIdModulo"
                            label="ID de módulo:"
                            variant="outlined"
                            color="secondary"
                            fullWidth
                            value={formModulo.txtIdModulo}
                            disabled
                        />
                    </Grid>
                ) : null}

                <Grid item xs={12}>
                    <TextField
                        name="txtNombre"
                        label="Nombre de módulo:"
                        variant="outlined"
                        color="secondary"
                        autoComplete="off"
                        fullWidth
                        autoFocus
                        value={formModulo.txtNombre}
                        onChange={handleChangeForm}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcSaveModulo}>
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

FormModulo.propTypes = {
    entModulo: PropTypes.shape({
        iIdModulo: PropTypes.number,
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

export default FormModulo;
