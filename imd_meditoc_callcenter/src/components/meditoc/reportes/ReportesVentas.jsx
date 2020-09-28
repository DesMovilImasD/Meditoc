import { IconButton, Tooltip, Grid, Divider, InputAdornment, TextField } from "@material-ui/core";
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

const ReportesVentas = (props) => {
    const { usuarioSesion, funcLoader, funcAlert } = props;

    const reportesController = new ReportesController();

    const [tabIndex, setTabIndex] = useState(0);

    const [entVentas, setEntVentas] = useState(null);

    const funcGetVentas = async () => {
        funcLoader(true, "Obteniendo reporte de venta...");

        const response = await reportesController.funcObtenerReporteGlobal();

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
            {entVentas !== null && (
                <MeditocBody>
                    <MeditocTabHeader
                        tabs={["VENTAS CONEKTA", "VENTAS ADMINISTRATIVO"]}
                        index={tabIndex}
                        setIndex={setTabIndex}
                    />
                    <MeditocTabBody index={tabIndex} setIndex={setTabIndex}>
                        <MeditocTabPanel id={0} index={tabIndex}>
                            <ResumenOrdenes entVentas={entVentas} />
                        </MeditocTabPanel>
                        <MeditocTabPanel id={1} index={tabIndex}></MeditocTabPanel>
                    </MeditocTabBody>
                </MeditocBody>
            )}
        </Fragment>
    );
};

export default ReportesVentas;
