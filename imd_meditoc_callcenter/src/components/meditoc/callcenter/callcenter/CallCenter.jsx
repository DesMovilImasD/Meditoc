import React, { Fragment } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import { makeStyles, withStyles } from "@material-ui/core/styles";
import theme from "../../../../configurations/themeConfig";
import { Button, FormControlLabel, Grid, Popover, Switch, Typography } from "@material-ui/core";
import { useState } from "react";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import { useEffect } from "react";
import MeditocBody from "../../../utilidades/MeditocBody";
import { urlBase, urlSystem } from "../../../../configurations/urlConfig";
import CallCenterController from "../../../../controllers/CallCenterController";
import FormBuscarFolio from "./FormBuscarFolio";
import FormCallCenter from "./FormCallCenter";
import FolioController from "../../../../controllers/FolioController";
import { green, red } from "@material-ui/core/colors";
import InfoIcon from "@material-ui/icons/Info";
import DirectotioMedico from "./DirectotioMedico";
import MeditocSwitch from "../../../utilidades/MeditocSwitch";

const useStyles = makeStyles(() => ({
    button: {
        color: theme.palette.primary.main,
        backgroundColor: "#fff",
        textTransform: "none",
        marginTop: 10,
        marginBottom: 10,
        marginRight: 20,
    },
}));

