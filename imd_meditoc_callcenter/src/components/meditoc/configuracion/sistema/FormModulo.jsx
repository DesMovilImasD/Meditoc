import PropTypes from "prop-types";
import React, { useState } from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import { Grid, TextField, Button } from "@material-ui/core";
import CGUController from "../../../../controllers/CGUController";
import { useEffect } from "react";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";

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

    const [formModuloOK, setFormModuloOK] = useState({
        txtNombre: true,
    });

    //Consumir servicio para guardar el modulo en la base
    const funcSaveModulo = async () => {
        let formModuloOKValidacion = {
            txtNombre: true,
        };

        let formError = false;

        if (formModulo.txtNombre === "") {
            formModuloOKValidacion.txtNombre = false;
            formError = true;
        }

        setFormModuloOK(formModuloOKValidacion);

        if (formError) {
            return;
        }

        funcLoader(true, "Guardando módulo...");

        const entSaveModulo = {
            iIdModulo: entModulo.iIdModulo,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: formModulo.txtNombre,
            bActivo: true,
            bBaja: false,
        };

        const response = await cguController.funcSaveModulo(entSaveModulo);

        if (response.Code === 0) {
            setOpen(false);
            await funcGetPermisosXPerfil();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    //Funcion para capturar los valores de los inputs
    const handleChangeForm = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNombre":
                if (!formModuloOK.txtNombre) {
                    if (valorCampo !== "") {
                        setFormModuloOK({
                            ...formModuloOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            default:
                break;
        }

        setFormModulo({
            ...formModulo,
            [nombreCampo]: valorCampo,
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
        <MeditocModal
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
                        error={!formModuloOK.txtNombre}
                        helperText={!formModuloOK.txtNombre ? "El nombre del módulo es requerido" : ""}
                    />
                </Grid>
                <MeditocModalBotones setOpen={setOpen} okMessage="Guardar módulo" okFunc={funcSaveModulo} />
            </Grid>
        </MeditocModal>
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
