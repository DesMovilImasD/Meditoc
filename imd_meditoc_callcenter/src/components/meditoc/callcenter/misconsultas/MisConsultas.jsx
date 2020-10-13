import { Button, Grid, IconButton, InputAdornment, MenuItem, TextField, Tooltip } from "@material-ui/core";
import React, { Fragment, useEffect, useState } from "react";

import CloudDownloadIcon from "@material-ui/icons/CloudDownload";
import ColaboradorController from "../../../../controllers/ColaboradorController";
import { DatePicker } from "@material-ui/pickers";
import DateRangeIcon from "@material-ui/icons/DateRange";
import DetalleConsulta from "../../reportes/doctores/DetalleConsulta";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocInfoNumber from "../../../utilidades/MeditocInfoNumber";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";
import MeditocTable from "../../../utilidades/MeditocTable";
import ReplayIcon from "@material-ui/icons/Replay";
import ReportesController from "../../../../controllers/ReportesController";
import { cellProps } from "../../../../configurations/dataTableIconsConfig";

const MisConsultas = (props) => {
    const { usuarioSesion, permisos, entCatalogos, funcLoader, funcAlert } = props;

    const columnas = [
        { title: "ID", field: "iIdConsulta", ...cellProps },
        { title: "Consulta inicio", field: "sFechaConsultaInicio", ...cellProps },
        { title: "Consulta fin", field: "sFechaConsultaFin", ...cellProps },
        { title: "Nombre", field: "name", ...cellProps },
        { title: "Estatus de consulta", field: "sEstatusConsulta", ...cellProps },
        { title: "Detalle", field: "sDetalle", ...cellProps },
    ];

    const reportesController = new ReportesController();
    const colaboradorController = new ColaboradorController();

    const formFiltroVacio = {
        txtFechaDe: null,
        txtFechaA: null,
        txtEstatusConsulta: "",
    };

    const [filtroForm, setFiltroForm] = useState(formFiltroVacio);
    const [usuarioColaborador, setUsuarioColaborador] = useState(null);
    const [reporteDoctor, setReporteDoctor] = useState(null);

    const [listaConsultas, setListaConsultas] = useState([]);

    const handleChangeFiltro = (e) => {
        setFiltroForm({
            ...filtroForm,
            [e.target.name]: e.target.value,
        });
    };

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

    const funcGetReporte = async (clean = false) => {
        funcLoader(true, "Obteniendo lista de doctores...");
        let response;

        if (!clean) {
            response = await reportesController.funcObtenerReporteDoctores(
                usuarioColaborador.iIdColaborador,
                "",
                "",
                "",
                filtroForm.txtEstatusConsulta,
                "",
                "",
                null,
                null,
                filtroForm.txtFechaDe === null ? null : filtroForm.txtFechaDe.toISOString(),
                filtroForm.txtFechaA === null ? null : filtroForm.txtFechaA.toISOString()
            );
        } else {
            response = await reportesController.funcObtenerReporteDoctores(usuarioColaborador.iIdColaborador);
            setFiltroForm(formFiltroVacio);
        }

        if (response.Code === 0) {
            if (response.Result.lstDoctores !== null) {
                if (response.Result.lstDoctores.length > 0) {
                    setReporteDoctor(response.Result);
                } else {
                    funcAlert("No se encontró información de consultas.");
                }
            } else {
                funcAlert("No se encontró información del colaborador");
            }
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcDescargaReporte = async () => {
        funcLoader(true, "Descargando reporte de doctores...");
        const response = await reportesController.funcDescargarReporteDoctores(
            usuarioColaborador.iIdColaborador,
            "",
            "",
            "",
            filtroForm.txtEstatusConsulta,
            "",
            "",
            null,
            null,
            filtroForm.txtFechaDe === null ? null : filtroForm.txtFechaDe.toISOString(),
            filtroForm.txtFechaA === null ? null : filtroForm.txtFechaA.toISOString()
        );
        if (response.ok) {
            let file = await response.blob();
            let link = document.createElement("a");
            link.href = window.URL.createObjectURL(file);
            link.download = "ReporteDoctores.xlsx";
            link.click();
            link.remove();

            funcAlert("El reporte se descargó exitosamente.", "success");
        } else {
            funcAlert("Ocurrió un error al descargar el reporte.");
        }
        funcLoader();
    };

    useEffect(() => {
        if (usuarioColaborador !== null) {
            funcGetReporte();
        }
        // eslint-disable-next-line
    }, [usuarioColaborador]);

    useEffect(() => {
        if (reporteDoctor !== null) {
            setListaConsultas(
                reporteDoctor.lstDoctores[0].lstConsultas.map((consulta) => ({
                    ...consulta,
                    name: consulta.entPaciente.name,
                    sFechaConsultaInicio:
                        consulta.sEstatusConsulta === "Creado/Programado" || consulta.sEstatusConsulta === "Cancelado"
                            ? consulta.sFechaProgramadaInicio
                            : consulta.sFechaConsultaInicio,
                    sFechaConsultaFin:
                        consulta.sEstatusConsulta === "Creado/Programado" || consulta.sEstatusConsulta === "Cancelado"
                            ? consulta.sFechaProgramadaFin
                            : consulta.sEstatusConsulta === "En consulta"
                            ? "Consultando"
                            : consulta.sFechaConsultaFin,
                    sDetalle: (
                        <Button variant="contained" color="primary" onClick={handleClickDetalleConsulta}>
                            DETALLE
                        </Button>
                    ),
                }))
            );
        }
    }, [reporteDoctor]);

    const [modalDetalleConsultaOpen, setModalDetalleConsultaOpen] = useState(false);
    const [consultaSeleccionada, setConsultaSeleccionada] = useState({ iIdConsulta: 0 });

    const handleClickDetalleConsulta = (id) => {
        //setIIdConsulta(id);
        setModalDetalleConsultaOpen(true);
    };

    return (
        <Fragment>
            <MeditocHeader1 title={permisos.Nombre}>
                {permisos.Botones["1"] !== undefined && ( //Descargar Reporte
                    <Tooltip title={permisos.Botones["1"].Nombre} arrow>
                        <IconButton onClick={funcDescargaReporte}>
                            <CloudDownloadIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
                {permisos.Botones["2"] !== undefined && ( //Actualizar Reporte
                    <Tooltip title={permisos.Botones["2"].Nombre} arrow>
                        <IconButton onClick={() => funcGetReporte(true)}>
                            <ReplayIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
            </MeditocHeader1>
            {usuarioColaborador !== null && reporteDoctor !== null && (
                <Fragment>
                    <MeditocBody>
                        <Grid container spacing={3}>
                            <Grid item xs={12}>
                                <MeditocSubtitulo title="FILTROS" />
                            </Grid>
                            <Grid item sm={4} xs={12}>
                                <DatePicker
                                    name="txtFechaDe"
                                    variant="inline"
                                    inputVariant="outlined"
                                    label="Fecha inicio:"
                                    fullWidth
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
                                        setFiltroForm({ ...filtroForm, txtFechaDe: date, txtFechaA: date })
                                    }
                                    value={filtroForm.txtFechaDe}
                                />
                            </Grid>
                            <Grid item sm={4} xs={12}>
                                <DatePicker
                                    name="txtFechaA"
                                    variant="inline"
                                    inputVariant="outlined"
                                    label="Fecha fin:"
                                    fullWidth
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
                                    onChange={(date) => setFiltroForm({ ...filtroForm, txtFechaA: date })}
                                    value={filtroForm.txtFechaA}
                                />
                            </Grid>
                            <Grid item sm={4} xs={12}>
                                <TextField
                                    name="txtEstatusConsulta"
                                    label="Estatus de consulta:"
                                    variant="outlined"
                                    fullWidth
                                    select
                                    onChange={handleChangeFiltro}
                                    value={filtroForm.txtEstatusConsulta}
                                >
                                    <MenuItem value="">Todos</MenuItem>
                                    {entCatalogos.catEstatusConsulta.map((estatus) => (
                                        <MenuItem key={estatus.fiId} value={estatus.fiId}>
                                            {estatus.fsDescripcion}
                                        </MenuItem>
                                    ))}
                                </TextField>
                            </Grid>
                            <MeditocModalBotones
                                okMessage="FILTRAR"
                                okFunc={() => funcGetReporte(false)}
                                cancelMessage="LIMPIAR"
                                cancelFunc={() => funcGetReporte(true)}
                            />
                            <Grid item sm={6} xs={12} className="center">
                                <MeditocInfoNumber
                                    label="TOTAL DE CONSULTAS"
                                    value={reporteDoctor.iTotalConsultas}
                                    color="color-1"
                                />
                            </Grid>
                            <Grid item sm={6} xs={12} className="center">
                                <MeditocInfoNumber
                                    label="TOTAL DE PACIENTES"
                                    value={reporteDoctor.iTotalPacientes}
                                    color="color-3"
                                />
                            </Grid>
                            <Grid item xs={12} className="center">
                                <MeditocTable
                                    columns={columnas}
                                    data={listaConsultas}
                                    rowSelected={consultaSeleccionada}
                                    setRowSelected={setConsultaSeleccionada}
                                    mainField="iIdConsulta"
                                    doubleClick={handleClickDetalleConsulta}
                                />
                            </Grid>
                        </Grid>
                    </MeditocBody>
                </Fragment>
            )}
            <DetalleConsulta
                entConsultaSeleccionada={consultaSeleccionada}
                open={modalDetalleConsultaOpen}
                setOpen={setModalDetalleConsultaOpen}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
            />
        </Fragment>
    );
};

export default MisConsultas;
