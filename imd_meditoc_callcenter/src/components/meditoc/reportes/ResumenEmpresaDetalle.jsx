import { Divider, Grid } from "@material-ui/core";
import React, { Fragment } from "react";
import InfoField from "../../utilidades/InfoField";
import MeditocModal from "../../utilidades/MeditocModal";
import MeditocTableSimple from "../../utilidades/MeditocTableSimple";
import ResumeInfo from "./ResumeInfo";

const ResumenEmpresaDetalle = (props) => {
    const { entEmpresa, open, setOpen } = props;

    const columnas = [
        { title: "Folio", field: "sFolio", align: "center" },
        //{ title: "Origen", field: "sOrigen", align: "center" },
        { title: "Producto", field: "sNombre", align: "center" },
        { title: "Precio", field: "nUnitPrice", align: "center" },
        { title: "Tipo", field: "sTipoProducto", align: "center" },
        { title: "Vencimiento", field: "sFechaVencimiento", align: "center" },
    ];

    return (
        <MeditocModal title={"Detalle de orden " + entEmpresa.sFolioEmpresa} size="large" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <ResumeInfo
                        label="Subtotal"
                        value={"$" + entEmpresa.dTotalSinIva.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                    <ResumeInfo
                        label="IVA"
                        value={"+$" + entEmpresa.dTotalIva.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                    <ResumeInfo
                        label="Total pagado"
                        value={entEmpresa.dTotal.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                </Grid>
                <Grid item xs={12}>
                    <span className="rob-nor bold size-20 color-4">DATOS DE EMPRESA</span>
                    <Divider />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Folio:" value={entEmpresa.sFolioEmpresa} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Nombre:" value={entEmpresa.sNombre} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Correo:" value={entEmpresa.sCorreo} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Folios generados:" value={entEmpresa.iTotalFolios} />
                </Grid>
                <Grid item xs={12}>
                    <span className="rob-nor bold size-20 color-4">FOLIOS</span>
                    <Divider />
                </Grid>
                <Grid item xs={12}>
                    <MeditocTableSimple
                        columns={columnas}
                        data={entEmpresa.lstProductos.map((folio) => ({
                            ...folio,
                            sFolio: folio.entFolio.sFolio,
                            sFechaVencimiento: folio.entFolio.sFechaVencimiento,
                            nUnitPrice: "$" + folio.nUnitPrice.toLocaleString("en", { minimumFractionDigits: 2 }),
                        }))}
                    />
                </Grid>
            </Grid>
        </MeditocModal>
    );
};

export default ResumenEmpresaDetalle;
