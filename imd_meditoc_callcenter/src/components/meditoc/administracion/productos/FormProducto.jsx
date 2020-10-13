import {
    Checkbox,
    FormControl,
    FormControlLabel,
    FormGroup,
    FormLabel,
    Grid,
    MenuItem,
    Radio,
    RadioGroup,
    TextField,
} from "@material-ui/core";
import { blurPrevent, funcPrevent } from "../../../../configurations/preventConfig";

import { EnumTipoProducto } from "../../../../configurations/enumConfig";
import MeditocModal from "../../../utilidades/MeditocModal";
import MeditocModalBotones from "../../../utilidades/MeditocModalBotones";
import ProductoController from "../../../../controllers/ProductoController";
import PropTypes from "prop-types";
import React from "react";
import { useEffect } from "react";
import { useState } from "react";

const FormProducto = (props) => {
    const {
        entProducto,
        open,
        setOpen,
        funcConsultarProductos,
        usuarioSesion,
        entCatalogos,
        funcLoader,
        funcAlert,
    } = props;

    const [formProducto, setFormProducto] = useState({
        rdTipoProducto: EnumTipoProducto.Membresia.toString(),
        chkComercial: false,
        txtNombreProducto: "",
        txtNombreCorto: "",
        txtDescripcion: "",
        txtGrupoProducto: 1,
        txtCosto: "",
        txtMesesVigencia: "",
        txtIcono: "",
        txtPrefijoFolio: "",
    });

    const validacionFormulario = {
        txtNombreProducto: true,
        txtNombreCorto: true,
        txtDescripcion: true,
        txtGrupoProducto: true,
        txtCosto: true,
        txtMesesVigencia: true,
        txtIcono: true,
        txtPrefijoFolio: true,
    };

    const [formProductoOK, setFormProductoOK] = useState(validacionFormulario);

    useEffect(() => {
        setFormProducto({
            rdTipoProducto: entProducto.iIdTipoProducto.toString(),
            chkComercial: entProducto.bComercial,
            txtNombreProducto: entProducto.sNombre,
            txtNombreCorto: entProducto.sNombreCorto,
            txtDescripcion: entProducto.sDescripcion,
            txtGrupoProducto: entProducto.iIdGrupoProducto === 0 ? 1 : entProducto.iIdGrupoProducto,
            txtCosto: entProducto.fCosto === 0 ? "" : entProducto.fCosto,
            txtMesesVigencia: entProducto.iMesVigencia === 0 ? "" : entProducto.iMesVigencia,
            txtIcono: entProducto.sIcon,
            txtPrefijoFolio: entProducto.sPrefijoFolio,
        });
        setFormProductoOK(validacionFormulario);
        // eslint-disable-next-line
    }, [entProducto]);

    const handleChangeFormProducto = (e) => {
        const nombreCampo = e.target.name;
        const valorCampo = e.target.value;

        switch (nombreCampo) {
            case "txtNombreProducto":
                if (valorCampo !== "" && !formProductoOK.txtNombreProducto) {
                    setFormProductoOK({ ...formProductoOK, [nombreCampo]: true });
                }
                break;

            case "txtNombreCorto":
                if (valorCampo !== "" && !formProductoOK.txtNombreCorto) {
                    setFormProductoOK({ ...formProductoOK, [nombreCampo]: true });
                }
                break;

            case "txtDescripcion":
                if (valorCampo !== "" && !formProductoOK.txtDescripcion) {
                    setFormProductoOK({ ...formProductoOK, [nombreCampo]: true });
                }
                break;

            case "txtCosto":
                if (valorCampo !== "") {
                    if (isNaN(valorCampo)) {
                        return;
                    }
                    if (!formProductoOK.txtCosto) {
                        setFormProductoOK({ ...formProductoOK, [nombreCampo]: true });
                    }
                }
                break;

            case "txtMesesVigencia":
                if (valorCampo !== "") {
                    if (valorCampo === "0" || valorCampo % 1 !== 0) {
                        return;
                    }
                    if (!formProductoOK.txtMesesVigencia) {
                        setFormProductoOK({ ...formProductoOK, [nombreCampo]: true });
                    }
                }
                break;

            case "txtIcono":
                if (valorCampo !== "" && !formProductoOK.txtIcono) {
                    setFormProductoOK({ ...formProductoOK, [nombreCampo]: true });
                }
                break;

            case "txtPrefijoFolio":
                if (valorCampo !== "" && !formProductoOK.txtPrefijoFolio) {
                    setFormProductoOK({ ...formProductoOK, [nombreCampo]: true });
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

    const handleClickGuardarProducto = async (e) => {
        funcPrevent(e);
        let formProductoOKValidacion = { ...validacionFormulario };
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
        } else if (formProducto.txtCosto <= 0) {
            formProductoOKValidacion.txtCosto = false;
            bFormError = true;
        }

        if (
            formProducto.txtMesesVigencia === "" &&
            formProducto.rdTipoProducto === EnumTipoProducto.Membresia.toString()
        ) {
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

        const entProductoSubmit = {
            iIdProducto: entProducto.iIdProducto,
            iIdTipoProducto: parseInt(formProducto.rdTipoProducto),
            sNombre: formProducto.txtNombreProducto,
            sNombreCorto: formProducto.txtNombreCorto,
            sDescripcion: formProducto.txtDescripcion,
            iIdGrupoProducto: parseInt(formProducto.txtGrupoProducto),
            fCosto: parseFloat(formProducto.txtCosto),
            iMesVigencia:
                formProducto.rdTipoProducto === EnumTipoProducto.Servicio.toString()
                    ? 0
                    : parseInt(formProducto.txtMesesVigencia),
            sIcon: formProducto.txtIcono,
            bComercial: formProducto.chkComercial,
            sPrefijoFolio: formProducto.txtPrefijoFolio,
            iIdUsuarioMod: usuarioSesion.iIdUsuario,
            bActivo: true,
            bBaja: false,
        };

        funcLoader(true, "Guardando producto...");

        const productoController = new ProductoController();

        const response = await productoController.funcSaveProducto(entProductoSubmit);

        if (response.Code === 0) {
            setOpen(false);
            await funcConsultarProductos();
            funcAlert(response.Message, "success");
        } else {
            funcAlert(response.Message);
        }

        funcLoader();
        blurPrevent();
    };

    return (
        <MeditocModal
            title={entProducto.iIdProducto === 0 ? "Nuevo producto" : "Editar producto"}
            size="small"
            open={open}
            setOpen={setOpen}
        >
            <form onSubmit={handleClickGuardarProducto} id="form-producto" noValidate>
                <Grid container spacing={3}>
                    <Grid item sm={6} xs={12}>
                        <TextField
                            name="txtNombreProducto"
                            label="Nombre del producto:"
                            fullWidth
                            variant="outlined"
                            placeholder="Ej. Membresía 6 meses"
                            value={formProducto.txtNombreProducto}
                            onChange={handleChangeFormProducto}
                            autoFocus
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
                            helperText={
                                !formProductoOK.txtDescripcion ? "La descripción del producto es requerido" : ""
                            }
                        />
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            name="txtGrupoProducto"
                            label="Grupo de productos:"
                            fullWidth
                            variant="outlined"
                            value={formProducto.txtGrupoProducto}
                            onChange={handleChangeFormProducto}
                            required
                            select
                        >
                            {entCatalogos.catGrupoProducto.map((grupo) => (
                                <MenuItem key={grupo.fiId} value={grupo.fiId}>
                                    {grupo.fsDescripcion}
                                </MenuItem>
                            ))}
                        </TextField>
                    </Grid>
                    <Grid item sm={6} xs={12}>
                        <FormControl componet="fieldset">
                            <FormLabel component="legend">Tipo de producto:</FormLabel>
                            <RadioGroup
                                row
                                name="rdTipoProducto"
                                value={formProducto.rdTipoProducto}
                                onChange={handleChangeFormProducto}
                            >
                                {entCatalogos.catTipoProducto.map((tipo) => (
                                    <FormControlLabel
                                        key={tipo.fiId}
                                        value={tipo.fiId.toString()}
                                        control={<Radio />}
                                        label={tipo.fsDescripcion}
                                    />
                                ))}
                            </RadioGroup>
                        </FormControl>
                    </Grid>

                    <Grid item sm={6} xs={12}>
                        {formProducto.rdTipoProducto === EnumTipoProducto.Membresia.toString() ? (
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
                        ) : null}
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
                            {entCatalogos.catIcon.map((icon) => (
                                <MenuItem key={icon.fiId} value={icon.fsDescripcion}>
                                    <i
                                        className="icon size-20 color-2"
                                        dangerouslySetInnerHTML={{
                                            __html: `&#x${icon.fsDescripcion};&nbsp;&nbsp;&nbsp;`,
                                        }}
                                    />
                                    {icon.fsDescripcion}
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
                    <MeditocModalBotones okMessage="Guardar" okFunc={handleClickGuardarProducto} setOpen={setOpen} />
                </Grid>
            </form>
        </MeditocModal>
    );
};

FormProducto.propTypes = {
    entProducto: PropTypes.shape({
        bComercial: PropTypes.any,
        fCosto: PropTypes.number,
        iIdProducto: PropTypes.number,
        iIdTipoProducto: PropTypes.shape({
            toString: PropTypes.func,
        }),
        iMesVigencia: PropTypes.number,
        sDescripcion: PropTypes.any,
        sIcon: PropTypes.any,
        sNombre: PropTypes.any,
        sNombreCorto: PropTypes.any,
        sPrefijoFolio: PropTypes.any,
    }),
    funcAlert: PropTypes.func,
    funcConsultarProductos: PropTypes.func,
    funcLoader: PropTypes.func,
    open: PropTypes.any,
    setOpen: PropTypes.func,
    usuarioSesion: PropTypes.shape({
        iIdUsuario: PropTypes.any,
    }),
};

export default FormProducto;
