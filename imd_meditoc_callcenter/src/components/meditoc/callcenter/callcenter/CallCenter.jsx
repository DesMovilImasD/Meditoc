import React, { Fragment } from "react";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import { makeStyles, withStyles } from "@material-ui/core/styles";
import theme from "../../../../configurations/themeConfig";
import { Button, Grid } from "@material-ui/core";
import { useState } from "react";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import { useEffect } from "react";
import MeditocBody from "../../../utilidades/MeditocBody";
import { urlBase } from "../../../../configurations/urlConfig";
import CallCenterController from "../../../../controllers/CallCenterController";
import FormBuscarFolio from "./FormBuscarFolio";
import FormCallCenter from "./FormCallCenter";
import FolioController from "../../../../controllers/FolioController";

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

    const funcOnlineMod = async (disponible) => {
        if (disponible === true && consultaIniciada === true) {
            funcAlert("No se puede marcar como DISPONIBLE hasta que la consulta haya finalizado", "warning");
            return;
        }
        funcLoader(true, "Actualizando estatus...");

        const entOnlineMod = {
            iIdColaborador: usuarioColaborador.iIdColaborador,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bOnline: true,
            bOcupado: !disponible,
        };

        const response = await callcenterController.funcColaboradorOnline(entOnlineMod);

        if (response.Code === 0) {
            setColaboradorDisponible(disponible);
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

    useEffect(() => {
        if (usuarioColaborador !== null) {
            localStorage.setItem("name", usuarioColaborador.sNombreDirectorio);
            localStorage.setItem("SalaID", usuarioColaborador.iNumSala);
            localStorage.setItem("screen", 0);
            localStorage.setItem("sessionId", usuarioColaborador.iNumSala);
            localStorage.setItem("DisplayName", usuarioColaborador.sNombreDirectorio);

            const getUIID = function () {
                return new Date().getTime() * 1000 + Math.floor(Math.random() * 1001);
            };

            const url = `${urlBase}/IceLink/index.html?var=${getUIID()}`;
            setSalaIceLink(<iframe id="iframeickelink" src={url} width="100%" height="600" scrolling="no"></iframe>);
            funcOnlineMod(false);
        }
    }, [usuarioColaborador]);

    const handleClickFinalizarConsulta = async () => {
        await funcSaveHistorialClinico();

        funcLoader(true, "Finalizando consulta...");

        const response = await callcenterController.funcFinalizarConsulta(
            entCallCenter.entConsulta.iIdConsulta,
            usuarioColaborador.iIdColaborador,
            usuarioSesion.iIdUsuario
        );

        if (response.Code === 0) {
            setConsultaIniciada(false);
            //await funcOnlineMod(false);
            funcAlert(response.Message, "success");
            funcDetenerTemporizador();
            setEntCallCenter(null);
            setFolioEncontrado(null);
            localStorage.removeItem("sFolio");
            document.getElementById("iframeickelink").contentWindow.CallBacks.FinalizarConsulta();
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const funcSaveHistorialClinico = async () => {
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
            setFormDiagnosticoTratamiento(formularioDiagnosticoYTratamientoVacia);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    return (
        <Fragment>
            <MeditocHeader1
                title={
                    consultaIniciada === false && entCallCenter === null ? (
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
                    )
                }
            >
                <Button variant="contained" className={classes.button}>
                    Directorio médico
                </Button>
                {/* <Button variant="contained" className={classes.button} onClick={funcIniciarTemporizador}>
                    Iniciar
                </Button>
                <Button variant="contained" className={classes.button} onClick={funcDetenerTemporizador}>
                    Detener
                </Button>
                <Button variant="contained" className={classes.button} onClick={funcReiniciarTemporizador}>
                    Reiniciar
                </Button> */}
                <Button
                    variant="text"
                    color="inherit"
                    style={{ marginRight: 40 }}
                    onClick={() => funcOnlineMod(!colaboradorDisponible)}
                    disabled={usuarioColaborador === null}
                >
                    Estatus: {colaboradorDisponible ? "Disponible" : "Ocupado"}
                </Button>
                <span className="rob-con bold size-20 vertical-align-middle">
                    Consulta: {temporizadorState.hora.toLocaleString("en", { minimumIntegerDigits: 2 })}:
                    {temporizadorState.minuto.toLocaleString("en", { minimumIntegerDigits: 2 })}:
                    {temporizadorState.segundo.toLocaleString("en", { minimumIntegerDigits: 2 })}
                </span>
            </MeditocHeader1>
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
        </Fragment>
    );
};

export default CallCenter;
