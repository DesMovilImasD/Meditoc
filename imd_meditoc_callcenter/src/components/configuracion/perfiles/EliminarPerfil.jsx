import React from "react";
import CGUController from "../../../controllers/CGUController";
import ModalForm from "../../ModalForm";
import { Grid, Button } from "@material-ui/core";

/*************************************************************
 * Descripcion: Diálogo de confirmación para eliminar un perfil
 * Creado: Cristopher Noh
 * Fecha: 27/08/2020
 * Invocado desde: Perfiles
 *************************************************************/
const EliminarPerfil = (props) => {
    const {
        entPerfil,
        setPerfilSeleccionado,
        open,
        setOpen,
        funcGetPerfiles,
        usuarioSesion,
        funcLoader,
        funcAlert,
    } = props;

    const cguController = new CGUController();

    const funcSavePerfil = async () => {
        funcLoader(true, "Eliminando perfil...");

        const entSavePerfil = {
            iIdPerfil: entPerfil.iIdPerfil,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            sNombre: null,
            bActivo: false,
            bBaja: true,
        };

        const response = await cguController.funcSavePerfil(entSavePerfil);
        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setOpen(false);
            setPerfilSeleccionado({ iIdPerfil: 0, sNombre: "" });
            funcAlert(response.Message, "success");
            funcGetPerfiles();
        }

        funcLoader();
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <ModalForm title="Eliminar perfil" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12} className="center">
                    ¿Desea eliminar el perfil {entPerfil.sNombre} y todos sus permisos?
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcSavePerfil}>
                        Eliminar
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

export default EliminarPerfil;
