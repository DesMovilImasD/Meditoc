import { IconButton, Tooltip, Grid, Divider, InputAdornment, TextField, MenuItem } from "@material-ui/core";
import React, { Fragment, useEffect } from "react";
import { useState } from "react";
import MeditocHeader1 from "../../utilidades/MeditocHeader1";
import FormatListBulletedIcon from "@material-ui/icons/FormatListBulleted";
import CloudDownloadIcon from "@material-ui/icons/CloudDownload";
import MeditocBody from "../../utilidades/MeditocBody";
import MeditocTable from "../../utilidades/MeditocTable";
import ReportesController from "../../../controllers/ReportesController";
import MeditocTabHeader from "../../utilidades/MeditocTabHeader";
import MeditocTabBody from "../../utilidades/MeditocTabBody";
import MeditocTabPanel from "../../utilidades/MeditocTabPanel";
import { DatePicker } from "@material-ui/pickers";
import DateRangeIcon from "@material-ui/icons/DateRange";
import ResumenOrdenes from "./ResumenOrdenes";
import ResumeNumero from "./ResumeNumero";
import ResumenEmpresas from "./ResumenEmpresas";
import { EnumOrigen, EnumStatusConekta, EnumTipoPago } from "../../../configurations/enumConfig";
import PromocionesController from "../../../controllers/PromocionesController";
import { Autocomplete } from "@material-ui/lab";
import MeditocModalBotones from "../../utilidades/MeditocModalBotones";

const ReportesVentas = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

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
    }, []);

    return (
        <Fragment>
            <MeditocHeader1 title="REPORTES VENTAS">
                {/* <Tooltip title="Ver detalle">
                    <IconButton>
                        <FormatListBulletedIcon className="color-0" />
                    </IconButton>
                </Tooltip> */}
                <Tooltip title="Descargar Reporte">
                    <IconButton onClick={funcDescargaReporte}>
                        <CloudDownloadIcon className="color-0" />
                    </IconButton>
                </Tooltip>
            </MeditocHeader1>
            {entVentas !== null && (
                <MeditocBody>
                    <Grid container spacing={3}>
                        <Grid item xs={12}>
                            <span className="rob-nor bold size-20 color-4">FILTROS</span>
                            <Divider />
                            <br></br>
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
                            />
                        </Grid>
                        <Grid item sm={4} xs={12}>
                            <TextField
                                name="txtFolioEmpresa"
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
                                <MeditocTabPanel id={0} index={tabIndex}>
                                    <ResumenOrdenes entVentas={entVentas} />
                                </MeditocTabPanel>
                                <MeditocTabPanel id={1} index={tabIndex}>
                                    <ResumenEmpresas entVentas={entVentas} />
                                </MeditocTabPanel>
                            </MeditocTabBody>
                        </Grid>
                    </Grid>
                </MeditocBody>
            )}
        </Fragment>
    );
};

export default ReportesVentas;
