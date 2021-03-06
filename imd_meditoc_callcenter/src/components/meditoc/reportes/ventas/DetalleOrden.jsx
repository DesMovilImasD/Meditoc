import React, { Fragment } from "react";

import { EnumStatusConekta } from "../../../../configurations/enumConfig";
import FolioController from "../../../../controllers/FolioController";
import { Grid } from "@material-ui/core";
import MeditocInfoField from "../../../utilidades/MeditocInfoField";
import MeditocInfoResumen from "../../../utilidades/MeditocInfoResumen";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import MeditocSubtitulo from "../../../utilidades/MeditocSubtitulo";
import MeditocTableSimple from "../../../utilidades/MeditocTableSimple";
import PropTypes from "prop-types";
import { cellProps } from "../../../../configurations/dataTableIconsConfig";

const DetalleOrden = (props) => {
    const { entOrden, open, setOpen, funcLoader, funcAlert, permisos } = props;

    const columnas = [
        { title: "Folio", field: "sFolio", ...cellProps },
        { title: "Confirmado", field: "bConfirmado", ...cellProps },
        { title: "Vencimiento", field: "sFechaVencimiento", ...cellProps },
    ];

    const handleClickReenviarCorreo = async () => {
        funcLoader(true, "Reenviando correo de orden...");

        const folioController = new FolioController();
        const response = await folioController.funcReenviarCorreoOrden(entOrden.sOrderId);
        if (response.Code === 0) {
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }
        funcLoader();
    };

    return (
        <MeditocModal title={"Detalle de orden " + entOrden.sOrderId} size="normal" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <MeditocInfoResumen
                        label="Subtotal"
                        value={entOrden.nAmount.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                    <MeditocInfoResumen
                        label="Descuento"
                        value={"- " + entOrden.nAmountDiscount.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                    <MeditocInfoResumen
                        label="IVA"
                        value={"+ " + entOrden.nAmountTax.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                    <MeditocInfoResumen
                        label="Total pagado"
                        value={entOrden.nAmountPaid.toLocaleString("en", { minimumFractionDigits: 2 })}
                    />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DE CLIENTE" />
                </Grid>
                <Grid item xs={12}>
                    <MeditocInfoField label="Nombre:" value={entOrden.customer_info.name} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Correo:" value={entOrden.customer_info.email} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Teléfono:" value={entOrden.customer_info.phone} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo title="DATOS DE PAGO" />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="Tipo de pago:" value={entOrden.charges.sType} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="No. de autorización del banco:" value={entOrden.charges.sAuthCode} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="ID de cargo:" value={entOrden.charges.sChargeId} />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <MeditocInfoField label="ID de control de compra:" value={entOrden.uId} />
                </Grid>
                <Grid item xs={12}>
                    <MeditocSubtitulo
                        title={
                            entOrden.sPaymentStatus === EnumStatusConekta.Declined
                                ? "PRODUCTOS RECHAZADOS"
                                : "PRODUCTOS ADQUIRIDOS"
                        }
                    />
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
                                <span className="rob-nor normal color-4 size-15">ID de control artículo:</span>{" "}
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
                <MeditocModalBotones
                    cancelMessage="Cerrar detalle de órden"
                    okMessage="Reenviar correo de órden"
                    setOpen={setOpen}
                    okFunc={handleClickReenviarCorreo}
                    hideOk={
                        entOrden.sPaymentStatus === EnumStatusConekta.Declined || permisos.Botones["3"] === undefined
                    }
                />
            </Grid>
        </MeditocModal>
    );
};

DetalleOrden.propTypes = {
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

export default DetalleOrden;
