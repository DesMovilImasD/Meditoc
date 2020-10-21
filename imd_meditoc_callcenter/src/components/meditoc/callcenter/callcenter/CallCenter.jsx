import { Button, Grid } from "@material-ui/core";
import React, { Fragment } from "react";
import { urlBase, urlSystem } from "../../../../configurations/urlConfig";

import CallCenterController from "../../../../controllers/CallCenterController";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import DirectorioMedico from "./DirectorioMedico";
import FolioController from "../../../../controllers/FolioController";
import FormBuscarFolio from "./FormBuscarFolio";
import FormCallCenter from "./FormCallCenter";
import HelperConsulta from "./HelperConsulta";
import HelperStatus from "./HelperStatus";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocHelper from "../../../utilidades/MeditocHelper";
import MeditocSwitch from "../../../utilidades/MeditocSwitch";
import PropTypes from "prop-types";
import { makeStyles } from "@material-ui/core/styles";
import theme from "../../../../configurations/themeConfig";
import { tiempoValidarEstatusColaborador } from "../../../../configurations/systemConfig";
import { useEffect } from "react";
import { useHistory } from "react-router-dom";
import { useState } from "react";

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
    const history = useHistory();
    const { usuarioSesion, permisos, funcLoader, funcAlert, funcCerrarTodo, setFuncCerrarTodo } = props;

    const classes = useStyles();

    const colaboradorController = new ColaboradorController();
    const callcenterController = new CallCenterController();
    const folioController = new FolioController();

    const [consultaIniciada, setConsultaIniciada] = useState(false);
    const [entCallCenter, setEntCallCenter] = useState(null);
    const [folioEncontrado, setFolioEncontrado] = useState(null);

    const [inicioAutomatico, setInicioAutomatico] = useState(true);

    const [usuarioColaborador, setUsuarioColaborador] = useState(null);
    const [salaIceLink, setSalaIceLink] = useState(null);

    const [colaboradorDisponible, setColaboradorDisponible] = useState(false);
    const [intervalColaboradorStatus, setIntervalColaboradorStatus] = useState(0);

    const [helperMessage, setHelperMessage] = useState(
        <Fragment>
            Cambia tu estatus a DISPONIBLE para comenzar a
            <br />
            recibir videollamadas y chats de tus pacientes
        </Fragment>
    );

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
    const [popoverConsulta, setPopoverConsulta] = useState(false);

    const handleClosePopoverOcupado = () => {
        setPopoverOcupadoInicio(false);
    };

    const handleClosePopoverConsulta = () => {
        setPopoverConsulta(false);
    };

    const handleClickPopoverDisponible = async () => {
        await funcOnlineMod(true);
        handleClosePopoverOcupado();
    };

    const handleClickPopoverConsultaReiniciar = async () => {
        funcReiniciarChat();
        handleClosePopoverConsulta();
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
        setPopoverConsulta(false);
        const folioLocalStorage = localStorage.getItem("sFolio");
        if (folioLocalStorage !== null && folioLocalStorage !== undefined && folioLocalStorage !== "") {
            funcLoader(true, "Buscando folio...");

            const response = await folioController.funcGetFolios(
                null,
                null,
                null,
                null,
                folioLocalStorage,
                "",
                "",
                "",
                ""
            );

            if (response.Code === 0) {
                if (response.Result.length === 1) {
                    setInicioAutomatico(true);
                    setFolioEncontrado(response.Result[0]);
                } else {
                    setInicioAutomatico(false);
                    setFolioEncontrado(null);
                    funcAlert("No se encontró el folio o ha expirado");
                }
            } else {
                setInicioAutomatico(false);
                setFolioEncontrado(null);
                funcAlert(response.Message);
            }

            funcLoader();
        } else {
            setInicioAutomatico(false);
            setFolioEncontrado(null);
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
                const entUsuarioColaboradorObtenido = response.Result[0];
                if (entUsuarioColaboradorObtenido.bAcceso === false) {
                    funcAlert(
                        "Aún no cuenta con acceso a las consultas de Meditoc Call Cennter. Contacte con su administrador."
                    );
                    funcLoader();
                    return;
                }
                if (entUsuarioColaboradorObtenido.iNumSala === null || entUsuarioColaboradorObtenido.iNumSala === 0) {
                    funcAlert(
                        "No hay un número de sala asignado a esta cuenta. Por favor, contacte con su administrador para asignarle un número de sala."
                    );
                    funcLoader();
                    return;
                }
                setUsuarioColaborador(entUsuarioColaboradorObtenido);
                return;
            } else {
                funcAlert("No se ha ingresado con una cuenta de colaborador");
            }
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const funcOnlineMod = async (disponible, online = true, state = true) => {
        if (usuarioColaborador === null) {
            return;
        }
        if (disponible === true && consultaIniciada === true) {
            funcAlert("No se puede marcar como DISPONIBLE hasta que la consulta haya finalizado", "warning");
            return;
        }

        const messageError = "No se puede marcar como DISPONIBLE hasta que la sala se haya cargado por completo";

        if (disponible === true && online === true) {
            const iframeickelink = document.getElementById("iframeickelink");
            if (iframeickelink !== null) {
                if (typeof iframeickelink.contentWindow.CallBack !== "function") {
                    funcAlert(messageError, "warning");
                    return;
                }
                if (typeof iframeickelink.contentWindow.CallBacks.FinalizarConsulta !== "function") {
                    funcAlert(messageError, "warning");
                    return;
                }
            } else {
                funcAlert(messageError, "warning");
                return;
            }
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
                if (state === true) {
                    setColaboradorDisponible(disponible);
                }
            }
            if (disponible === true) {
                funcReiniciarChat();
            }
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

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
            setSalaIceLink(
                <iframe
                    id="iframeickelink"
                    title="iframeickelink"
                    src={url}
                    width="100%"
                    height="600px"
                    style={{ border: "none" }}
                    scrolling="no"
                />
            );
            await funcOnlineMod(false);
            setPopoverOcupadoInicio(true);

            window.onbeforeunload = () => {
                return "Al salir de esta sección se finalizará la consulta actual. ¿Desea salir de esta sección y finalizar la consulta?";
            };

            window.onunload = () => {
                funcCerrarTodoCallCenter();
            };
        }
    };

    const funcReiniciarChat = async () => {
        const iframeickelink = document.getElementById("iframeickelink");

        if (iframeickelink !== null) {
            if (iframeickelink.contentWindow) {
                if (typeof iframeickelink.contentWindow.CallBack === "function") {
                    iframeickelink.contentWindow.CallBack();
                }

                if (iframeickelink.contentWindow.CallBacks) {
                    if (typeof iframeickelink.contentWindow.CallBacks.FinalizarConsulta === "function") {
                        iframeickelink.contentWindow.CallBacks.FinalizarConsulta();
                    }
                }
            }
        }

        localStorage.removeItem("sFolio");
    };

    const handleClickFinalizarConsulta = async (cerrarVentana = false) => {
        await funcSaveHistorialClinico(cerrarVentana);
        funcLoader(true, "Finalizando consulta...");

        const response = await callcenterController.funcFinalizarConsulta(
            entCallCenter.entConsulta.iIdConsulta,
            usuarioColaborador.iIdColaborador,
            usuarioSesion.iIdUsuario
        );

        if (response.Code === 0) {
            if (cerrarVentana === false) {
                setConsultaIniciada(false);
                //await funcOnlineMod(false);
                funcDetenerTemporizador();
                funcReiniciarTemporizador();
                setEntCallCenter(null);
                setFolioEncontrado(null);
            }

            funcReiniciarChat();
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
        // eslint-disable-next-line
    }, [entCallCenter]);

    const handleClickDirectorio = () => {
        setModalDirectorioOpen(true);
    };

    const handleClickReiniciarChat = async () => {
        funcReiniciarChat();
        await funcOnlineMod(false, true);
    };

    const funcCerrarTodoCallCenter = async () => {
        await funcOnlineMod(false, false, false);
        if (consultaIniciada) {
            await handleClickFinalizarConsulta(true);
        }
        funcReiniciarChat();
    };

    //Actualizar función para cerrar la sala o finalizar la sala al cerrar la sesión o cambiar a otro módulo/submódulo
    useEffect(() => {
        const f = {};
        f.e = funcCerrarTodoCallCenter;
        setFuncCerrarTodo(f);

        // eslint-disable-next-line
    }, [entCallCenter, consultaIniciada, usuarioSesion, usuarioColaborador]);

    useEffect(() => {
        funcPrepararSalaCallCenterInicio();

        // eslint-disable-next-line
    }, [usuarioColaborador]);

    const funcComprobarColaboradorStatus = async () => {
        const response = await colaboradorController.funcGetColaboradorStatus(usuarioColaborador.iIdColaborador);
        if (response.Code === 0) {
            if (response.Result.bOcupado === true) {
                await funcOnlineMod(false, true);
                const sFolioGuardado = localStorage.getItem("sFolio");
                if (sFolioGuardado) {
                    setPopoverConsulta(true);
                } else {
                    setPopoverOcupadoInicio(true);
                    setHelperMessage(
                        <Fragment>
                            Un paciente intentó entrar a tu sala pero <br />
                            la conexión no fue exitosa.
                            <br />
                            <br />
                            Cambia tu estatus a DISPONIBLE para
                            <br />
                            continuar recibiendo llamadas.
                        </Fragment>
                    );
                }
                window.clearInterval(intervalColaboradorStatus);
            }
        }
    };

    useEffect(() => {
        return () => {
            window.clearInterval(intervalColaboradorStatus);
        };
    }, [intervalColaboradorStatus]);

    useEffect(() => {
        if (usuarioColaborador) {
            if (colaboradorDisponible === true) {
                const intervalCol = window.setInterval(() => {
                    funcComprobarColaboradorStatus();
                }, tiempoValidarEstatusColaborador);
                setIntervalColaboradorStatus(intervalCol);
            } else {
                window.clearInterval(intervalColaboradorStatus);
            }
        }
    }, [colaboradorDisponible]);

    useEffect(() => {
        localStorage.removeItem("name");
        localStorage.removeItem("SalaID");
        localStorage.removeItem("screen");
        localStorage.removeItem("sessionId");
        localStorage.removeItem("DisplayName");
        funcGetColaboradorUser();
        // eslint-disable-next-line
    }, []);
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
                        {consultaIniciada === false && entCallCenter === null
                            ? permisos.Botones["2"] !== undefined && ( //Iniciar consulta
                                  <Button
                                      variant="contained"
                                      className={classes.button}
                                      onClick={handleClickIniciarConsulta}
                                      disabled={usuarioColaborador === null}
                                  >
                                      {permisos.Botones["2"].Nombre}
                                  </Button>
                              )
                            : permisos.Botones["3"] !== undefined && ( //Finalizar consulta
                                  <Button
                                      variant="contained"
                                      className={classes.button}
                                      onClick={() => handleClickFinalizarConsulta(false)}
                                      disabled={usuarioColaborador === null}
                                  >
                                      {permisos.Botones["3"].Nombre}
                                  </Button>
                              )}
                        {permisos.Botones["4"] !== undefined && ( //Reiniciar Chat
                            <Button
                                variant="contained"
                                className={classes.button}
                                onClick={handleClickReiniciarChat}
                                disabled={usuarioColaborador === null || consultaIniciada === true}
                            >
                                {permisos.Botones["4"].Nombre}
                            </Button>
                        )}
                        {permisos.Botones["5"] !== undefined && ( //Directorio
                            <Button variant="contained" className={classes.button} onClick={handleClickDirectorio}>
                                {permisos.Botones["5"].Nombre}
                            </Button>
                        )}
                    </Fragment>
                }
            >
                {permisos.Botones["1"] !== undefined && ( //Estatus
                    <Fragment>
                        Estatus: {colaboradorDisponible ? "DISPONIBLE" : "OCUPADO"}
                        <MeditocSwitch
                            checked={colaboradorDisponible}
                            onChange={() => funcOnlineMod(!colaboradorDisponible)}
                            name="swtEstatusDoctor"
                            disabled={usuarioColaborador === null}
                        />
                    </Fragment>
                )}
            </MeditocHeader1>
            {permisos.Botones["1"] !== undefined && ( //Estatus
                <Fragment>
                    <HelperStatus
                        helperMessage={helperMessage}
                        popoverOcupadoInicio={popoverOcupadoInicio}
                        handleClosePopoverOcupado={handleClosePopoverOcupado}
                        handleClickPopoverDisponible={handleClickPopoverDisponible}
                    />
                    <HelperConsulta
                        popoverConsulta={popoverConsulta}
                        handleClosePopoverConsulta={handleClosePopoverConsulta}
                        handleClickPopoverConsultaReiniciar={handleClickPopoverConsultaReiniciar}
                        handleClickIniciarConsulta={handleClickIniciarConsulta}
                    />
                </Fragment>
            )}
            <MeditocBody>
                <Grid container spacing={3}>
                    <Grid item md={5} xs={12}>
                        {salaIceLink}
                    </Grid>
                    {consultaIniciada === true && entCallCenter !== null ? (
                        <Grid item md={7} xs={12}>
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
                inicioAutomatico={inicioAutomatico}
            />
            <DirectorioMedico
                open={modalDirectorioOpen}
                setOpen={setModalDirectorioOpen}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
            <MeditocHelper title="Información de botones:">
                <div>
                    <span className="bold">Iniciar consulta:</span>
                </div>
                <div>
                    <ul>
                        <li>
                            Inicia la consulta del paciente conectado en el chat, llamada o videollamada. Podrá acceder
                            a la consulta y el historial clínico del paciente.
                        </li>
                        <li>
                            Las consultas NO inician automáticamente al conectarse el paciente. Confirme con su paciente
                            antes de iniciar la consulta.
                        </li>
                    </ul>
                </div>
                <div>
                    <span className="bold">Finalizar consulta:</span>
                </div>
                <div>
                    <ul>
                        <li>
                            Finaliza la consulta del paciente. El diagnóstico y tratamiento será guardado en el
                            historial clínico del paciente.
                        </li>
                        <li>Para orientaciones únicas, el folio del paciente quedará inhabilitado.</li>
                    </ul>
                </div>
                <div>
                    <span className="bold">Reiniciar Chat:</span>
                </div>
                <div>
                    <ul>
                        <li>
                            Reinicie el chat cuando no le sea posible atender al paciente y necesite expulsarlo de la
                            sala.
                        </li>
                        <li>IMPORTANTE: No es posible reiniciar el chat cuando ha iniciado la consulta.</li>
                    </ul>
                </div>
            </MeditocHelper>
        </Fragment>
    );
};

CallCenter.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    setFuncCerrarTodo: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdConsulta: PropTypes.any,
        iIdUsuario: PropTypes.any,
    }),
};

export default CallCenter;
