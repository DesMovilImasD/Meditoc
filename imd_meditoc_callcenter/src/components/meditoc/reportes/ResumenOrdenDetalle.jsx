import { Divider, Grid, Typography } from "@material-ui/core";
import React, { Fragment } from "react";
import { EnumStatusConekta } from "../../../configurations/enumConfig";
import InfoField from "../../utilidades/InfoField";
import MeditocModal from "../../utilidades/MeditocModal";
import MeditocTableSimple from "../../utilidades/MeditocTableSimple";
import ResumeInfo from "./ResumeInfo";
import ResumeNumero from "./ResumeNumero";

const ResumenOrdenDetalle = (props) => {
    const { entOrden, open, setOpen } = props;

    const columnas = [
        { title: "Folio", field: "sFolio", align: "center" },
        //{ title: "Origen", field: "sOrigen", align: "center" },
        { title: "Vencimiento", field: "sFechaVencimiento", align: "center" },
    ];

    return (
        <MeditocModal title={"Detalle de orden " + entOrden.sOrderId} size="normal" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <ResumeInfo
                        label="Subtotal"
                        value={entOrden.nAmount.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                    <ResumeInfo
                        label="Descuento"
                        value={`-${entOrden.nAmountDiscount.toLocaleString("en", { minimumFractionDigits: 2 })}`}
                    />
                    <ResumeInfo
                        label="IVA"
                        value={`+${entOrden.nAmountTax.toLocaleString("en", { minimumFractionDigits: 2 })}`}
                    />
                    <ResumeInfo
                        label="Total pagado"
                        value={entOrden.nAmountPaid.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                </Grid>
                {/* <Grid item md={3} sm={6} xs={12} className="center">
                    <ResumeNumero
                        label="SUBTOTAL"
                        value={entOrden.nAmount.toLocaleString("en", { minimumFractionDigits: 2 })}
                        color="color-1"
                    />
                </Grid>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <ResumeNumero
                        label="DESCUENTO"
                        value={`-${entOrden.nAmountDiscount.toLocaleString("en", { minimumFractionDigits: 2 })}`}
                        color="color-4"
                    />
                </Grid>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <ResumeNumero
                        label="IVA"
                        value={`+${entOrden.nAmountTax.toLocaleString("en", { minimumFractionDigits: 2 })}`}
                        color="color-3"
                    />
                </Grid>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <ResumeNumero
                        label="TOTAL PAGADO"
                        value={entOrden.nAmountPaid.toLocaleString("en", { minimumFractionDigits: 2 })}
                        color="color-2"
                    />
                </Grid> */}
                <Grid item xs={12}>
                    <span className="rob-nor bold size-20 color-4">DATOS DEL CLIENTE</span>
                    <Divider />
                </Grid>
                <Grid item xs={12}>
                    <InfoField label="Nombre:" value={entOrden.customer_info.name} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Correo:" value={entOrden.customer_info.email} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Teléfono:" value={entOrden.customer_info.phone} />
                </Grid>
                <Grid item xs={12}>
                    <span className="rob-nor bold size-20 color-4">DATOS DE PAGO</span>
                    <Divider />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="Tipo de pago:" value={entOrden.charges.sType} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="No. de autorización del banco:" value={entOrden.charges.sAuthCode} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="ID de cargo Conekta:" value={entOrden.charges.sChargeId} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <InfoField label="ID de control de compra:" value={entOrden.uId} />
                </Grid>
                <Grid item xs={12}>
                    <span className="rob-nor bold size-20 color-4">
                        {entOrden.sPaymentStatus === EnumStatusConekta.Declined
                            ? "PRODUCTOS RECHAZADOS"
                            : "PRODUCTOS ADQUIRIDOS"}
                    </span>
                    <Divider />
                </Grid>
                <Grid item xs={12}>
                    {entOrden.lstProductos.map((producto, index) => (
                        <Fragment key={producto.sItemId}>
                            <div className={"reporte-producto-header " + (index === 0 ? "mar-top-20" : "mar-top-50")}>
                                {`${index + 1}.- ${producto.sNombre}${
                                    producto.sTipoProducto === null ? "" : " - " + producto.sTipoProducto
                                }`}
                            </div>
                            <div className="center">
                                <span className="rob-nor normal color-4 size-15">ID de artículo Conekta:</span>{" "}
                                <span className="rob-nor bold color-3 size-15">{producto.sItemId}</span>
                                <br />
                                <span className="rob-nor normal color-4 size-15">Precio unitario:</span>{" "}
                                <span className="rob-nor bold color-3 size-15">
                                    ${producto.nUnitPrice.toLocaleString("en", { minimumFractionDigits: 2 })}
                                </span>
                                <br />
                                <span className="rob-nor normal color-4 size-15">Cantidad:</span>{" "}
                                <span className="rob-nor bold color-3 size-15">{producto.iQuantity}</span>
                            </div>
                            {producto.lstFolios.length > 0 && (
                                <div>
                                    <MeditocTableSimple columns={columnas} data={producto.lstFolios} />
                                </div>
                            )}
                        </Fragment>
                    ))}
                </Grid>
            </Grid>
        </MeditocModal>
    );
};

export default ResumenOrdenDetalle;
