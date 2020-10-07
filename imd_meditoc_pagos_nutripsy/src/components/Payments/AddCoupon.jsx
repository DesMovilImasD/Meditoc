import { Button, Dialog, DialogContent, Grid, TextField, Typography } from "@material-ui/core";
import React, { useState } from "react";

import PropTypes from "prop-types";
import { apiValidateCoupon } from "../../configuration/apiConfig";
import { serverMain } from "../../configuration/serverConfig";

/*****************************************************
 * Descripción: Despliega el dialogo para ingresar y validar un cupón
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const AddCoupon = (props) => {
    const { couponDialogOpen, setCouponDialogOpen, setEntCoupon, funcLoader } = props;

    //Guardar valor del cupón
    const [couponCode, setCouponCode] = useState("");

    //Guardar mensaje de validación de cupón
    const [couponCodeMessage, setCouponCodeMessage] = useState("");

    //Evento change para capturar el valor del cupón
    const handleChangeCouponCode = (e) => {
        const couponValue = e.target.value;
        setCouponCode(couponValue === "" ? "" : couponValue.toUpperCase());
    };

    //Evento close para cerrar el diálogo de cupón
    const handleCloseCouponDialog = () => {
        setCouponCode("");
        setCouponCodeMessage("");

        setCouponDialogOpen(false);
    };

    //Consumir servicio para validar cupón
    const funcValidateCoupon = async (e) => {
        e.preventDefault();
        funcLoader(true, "Validando cupón...");

        try {
            const apiResponse = await fetch(`${serverMain}${apiValidateCoupon}?psCodigo=${couponCode}`);

            const response = await apiResponse.json();

            if (response.Code === 0) {
                setCouponCodeMessage("");
                setEntCoupon(response.Result);

                handleCloseCouponDialog();
                document.activeElement.blur();
            } else {
                setCouponCodeMessage(response.Message);
            }
        } catch (error) {
            setCouponCodeMessage("Ocurrió un error al intentar validar el cupón");
        }

        funcLoader();
    };

    return (
        <Dialog open={couponDialogOpen} onClose={handleCloseCouponDialog}>
            <DialogContent>
                <form id="form-cupon" onSubmit={funcValidateCoupon} noValidate>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <span className="pay-add-coupon-title">Ingrese código de descuento</span>
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                name="txtCouponCode"
                                variant="standard"
                                fullWidth
                                autoFocus
                                value={couponCode}
                                onChange={handleChangeCouponCode}
                                error={couponCodeMessage !== ""}
                                helperText={couponCodeMessage}
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <Button className="secondary-gray" onClick={handleCloseCouponDialog}>
                                <Typography className="secondary-gray" variant="button">
                                    Cancelar
                                </Typography>
                            </Button>
                        </Grid>
                        <Grid item xs={6} className="right">
                            <Button color="primary" type="submit" disabled={couponCode === ""}>
                                Aceptar
                            </Button>
                        </Grid>
                    </Grid>
                </form>
            </DialogContent>
        </Dialog>
    );
};

AddCoupon.propTypes = {
    couponDialogOpen: PropTypes.bool,
    funcLoader: PropTypes.func,
    setCouponDialogOpen: PropTypes.func,
    setEntCoupon: PropTypes.func,
};

export default AddCoupon;
