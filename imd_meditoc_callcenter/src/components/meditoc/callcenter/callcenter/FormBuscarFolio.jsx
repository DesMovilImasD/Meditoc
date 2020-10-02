import { Divider, Grid, TextField } from "@material-ui/core";
import React, { Fragment } from "react";
import { useEffect } from "react";
import { useState } from "react";
import CallCenterController from "../../../../controllers/CallCenterController";
import FolioController from "../../../../controllers/FolioController";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";

const FormBuscarFolio = (props) => {
    const {
        open,
        setOpen,
        usuarioSesion,
        usuarioColaborador,
        funcLoader,
        funcAlert,
        setEntCallCenter,
        setConsultaIniciada,
        funcOnlineMod,
        funcIniciarTemporizador,
        funcReiniciarTemporizador,
        folioEncontrado,
        setFolioEncontrado,
    } = props;

    const folioController = new FolioController();
    const callcenterController = new CallCenterController();

    const [formBuscarFolio, setFormBuscarFolio] = useState({
        txtBuscarFolio: "",
        txtBuscarNombrePaciente: "",
        txtBuscarTelefonoPaciente: "",
        txtBuscarCorreoPaciente: "",
    });

    const [formBuscarFolioOK, setFormBuscarFolioOK] = useState(true);

    const handleChangeFormBuscar = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtBuscarFolio":
                if (!formBuscarFolioOK) {
                    if (formBuscarFolio.txtBuscarFolio !== "") {
                        setFormBuscarFolioOK(true);
                    }
                }
                break;

            default:
                break;
        }

        setFormBuscarFolio({
            ...formBuscarFolio,
            [nombreCampo]: valorCampo,
        });
    };

    const funcGetFolio = async () => {
        let formError = false;

        if (formBuscarFolio.txtBuscarFolio === "") {
            setFormBuscarFolioOK(false);
            formError = true;
        }

        if (formError) {
            return;
        }

        funcLoader(true, "Buscando folio...");

        const response = await folioController.funcGetFolios(null, null, null, null, formBuscarFolio.txtBuscarFolio);

        if (response.Code === 0) {
            if (response.Result.length === 1) {
                setFolioEncontrado(response.Result[0]);
            } else {
                setFolioEncontrado(null);
                funcAlert("No se encontró el folio o ha expirado");
            }
        } else {
            setFolioEncontrado(null);
            funcAlert(response.Message);
        }

        funcLoader();
    };

    useEffect(() => {
        if (folioEncontrado !== null) {
            setFormBuscarFolio({
                txtBuscarFolio: folioEncontrado.sFolio,
                txtBuscarNombrePaciente: folioEncontrado.sNombrePaciente,
                txtBuscarTelefonoPaciente:
                    folioEncontrado.sTelefonoPaciente === null ? "" : folioEncontrado.sTelefonoPaciente,
                txtBuscarCorreoPaciente: folioEncontrado.sCorreoPaciente,
            });
        } else {
            setFormBuscarFolio({
                txtBuscarFolio: "",
                txtBuscarNombrePaciente: "",
                txtBuscarTelefonoPaciente: "",
                txtBuscarCorreoPaciente: "",
            });
        }
    }, [folioEncontrado]);

    const handleClickLimpiar = () => {
        setFolioEncontrado(null);
        setFormBuscarFolio({
            txtBuscarFolio: "",
            txtBuscarNombrePaciente: "",
            txtBuscarTelefonoPaciente: "",
            txtBuscarCorreoPaciente: "",
        });
    };

    const handleClickIniciarConsulta = async () => {
        funcLoader(true, "Creando consulta...");

        const response = await callcenterController.funcCrearConsultaFolio(
            usuarioColaborador.iIdColaborador,
            folioEncontrado.sFolio,
            usuarioSesion.iIdUsuario
        );

        if (response.Code === 0) {
            setEntCallCenter(response.Result);
            setOpen(false);
            await funcIniciarConsulta(response.Result);
            //funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcIniciarConsulta = async (entCallCenterRespuesta) => {
        funcLoader(true, "Iniciando consulta...");

        const response = await callcenterController.funcIniciarConsulta(
            entCallCenterRespuesta.entConsulta.iIdConsulta,
            usuarioColaborador.iIdColaborador,
            usuarioSesion.iIdUsuario
        );

        if (response.Code === 0) {
            setConsultaIniciada(true);
            await funcOnlineMod(false);
            funcAlert(response.Message, "success");
            funcReiniciarTemporizador();
            funcIniciarTemporizador();
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    return (
        <MeditocModal title="Confirmar folio" open={open} setOpen={setOpen} size="small">
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="CONFIRMAR FOLIO PARA INICIAR CONSULTA" />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="txtBuscarFolio"
                        label="Folio:"
                        variant="outlined"
                        fullWidth
                        required
                        value={formBuscarFolio.txtBuscarFolio}
                        onChange={handleChangeFormBuscar}
                        error={!formBuscarFolioOK}
                        helperText={!formBuscarFolioOK ? "El folio es requerido" : ""}
                    />
                </Grid>
                <MeditocModalBotones
                    okMessage="Buscar Folio"
                    cancelMessage="Limpiar"
                    cancelFunc={handleClickLimpiar}
                    open={open}
                    setOpen={setOpen}
                    okFunc={funcGetFolio}
                />
                {folioEncontrado !== null ? (
                    <Fragment>
                        <Grid item xs={12}>
                            <MeditocSubtitulo title="CONFIRMAR DATOS DEL PACIENTE" />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                name="txtBuscarNombrePaciente"
                                label="Nombre:"
                                variant="outlined"
                                fullWidth
                                disabled
                                value={formBuscarFolio.txtBuscarNombrePaciente}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                name="txtBuscarTelefonoPaciente"
                                label="Teléfono:"
                                variant="outlined"
                                fullWidth
                                disabled
                                value={formBuscarFolio.txtBuscarTelefonoPaciente}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                name="txtBuscarCorreoPaciente"
                                label="Correo electrónico:"
                                variant="outlined"
                                fullWidth
                                disabled
                                value={formBuscarFolio.txtBuscarCorreoPaciente}
                            />
                        </Grid>
                        <MeditocModalBotones
                            okMessage="Iniciar consulta"
                            open={open}
                            setOpen={setOpen}
                            hideCancel
                            okFunc={handleClickIniciarConsulta}
                        />
                    </Fragment>
                ) : null}
            </Grid>
        </MeditocModal>
    );
};

export default FormBuscarFolio;
