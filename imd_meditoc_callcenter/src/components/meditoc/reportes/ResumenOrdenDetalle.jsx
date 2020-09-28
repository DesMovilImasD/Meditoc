import { Divider, Grid, Typography } from "@material-ui/core";
import React, { Fragment } from "react";
import { EnumStatusConekta } from "../../../configurations/enumConfig";
import InfoField from "../../utilidades/InfoField";
import MeditocModal from "../../utilidades/MeditocModal";
import MeditocTableSimple from "../../utilidades/MeditocTableSimple";

const ResumenOrdenDetalle = (props) => {
    const { entOrden, open, setOpen } = props;

    const columnas = [
        { title: "Folio", field: "sFolio", align: "center" },
        //{ title: "Origen", field: "sOrigen", align: "center" },
        { title: "Vencimiento", field: "sFechaVencimiento", align: "center" },
    ];

    return (
        <MeditocModal title={"Detalle de orden " + entOrden.sOrderId} size="large" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <span className="rob-nor lighter size-50 color-1">
                        {entOrden.nAmount.toLocaleString("en", { minimumFractionDigits: 2 })}
                    </span>
                    <br />
                    <span className="rob-nor size-13 color-3">SUBTOTAL</span>
                </Grid>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <span className="rob-nor lighter size-50 color-4">
                        -{entOrden.nAmountDiscount.toLocaleString("en", { minimumFractionDigits: 2 })}
                    </span>
                    <br />
                    <span className="rob-nor size-13 color-3">DESCUENTO</span>
                </Grid>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <span className="rob-nor lighter size-50 color-3">
                        +{entOrden.nAmountTax.toLocaleString("en", { minimumFractionDigits: 2 })}
                    </span>
                    <br />
                    <span className="rob-nor size-13 color-3">IVA</span>
                </Grid>
                <Grid item md={3} sm={6} xs={12} className="center">
                    <span className="rob-nor size-50 color-2">
                        {entOrden.nAmountPaid.toLocaleString("en", { minimumFractionDigits: 2 })}
                    </span>
                    <br />
                    <span className="rob-nor size-13 color-3">TOTAL PAGADO</span>
                </Grid>
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
                        <Fragment>
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
