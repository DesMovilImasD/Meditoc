import React from "react";
import CGUController from "../../../controllers/CGUController";
import ModalForm from "../../ModalForm";
import { Grid, Button } from "@material-ui/core";

const EliminarPermisoSubmodulo = (props) => {
    const {
        entPerfil,
        entSubmodulo,
        funcGetPermisosXPerfil,
        open,
        setOpen,
        usuarioSesion,
        funcLoader,
        funcAlert,
    } = props;

    const cguController = new CGUController();

    const funcSavePermisoSubmodulo = async () => {
        const listaPermisosParaGuardar = [
            {
                iIdPerfil: entPerfil.iIdPerfil,
                iIdModulo: entSubmodulo.iIdModulo,
                iIdSubModulo: entSubmodulo.iIdSubModulo,
                iIdBoton: 0,
                iIdUsuarioMod: usuarioSesion.iIdUsuario,
                bActivo: false,
                bBaja: true,
            },
        ];

        funcLoader(true, "Removiendo permisos de submódulo...");

        const response = await cguController.funcSavePermiso(listaPermisosParaGuardar);
        if (response.Code !== 0) {
            funcAlert(response.Message);
        } else {
            setOpen(false);
            funcAlert(response.Message, "success");
            funcGetPermisosXPerfil();
        }

        funcLoader();
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <ModalForm title="Quitar permiso para submódulo" size="small" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12} className="center">
                    ¿Desea remover el permiso para el submódulo {entSubmodulo.sNombre} y todos sus botones?
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth onClick={funcSavePermisoSubmodulo}>
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

export default EliminarPermisoSubmodulo;
