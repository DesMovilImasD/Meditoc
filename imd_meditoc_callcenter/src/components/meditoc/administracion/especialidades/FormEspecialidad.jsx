import { Grid, TextField } from "@material-ui/core";
import { blurPrevent, funcPrevent } from "../../../../configurations/preventConfig";

import EspecialidadController from "../../../../controllers/EspecialidadController";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PropTypes from "prop-types";
import React from "react";
import { useEffect } from "react";
import { useState } from "react";

const FormEspecialidad = (props) => {
    const { entEspecialidad, open, setOpen, funcGetEspecialidades, usuarioSesion, funcLoader, funcAlert } = props;

    const especialidadController = new EspecialidadController();

    const [formEspecialidad, setFormEspecialidad] = useState({
        txtNombreEspecialidad: "",
    });

    const validacionFormulario = {
        txtNombreEspecialidad: true,
    };
    const [formEspecialidadOK, setFormEspecialidadOK] = useState(validacionFormulario);

    useEffect(() => {
        setFormEspecialidad({
            txtNombreEspecialidad: entEspecialidad.sNombre,
        });
        setFormEspecialidadOK(validacionFormulario);
        // eslint-disable-next-line
    }, [entEspecialidad]);

    const handleChangeFormEspecialidad = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNombreEspecialidad":
                if (formEspecialidadOK.txtNombreEspecialidad === false) {
                    if (valorCampo !== "") {
                        setFormEspecialidadOK({
                            ...formEspecialidadOK,
                            [nombreCampo]: true,
                        });
                    }
                }
                break;

            default:
                break;
        }

        setFormEspecialidad({
            ...formEspecialidad,
            [nombreCampo]: valorCampo,
        });
    };

    const handleClickSaveEspecialidad = async (e) => {
        funcPrevent(e);

        let formEspecialidadOKValidacion = { ...validacionFormulario };

        let formError = false;

        if (formEspecialidad.txtNombreEspecialidad === "") {
            formEspecialidadOKValidacion.txtNombreEspecialidad = false;
            formError = true;
        }

        setFormEspecialidadOK(formEspecialidadOKValidacion);

        if (formError === true) {
            return;
        }

        funcLoader(true, "Guardando especialidad");

        const entEspecialidadSubmit = {
            iIdEspecialidad: entEspecialidad.iIdEspecialidad,
            sNombre: formEspecialidad.txtNombreEspecialidad,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: true,
            bBaja: false,
        };

        const response = await especialidadController.funcSaveEspecialidad(entEspecialidadSubmit);

        if (response.Code === 0) {
            setOpen(false);
            await funcGetEspecialidades();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
        blurPrevent();
    };

    return (
        <MeditocModal
            title={entEspecialidad.iIdEspecialidad === 0 ? "Crear especialidad" : "Editar especialidad"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <form id="form-especialidad" onSubmit={handleClickSaveEspecialidad} noValidate>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <TextField
                            name="txtNombreEspecialidad"
                            label="Nombre de especialidad:"
                            variant="outlined"
                            fullWidth
                            required
                            autoFocus
                            value={formEspecialidad.txtNombreEspecialidad}
                            onChange={handleChangeFormEspecialidad}
                            error={formEspecialidadOK.txtNombreEspecialidad === false}
                            helperText={
                                formEspecialidadOK.txtNombreEspecialidad === true
                                    ? ""
                                    : "El nombre de la especialidad es requerido"
                            }
                        />
                    </Grid>
                    <MeditocModalBotones
                        okMessage="Guardar"
                        okFunc={handleClickSaveEspecialidad}
                        open={open}
                        setOpen={setOpen}
                    />
                </Grid>
            </form>
        </MeditocModal>
    );
};

FormEspecialidad.propTypes = {
    entEspecialidad: PropTypes.shape({
        iIdEspecialidad: PropTypes.number,
        sNombre: PropTypes.any,
    }),
    funcAlert: PropTypes.func,
    funcGetEspecialidades: PropTypes.func,
    funcLoader: PropTypes.func,
    open: PropTypes.any,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default FormEspecialidad;
