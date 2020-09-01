import React from "react";
import ModalForm from "../../../utilidades/ModalForm";
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
} from "@material-ui/core";
import { useState } from "react";
import { useEffect } from "react";

const FormProducto = (props) => {
    const { entProducto, open, setOpen } = props;

    const [formProducto, setFormProducto] = useState({
        rdTipoProducto: "",
        chkComercial: false,
        txtNombre: "",
        txtNombreCorto: "",
        txtDescripcion: "",
        txtCosto: "",
        txtMesesVigencia: "",
        txtIcono: "",
        txtPrefijoFolio: "",
    });

    useEffect(() => {
        setFormProducto({
            rdTipoProducto: entProducto.iIdTipoProducto,
            chkComercial: entProducto.bComercial,
            txtNombre: entProducto.sNombre,
            txtNombreCorto: entProducto.sNombreCorto,
            txtDescripcion: entProducto.sDescripcion,
            txtCosto: entProducto.fCosto,
            txtMesesVigencia: entProducto.iMesVigencia,
            txtIcono: entProducto.sIcon,
            txtPrefijoFolio: entProducto.sPrefijoFolio,
        });
    }, [entProducto]);

    const handleChangeFormProducto = (e) => {
        const nameTextField = e.target.name;
        const valueTextField = e.target.value;

        if (nameTextField === "txtCosto") {
            if (valueTextField !== "") {
                if (isNaN(valueTextField)) {
                    return;
                }
            }
        }

        if (nameTextField === "txtMesesVigencia") {
            if (valueTextField !== "") {
                if (valueTextField % 1 !== 0) {
                    return;
                }
            }
        }

        if (nameTextField === "chkComercial") {
            setFormProducto({
                ...formProducto,
                [nameTextField]: e.target.checked,
            });
            return;
        }
        setFormProducto({
            ...formProducto,
            [nameTextField]: valueTextField,
        });
    };

    //Funcion para cerrar este modal
    const handleClose = () => {
        setOpen(false);
    };

    return (
        <ModalForm
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
                            <FormControlLabel value={1} control={<Radio />} label="Membresía" />
                            <FormControlLabel value={2} control={<Radio />} label="Servicio" />
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
                        name="txtNombre"
                        label="Nombre del producto:"
                        fullWidth
                        variant="outlined"
                        placeholder="Ej. Membresía 6 meses"
                        value={formProducto.txtNombre}
                        onChange={handleChangeFormProducto}
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
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        name="txtCosto"
                        label="Costo del producto (sin IVA):"
                        fullWidth
                        variant="outlined"
                        placeholder="Ej. 600.00"
                        value={formProducto.txtCosto}
                        onChange={handleChangeFormProducto}
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
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <TextField
                        name="txtIcono"
                        label="Ícono (Consultar manual):"
                        fullWidth
                        variant="outlined"
                        placeholder="Ej. f479"
                        value={formProducto.txtIcono}
                        onChange={handleChangeFormProducto}
                    />
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
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="primary" fullWidth>
                        Guardar
                    </Button>
                </Grid>
                <Grid item sm={6} xs={12}>
                    <Button variant="contained" color="secondary" fullWidth onClick={handleClose}>
                        Cancelar
                    </Button>
                </Grid>
            </Grid>
        </ModalForm>
    );
};

export default FormProducto;
