import React, { useState } from "react";
import {
    Dialog,
    DialogContent,
    TextField,
    Grid,
    Button,
    Typography,
} from "@material-ui/core";
import serverMain from "../../configuration/serverConfig";

const AgregarCupon = (props) => {
    const {
        agregarCuponOpen,
        setAgregarCuponOpen,
        setEntCupon,
        funcLoader,
    } = props;

    const apiValidarCupon = "api/promociones/validar/cupon";

    const [txtCodigo, setTxtCodigo] = useState("");

    const [msgErrorValidarCupon, setMsgErrorValidarCupon] = useState("");

    const handleChangeCodigo = (e) => {
        const value = e.target.value;
        setTxtCodigo(value === "" ? "" : value.toUpperCase());
    };

    const handleCloseAgregarCupon = () => {
        setTxtCodigo("");
        setMsgErrorValidarCupon("");
        setAgregarCuponOpen(false);
    };

    const funcValidarCupon = async () => {
        funcLoader(true, "Validando cupón...");
        try {
            const apiResponse = await fetch(
                `${serverMain}${apiValidarCupon}?psCodigo=${txtCodigo}`
            );
            const response = await apiResponse.json();
            if (response.Code === 0) {
                setMsgErrorValidarCupon("");
                setEntCupon(response.Result);
                handleCloseAgregarCupon();
            } else {
                setMsgErrorValidarCupon(response.Message);
            }
        } catch (error) {
            setMsgErrorValidarCupon(
                "Ocurrió un error al intentar validar el cupón"
            );
        }
        funcLoader();
    };

    return (
        <Dialog open={agregarCuponOpen} onClose={handleCloseAgregarCupon}>
            <DialogContent>
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <span className="pagos-ingresar-cupon-titulo">
                            Ingrese código de descuento
                        </span>
                    </Grid>
                    <Grid item xs={12}>
                        <TextField
                            name="txtCodigoDescuento"
                            variant="standard"
                            fullWidth
                            autoFocus
                            value={txtCodigo}
                            onChange={handleChangeCodigo}
                            error={msgErrorValidarCupon !== ""}
                            helperText={msgErrorValidarCupon}
                        />
                    </Grid>
                    <Grid item xs={6}>
                        <Button
                            className="secondary-gray"
                            onClick={handleCloseAgregarCupon}
                        >
                            <Typography
                                className="secondary-gray"
                                variant="button"
                            >
                                Cancelar
                            </Typography>
                        </Button>
                    </Grid>
                    <Grid item xs={6} className="derecha">
                        <Button
                            color="primary"
                            onClick={funcValidarCupon}
                            disabled={txtCodigo === ""}
                        >
                            Aceptar
                        </Button>
                    </Grid>
                </Grid>
            </DialogContent>
        </Dialog>
    );
};

export default AgregarCupon;
