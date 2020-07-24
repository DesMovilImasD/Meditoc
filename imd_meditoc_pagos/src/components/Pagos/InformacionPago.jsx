import React, { Fragment, useState } from "react";
import {
    Grid,
    TextField,
    InputAdornment,
    InputLabel,
    Select,
    MenuItem,
    FormControl,
    Button,
    Typography,
} from "@material-ui/core";
import {
    MdEmail,
    MdPerson,
    MdDateRange,
    MdVerifiedUser,
    MdAttachMoney,
} from "react-icons/md";
import {
    FaCreditCard,
    FaCcVisa,
    FaCcMastercard,
    FaCcAmex,
    FaCcDinersClub,
    FaCcDiscover,
    FaCcJcb,
} from "react-icons/fa";
import AgregarCupon from "./AgregarCupon";
import InputFechaVencimiento from "./InputFechaVencimiento";
import InputNumeroTarjeta from "./InputNumeroTarjeta";
import InputCodigoVerificacion from "./InputCodigoVerificacion";

const InformacionPago = (props) => {
    const {
        listaProductos,
        entCupon,
        setEntCupon,
        setOrdenGenerada,
        funcLoader,
    } = props;

    const iconSize = 25;

    const [formularioPago, setFormularioPago] = useState({
        txtTarjeta: "",
        txtFechaVencimiento: "",
        txtCVV: "",
        txtNombre: "",
        txtCorreo: "",
        txtModalidad: "",
    });

    const [errorServiceMsg, setErrorServiceMsg] = useState("");

    const [agregarCuponOpen, setAgregarCuponOpen] = useState(false);

    const iconStateClass =
        listaProductos.length === 0 ? "secondary-gray" : "secondary-blue";

    //Validar tarjeta
    const [numeroTarjetaInvalido, setNumeroTarjetaInvalido] = useState(true);
    const [tarjetaIcono, setTarjetaIcono] = useState(
        <FaCreditCard size={iconSize} />
    );

    //Validar fecha vencimiento
    const [fechaVencimientoInvalido, setFechaVencimientoInvalido] = useState(
        true
    );

    const [correoInvalido, setCorreoInvalido] = useState(true);

    const handleChangeFormularioPago = (e) => {
        const inputName = e.target.name;

        if (inputName === "txtTarjeta") {
            funcValidarTarjeta(e.target.value);
            return;
        }

        if (inputName === "txtFechaVencimiento") {
            funcValidarFechaVencimiento(e.target.value);
            return;
        }

        if (inputName === "txtCorreo") {
            funcValidarCorreo(e.target.value);
            return;
        }

        setFormularioPago({
            ...formularioPago,
            [inputName]: e.target.value,
        });
    };

    const funcValidarTarjeta = (inputValue) => {
        const valueCompare = inputValue.replace(/ /g, "");

        if (isNaN(valueCompare)) {
            return;
        }

        if (valueCompare.length > 16) {
            return;
        }

        setNumeroTarjetaInvalido(!funcValidarTipoTarjeta(valueCompare));
        validarIconoTarjeta(valueCompare);
        //setNumeroTarjeta(inputValue);
        setFormularioPago({
            ...formularioPago,
            txtTarjeta: inputValue,
        });
    };

    const funcValidarTipoTarjeta = (numero = "") => {
        //visa
        if (/^4[0-9]{12}(?:[0-9]{3})?$/.test(numero)) {
            return true;
        }
        //mastercard
        if (
            /^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$/.test(
                numero
            )
        ) {
            return true;
        }
        //american express
        if (/^3[47][0-9]{13}$/.test(numero)) {
            return true;
        }
        //diners club
        if (/^3(?:0[0-5]|[68][0-9])[0-9]{11}$/.test(numero)) {
            return true;
        }
        //discover
        if (/^6(?:011|5[0-9]{2})[0-9]{12}$/.test(numero)) {
            return true;
        }
        //jcb
        if (/^(?:2131|1800|35\d{3})\d{11}$/.test(numero)) {
            return true;
        }
        return false;
    };

    const validarIconoTarjeta = (numero = "") => {
        //visa
        if (/^4\d+$/.test(numero)) {
            setTarjetaIcono(
                <FaCcVisa size={iconSize} className={iconStateClass} />
            );
            return;
        }
        //mastercard
        if (
            /^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)\d+$/.test(
                numero
            )
        ) {
            setTarjetaIcono(
                <FaCcMastercard size={iconSize} className={iconStateClass} />
            );
            return;
        }
        //american express
        if (/^3[47]\d+$/.test(numero)) {
            setTarjetaIcono(
                <FaCcAmex size={iconSize} className={iconStateClass} />
            );
            return;
        }
        //diners club
        if (/^3(?:0[0-5]|[68][0-9])\d+$/.test(numero)) {
            setTarjetaIcono(
                <FaCcDinersClub size={iconSize} className={iconStateClass} />
            );
            return;
        }
        //discover
        if (/^6(?:011|5[0-9]{2})\d+$/.test(numero)) {
            setTarjetaIcono(
                <FaCcDiscover size={iconSize} className={iconStateClass} />
            );
            return;
        }
        //jcb
        if (/^(?:2131|1800|35\d{3})\d+$/.test(numero)) {
            setTarjetaIcono(
                <FaCcJcb size={iconSize} className={iconStateClass} />
            );
            return;
        }
        setTarjetaIcono(<FaCreditCard size={iconSize} />);
    };

    const yearNow = new Date().getFullYear();
    const yearMil = yearNow.toString().substring(0, 2);

    const [monthForm, setMonthForm] = useState(0);
    const [yearForm, setYearForm] = useState(0);

    const funcValidarFechaVencimiento = (fechaVencimientoValue) => {
        let month = 0;
        let year = 0;

        if (fechaVencimientoValue.length > 0) {
            month = parseInt(fechaVencimientoValue.substring(0, 2));
            setMonthForm(month);
            if (fechaVencimientoValue.length > 3) {
                year = parseInt(yearMil + fechaVencimientoValue.substring(3));
                setYearForm(year);
            }
        } else {
            setFechaVencimientoInvalido(true);
        }

        if (month <= 0 || month > 12 || year < yearNow || year > yearNow + 15) {
            setFechaVencimientoInvalido(true);
        } else {
            setFechaVencimientoInvalido(false);
        }

        //setFechaVencimiento(fechaVencimientoValue);
        setFormularioPago({
            ...formularioPago,
            txtFechaVencimiento: fechaVencimientoValue,
        });
    };

    const funcValidarCorreo = (correoValue) => {
        if (/^[^@]+@[^@]+\.[a-zA-Z]{2,}$/.test(correoValue)) {
            setCorreoInvalido(false);
        } else {
            setCorreoInvalido(true);
        }
        setFormularioPago({
            ...formularioPago,
            txtCorreo: correoValue,
        });
    };

    const handleClickSubmitPago = (e) => {
        e.preventDefault();

        window.Conekta.setPublicKey("key_GyCqFsGWvYaFP3a7C9Lyfjg");
        window.Conekta.setLanguage("es");

        if (numeroTarjetaInvalido) {
            return;
        }
        if (fechaVencimientoInvalido) {
            return;
        }
        if (formularioPago.txtCVV.length < 3) {
            return;
        }
        if (formularioPago.txtNombre === "") {
            return;
        }
        if (correoInvalido) {
            return;
        }
        if (formularioPago.txtModalidad === "") {
            return;
        }
        const tokenParams = {
            card: {
                number: formularioPago.txtTarjeta, //4263982640269299
                name: formularioPago.txtNombre, //JUANITO PEREZ
                exp_year: yearForm, //2023
                exp_month: monthForm, //02  --------   0223
                cvc: formularioPago.txtCVV, //837
            },
        };
        funcLoader(true, "Validando datos de compra...");
        window.Conekta.Token.create(
            tokenParams,
            successResponseHandler,
            errorResponseHandler
        );
        funcLoader();
    };

    const successResponseHandler = (token) => {
        const entCreateOrder = {
            currency: "MXN",
            coupon: entCupon === null ? null : entCupon.fiIdCupon,
            pacienteUnico: {
                bBaja: false,
                bTerminosyCondiciones: true,
                iIdUsuario: 0,
                sApeMaterno: null,
                sApePaterno: null,
                sEmail: formularioPago.txtCorreo,
                sFolio: null,
                sMensajeRespuesta: null,
                sNombre: formularioPago.txtNombre,
                sPassword: null,
                sTelefono: null,
                sTipoServicio: null,
            },
            lstLineItems: listaProductos.map((producto) => ({
                name: producto.nombre,
                product_id: producto.id,
                quantity: producto.cantidad,
                unit_price: producto.precio * 100,
                monthsExpiration:
                    producto.id === 289 ? 6 : producto.id === 290 ? 12 : 0,
            })),
            lstCharges: [
                {
                    payment_: {
                        monthly_installments: parseInt(
                            formularioPago.txtModalidad
                        ),
                        type: "card",
                        token_id: token.id,
                        expires_at: 0,
                    },
                },
            ],
        };
        console.log(entCreateOrder);

        funcBuy(entCreateOrder);
    };

    const errorResponseHandler = (error) => {
        console.log(error);
        funcLoader();
    };

    const funcBuy = async (entCreateOrder) => {
        funcLoader(true, "Realizando compra...");
        try {
            const apiResponse = await fetch(
                "/ClientesService.svc/NewUniqueQuery",
                {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify(entCreateOrder),
                }
            );
            const response = await apiResponse.json();
            console.log(response);
            if (response.bRespuesta === true) {
                setOrdenGenerada(response.sParameter1);
                sessionStorage.removeItem("lstItems");
            } else {
                setErrorServiceMsg(response.sMensaje);
            }
        } catch (error) {
            setErrorServiceMsg(
                "Ocurrió un error al procesar la compra. Intente más tarde."
            );
        }
        funcLoader();
    };

    return (
        <Fragment>
            <div className="pagos-info-pago-contenedor">
                <div className="pagos-info-pago-formulario">
                    <Grid container spacing={3}>
                        <Grid item xs={12}>
                            <TextField
                                id="txtTarjeta"
                                name="txtTarjeta"
                                variant="outlined"
                                label="Número de tarjeta:"
                                placeholder="0000 0000 0000 0000"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            {tarjetaIcono}
                                        </InputAdornment>
                                    ),
                                    inputComponent: InputNumeroTarjeta,
                                }}
                                disabled={listaProductos.length === 0}
                                value={formularioPago.txtTarjeta}
                                onChange={handleChangeFormularioPago}
                                error={numeroTarjetaInvalido}
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <TextField
                                id="txtFechaVencimiento"
                                name="txtFechaVencimiento"
                                variant="outlined"
                                label="Fecha de vencimiento:"
                                placeholder="MM/AA"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <MdDateRange
                                                size={iconSize}
                                                className={iconStateClass}
                                            />
                                        </InputAdornment>
                                    ),
                                    inputComponent: InputFechaVencimiento,
                                }}
                                disabled={listaProductos.length === 0}
                                value={formularioPago.txtFechaVencimiento}
                                onChange={handleChangeFormularioPago}
                                error={fechaVencimientoInvalido}
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <TextField
                                id="txtCVV"
                                name="txtCVV"
                                variant="outlined"
                                label="CVV:"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <MdVerifiedUser
                                                size={iconSize}
                                                className={iconStateClass}
                                            />
                                        </InputAdornment>
                                    ),
                                    inputComponent: InputCodigoVerificacion,
                                }}
                                disabled={listaProductos.length === 0}
                                type="password"
                                autoComplete="new-password"
                                value={formularioPago.txtCVV}
                                onChange={handleChangeFormularioPago}
                                error={formularioPago.txtCVV.length < 3}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                id="txtNombre"
                                name="txtNombre"
                                variant="outlined"
                                label="Nombre completo:"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <MdPerson
                                                size={iconSize}
                                                className={iconStateClass}
                                            />
                                        </InputAdornment>
                                    ),
                                }}
                                disabled={listaProductos.length === 0}
                                value={formularioPago.txtNombre}
                                onChange={handleChangeFormularioPago}
                                error={formularioPago.txtNombre === ""}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                id="txtCorreo"
                                name="txtCorreo"
                                variant="outlined"
                                label="Correo electrónico"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <MdEmail
                                                size={iconSize}
                                                className={iconStateClass}
                                            />
                                        </InputAdornment>
                                    ),
                                }}
                                disabled={listaProductos.length === 0}
                                value={formularioPago.txtCorreo}
                                onChange={handleChangeFormularioPago}
                                error={correoInvalido}
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <FormControl
                                variant="outlined"
                                fullWidth
                                disabled={listaProductos.length === 0}
                            >
                                <InputLabel id="lblModalidad">
                                    Modalidad de pago:
                                </InputLabel>
                                <Select
                                    id="txtModalidad"
                                    name="txtModalidad"
                                    labelId="lblModalidad"
                                    label="Modalidad de pago:"
                                    displayEmpty
                                    value={formularioPago.txtModalidad}
                                    onChange={handleChangeFormularioPago}
                                    startAdornment={
                                        <InputAdornment position="start">
                                            <MdAttachMoney
                                                size={iconSize}
                                                className={iconStateClass}
                                            />
                                        </InputAdornment>
                                    }
                                    error={formularioPago.txtModalidad === ""}
                                >
                                    <MenuItem value="">
                                        --Seleccionar modalidad--
                                    </MenuItem>
                                    <MenuItem value={1}>
                                        Una sola exhibición
                                    </MenuItem>
                                    <MenuItem value={3}>
                                        3 meses sin intereses
                                    </MenuItem>
                                    <MenuItem value={6}>
                                        6 meses sin intereses
                                    </MenuItem>
                                    <MenuItem value={9}>
                                        9 meses sin intereses
                                    </MenuItem>
                                    <MenuItem value={12}>
                                        12 meses sin intereses
                                    </MenuItem>
                                    <MenuItem value={18}>
                                        18 meses sin intereses
                                    </MenuItem>
                                </Select>
                            </FormControl>
                        </Grid>
                        <Grid item xs={6}>
                            <Button
                                variant="contained"
                                color="secondary"
                                size="large"
                                fullWidth
                                disabled={listaProductos.length === 0}
                                onClick={() => setAgregarCuponOpen(true)}
                            >
                                <Typography variant="caption">
                                    AGREGAR CUPÓN <br /> DE DESCUENTO
                                </Typography>
                            </Button>
                        </Grid>
                        <Grid item xs={12}>
                            <Button
                                variant="contained"
                                color="primary"
                                size="large"
                                fullWidth
                                disabled={listaProductos.length === 0}
                                onClick={handleClickSubmitPago}
                            >
                                <Typography variant="caption">PAGAR</Typography>
                            </Button>
                        </Grid>
                        <Grid item xs={12}>
                            <Typography className="error">
                                {errorServiceMsg}
                            </Typography>
                        </Grid>
                    </Grid>
                </div>
            </div>
            <AgregarCupon
                agregarCuponOpen={agregarCuponOpen}
                setAgregarCuponOpen={setAgregarCuponOpen}
                setEntCupon={setEntCupon}
                funcLoader={funcLoader}
            />
        </Fragment>
    );
};

export default InformacionPago;
