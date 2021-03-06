import { Grid, IconButton, InputAdornment, MenuItem, TextField, Tooltip } from "@material-ui/core";
import React, { Fragment, useEffect } from "react";

import CloudDownloadIcon from "@material-ui/icons/CloudDownload";
import { DatePicker } from "@material-ui/pickers";
import DateRangeIcon from "@material-ui/icons/DateRange";
import DetalleDoctor from "./DetalleDoctor";
import EspecialidadController from "../../../../controllers/EspecialidadController";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocInfoNumber from "../../../utilidades/MeditocInfoNumber";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";
import MeditocTable from "../../../utilidades/MeditocTable";
import PropTypes from "prop-types";
import ReplayIcon from "@material-ui/icons/Replay";
import ReportesController from "../../../../controllers/ReportesController";
import VisibilityIcon from "@material-ui/icons/Visibility";
import { cellProps } from "../../../../configurations/dataTableIconsConfig";
import { useState } from "react";

const ReportesDoctores = (props) => {
    const { permisos, entCatalogos, funcLoader, funcAlert } = props;

    const reportesController = new ReportesController();
    const especialidadController = new EspecialidadController();

    const [listaEspecialidades, setListaEspecialidades] = useState([]);

    const columnas = [
        { title: "ID", field: "iIdDoctor", ...cellProps },
        { title: "Nombre", field: "sNombre", ...cellProps },
        { title: "Tipo de doctor", field: "sTipoDoctor", ...cellProps },
        { title: "Especialidad", field: "sEspecialidad", ...cellProps },
        { title: "Consultas", field: "iTotalConsultas", ...cellProps },
        { title: "Ver", field: "sDetalle", ...cellProps, sorting: false },
    ];

    const formFiltroVacio = {
        txtFechaDe: null,
        txtFechaA: null,
        txtTipoDoctor: "",
        txtEspecialidad: "",
        txtEstatusConsulta: "",
    };

    const [filtroForm, setFiltroForm] = useState(formFiltroVacio);

    const handleChangeFiltro = (e) => {
        setFiltroForm({
            ...filtroForm,
            [e.target.name]: e.target.value,
        });
    };

    const [entDoctores, setEntDoctores] = useState(null);

    const [doctorSeleccionado, setDoctorSeleccionado] = useState({
        iIdDoctor: 0,
        lstConsultas: [],
    });

    const [modalDetalleDoctorOpen, setModalDetalleDoctorOpen] = useState(false);

    const [listaDoctores, setListaDoctores] = useState([]);

    const handleClickVerDetalle = () => {
        if (doctorSeleccionado.iIdDoctor === 0) {
            funcAlert("Seleccione una orden para continuar");
            return;
        }

        setModalDetalleDoctorOpen(true);
    };

    const funcGetDoctores = async (clean = false) => {
        funcLoader(true, "Obteniendo lista de doctores...");
        let response;

        if (!clean) {
            response = await reportesController.funcObtenerReporteDoctores(
                "",
                "",
                filtroForm.txtTipoDoctor,
                filtroForm.txtEspecialidad,
                filtroForm.txtEstatusConsulta,
                "",
                "",
                null,
                null,
                filtroForm.txtFechaDe === null ? null : filtroForm.txtFechaDe.toISOString(),
                filtroForm.txtFechaA === null ? null : filtroForm.txtFechaA.toISOString()
            );
        } else {
            response = await reportesController.funcObtenerReporteDoctores();
            setFiltroForm(formFiltroVacio);
        }

        if (response.Code === 0) {
            setEntDoctores(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const funcDescargaReporte = async () => {
        funcLoader(true, "Descargando reporte de doctores...");
        const response = await reportesController.funcDescargarReporteDoctores(
            "",
            "",
            filtroForm.txtTipoDoctor,
            filtroForm.txtEspecialidad,
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

            funcAlert("El reporte de doctores se descargó exitosamente", "success");
        } else {
            funcAlert("Ocurrió un error al descargar el reporte de doctores");
        }
        funcLoader();
    };

    const funcGetEspecialidades = async () => {
        funcLoader(true, "Consultando especialidades...");

        const response = await especialidadController.funcGetEspecialidad();

        if (response.Code === 0) {
            setListaEspecialidades(response.Result);
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
    };

    const handleClickDetalle = () => {
        setModalDetalleDoctorOpen(true);
    };

    const getData = async () => {
        await funcGetDoctores(false);
        await funcGetEspecialidades();
    };

    useEffect(() => {
        getData();
        // eslint-disable-next-line
    }, []);

    useEffect(() => {
        if (entDoctores !== null) {
            setListaDoctores(
                entDoctores.lstDoctores.map((doctor) => ({
                    ...doctor,
                    sDetalle: (
                        <Tooltip title="Ver detalle" arrow placement="top">
                            <IconButton onClick={handleClickDetalle}>
                                <VisibilityIcon className="color-1" />
                            </IconButton>
                        </Tooltip>
                    ),
                }))
            );
        }
    }, [entDoctores]);

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
                        <IconButton onClick={getData}>
                            <ReplayIcon className="color-0" />
                        </IconButton>
                    </Tooltip>
                )}
            </MeditocHeader1>
            {entDoctores !== null && (
                <Fragment>
                    <MeditocBody>
                        <Grid container spacing={3}>
                            <Grid item xs={12}>
                                <MeditocSubtitulo title="FILTROS" />
                            </Grid>
                            <Grid item sm={6} xs={12}>
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
                            <Grid item sm={6} xs={12}>
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
                                    name="txtTipoDoctor"
                                    label="Tipo de doctor:"
                                    variant="outlined"
                                    fullWidth
                                    select
                                    onChange={handleChangeFiltro}
                                    value={filtroForm.txtTipoDoctor}
                                >
                                    <MenuItem value="">Todos</MenuItem>
                                    {entCatalogos.catTipoDoctor.map((tipoDoctor) => (
                                        <MenuItem key={tipoDoctor.fiId} value={tipoDoctor.fiId}>
                                            {tipoDoctor.fsDescripcion}
                                        </MenuItem>
                                    ))}
                                </TextField>
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
                            <Grid item sm={4} xs={12}>
                                <TextField
                                    name="txtEspecialidad"
                                    label="Especialidad:"
                                    variant="outlined"
                                    fullWidth
                                    select
                                    SelectProps={{ MenuProps: { PaperProps: { style: { maxHeight: 300 } } } }}
                                    onChange={handleChangeFiltro}
                                    value={filtroForm.txtEspecialidad}
                                >
                                    <MenuItem value="">Todas las especialidades</MenuItem>
                                    {listaEspecialidades
                                        .sort((a, b) => (a.sNombre > b.sNombre ? 1 : -1))
                                        .map((especialidad) => (
                                            <MenuItem
                                                key={especialidad.iIdEspecialidad}
                                                value={especialidad.iIdEspecialidad.toString()}
                                            >
                                                {especialidad.sNombre}
                                            </MenuItem>
                                        ))}
                                </TextField>
                            </Grid>
                            <MeditocModalBotones
                                okMessage="FILTRAR"
                                okFunc={() => funcGetDoctores(false)}
                                cancelMessage="LIMPIAR"
                                cancelFunc={() => funcGetDoctores(true)}
                            />
                            <Grid item md={4} sm={6} xs={12} className="center">
                                <MeditocInfoNumber
                                    label="TOTAL DE DOCTORES"
                                    value={entDoctores.iTotalDoctores}
                                    color="color-2"
                                />
                            </Grid>
                            <Grid item md={4} sm={6} xs={12} className="center">
                                <MeditocInfoNumber
                                    label="TOTAL DE CONSULTAS"
                                    value={entDoctores.iTotalConsultas}
                                    color="color-1"
                                />
                            </Grid>
                            <Grid item md={4} sm={6} xs={12} className="center">
                                <MeditocInfoNumber
                                    label="TOTAL DE PACIENTES"
                                    value={entDoctores.iTotalPacientes}
                                    color="color-3"
                                />
                            </Grid>
                            <Grid item xs={12} className="center">
                                <MeditocTable
                                    columns={columnas}
                                    data={listaDoctores}
                                    rowSelected={doctorSeleccionado}
                                    setRowSelected={setDoctorSeleccionado}
                                    mainField="iIdDoctor"
                                    doubleClick={handleClickVerDetalle}
                                />
                            </Grid>
                        </Grid>
                    </MeditocBody>
                    <DetalleDoctor
                        entDoctor={doctorSeleccionado}
                        open={modalDetalleDoctorOpen}
                        setOpen={setModalDetalleDoctorOpen}
                        funcLoader={funcLoader}
                        funcAlert={funcAlert}
                    />
                </Fragment>
            )}
        </Fragment>
    );
};

ReportesDoctores.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    permisos: PropTypes.shape({
        Botones: PropTypes.any,
        Nombre: PropTypes.any,
    }),
};

export default ReportesDoctores;
