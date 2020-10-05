import { Divider, FormControlLabel, Grid, MenuItem, Radio, RadioGroup, TextField } from "@material-ui/core";
import React, { useState } from "react";
import { blurPrevent, funcPrevent } from "../../../../configurations/preventConfig";

import { EnumCuponCategoria } from "../../../../configurations/enumConfig";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import PromocionesController from "../../../../controllers/PromocionesController";
import PropTypes from "prop-types";
import { useEffect } from "react";

const FormCupon = (props) => {
    const { entCupon, open, setOpen, funcObtenerCupones, usuarioSesion, funcLoader, funcAlert } = props;

    const personalizado = "personalizado";
    const aleatorio = "aleatorio";

    const [rdCuponCodigoValue, setRdCuponCodigoValue] = useState(personalizado);

    const handleChandeRdCuponCodigo = (e) => {
        setRdCuponCodigoValue(e.target.value);
    };

    const [formCupon, setFormCupon] = useState({
        txtCodigoCupon: "",
        txtLongitudCupon: "",
        txtTipoCupon: EnumCuponCategoria.DescuentoMonto.toString(),
        txtMontoDescuento: "",
        txtPorcentajeDescuento: "",
        txtTotalCupones: "",
        txtDiasActivos: "",
        txtDescripcion: "",
    });

    const validacionFormulario = {
        txtCodigoCupon: true,
        txtLongitudCupon: true,
        txtTipoCupon: true,
        txtMontoDescuento: true,
        txtPorcentajeDescuento: true,
        txtTotalCupones: true,
        txtDiasActivos: true,
        txtDescripcion: true,
    };

    const [formCuponOK, setFormCuponOK] = useState(validacionFormulario);

    useEffect(() => {
        setFormCupon({
            txtCodigoCupon: entCupon.fsCodigo,
            txtLongitudCupon: entCupon.fiLongitudCodigo === 0 ? "" : entCupon.fiLongitudCodigo,
            txtTipoCupon: entCupon.fiIdCuponCategoria.toString(),
            txtMontoDescuento: entCupon.fnMontoDescuento === 0 ? "" : entCupon.fnMontoDescuento,
            txtPorcentajeDescuento: entCupon.fnPorcentajeDescuento === 0 ? "" : entCupon.fnPorcentajeDescuento,
            txtTotalCupones: entCupon.fiTotalLanzamiento === 0 ? "" : entCupon.fiTotalLanzamiento,
            txtDiasActivos: entCupon.fiDiasActivo === 0 ? "" : entCupon.fiDiasActivo,
            txtDescripcion: entCupon.fsDescripcion,
        });
        setFormCuponOK(validacionFormulario);
        // eslint-disable-next-line
    }, [entCupon]);

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

    const handleClickGuardarCupon = async (e) => {
        funcPrevent(e);

        let formCuponOKValidacion = { ...validacionFormulario };

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

        if (formCupon.txtTipoCupon === EnumCuponCategoria.DescuentoMonto.toString()) {
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

        const entCreateCupon = {
            fiIdCuponCategoria: parseInt(formCupon.txtTipoCupon),
            fsDescripcion: formCupon.txtDescripcion,
            fsCodigo: rdCuponCodigoValue === personalizado ? formCupon.txtCodigoCupon : null,
            fiLongitudCodigo: rdCuponCodigoValue === aleatorio ? parseInt(formCupon.txtLongitudCupon) : null,
            fnMontoDescuento: formCupon.txtTipoCupon === "1" ? parseFloat(formCupon.txtMontoDescuento) * 100 : null,
            fnPorcentajeDescuento: formCupon.txtTipoCupon === "2" ? parseFloat(formCupon.txtPorcentajeDescuento) : null,
            fiTotalLanzamiento: parseInt(formCupon.txtTotalCupones),
            fiDiasActivo: formCupon.txtDiasActivos === "" ? null : parseInt(formCupon.txtDiasActivos),
        };

        funcLoader(true, "Generando cupón...");

        const promocionesController = new PromocionesController();

        const response = await promocionesController.funcCrearCupon(entCreateCupon, usuarioSesion.iIdUsuario);

        if (response.Code === 0) {
            setOpen(false);
            await funcObtenerCupones();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
        blurPrevent();
    };

    return (
        <MeditocModal title="Crear cupón" size="normal" open={open} setOpen={setOpen}>
            <form id="form-cupon" onSubmit={handleClickGuardarCupon} noValidate>
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
                                autoFocus
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
                                    !formCuponOK.txtPorcentajeDescuento
                                        ? "Ingrese un porcentaje válido entre 1 y 100"
                                        : ""
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
                    <MeditocModalBotones okMessage="Guardar" okFunc={handleClickGuardarCupon} setOpen={setOpen} />
                </Grid>
            </form>
        </MeditocModal>
    );
};

FormCupon.propTypes = {
    entCupon: PropTypes.shape({
        fiDiasActivo: PropTypes.number,
        fiIdCuponCategoria: PropTypes.shape({
            toString: PropTypes.func,
        }),
        fiLongitudCodigo: PropTypes.number,
        fiTotalLanzamiento: PropTypes.number,
        fnMontoDescuento: PropTypes.number,
        fnPorcentajeDescuento: PropTypes.number,
        fsCodigo: PropTypes.any,
        fsDescripcion: PropTypes.any,
    }),
    funcAlert: PropTypes.func,
    funcLoader: PropTypes.func,
    funcObtenerCupones: PropTypes.func,
    open: PropTypes.any,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default FormCupon;
