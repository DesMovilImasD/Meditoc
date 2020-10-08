import { EnumOrigen, EnumReportesTabs, EnumStatusConekta, EnumTipoPago } from "../../../../configurations/enumConfig";
import { Grid, IconButton, InputAdornment, MenuItem, TextField, Tooltip } from "@material-ui/core";
import React, { Fragment, useEffect } from "react";

import { Autocomplete } from "@material-ui/lab";
import CloudDownloadIcon from "@material-ui/icons/CloudDownload";
import { DatePicker } from "@material-ui/pickers";
import DateRangeIcon from "@material-ui/icons/DateRange";
import MeditocBody from "../../../utilidades/MeditocBody";
import MeditocHeader1 from "../../../utilidades/MeditocHeader1";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";
import MeditocTabBody from "../../../utilidades/MeditocTabBody";
import MeditocTabHeader from "../../../utilidades/MeditocTabHeader";
import MeditocTabPanel from "../../../utilidades/MeditocTabPanel";
import PromocionesController from "../../../../controllers/PromocionesController";
import PropTypes from "prop-types";
import ReplayIcon from "@material-ui/icons/Replay";
import ReportesController from "../../../../controllers/ReportesController";
import ResumenAdmin from "./ResumenAdmin";
import ResumenConekta from "./ResumenConekta";
import { useState } from "react";

