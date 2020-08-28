import React, { useState } from "react";
import ModalForm from "../../ModalForm";
import { Grid, TextField, Button } from "@material-ui/core";
import CGUController from "../../../controllers/CGUController";

/*************************************************************
 * Descripcion: Modal del formulario para Agregar/Modificar un submódulo
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde: SistemaSubmodulo (Modificar), SistemaModulo (Agregar)
 *************************************************************/
const FormSubmodulo = (props) => {
    const { entSubmodulo, open, setOpen, usuarioSesion, funcGetPermisosXPerfil, funcLoader, funcAlert } = props;

    const cguController = new CGUController();

    const [formSubmodulo, setFormSubmodulo] = useState({
        txtIdModulo: entSubmodulo.iIdModulo,
        txtIdSubmodulo: entSubmodulo.iIdSubModulo,
        txtNombre: entSubmodulo.sNombre,
    });

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
            //setFormSubmodulo({ ...formSubmodulo, txtNombre: "" });
            funcGetPermisosXPerfil();
        }

        funcLoader();
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleChangeForm = (e) => {
        setFormSubmodulo({
            ...formSubmodulo,
            [e.target.name]: e.target.value,
        });
    };

    return (
        <ModalForm
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
        </ModalForm>
    );
};

export default FormSubmodulo;
