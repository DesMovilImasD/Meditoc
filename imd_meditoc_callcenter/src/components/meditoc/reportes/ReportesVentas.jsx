import {
  IconButton,
  Tooltip,
  Grid,
  Divider,
  InputAdornment,
  TextField,
} from "@material-ui/core";
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

const ReportesVentas = (props) => {
  const { usuarioSesion, funcLoader, funcAlert } = props;

  const reportesController = new ReportesController();

  const [tabIndex, setTabIndex] = useState(0);

  const columnas = [
    { title: "ID", field: "iIdFolio", align: "center", hidden: true },
    { title: "Folio", field: "sFolio", align: "center" },
    { title: "Origen", field: "sOrigen", align: "center" },
    { title: "Empresa", field: "sEmpresa", align: "center" },
    { title: "Producto", field: "sProducto", align: "center" },
    { title: "Costo", field: "fCosto", align: "center" },
    { title: "Total Pagado", field: "nAmountPaid", align: "center" },
    { title: "Número Orden", field: "sOrderId", align: "center" },
    {
      title: "Fecha de vencimiento",
      field: "sFechaVencimiento",
      align: "center",
    },
  ];

  const [entVentas, setEntVentas] = useState({
    lstFolios: [],
  });

  const [folioSeleccionado, setFolioSeleccionado] = useState({
    iIdFolio: 0,
  });

  const funcGetVentas = async () => {
    funcLoader(true, "Obteniendo folios...");

    const response = await reportesController.funcObtenerReporteVentas();

    if (response.Code === 0) {
      setEntVentas(response.Result);
    } else {
      funcAlert(response.Message);
    }
    funcLoader();
  };

  useEffect(() => {
    funcGetVentas();
  }, []);

  return (
    <Fragment>
      <MeditocHeader1 title="REPORTES VENTAS">
        <Tooltip title="Ver detalle">
          <IconButton>
            <FormatListBulletedIcon className="color-0" />
          </IconButton>
        </Tooltip>
        <Tooltip title="Descargar Reporte">
          <IconButton>
            <CloudDownloadIcon className="color-0" />
          </IconButton>
        </Tooltip>
      </MeditocHeader1>
      <MeditocBody>
        <MeditocTabHeader
          tabs={["VENTAS CONEKTA", "VENTAS ADMINISTRATIVO"]}
          index={tabIndex}
          setIndex={setTabIndex}
        />
        <Grid container spacing={0}>
          <Grid item xs={12}>
            <MeditocTabBody index={tabIndex} setIndex={setTabIndex}>
              <MeditocTabPanel id={0} index={tabIndex}>
                <Grid container spacing={3}>
                  <Grid item sm={6} xs={12}>
                    <span className="rob-nor bold size-15 color-4">
                      FILTROS
                    </span>
                    <Divider />
                    <br></br>
                    <Grid container spacing={3}>
                      <Grid item sm={6} xs={12}>
                        <DatePicker
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
                        />
                      </Grid>
                      <Grid item sm={6} xs={12}>
                        <DatePicker
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
                        />
                      </Grid>
                      <Grid item sm={6} xs={12}>
                        <TextField
                          name="txtEstatus"
                          label="Estatus:"
                          variant="outlined"
                          fullWidth
                        ></TextField>
                      </Grid>
                    </Grid>
                  </Grid>
                  <Grid item sm={6} xs={12}>
                    <span className="rob-nor bold size-15 color-4">
                      RESUMÉN DE VENTAS
                    </span>
                    <Divider />
                  </Grid>
                  <Grid item xs={12}>
                    <span className="rob-nor bold size-15 color-4">
                      RESULTADOS
                    </span>
                    <Divider />
                    <MeditocTable
                      columns={columnas}
                      data={entVentas.lstFolios.map((folio) => ({
                        ...folio,
                        sEmpresa: folio.entEmpresa.sNombre,
                        sProducto: folio.entProducto.sNombre,
                        fCosto: folio.entProducto.fCosto,
                        nAmountPaid: folio.entOrden.nAmountPaid,
                        sOrderId: folio.entOrden.sOrderId,
                      }))}
                      rowSelected={folioSeleccionado}
                      setRowSelected={setFolioSeleccionado}
                      mainField="iIdFolio"
                    ></MeditocTable>
                  </Grid>
                </Grid>
              </MeditocTabPanel>
              <MeditocTabPanel id={1} index={tabIndex}></MeditocTabPanel>
            </MeditocTabBody>
          </Grid>
        </Grid>
      </MeditocBody>
    </Fragment>
  );
};

export default ReportesVentas;
