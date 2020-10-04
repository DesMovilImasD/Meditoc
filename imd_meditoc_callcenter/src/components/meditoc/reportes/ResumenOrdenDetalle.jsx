import PropTypes from "prop-types";
import { Divider, Grid } from "@material-ui/core";
import React, { Fragment } from "react";
import { EnumStatusConekta } from "../../../configurations/enumConfig";
import InfoField from "../../utilidades/InfoField";
import MeditocModal from "../../utilidades/MeditocModal";
import MeditocModalBotones from "../../utilidades/MeditocModalBotones";
import MeditocTableSimple from "../../utilidades/MeditocTableSimple";
import ResumeInfo from "./ResumeInfo";

const ResumenOrdenDetalle = (props) => {
    const { entOrden, open, setOpen } = props;

    const columnas = [
        { title: "Folio", field: "sFolio", align: "center" },
        { title: "Vencimiento", field: "sFechaVencimiento", align: "center" },
    ];

    return (
        <MeditocModal title={"Detalle de orden " + entOrden.sOrderId} size="normal" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <ResumeInfo
                        label="Subtotal"
                        value={"$" + entOrden.nAmount.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                    <ResumeInfo
                        label="Descuento"
                        value={"- $" + entOrden.nAmountDiscount.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                    <ResumeInfo
                        label="IVA"
                        value={"+ $" + entOrden.nAmountTax.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                    <ResumeInfo
                        label="Total pagado"
                        value={"$" + entOrden.nAmountPaid.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
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
                <MeditocModalBotones cancelMessage="Cerrar detalle de órden" setOpen={setOpen} hideOk />
            </Grid>
        </MeditocModal>
    );
};

ResumenOrdenDetalle.propTypes = {
    entOrden: PropTypes.shape({
        charges: PropTypes.shape({
            sAuthCode: PropTypes.any,
            sChargeId: PropTypes.any,
            sType: PropTypes.any,
        }),
        customer_info: PropTypes.shape({
            email: PropTypes.any,
            name: PropTypes.any,
            phone: PropTypes.any,
        }),
        lstProductos: PropTypes.shape({
            map: PropTypes.func,
        }),
        nAmount: PropTypes.shape({
            toLocaleString: PropTypes.func,
        }),
        nAmountDiscount: PropTypes.shape({
            toLocaleString: PropTypes.func,
        }),
        nAmountPaid: PropTypes.shape({
            toLocaleString: PropTypes.func,
        }),
        nAmountTax: PropTypes.shape({
            toLocaleString: PropTypes.func,
        }),
        sOrderId: PropTypes.any,
        sPaymentStatus: PropTypes.any,
        uId: PropTypes.any,
    }),
    open: PropTypes.any,
    setOpen: PropTypes.any,
};

export default ResumenOrdenDetalle;
