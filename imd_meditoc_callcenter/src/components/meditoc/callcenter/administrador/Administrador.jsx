import { Button, Divider, Grid, IconButton, InputAdornment, Tooltip } from "@material-ui/core";
import React from "react";
import { useState } from "react";
import { useEffect } from "react";
import { Fragment } from "react";
import AddIcon from "@material-ui/icons/Add";
import EditIcon from "@material-ui/icons/Edit";
import DeleteIcon from "@material-ui/icons/Delete";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import CallCenterController from "../../../../controllers/CallCenterController";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocTable from "../../../utilidades/MeditocTable";
import FormConsulta from "./FormConsulta";
import MeditocConfirmacion from "../../../utilidades/MeditocConfirmacion";
import { DatePicker } from "@material-ui/pickers";
import DateRangeIcon from "@material-ui/icons/DateRange";
import { SignalCellularNullSharp } from "@material-ui/icons";

const Administrador = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const colaboradorController = new ColaboradorController();
    const callcenterController = new CallCenterController();

    const columns = [
        { title: "ID", field: "iIdConsulta", align: "center", hidden: true },
        { title: "Inicio", field: "sFechaProgramadaInicio", align: "center" },
        { title: "Fin", field: "sFechaProgramadaFin", align: "center" },
        { title: "Paciente", field: "sNombrePaciente", align: "center" },
        { title: "Folio", field: "sFolio", align: "center" },
        { title: "Estatus", field: "sEstatusConsulta", align: "center" },
        { title: "Creado", field: "sFechaCreacion", align: "center" },
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
            setListaConsultas(response.Result);
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    useEffect(() => {
        if (usuarioColaborador !== null) {
            funcGetConsultas();
        }
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
        if (consultaSeleccionada.iIdEstatusConsulta === 5) {
            funcAlert("Las consultas canceladas no se pueden reprogramar", "warning");
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
        if (consultaSeleccionada.iIdEstatusConsulta === 5) {
            funcAlert("Seleccione una consulta que no haya sido cancelada", "warning");
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
            <MeditocHeader1 title="ADMINISTRADOR CONSULTAS">
                <Tooltip title="Nueva consulta" arrow>
                    <IconButton onClick={handleClickNuevaConsulta}>
                        <AddIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Reprogramar consulta" arrow>
                    <IconButton onClick={handleEditarConsulta}>
                        <EditIcon className="color-0" />
                    </IconButton>
                </Tooltip>
                <Tooltip title="Cancelar consulta" arrow>
                    <IconButton onClick={handleCancelarConsulta}>
                        <DeleteIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </MeditocHeader1>
            <MeditocBody>
                <Grid container spacing={3}>
                    <Grid item xs={12}>
                        <span className="rob-nor bold size-15 color-4">FILTRAR CONSULTAS</span>
                        <Divider />
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
                                })
                            }
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
                        >
                            LIMIPIAR
                        </Button>
                        <Button variant="contained" color="primary" onClick={funcGetConsultas}>
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
        </Fragment>
    );
};

export default Administrador;