const CallCenter = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const classes = useStyles();

    const colaboradorController = new ColaboradorController();
    const callcenterController = new CallCenterController();
    const folioController = new FolioController();

    const [consultaIniciada, setConsultaIniciada] = useState(false);
    const [entCallCenter, setEntCallCenter] = useState(null);
    const [folioEncontrado, setFolioEncontrado] = useState(null);

    const [usuarioColaborador, setUsuarioColaborador] = useState(null);
    const [salaIceLink, setSalaIceLink] = useState(null);

    const [colaboradorDisponible, setColaboradorDisponible] = useState(false);

    const [modalBuscarFolioOpen, setModalBuscarFolioOpen] = useState(false);
    const [modalDirectorioOpen, setModalDirectorioOpen] = useState(false);

    const [temporizadorState, setTemporizadorState] = useState({
        segundo: 0,
        minuto: 0,
        hora: 0,
    });

    let temporizador = {
        segundo: 0,
        minuto: 0,
        hora: 0,
    };

    const [interTemporizador, setInterTemporizador] = useState(0);

    const [popoverOcupadoInicio, setPopoverOcupadoInicio] = useState(false);
    const handleClosePopoverOcupado = () => {
        setPopoverOcupadoInicio(false);
    };

    const handleClickPopoverDisponible = async () => {
        await funcOnlineMod(true);
        handleClosePopoverOcupado();
    };

    const formularioDiagnosticoYTratamientoVacia = {
        txtCCPeso: "",
        txtCCAltura: "",
        txtCCAlergias: "",
        txtCCSintomas: "",
        txtCCDiagnostico: "",
        txtCCTratamiento: "",
        txtCCComentarios: "",
    };

    const [formDiagnosticoTratamiento, setFormDiagnosticoTratamiento] = useState(
        formularioDiagnosticoYTratamientoVacia
    );

    const handleClickIniciarConsulta = async () => {
        const folioLocalStorage = localStorage.getItem("sFolio");
        if (folioLocalStorage !== null && folioLocalStorage !== undefined && folioLocalStorage !== "") {
            funcLoader(true, "Buscando folio...");

            const response = await folioController.funcGetFolios(null, null, null, null, folioLocalStorage);

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
        }
        setModalBuscarFolioOpen(true);
    };

    const funcIniciarTemporizador = () => {
        const intervalID = setInterval(() => {
            temporizador = {
                segundo: temporizador.segundo === 59 ? 0 : temporizador.segundo + 1,
                minuto:
                    temporizador.segundo === 59 && temporizador.minuto === 59
                        ? 0
                        : temporizador.segundo === 59
                        ? temporizador.minuto + 1
                        : temporizador.minuto,
                hora:
                    temporizador.segundo === 59 && temporizador.minuto === 59
                        ? temporizador.hora + 1
                        : temporizador.hora,
            };
            setTemporizadorState(temporizador);
        }, 1000);
        console.log("interTemporizador", intervalID);
        setInterTemporizador(intervalID);
    };

    const funcDetenerTemporizador = () => {
        clearInterval(interTemporizador);
    };

    const funcReiniciarTemporizador = () => {
        setTemporizadorState({
            segundo: 0,
            minuto: 0,
            hora: 0,
        });
    };

    const funcGetColaboradorUser = async () => {
        funcLoader(true, "Obteniendo usuario administrativo...");

        const response = await colaboradorController.funcGetColaboradores(null, null, null, usuarioSesion.iIdUsuario);

        if (response.Code === 0) {
            if (response.Result.length > 0) {
                setUsuarioColaborador(response.Result[0]);
                return;
            } else {
                funcAlert("No se ha ingresado con una cuenta de colaborador");
            }
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const funcOnlineMod = async (disponible, online = true) => {
        if (usuarioColaborador === null) {
            return;
        }
        if (disponible === true && consultaIniciada === true) {
            funcAlert("No se puede marcar como DISPONIBLE hasta que la consulta haya finalizado", "warning");
            return;
        }
        funcLoader(true, "Actualizando estatus...");

        const entOnlineMod = {
            iIdColaborador: usuarioColaborador.iIdColaborador,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bOnline: online,
            bOcupado: !disponible,
        };

        const response = await callcenterController.funcColaboradorOnline(entOnlineMod);

        if (response.Code === 0) {
            if (online === true) {
                setColaboradorDisponible(disponible);
            }
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    useEffect(() => {
        localStorage.removeItem("name");
        localStorage.removeItem("SalaID");
        localStorage.removeItem("screen");
        localStorage.removeItem("sessionId");
        localStorage.removeItem("DisplayName");
        funcGetColaboradorUser();
    }, []);

    const funcPrepararSalaCallCenterInicio = async () => {
        if (usuarioColaborador !== null) {
            localStorage.setItem("name", usuarioColaborador.sNombreDirectorio);
            localStorage.setItem("SalaID", usuarioColaborador.iNumSala);
            localStorage.setItem("screen", 0);
            localStorage.setItem("sessionId", usuarioColaborador.iNumSala);
            localStorage.setItem("DisplayName", usuarioColaborador.sNombreDirectorio);

            localStorage.removeItem("sFolio");

            const getUIID = function () {
                return new Date().getTime() * 1000 + Math.floor(Math.random() * 1001);
            };

            const url = `${urlBase}/IceLink/index.html?var=${getUIID()}`;
            setSalaIceLink(<iframe id="iframeickelink" src={url} width="100%" height="600px" scrolling="no"></iframe>);
            await funcOnlineMod(false);
            setPopoverOcupadoInicio(true);

            window.onbeforeunload = (e) => {
                return "Al salir de esta sección se finalizará la consulta actual. ¿Desea salir de esta sección y finalizar la consulta?";
            };

            window.onunload = (e) => {
                funcOnlineMod(false, false);
            };

            window.onhashchange = async (e) => {
                if (e.oldURL.includes(urlSystem.callcenter.consultas.replace("/", ""))) {
                    await funcOnlineMod(false, false);

                    if (consultaIniciada) {
                        await handleClickFinalizarConsulta(true);
                    }
                }
            };
        }
    };

    useEffect(() => {
        funcPrepararSalaCallCenterInicio();

        return () => {
            funcOnlineMod(false, false);
        };
    }, [usuarioColaborador]);

    const handleClickFinalizarConsulta = async (cerrarVentana = false) => {
        await funcSaveHistorialClinico(cerrarVentana);

        funcLoader(true, "Finalizando consulta...");

        const response = await callcenterController.funcFinalizarConsulta(
            entCallCenter.entConsulta.iIdConsulta,
            usuarioColaborador.iIdColaborador,
            usuarioSesion.iIdUsuario
        );

        if (response.Code === 0) {
            if (!cerrarVentana) {
                setConsultaIniciada(false);
                //await funcOnlineMod(false);
                funcDetenerTemporizador();
                funcReiniciarTemporizador();
                setEntCallCenter(null);
                setFolioEncontrado(null);
            }

            localStorage.removeItem("sFolio");

            const iframeickelink = document.getElementById("iframeickelink");
            if (iframeickelink !== null) {
                iframeickelink.contentWindow.CallBack();
                iframeickelink.contentWindow.CallBacks.FinalizarConsulta();
            }
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const funcSaveHistorialClinico = async (cerrarVentana = false) => {
        const entHistorialClinico = {
            iIdConsulta: entCallCenter.entConsulta.iIdConsulta,
            sSintomas: formDiagnosticoTratamiento.txtCCSintomas,
            sDiagnostico: formDiagnosticoTratamiento.txtCCDiagnostico,
            sTratamiento: formDiagnosticoTratamiento.txtCCTratamiento,
            fPeso:
                formDiagnosticoTratamiento.txtCCPeso === "" ? null : parseFloat(formDiagnosticoTratamiento.txtCCPeso),
            fAltura:
                formDiagnosticoTratamiento.txtCCAltura === ""
                    ? null
                    : parseFloat(formDiagnosticoTratamiento.txtCCAltura),
            sAlergias: formDiagnosticoTratamiento.txtCCAlergias,
            sComentarios: formDiagnosticoTratamiento.txtCCComentarios,
            iIdUsuarioMod: usuarioSesion.iIdConsulta,
        };

        funcLoader(true, "Guardando historial clínico...");

        const response = await callcenterController.funcSaveHistorialClinico(entHistorialClinico);

        if (response.Code === 0) {
            funcAlert(response.Message, "success");

            if (!cerrarVentana) {
                setFormDiagnosticoTratamiento(formularioDiagnosticoYTratamientoVacia);
            }
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    useEffect(() => {
        if (entCallCenter !== null) {
            if (entCallCenter.lstHistorialClinico !== undefined) {
                if (entCallCenter.lstHistorialClinico.length > 0) {
                    const ultimoHistorialClinico =
                        entCallCenter.lstHistorialClinico[entCallCenter.lstHistorialClinico.length - 1];

                    setFormDiagnosticoTratamiento({
                        ...formDiagnosticoTratamiento,
                        txtCCAltura: ultimoHistorialClinico.fAltura,
                        txtCCPeso: ultimoHistorialClinico.fPeso,
                        txtCCAlergias: ultimoHistorialClinico.sAlergias,
                    });
                }
            }
        }
    }, [entCallCenter]);

    const handleClickDirectorio = () => {
        setModalDirectorioOpen(true);
    };

    return (
        <Fragment>
            <MeditocHeader1
                title={
                    <Fragment>
                        <span className="rob-con bold size-20 vertical-align-middle mar-right-30">
                            Consulta: {temporizadorState.hora.toLocaleString("en", { minimumIntegerDigits: 2 })}:
                            {temporizadorState.minuto.toLocaleString("en", { minimumIntegerDigits: 2 })}:
                            {temporizadorState.segundo.toLocaleString("en", { minimumIntegerDigits: 2 })}
                        </span>
                        {consultaIniciada === false && entCallCenter === null ? (
                            <Button
                                variant="contained"
                                className={classes.button}
                                onClick={handleClickIniciarConsulta}
                                disabled={usuarioColaborador === null}
                            >
                                Iniciar consulta
                            </Button>
                        ) : (
                            <Button
                                variant="contained"
                                className={classes.button}
                                onClick={handleClickFinalizarConsulta}
                                disabled={usuarioColaborador === null}
                            >
                                Finalizar consulta
                            </Button>
                        )}
                        <Button variant="contained" className={classes.button} onClick={handleClickDirectorio}>
                            Directorio médico
                        </Button>
                    </Fragment>
                }
            >
                Estatus: {colaboradorDisponible ? "DISPONIBLE" : "OCUPADO"}
                <MeditocSwitch
                    checked={colaboradorDisponible}
                    onChange={() => funcOnlineMod(!colaboradorDisponible)}
                    name="swtEstatusDoctor"
                    disabled={usuarioColaborador === null}
                />
            </MeditocHeader1>
            <Popover
                anchorReference="anchorPosition"
                anchorPosition={{ top: 140, left: 20 }}
                anchorOrigin={{
                    vertical: "top",
                    horizontal: "left",
                }}
                transformOrigin={{
                    vertical: "top",
                    horizontal: "left",
                }}
                open={popoverOcupadoInicio}
                onClose={handleClosePopoverOcupado}
            >
                <div style={{ padding: 30, textAlign: "center" }}>
                    <div>
                        <InfoIcon style={{ fontSize: 80, color: theme.palette.primary.main }} />
                    </div>
                    <div>
                        <Typography variant="body2" paragraph>
                            TU ESTATUS ACTUAL ES OCUPADO.
                        </Typography>
                        <Typography variant="body2" paragraph>
                            Cambia tu estatus a DISPONIBLE para comenzar a
                            <br />
                            recibir videollamadas y chats de tus pacientes
                        </Typography>
                        <Button color="default" style={{ color: "#888" }} onClick={handleClosePopoverOcupado}>
                            <Typography variant="body2">CONTINUAR OCUPADO</Typography>
                        </Button>
                        <Button color="primary" onClick={handleClickPopoverDisponible}>
                            <Typography variant="body2">CAMBIAR A DISPONIBLE</Typography>
                        </Button>
                    </div>
                </div>
            </Popover>
            <MeditocBody>
                <Grid container spacing={3}>
                    <Grid item sm={5}>
                        {salaIceLink}
                    </Grid>
                    {consultaIniciada === true && entCallCenter !== null ? (
                        <Grid item sm={7}>
                            <FormCallCenter
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                                usuarioSesion={usuarioSesion}
                                usuarioColaborador={usuarioColaborador}
                                entCallCenter={entCallCenter}
                                setEntCallCenter={setEntCallCenter}
                                formDiagnosticoTratamiento={formDiagnosticoTratamiento}
                                setFormDiagnosticoTratamiento={setFormDiagnosticoTratamiento}
                            />
                        </Grid>
                    ) : null}
                </Grid>
            </MeditocBody>
            <FormBuscarFolio
                open={modalBuscarFolioOpen}
                setOpen={setModalBuscarFolioOpen}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
                usuarioSesion={usuarioSesion}
                usuarioColaborador={usuarioColaborador}
                setEntCallCenter={setEntCallCenter}
                setConsultaIniciada={setConsultaIniciada}
                funcOnlineMod={funcOnlineMod}
                funcIniciarTemporizador={funcIniciarTemporizador}
                funcReiniciarTemporizador={funcReiniciarTemporizador}
                folioEncontrado={folioEncontrado}
                setFolioEncontrado={setFolioEncontrado}
            />
            <DirectotioMedico
                open={modalDirectorioOpen}
                setOpen={setModalDirectorioOpen}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

export default CallCenter;
