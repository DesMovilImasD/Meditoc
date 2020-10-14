import { Button, Grid, IconButton, InputAdornment, Tooltip } from "@material-ui/core";
import { blue, green, red, yellow } from "@material-ui/core/colors";

import AddIcon from "@material-ui/icons/Add";
import BlockIcon from "@material-ui/icons/Block";
import CallCenterController from "../../../../controllers/CallCenterController";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import { DatePicker } from "@material-ui/pickers";
import DateRangeIcon from "@material-ui/icons/DateRange";
import DoneIcon from "@material-ui/icons/Done";
import EditIcon from "@material-ui/icons/Edit";
import { EnumEstatusConsulta } from "../../../../configurations/enumConfig";
import FormConsulta from "./FormConsulta";
import { Fragment } from "react";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocHelper from "../../../utilidades/MeditocHelper";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";
import MeditocTable from "../../../utilidades/MeditocTable";
import PropTypes from "prop-types";
import QueryBuilderIcon from "@material-ui/icons/QueryBuilder";
import React from "react";
import ReplayIcon from "@material-ui/icons/Replay";
import TimerIcon from "@material-ui/icons/Timer";
import UpdateIcon from "@material-ui/icons/Update";
import { cellProps } from "../../../../configurations/dataTableIconsConfig";
import { emptyFunc } from "../../../../configurations/preventConfig";
import { useEffect } from "react";
import { useState } from "react";