const ReportesVentas = (props) => {
    const { permisos, funcLoader, funcAlert } = props;

    const reportesController = new ReportesController();
    const promocionesController = new PromocionesController();

    const [tabIndex, setTabIndex] = useState(0);

    const [entVentas, setEntVentas] = useState(null);
    const [listaCupones, setListaCupones] = useState([]);

    const formFiltroVacio = {
        txtFechaDe: null,
        txtFechaA: null,
        txtEstatus: "",
        txtOrigen: "",
        txtTipoPago: "",
        txtOrden: "",
        txtFolio: "",
        txtFolioEmpresa: "",
        txtCupon: null,
    };

    const [filtroForm, setFiltroForm] = useState(formFiltroVacio);

    const handleChangeFiltro = (e) => {
        setFiltroForm({
            ...filtroForm,
            [e.target.name]: e.target.value,
        });
    };

    const funcGetVentas = async (clean = false) => {
        funcLoader(true, "Obteniendo reporte de venta...");
        let response;
        if (!clean) {
            response = await reportesController.funcObtenerReporteGlobal(
                filtroForm.txtFolio,
                filtroForm.txtFolioEmpresa,
                "",
                "",
                filtroForm.txtOrigen,
                filtroForm.txtOrden,
                filtroForm.txtEstatus,
                filtroForm.txtCupon === null ? "" : filtroForm.txtCupon,
                filtroForm.txtTipoPago,
                filtroForm.txtFechaDe === null ? null : filtroForm.txtFechaDe.toISOString(),
                filtroForm.txtFechaA === null ? null : filtroForm.txtFechaA.toISOString()
            );
        } else {
            response = await reportesController.funcObtenerReporteGlobal();
            setFiltroForm(formFiltroVacio);
        }
        if (response.Code === 0) {
            setEntVentas(response.Result);
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    const funcGetCupones = async () => {
        funcLoader(true, "Obteniendo lista de cupones...");

        const response = await promocionesController.funcAutocompletarCupon();

        if (response.Code === 0) {
            setListaCupones(response.Result);
        }

        funcLoader();
    };

    const funcDescargaReporte = async () => {
        funcLoader(true, "Descargando reporte de venta...");
        const response = await reportesController.funcDescargarReporteVentas(
            filtroForm.txtFolio,
            tabIndex === EnumReportesTabs.Conekta ? "" : filtroForm.txtFolioEmpresa,
            "",
            "",
            tabIndex === EnumReportesTabs.Administrativo ? "" : filtroForm.txtOrigen,
            tabIndex === EnumReportesTabs.Administrativo ? "" : filtroForm.txtOrden,
            tabIndex === EnumReportesTabs.Administrativo ? "" : filtroForm.txtEstatus,
            tabIndex === EnumReportesTabs.Administrativo ? "" : filtroForm.txtCupon === null ? "" : filtroForm.txtCupon,
            tabIndex === EnumReportesTabs.Administrativo ? "" : filtroForm.txtTipoPago,
            filtroForm.txtFechaDe === null ? null : filtroForm.txtFechaDe.toISOString(),
            filtroForm.txtFechaA === null ? null : filtroForm.txtFechaA.toISOString()
        );
        if (response.ok) {
            let file = await response.blob();
            let link = document.createElement("a");
            link.href = window.URL.createObjectURL(file);
            link.download = "ReporteVenta.xlsx";
            link.click();
            link.remove();

            funcAlert("El reporte de venta se descargó exitosamente", "success");
        } else {
            funcAlert("Ocurrió un error al descargar el reporte de venta");
        }
        funcLoader();
    };

    const getData = async () => {
        await funcGetVentas();
        await funcGetCupones();
    };
    useEffect(() => {
        getData();
        // eslint-disable-next-line
    }, []);

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
            {entVentas !== null && (
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
                                onChange={(date) => setFiltroForm({ ...filtroForm, txtFechaDe: date, txtFechaA: date })}
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
                                name="txtOrigen"
                                label="Origen:"
                                variant="outlined"
                                fullWidth
                                select
                                disabled={tabIndex === EnumReportesTabs.Administrativo}
                                onChange={handleChangeFiltro}
                                value={filtroForm.txtOrigen}
                            >
                                <MenuItem value="">Todos</MenuItem>
                                <MenuItem value={EnumOrigen.APP}>APP</MenuItem>
                                <MenuItem value={EnumOrigen.WEB}>WEB</MenuItem>
                                <MenuItem value={EnumOrigen.PanelAdministrativo}>
                                    Panel administrativo (Empresas)
                                </MenuItem>
                            </TextField>
                        </Grid>
                        <Grid item sm={4} xs={12}>
                            <TextField
                                name="txtEstatus"
                                label="Estatus:"
                                variant="outlined"
                                disabled={tabIndex === EnumReportesTabs.Administrativo}
                                fullWidth
                                select
                                onChange={handleChangeFiltro}
                                value={filtroForm.txtEstatus}
                            >
                                <MenuItem value="">Todos</MenuItem>
                                <MenuItem value={EnumStatusConekta.Paid}>{EnumStatusConekta.Paid}</MenuItem>
                                <MenuItem value={EnumStatusConekta.Declined}>{EnumStatusConekta.Declined}</MenuItem>
                            </TextField>
                        </Grid>

                        <Grid item sm={4} xs={12}>
                            <TextField
                                name="txtTipoPago"
                                label="Tipo de pago:"
                                variant="outlined"
                                disabled={tabIndex === EnumReportesTabs.Administrativo}
                                fullWidth
                                select
                                onChange={handleChangeFiltro}
                                value={filtroForm.txtTipoPago}
                            >
                                <MenuItem value="">Todos</MenuItem>
                                <MenuItem value={EnumTipoPago.Credit}>Crédito</MenuItem>
                                <MenuItem value={EnumTipoPago.Debit}>Débito</MenuItem>
                            </TextField>
                        </Grid>
                        <Grid item sm={4} xs={12}>
                            <Autocomplete
                                id="txtCupon"
                                options={listaCupones}
                                disabled={tabIndex === EnumReportesTabs.Administrativo}
                                //getOptionLabel={(cupon) => cupon}
                                renderInput={(props) => (
                                    <TextField {...props} name="txtCupon" label="Cupón" variant="outlined" fullWidth />
                                )}
                                noOptionsText="Sin coincidencias"
                                onChange={(e, value) => setFiltroForm({ ...filtroForm, txtCupon: value })}
                                value={filtroForm.txtCupon}
                            />
                        </Grid>
                        <Grid item sm={4} xs={12}>
                            <TextField
                                name="txtOrden"
                                label="Órden Conekta:"
                                variant="outlined"
                                fullWidth
                                onChange={handleChangeFiltro}
                                value={filtroForm.txtOrden}
                                disabled={tabIndex === EnumReportesTabs.Administrativo}
                            />
                        </Grid>
                        <Grid item sm={4} xs={12}>
                            <TextField
                                name="txtFolioEmpresa"
                                disabled={tabIndex === EnumReportesTabs.Conekta}
                                label="Folio Empresa:"
                                variant="outlined"
                                fullWidth
                                onChange={handleChangeFiltro}
                                value={filtroForm.txtFolioEmpresa}
                            />
                        </Grid>
                        <Grid item sm={4} xs={12}>
                            <TextField
                                name="txtFolio"
                                label="Folio Paciente:"
                                variant="outlined"
                                fullWidth
                                onChange={handleChangeFiltro}
                                value={filtroForm.txtFolio}
                            />
                        </Grid>

                        <MeditocModalBotones
                            okMessage="Filtrar"
                            cancelMessage="Limpiar"
                            okFunc={() => funcGetVentas(false)}
                            cancelFunc={() => funcGetVentas(true)}
                        />
                        <Grid item xs={12}>
                            <MeditocTabHeader
                                tabs={["VENTAS CONEKTA", "VENTAS ADMINISTRATIVO"]}
                                index={tabIndex}
                                setIndex={setTabIndex}
                            />
                            <MeditocTabBody index={tabIndex} setIndex={setTabIndex}>
                                <MeditocTabPanel id={EnumReportesTabs.Conekta} index={tabIndex}>
                                    <ResumenConekta
                                        entVentas={entVentas}
                                        funcAlert={funcAlert}
                                        funcLoader={funcLoader}
                                        permisos={permisos}
                                    />
                                </MeditocTabPanel>
                                <MeditocTabPanel id={EnumReportesTabs.Administrativo} index={tabIndex}>
                                    <ResumenAdmin
                                        entVentas={entVentas}
                                        funcAlert={funcAlert}
                                        funcLoader={funcLoader}
                                        permisos={permisos}
                                    />
                                </MeditocTabPanel>
                            </MeditocTabBody>
                        </Grid>
                    </Grid>
                </MeditocBody>
            )}
        </Fragment>
    );
};

ReportesVentas.propTypes = {
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    title: PropTypes.any,
};

export default ReportesVentas;
