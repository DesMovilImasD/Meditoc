import React, { useState } from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import {
    Grid,
    FormControl,
    FormLabel,
    RadioGroup,
    FormControlLabel,
    Radio,
    TextField,
    MenuItem,
    Divider,
} from "@material-ui/core";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";

const FormCupon = (props) => {
    const { open, setOpen } = props;

    const personalizado = "personalizado";
    const aleatorio = "aleatorio";

    const [rdCuponCodigoValue, setRdCuponCodigoValue] = useState(personalizado);

    const handleChandeRdCuponCodigo = (e) => {
        setRdCuponCodigoValue(e.target.value);
    };

    const [formCupon, setFormCupon] = useState({
        txtCodigoCupon: "",
        txtLongitudCupon: "",
        txtTipoCupon: "1",
        txtMontoDescuento: "",
        txtPorcentajeDescuento: "",
        txtTotalCupones: "",
        txtDiasActivos: "",
        txtDescripcion: "",
    });

    const [formCuponOK, setFormCuponOK] = useState({
        txtCodigoCupon: true,
        txtLongitudCupon: true,
        txtTipoCupon: true,
        txtMontoDescuento: true,
        txtPorcentajeDescuento: true,
        txtTotalCupones: true,
        txtDiasActivos: true,
        txtDescripcion: true,
    });

    const handleChangeFormCupon = (e) => {
        const nombreCampo = e.target.name;
        let valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtCodigoCupon":
                if (valorCampo !== "") {
                    if (valorCampo.length > 25) {
                        return;
                    }
                    valorCampo = valorCampo.replace(" ", "").toUpperCase();
                    if (!formCuponOK.txtNombreProducto) {
                        setFormCuponOK({ ...formCuponOK, [nombreCampo]: true });
                    }
                }
                break;

            case "txtLongitudCupon":
                if (valorCampo !== "") {
                    if (valorCampo === "0" || valorCampo % 1 !== 0) {
                        return;
                    }
                    if (!formCuponOK.txtNombreProducto) {
                        setFormCuponOK({ ...formCuponOK, [nombreCampo]: true });
                    }
                }
                break;

            case "txtTipoCupon":
                if (valorCampo !== "" && !formCuponOK.txtTipoCupon) {
                    setFormCuponOK({ ...formCuponOK, [nombreCampo]: true });
                }
                break;

            case "txtMontoDescuento":
                if (valorCampo !== "") {
                    if (isNaN(valorCampo)) {
                        return;
                    }
                    if (!formCuponOK.txtMontoDescuento) {
                        setFormCuponOK({ ...formCuponOK, [nombreCampo]: true });
                    }
                }
                break;

            case "txtPorcentajeDescuento":
                if (valorCampo !== "") {
                    if (isNaN(valorCampo)) {
                        return;
                    }
                    if (!formCuponOK.txtPorcentajeDescuento) {
                        setFormCuponOK({ ...formCuponOK, [nombreCampo]: true });
                    }
                }
                break;

            case "txtTotalCupones":
                if (valorCampo !== "") {
                    if (valorCampo === "0" || valorCampo % 1 !== 0) {
                        return;
                    }
                    if (!formCuponOK.txtTotalCupones) {
                        setFormCuponOK({ ...formCuponOK, [nombreCampo]: true });
                    }
                }
                break;

            case "txtDiasActivos":
                if (valorCampo !== "") {
                    if (valorCampo === "0" || valorCampo % 1 !== 0) {
                        return;
                    }
                    if (!formCuponOK.txtDiasActivos) {
                        setFormCuponOK({ ...formCuponOK, [nombreCampo]: true });
                    }
                }
                break;

            default:
                break;
        }

        setFormCupon({
            ...formCupon,
            [nombreCampo]: valorCampo,
        });
    };

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

    const handleClickGuardarCupon = () => {
        let formCuponOKValidacion = {
            txtCodigoCupon: true,
            txtLongitudCupon: true,
            txtTipoCupon: true,
            txtMontoDescuento: true,
            txtPorcentajeDescuento: true,
            txtTotalCupones: true,
            txtDiasActivos: true,
            txtDescripcion: true,
        };

        let bFormError = false;

        if (rdCuponCodigoValue === personalizado) {
            if (formCupon.txtCodigoCupon === "") {
                formCuponOKValidacion.txtCodigoCupon = false;
                bFormError = true;
            }
        } else {
            if (formCupon.txtLongitudCupon === "") {
                formCuponOKValidacion.txtLongitudCupon = false;
                bFormError = true;
            }
        }

        if (formCupon.txtTipoCupon === "") {
            formCuponOKValidacion.txtTipoCupon = false;
            bFormError = true;
        }

        if (formCupon.txtTipoCupon === "1") {
            if (formCupon.txtMontoDescuento === "") {
                formCuponOKValidacion.txtMontoDescuento = false;
                bFormError = true;
            } else if (formCupon.txtMontoDescuento <= 0) {
                formCuponOKValidacion.txtMontoDescuento = false;
                bFormError = true;
            }
        } else {
            if (formCupon.txtPorcentajeDescuento === "") {
                formCuponOKValidacion.txtPorcentajeDescuento = false;
                bFormError = true;
            } else if (formCupon.txtPorcentajeDescuento <= 0 || formCupon.txtPorcentajeDescuento > 100) {
                formCuponOKValidacion.txtPorcentajeDescuento = false;
                bFormError = true;
            }
        }
        if (formCupon.txtTotalCupones === "") {
            formCuponOKValidacion.txtTotalCupones = false;
            bFormError = true;
        }

        setFormCuponOK(formCuponOKValidacion);

        if (bFormError) {
            return;
        }
    };

    return (
        <MeditocModal title="Crear cupón" size="normal" open={open} setOpen={setOpen}>
            <Grid container spacing={3}>
                <Grid item sm={6} xs={12}>
                    <RadioGroup
                        row
                        name="rdCuponCodigo"
                        value={rdCuponCodigoValue}
                        onChange={handleChandeRdCuponCodigo}
                    >
                        <FormControlLabel control={<Radio />} label="Código personalizado" value={personalizado} />
                        <FormControlLabel control={<Radio />} label="Código aleatorio" value={aleatorio} />
                    </RadioGroup>
                </Grid>
                <Grid item sm={6} xs={12} className="align-self-center">
                    {rdCuponCodigoValue === personalizado ? (
                        <TextField
                            name="txtCodigoCupon"
                            label="Código personalizado:"
                            variant="outlined"
                            fullWidth
                            required={rdCuponCodigoValue === personalizado}
                            value={formCupon.txtCodigoCupon}
                            onChange={handleChangeFormCupon}
                            error={!formCuponOK.txtCodigoCupon}
                            helperText={!formCuponOK.txtCodigoCupon ? "El código nuevo es requerido" : ""}
                        />
                    ) : (
                        <TextField
                            name="txtLongitudCupon"
                            label="Total de caracteres:"
                            variant="outlined"
                            fullWidth
                            required={rdCuponCodigoValue === aleatorio}
                            value={formCupon.txtLongitudCupon}
                            onChange={handleChangeFormCupon}
                            error={!formCuponOK.txtLongitudCupon}
                            helperText={
                                !formCuponOK.txtLongitudCupon ? "Ingrese un número entero válido mayor a 0" : ""
                            }
                        />
                    )}
                </Grid>
                <Grid item xs={12}>
                    <Divider />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        name="txtTipoCupon"
                        label="Tipo de cupón"
                        variant="outlined"
                        fullWidth
                        required
                        select
                        value={formCupon.txtTipoCupon}
                        onChange={handleChangeFormCupon}
                        error={!formCuponOK.txtTipoCupon}
                        helperText={!formCuponOK.txtTipoCupon ? "Seleccione el tipo de cupón" : ""}
                    >
                        <MenuItem value="1">Descuento por monto</MenuItem>
                        <MenuItem value="2">Descuento por porcentaje</MenuItem>
                    </TextField>
                </Grid>
                <Grid item sm={6} xs={12}>
                    {formCupon.txtTipoCupon === "1" ? (
                        <TextField
                            name="txtMontoDescuento"
                            label="Monto de descuento:"
                            variant="outlined"
                            fullWidth
                            required={formCupon.txtTipoCupon === "1"}
                            value={formCupon.txtMontoDescuento}
                            onChange={handleChangeFormCupon}
                            error={!formCuponOK.txtMontoDescuento}
                            helperText={
                                !formCuponOK.txtMontoDescuento ? "Ingrese un monto válido mayor a 0.00 pesos" : ""
                            }
                        />
                    ) : (
                        <TextField
                            name="txtPorcentajeDescuento"
                            label="Porcentaje de descuento"
                            variant="outlined"
                            fullWidth
                            required={formCupon.txtTipoCupon === "2"}
                            value={formCupon.txtPorcentajeDescuento}
                            onChange={handleChangeFormCupon}
                            error={!formCuponOK.txtPorcentajeDescuento}
                            helperText={
                                !formCuponOK.txtPorcentajeDescuento ? "Ingrese un porcentaje válido entre 1 y 100" : ""
                            }
                        />
                    )}
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        name="txtTotalCupones"
                        label="Total de códigos:"
                        variant="outlined"
                        fullWidth
                        required
                        value={formCupon.txtTotalCupones}
                        onChange={handleChangeFormCupon}
                        error={!formCuponOK.txtTotalCupones}
                        helperText={!formCuponOK.txtTotalCupones ? "Ingrese un número entero válido mayor a 0" : ""}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        name="txtDiasActivos"
                        label="Días activos:"
                        variant="outlined"
                        fullWidth
                        value={formCupon.txtDiasActivos}
                        onChange={handleChangeFormCupon}
                        error={!formCuponOK.txtDiasActivos}
                        helperText={!formCuponOK.txtDiasActivos ? "Ingrese un número entero válido mayor a 0" : ""}
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="txtDescripcion"
                        multiline
                        label="Descripción:"
                        variant="outlined"
                        fullWidth
                        value={formCupon.txtDescripcion}
                        onChange={handleChangeFormCupon}
                    />
                </Grid>
                <MeditocModalBotones okMessage="Guardar" okFunc={handleClickGuardarCupon} cancelFunc={handleClose} />
            </Grid>
        </MeditocModal>
    );
};

export default FormCupon;