const Administrador = (props) => {
    const { usuarioSesion, permisos, funcLoader, funcAlert } = props;

    const colaboradorController = new ColaboradorController();
    const callcenterController = new CallCenterController();

    const columns = [
        { title: "ID", field: "iIdConsulta", ...cellProps, hidden: true },
        { title: "", field: "sIconStatus", ...cellProps, sorting: false },
        { title: "Inicio", field: "sFechaProgramadaInicio", ...cellProps },
        { title: "Fin", field: "sFechaProgramadaFin", ...cellProps },
        { title: "Paciente", field: "sNombrePaciente", ...cellProps },
        { title: "Folio", field: "sFolio", ...cellProps },
        { title: "Estatus", field: "sEstatusConsulta", ...cellProps },
        { title: "Creado", field: "sFechaCreacion", ...cellProps },
    ];

    const consultaEntidadVacia = {
        iIdConsulta: 0,
    };

    const [filtroForm, setFiltroForm] = useState({
        txtFechaDe: new Date(),
        txtFechaA: new Date(),
    });

    const [usuarioColaborador, setUsuarioColaborador] = useState(null);
    const [listaConsultas, setListaConsultas] = useState([]);
    const [consultaSeleccionada, setConsultaSeleccionada] = useState(consultaEntidadVacia);
    const [consultaParaModal, setConsultaParaModal] = useState(consultaEntidadVacia);

    const [modalFormConsultaOpen, setModalFormConsultaOpen] = useState(false);
    const [modalCancelarConsultaOpen, setModalCancelarConsultaOpen] = useState(false);

    const funcGetColaboradorUser = async () => {
        funcLoader(true, "Obteniendo usuario administrativo");

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

    useEffect(() => {
        funcGetColaboradorUser();
        // eslint-disable-next-line
    }, []);

    const funcGetConsultas = async () => {
        funcLoader(true, "Obteniendo consultas");

        const fechaProgramadaInicio = filtroForm.txtFechaDe === null ? null : filtroForm.txtFechaDe.toISOString();
        const fechaProgramadaFin = filtroForm.txtFechaA === null ? null : filtroForm.txtFechaA.toISOString();

        const response = await callcenterController.funcGetConsulta(
            null,
            null,
            usuarioColaborador.iIdColaborador,
            null,
            fechaProgramadaInicio,
            fechaProgramadaFin
        );

        if (response.Code === 0) {
            setListaConsultas(
                response.Result.map((consulta) => ({
                    ...consulta,
                    sIconStatus:
                        consulta.iIdEstatusConsulta === EnumEstatusConsulta.CreadoProgramado ? (
                            <QueryBuilderIcon style={{ color: blue[500] }} />
                        ) : consulta.iIdEstatusConsulta === EnumEstatusConsulta.Reprogramado ? (
                            <UpdateIcon style={{ color: blue[500] }} />
                        ) : consulta.iIdEstatusConsulta === EnumEstatusConsulta.EnConsulta ? (
                            <TimerIcon style={{ color: yellow[500] }} />
                        ) : consulta.iIdEstatusConsulta === EnumEstatusConsulta.Finalizado ? (
                            <DoneIcon style={{ color: green[500] }} />
                        ) : consulta.iIdEstatusConsulta === EnumEstatusConsulta.Cancelado ? (
                            <BlockIcon style={{ color: red[500] }} />
                        ) : null,
                }))
            );
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    useEffect(() => {
        if (usuarioColaborador !== null) {
            funcGetConsultas();
        }
        // eslint-disable-next-line
    }, [usuarioColaborador]);

    const handleClickNuevaConsulta = () => {
        setConsultaParaModal(consultaEntidadVacia);
        setModalFormConsultaOpen(true);
    };

    const handleEditarConsulta = () => {
        if (consultaSeleccionada.iIdConsulta === 0) {
            funcAlert("Seleccione una consulta de la tabla para continuar", "warning");
            return;
        }
        if (consultaSeleccionada.iIdEstatusConsulta === EnumEstatusConsulta.Cancelado) {
            funcAlert("Las consultas canceladas no se pueden reprogramar", "warning");
            return;
        }
        if (consultaSeleccionada.iIdEstatusConsulta === EnumEstatusConsulta.Finalizado) {
            funcAlert("Las consultas que ya han finalizado no se pueden reprogramar", "warning");
            return;
        }
        if (consultaSeleccionada.iIdEstatusConsulta === EnumEstatusConsulta.EnConsulta) {
            funcAlert("El paciente ya se encuentra consultando", "warning");
            return;
        }
        setConsultaParaModal(consultaSeleccionada);
        setModalFormConsultaOpen(true);
    };

    const handleCancelarConsulta = () => {
        if (consultaSeleccionada.iIdConsulta === 0) {
            funcAlert("Seleccione una consulta de la tabla para continuar", "warning");
            return;
        }
        if (consultaSeleccionada.iIdEstatusConsulta === EnumEstatusConsulta.Cancelado) {
            funcAlert("Seleccione una consulta que no haya sido cancelada", "warning");
            return;
        }
        if (consultaSeleccionada.iIdEstatusConsulta === EnumEstatusConsulta.Finalizado) {
            funcAlert("Las consultas que ya han finalizado no se pueden cancelar", "warning");
            return;
        }
        if (consultaSeleccionada.iIdEstatusConsulta === EnumEstatusConsulta.EnConsulta) {
            funcAlert("El paciente ya se encuentra consultando", "warning");
            return;
        }
        setModalCancelarConsultaOpen(true);
    };

    const funcCancelarConsulta = async () => {
        const entNuevaConsulta = {
            consulta: {
                iIdConsulta: consultaSeleccionada.iIdConsulta,
                iIdColaborador: usuarioColaborador.iIdColaborador,
            },
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
        };

        funcLoader(true, "Cancelando consulta...");

        const response = await callcenterController.funcCancelarConsulta(entNuevaConsulta);

        if (response.Code === 0) {
            setModalCancelarConsultaOpen(false);
            setConsultaSeleccionada(consultaEntidadVacia);
            await funcGetConsultas();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    return (
        <Fragment>
            <MeditocHeader1 title={permisos.Nombre}>
                {permisos.Botones["1"] !== undefined && ( //Nueva consulta
                    <Tooltip title={permisos.Botones["1"].Nombre} arrow>
                        <span>
                            <IconButton onClick={handleClickNuevaConsulta} disabled={usuarioColaborador === null}>
                                <AddIcon className="color-0" />
                            </IconButton>
                        </span>
                    </Tooltip>
                )}
                {permisos.Botones["2"] !== undefined && ( //Reprogramar consulta
                    <Tooltip title={permisos.Botones["2"].Nombre} arrow>
                        <span>
                            <IconButton onClick={handleEditarConsulta} disabled={usuarioColaborador === null}>
                                <EditIcon className="color-0" />
                            </IconButton>
                        </span>
                    </Tooltip>
                )}
                {permisos.Botones["3"] !== undefined && ( //Cancelar consulta
                    <Tooltip title={permisos.Botones["3"].Nombre} arrow>
                        <span>
                            <IconButton onClick={handleCancelarConsulta} disabled={usuarioColaborador === null}>
                                <BlockIcon className="color-0" />
                            </IconButton>
                        </span>
                    </Tooltip>
                )}
                {permisos.Botones["4"] !== undefined && ( //Actualizar tabla
                    <Tooltip title={permisos.Botones["4"].Nombre} arrow>
                        <span>
                            <IconButton onClick={funcGetConsultas} disabled={usuarioColaborador === null}>
                                <ReplayIcon className="color-0" />
                            </IconButton>
                        </span>
                    </Tooltip>
                )}
            </MeditocHeader1>
            <MeditocBody>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <MeditocSubtitulo title="FILTRAR CONSULTAS" />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <DatePicker
                            variant="inline"
                            inputVariant="outlined"
                            label="Fecha de:"
                            helperText=""
                            format="dd/MM/yyyy"
                            InputProps={{
                                endAdornment: (
                                    <InputAdornment position="end">
                                        <IconButton>
                                            <DateRangeIcon />
                                        </IconButton>
                                    </InputAdornment>
                                ),
                            }}
                            onChange={(date) =>
                                setFiltroForm({
                                    ...filtroForm,
                                    txtFechaDe: date,
                                    txtFechaA: date,
                                })
                            }
                            disabled={usuarioColaborador === null}
                            fullWidth
                            value={filtroForm.txtFechaDe}
                        />
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <DatePicker
                            variant="inline"
                            inputVariant="outlined"
                            label="Fecha a:"
                            helperText=""
                            format="dd/MM/yyyy"
                            InputProps={{
                                endAdornment: (
                                    <InputAdornment position="end">
                                        <IconButton>
                                            <DateRangeIcon />
                                        </IconButton>
                                    </InputAdornment>
                                ),
                            }}
                            disabled={usuarioColaborador === null}
                            onChange={(date) =>
                                setFiltroForm({
                                    ...filtroForm,
                                    txtFechaA: date,
                                })
                            }
                            fullWidth
                            value={filtroForm.txtFechaA}
                        />
                    </Grid>
                    <Grid item xs={12} className="right">
                        <Button
                            variant="contained"
                            style={{ color: "#555", marginRight: "10px" }}
                            onClick={() => {
                                setFiltroForm({
                                    txtFechaDe: null,
                                    txtFechaA: null,
                                });
                            }}
                            disabled={usuarioColaborador === null}
                        >
                            LIMIPIAR
                        </Button>
                        <Button
                            variant="contained"
                            color="primary"
                            onClick={funcGetConsultas}
                            disabled={usuarioColaborador === null}
                        >
                            FILTRAR
                        </Button>
                    </Grid>
                    <Grid item xs={12}>
                        <MeditocTable
                            columns={columns}
                            data={listaConsultas}
                            rowSelected={consultaSeleccionada}
                            setRowSelected={setConsultaSeleccionada}
                            mainField="iIdConsulta"
                            doubleClick={permisos.Botones["2"] !== undefined ? handleEditarConsulta : emptyFunc}
                        />
                    </Grid>
                </Grid>
            </MeditocBody>
            <FormConsulta
                entConsulta={consultaParaModal}
                open={modalFormConsultaOpen}
                setOpen={setModalFormConsultaOpen}
                funcGetConsultas={funcGetConsultas}
                consultaEntidadVacia={consultaEntidadVacia}
                setConsultaSeleccionada={setConsultaSeleccionada}
                usuarioSesion={usuarioSesion}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
                usuarioColaborador={usuarioColaborador}
            />
            <MeditocConfirmacion
                title="Eliminar consulta"
                open={modalCancelarConsultaOpen}
                setOpen={setModalCancelarConsultaOpen}
                okFunc={funcCancelarConsulta}
            >
                ¿Desea cancelar esta consulta?
                <br />
                <br />
                Las consultas canceladas ya no pueden reprogramarse
            </MeditocConfirmacion>
            <MeditocHelper title="Estatus de consulta">
                <div>
                    <QueryBuilderIcon style={{ color: blue[500], verticalAlign: "middle" }} />
                    {"  "}
                    Creado/Programado
                </div>
                <div>
                    <UpdateIcon style={{ color: blue[500], verticalAlign: "middle" }} />
                    {"  "}
                    Reprogramado
                </div>
                <div>
                    <TimerIcon style={{ color: yellow[500], verticalAlign: "middle" }} />
                    {"  "}
                    En consulta
                </div>
                <div>
                    <DoneIcon style={{ color: green[500], verticalAlign: "middle" }} />
                    {"  "}
                    Finalizado
                </div>
                <div>
                    <BlockIcon style={{ color: red[500], verticalAlign: "middle" }} />
                    {"  "}
                    Cancelado
                </div>
            </MeditocHelper>
        </Fragment>
    );
};

Administrador.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    title: PropTypes.any,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default Administrador;
