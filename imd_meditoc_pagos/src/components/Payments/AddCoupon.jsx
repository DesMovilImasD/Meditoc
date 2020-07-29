import PropTypes from "prop-types";
import React, { useState } from "react";
import { Dialog, DialogContent, TextField, Grid, Button, Typography } from "@material-ui/core";
import { serverWa } from "../../configuration/serverConfig";
import { apiValidateCoupon } from "../../configuration/apiConfig";

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
    const funcValidateCoupon = async () => {
        funcLoader(true, "Validando cupón...");

        try {
            const apiResponse = await fetch(`${serverWa}${apiValidateCoupon}?psCodigo=${couponCode}`);

            const response = await apiResponse.json();

            if (response.Code === 0) {
                setCouponCodeMessage("");
                setEntCoupon(response.Result);

                handleCloseCouponDialog();
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
                        <Button color="primary" onClick={funcValidateCoupon} disabled={couponCode === ""}>
                            Aceptar
                        </Button>
                    </Grid>
                </Grid>
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
