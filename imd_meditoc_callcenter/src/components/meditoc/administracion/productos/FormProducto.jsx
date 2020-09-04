import React from "react";
import MeditocModal from "../../../utilidades/MeditocModal";
import {
    Grid,
    TextField,
    FormControl,
    FormLabel,
    RadioGroup,
    FormControlLabel,
    Radio,
    FormGroup,
    Checkbox,
    Button,
    MenuItem,
} from "@material-ui/core";
import { useState } from "react";
import { useEffect } from "react";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import { listIconsMedicalProducts } from "../../../../configurations/iconProductConfig";

const FormProducto = (props) => {
    const { entProducto, open, setOpen } = props;

    const [formProducto, setFormProducto] = useState({
        rdTipoProducto: "1",
        chkComercial: false,
        txtNombreProducto: "",
        txtNombreCorto: "",
        txtDescripcion: "",
        txtCosto: "",
        txtMesesVigencia: "",
        txtIcono: "",
        txtPrefijoFolio: "",
    });

    const [formProductoOK, setFormProductoOK] = useState({
        txtNombreProducto: true,
        txtNombreCorto: true,
        txtDescripcion: true,
        txtCosto: true,
        txtMesesVigencia: true,
        txtIcono: true,
        txtPrefijoFolio: true,
    });

    useEffect(() => {
        setFormProducto({
            rdTipoProducto: entProducto.iIdTipoProducto.toString(),
            chkComercial: entProducto.bComercial,
            txtNombreProducto: entProducto.sNombre,
            txtNombreCorto: entProducto.sNombreCorto,
            txtDescripcion: entProducto.sDescripcion,
            txtCosto: entProducto.fCosto === 0 ? "" : entProducto.fCosto,
            txtMesesVigencia: entProducto.iMesVigencia === 0 ? "" : entProducto.iMesVigencia,
            txtIcono: entProducto.sIcon,
            txtPrefijoFolio: entProducto.sPrefijoFolio,
        });
    }, [entProducto]);

    const handleChangeFormProducto = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNombreProducto":
                if (valorCampo !== "" && !formProductoOK.txtNombreProducto) {
                    setFormProductoOK({ ...formProductoOK, [nombreCampo]: valorCampo });
                }
                break;

            case "txtNombreCorto":
                if (valorCampo !== "" && !formProductoOK.txtNombreCorto) {
                    setFormProductoOK({ ...formProductoOK, [nombreCampo]: valorCampo });
                }
                break;

            case "txtDescripcion":
                if (valorCampo !== "" && !formProductoOK.txtDescripcion) {
                    setFormProductoOK({ ...formProductoOK, [nombreCampo]: valorCampo });
                }
                break;

            case "txtCosto":
                if (valorCampo !== "") {
                    if (isNaN(valorCampo)) {
                        return;
                    }
                    if (!formProductoOK.txtCosto) {
                        setFormProductoOK({ ...formProductoOK, [nombreCampo]: valorCampo });
                    }
                }
                break;

            case "txtMesesVigencia":
                if (valorCampo !== "") {
                    if (valorCampo % 1 !== 0) {
                        return;
                    }
                    if (!formProductoOK.txtMesesVigencia) {
                        setFormProductoOK({ ...formProductoOK, [nombreCampo]: valorCampo });
                    }
                }
                break;

            case "txtIcono":
                if (valorCampo !== "" && !formProductoOK.txtIcono) {
                    setFormProductoOK({ ...formProductoOK, [nombreCampo]: valorCampo });
                }
                break;

            case "txtPrefijoFolio":
                if (valorCampo !== "" && !formProductoOK.txtPrefijoFolio) {
                    setFormProductoOK({ ...formProductoOK, [nombreCampo]: valorCampo });
                }
                break;

            default:
                break;
        }

        if (nombreCampo === "chkComercial") {
            setFormProducto({
                ...formProducto,
                [nombreCampo]: e.target.checked,
            });
            return;
        }

        setFormProducto({
            ...formProducto,
            [nombreCampo]: valorCampo,
        });
    };

    const handleClickGuardarProducto = () => {
        let formProductoOKValidacion = {
            txtNombreProducto: true,
            txtNombreCorto: true,
            txtDescripcion: true,
            txtCosto: true,
            txtMesesVigencia: true,
            txtIcono: true,
            txtPrefijoFolio: true,
        };
        let bFormError = false;

        if (formProducto.txtNombreProducto === "") {
            formProductoOKValidacion.txtNombreProducto = false;
            bFormError = true;
        }

        if (formProducto.txtNombreCorto === "") {
            formProductoOKValidacion.txtNombreCorto = false;
            bFormError = true;
        }

        if (formProducto.txtDescripcion === "") {
            formProductoOKValidacion.txtDescripcion = false;
            bFormError = true;
        }

        if (formProducto.txtCosto === "") {
            formProductoOKValidacion.txtCosto = false;
            bFormError = true;
        } else if (isNaN(formProducto.txtCosto)) {
            formProductoOKValidacion.txtCosto = false;
            bFormError = true;
        } else if (parseInt(formProducto.txtCosto) <= 0) {
            formProductoOKValidacion.txtCosto = false;
            bFormError = true;
        }

        if (formProducto.txtMesesVigencia === "") {
            formProductoOKValidacion.txtMesesVigencia = false;
            bFormError = true;
        } else if (isNaN(formProducto.txtMesesVigencia)) {
            formProductoOKValidacion.txtMesesVigencia = false;
            bFormError = true;
        } else if (parseInt(formProducto.txtMesesVigencia) <= 0 || parseInt(formProducto.txtMesesVigencia) % 1 !== 0) {
            formProductoOKValidacion.txtMesesVigencia = false;
            bFormError = true;
        }

        if (formProducto.txtIcono === "") {
            formProductoOKValidacion.txtIcono = false;
            bFormError = true;
        }

        if (formProducto.txtPrefijoFolio === "") {
            formProductoOKValidacion.txtPrefijoFolio = false;
            bFormError = true;
        }

        setFormProductoOK(formProductoOKValidacion);
        if (bFormError) {
            return;
        }
    };

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

    return (
        <MeditocModal
            title={entProducto.iIdProducto === 0 ? "Nuevo producto" : "Editar producto"}
            size="normal"
            open={open}
            setOpen={setOpen}
        >
            <Grid container spacing={3}>
                <Grid item sm={6} xs={12}>
                    <FormControl componet="fieldset">
                        <FormLabel component="legend">Tipo de producto:</FormLabel>
                        <RadioGroup
                            row
                            name="rdTipoProducto"
                            value={formProducto.rdTipoProducto}
                            onChange={handleChangeFormProducto}
                        >
                            <FormControlLabel value="1" control={<Radio />} label="Membresía" />
                            <FormControlLabel value="2" control={<Radio />} label="Servicio" />
                        </RadioGroup>
                    </FormControl>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <FormGroup row>
                        <FormControlLabel
                            control={
                                <Checkbox
                                    name="chkComercial"
                                    checked={formProducto.chkComercial}
                                    onChange={handleChangeFormProducto}
                                />
                            }
                            label="Es producto comercial"
                        />
                    </FormGroup>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        name="txtNombreProducto"
                        label="Nombre del producto:"
                        fullWidth
                        variant="outlined"
                        placeholder="Ej. Membresía 6 meses"
                        value={formProducto.txtNombreProducto}
                        onChange={handleChangeFormProducto}
                        required
                        error={!formProductoOK.txtNombreProducto}
                        helperText={!formProductoOK.txtNombreProducto ? "El nombre del producto es requerido" : ""}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        name="txtNombreCorto"
                        label="Nombre corto del producto:"
                        fullWidth
                        variant="outlined"
                        placeholder="Ej. 6 meses"
                        value={formProducto.txtNombreCorto}
                        onChange={handleChangeFormProducto}
                        required
                        error={!formProductoOK.txtNombreCorto}
                        helperText={
                            !formProductoOK.txtNombreCorto ? "El nombre corto para el producto es requerido" : ""
                        }
                    />
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        name="txtDescripcion"
                        label="Descripción:"
                        fullWidth
                        variant="outlined"
                        multiline
                        placeholder="Ej. Un médico contigo, siempre, en donde sea."
                        value={formProducto.txtDescripcion}
                        onChange={handleChangeFormProducto}
                        required
                        error={!formProductoOK.txtDescripcion}
                        helperText={!formProductoOK.txtDescripcion ? "La descripción del producto es requerido" : ""}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        name="txtCosto"
                        label="Costo del producto (en MXN sin IVA):"
                        fullWidth
                        variant="outlined"
                        placeholder="Ej. 600.00"
                        value={formProducto.txtCosto}
                        onChange={handleChangeFormProducto}
                        required
                        error={!formProductoOK.txtCosto}
                        helperText={!formProductoOK.txtCosto ? "Ingrese un monto válido para el producto" : ""}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        name="txtMesesVigencia"
                        label="Meses de vigencia de folio:"
                        fullWidth
                        variant="outlined"
                        placeholder="Ej. 6"
                        value={formProducto.txtMesesVigencia}
                        onChange={handleChangeFormProducto}
                        required
                        error={!formProductoOK.txtMesesVigencia}
                        helperText={
                            !formProductoOK.txtMesesVigencia
                                ? "Ingrese un número válido para los meses de vigencia"
                                : ""
                        }
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        name="txtIcono"
                        label="Ícono:"
                        fullWidth
                        variant="outlined"
                        select
                        SelectProps={{ MenuProps: { PaperProps: { style: { maxHeight: 300 } } } }}
                        value={formProducto.txtIcono}
                        onChange={handleChangeFormProducto}
                        required
                        error={!formProductoOK.txtIcono}
                        helperText={!formProductoOK.txtIcono ? "Seleccione un ícono para el producto" : ""}
                    >
                        {listIconsMedicalProducts.map((icon) => (
                            <MenuItem key={icon.key} value={icon.key}>
                                <i
                                    className="icon size-20 color-2"
                                    dangerouslySetInnerHTML={{ __html: icon.htmlIcon }}
                                />
                            </MenuItem>
                        ))}
                    </TextField>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        name="txtPrefijoFolio"
                        label="Prefijo para los folios:"
                        fullWidth
                        variant="outlined"
                        placeholder="Ej. VS"
                        value={formProducto.txtPrefijoFolio}
                        onChange={handleChangeFormProducto}
                        required
                        error={!formProductoOK.txtPrefijoFolio}
                        helperText={!formProductoOK.txtPrefijoFolio ? "El prefijo para el folio es requerido" : ""}
                    />
                </Grid>
                <MeditocModalBotones okMessage="Guardar" okFunc={handleClickGuardarProducto} cancelFunc={handleClose} />
            </Grid>
        </MeditocModal>
    );
};

export default FormProducto;
