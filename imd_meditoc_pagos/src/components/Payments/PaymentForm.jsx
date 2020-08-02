import PropTypes from "prop-types";
import React, { Fragment, useState, useEffect } from "react";
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
import { MdEmail, MdPerson, MdDateRange, MdVerifiedUser, MdAttachMoney } from "react-icons/md";
import { FaCreditCard, FaCcVisa, FaCcMastercard, FaCcAmex } from "react-icons/fa";
import AddCoupon from "./AddCoupon";
import InputExpirationDate from "./InputExpirationDate";
import InputCardNumber from "./InputCardNumber";
import InputCVV from "./InputCVV";
import { serverWs, serverWa } from "../../configuration/serverConfig";
import { apiBuy, apiRevalidateCoupon } from "../../configuration/apiConfig";
import apiKeyToken, { apiKeyLanguage } from "../../configuration/tokenConfig";
import {
    rxVisaCard,
    rxMasterCard,
    rxAmexCard,
    rxVisaIcon,
    rxMasterIcon,
    rxAmexIcon,
    rxEmail,
} from "../../configuration/regexConfig";

/*****************************************************
 * Descripción: Formulario de pago
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const PaymentForm = (props) => {
    const { productList, monthlyPayments, entCoupon, setEntCoupon, setEntOrder, setErrorOrder, funcLoader } = props;

    //Tamaño del icono para los inputs del formulario
    const iconSize = 25;

    //Color de los iconos del formulario (blue - habilitado / gray - deshabilitado)
    const [iconClass, setIconClass] = useState("secondary-gray");

    //Año actual (formato completo)
    const fullYearNow = new Date().getFullYear();

    //Año actual (primeros 2 dígitos)
    const yearNow = fullYearNow.toString().substring(0, 2);

    //Máximo de años permitidos para la fecha de expiración a partir del año actual
    const maxYearExpirationDate = 15;

    //Datos del formulario de pagos
    const [paymentForm, setPaymentForm] = useState({
        txtCardNumber: "",
        txtExpirationDate: "",
        txtCVV: "",
        txtName: "",
        txtEmail: "",
        txtModality: "1",
    });

    //Mes de expiración ingresado
    const [monthExpirationDate, setMonthExpirationDate] = useState(0);

    //Año de expiración ingresado
    const [yearExpirationDate, setYearExpirationDate] = useState(0);

    //Mostrar diálogo para ingresar cupón
    const [couponDialogOpen, setCouponDialogOpen] = useState(false);

    //Número de tarjeta inválido
    const [invalidCardNumber, setInvalidCardNumber] = useState(true);

    //Fecha de expiración inválida
    const [invalidExpirationDate, setInvalidExpirationDate] = useState(true);

    //Email proporcionado inválido
    const [invalidEmail, setInvalidEmail] = useState(true);

    //Ícono de tarjeta ingresada
    const [iconCardNumber, setIconCardNumber] = useState(<FaCreditCard size={iconSize} />);

    const [formErrorMessage, setFormErrorMessage] = useState("");

    useEffect(() => {
        setIconClass(productList.length === 0 ? "secondary-gray" : "secondary-blue");
    }, [productList]);

    //Evento change para los valores del formulario
    const handleChangePaymentForm = (e) => {
        switch (e.target.name) {
            case "txtCardNumber":
                funcValidateCardNumber(e.target.value);
                break;

            case "txtExpirationDate":
                funcValidateExpirationDate(e.target.value);
                break;

            case "txtEmail":
                funcValidateEmail(e.target.value);
                break;

            default:
                setPaymentForm({
                    ...paymentForm,
                    [e.target.name]: e.target.value,
                });
                break;
        }
    };

    //Validar número de tarjeta ingresado
    const funcValidateCardNumber = (cardNumber) => {
        const cardNumberCompare = cardNumber.replace(/ /g, "");

        if (isNaN(cardNumberCompare)) {
            setInvalidCardNumber(true);
            return;
        }

        if (cardNumberCompare.length > 16) {
            setInvalidCardNumber(true);
            return;
        }

        const validateCardNetWork = funcValidateCardNetwork(cardNumberCompare);
        const validateCardNetworkIcon = funcValidateCardNetworkIcon(cardNumberCompare);

        setInvalidCardNumber(!validateCardNetWork);
        setIconCardNumber(validateCardNetworkIcon);

        setPaymentForm({
            ...paymentForm,
            txtCardNumber: cardNumber,
        });
    };

    //Validar proveedor de servicio financiero de la tarjeta ingresada
    const funcValidateCardNetwork = (cardNumber = "") => {
        if (rxVisaCard.test(cardNumber)) {
            return true;
        }

        if (rxMasterCard.test(cardNumber)) {
            return true;
        }

        if (rxAmexCard.test(cardNumber)) {
            return true;
        }

        return false;
    };

    //Validar icono de proveedor de servicio financiero de la tarjeta ingresada
    const funcValidateCardNetworkIcon = (numero = "") => {
        if (rxVisaIcon.test(numero)) {
            return <FaCcVisa size={iconSize} className={iconClass} />;
        }

        if (rxMasterIcon.test(numero)) {
            return <FaCcMastercard size={iconSize} className={iconClass} />;
        }

        if (rxAmexIcon.test(numero)) {
            return <FaCcAmex size={iconSize} className={iconClass} />;
        }

        return <FaCreditCard size={iconSize} />;
    };

    //Validar fecha de expiración ingresado
    const funcValidateExpirationDate = (expirationDate) => {
        let month = 0;
        let year = 0;

        if (expirationDate.length > 0) {
            month = parseInt(expirationDate.substring(0, 2));

            setMonthExpirationDate(month);

            if (expirationDate.length > 3) {
                year = parseInt(yearNow + expirationDate.substring(3));

                setYearExpirationDate(year);
            }
        } else {
            setInvalidExpirationDate(true);
        }

        if (month <= 0 || month > 12 || year < fullYearNow || year > fullYearNow + maxYearExpirationDate) {
            setInvalidExpirationDate(true);
        } else {
            setInvalidExpirationDate(false);
        }

        setPaymentForm({
            ...paymentForm,
            txtExpirationDate: expirationDate,
        });
    };

    //Validar formato de email ingresado
    const funcValidateEmail = (email) => {
        if (rxEmail.test(email)) {
            setInvalidEmail(false);
        } else {
            setInvalidEmail(true);
        }

        setPaymentForm({
            ...paymentForm,
            txtEmail: email,
        });
    };

    //Evento submit del formulario de pagos
    const handleClickSubmitPaymentForm = (e) => {
        e.preventDefault();

        setFormErrorMessage("");

        window.Conekta.setPublicKey(apiKeyToken);
        window.Conekta.setLanguage(apiKeyLanguage);

        if (invalidCardNumber) {
            setFormErrorMessage("Número de tarjeta no válido");
            return;
        }

        if (invalidExpirationDate) {
            setFormErrorMessage("Fecha de vencimiento no válido");
            return;
        }

        if (paymentForm.txtCVV.length < 3) {
            setFormErrorMessage("Código de verificación no válido");
            return;
        }

        if (paymentForm.txtName === "") {
            setFormErrorMessage("Ingrese el nombre de tarjetahabiente");
            return;
        }

        if (invalidEmail) {
            setFormErrorMessage("Correo electrónico no válido");
            return;
        }

        if (paymentForm.txtModality === "") {
            setFormErrorMessage("Seleccione modalidad de pago");
            return;
        }

        funcLoader(true, "Validando datos de compra...");

        const tokenParams = {
            card: {
                number: paymentForm.txtCardNumber, //4263982640269299
                name: paymentForm.txtName, //JUANITO PEREZ
                exp_year: yearExpirationDate, //2023
                exp_month: monthExpirationDate, //02  --------   0223
                cvc: paymentForm.txtCVV, //837
            },
        };

        window.Conekta.Token.create(tokenParams, successResponseHandler, errorResponseHandler);
    };

    //Evento para tokenización correcta de la tarjeta ingresada
    const successResponseHandler = (token) => {
        const entCreateOrder = {
            currency: "MXN",
            coupon: entCoupon === null ? null : entCoupon.fiIdCupon,
            pacienteUnico: {
                sEmail: paymentForm.txtEmail,
                sNombre: paymentForm.txtName,
                sTelefono: null,
            },
            lstLineItems: productList.map((product) => ({
                product_id: product.id,
                quantity: product.qty,
            })),
            lstCharges: [
                {
                    payment_: {
                        monthly_installments: parseInt(paymentForm.txtModality),
                        type: "card",
                        token_id: token.id,
                    },
                },
            ],
        };

        funcApiBuy(entCreateOrder);
    };

    //Evento para error de tokenización de la tarjeta ingresada
    const errorResponseHandler = () => {
        setErrorOrder(true);
        setEntOrder({ error: true });

        funcLoader();
    };

    //Consumir servicio para realizar la compra de las membresias/orientaciones
    const funcApiBuy = async (entCreateOrder) => {
        try {
            if (entCreateOrder.coupon !== null) {
                const responseRevalidate = await funcApiRevalidateCoupon(
                    entCreateOrder.coupon,
                    entCreateOrder.pacienteUnico.sEmail
                );

                if (!responseRevalidate) {
                    return;
                }
            }

            funcLoader(true, "Realizando compra...");

            const apiResponse = await fetch(`${serverWs}${apiBuy}`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(entCreateOrder),
            });

            const response = await apiResponse.json();

            if (response.bRespuesta === true) {
                setEntOrder(response.sParameter1);
                setErrorOrder(false);

                sessionStorage.removeItem("lstItems");
            } else {
                setEntOrder({ error: true });
                setErrorOrder(true);
            }
        } catch (error) {
            setEntOrder({ error: true });
            setErrorOrder(true);
        }

        funcLoader();
    };

    //Cosumir servicio para revalidar el cupón con el correo de compra
    const funcApiRevalidateCoupon = async (piIdCupon, psEmail) => {
        const errorRevalidateCoupon = "Ocurrió un error al procesar la compra. Intente más tarde";
        funcLoader(true, "Aplicando cupón...");

        let responseMethod = false;

        try {
            const apiResponse = await fetch(
                `${serverWa}${apiRevalidateCoupon}?piIdCupon=${piIdCupon}&psEmail=${psEmail}`
            );

            const response = await apiResponse.json();

            if (response.Code === 0) {
                if (response.Result === true) {
                    responseMethod = true;
                } else {
                    setFormErrorMessage("Ya has aplicado este cupón anteriormente");
                }
            } else {
                setFormErrorMessage(errorRevalidateCoupon);
            }
        } catch (error) {
            setFormErrorMessage(errorRevalidateCoupon);
        }
        funcLoader();
        return responseMethod;
    };

    return (
        <Fragment>
            <div className="pay-info-payment-container">
                <div className="pay-info-payment-form">
                    <Grid container spacing={3}>
                        <Grid item xs={12}>
                            <TextField
                                id="txtCardNumber"
                                name="txtCardNumber"
                                variant="outlined"
                                label="Número de tarjeta:"
                                placeholder="0000 0000 0000 0000"
                                fullWidth
                                InputProps={{
                                    startAdornment: <InputAdornment position="start">{iconCardNumber}</InputAdornment>,
                                    inputComponent: InputCardNumber,
                                }}
                                disabled={productList.length === 0}
                                value={paymentForm.txtCardNumber}
                                onChange={handleChangePaymentForm}
                                error={invalidCardNumber}
                                autoComplete="off"
                            />
                        </Grid>
                        <Grid item xs={6}>
                            <TextField
                                id="txtExpirationDate"
                                name="txtExpirationDate"
                                variant="outlined"
                                label="Fecha de vencimiento:"
                                placeholder="MM/AA"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <MdDateRange size={iconSize} className={iconClass} />
                                        </InputAdornment>
                                    ),
                                    inputComponent: InputExpirationDate,
                                }}
                                disabled={productList.length === 0}
                                value={paymentForm.txtExpirationDate}
                                onChange={handleChangePaymentForm}
                                error={invalidExpirationDate}
                                autoComplete="off"
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
                                            <MdVerifiedUser size={iconSize} className={iconClass} />
                                        </InputAdornment>
                                    ),
                                    inputComponent: InputCVV,
                                }}
                                disabled={productList.length === 0}
                                type="password"
                                autoComplete="new-password"
                                value={paymentForm.txtCVV}
                                onChange={handleChangePaymentForm}
                                error={paymentForm.txtCVV.length < 3}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                id="txtName"
                                name="txtName"
                                variant="outlined"
                                label="Nombre completo:"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <MdPerson size={iconSize} className={iconClass} />
                                        </InputAdornment>
                                    ),
                                }}
                                disabled={productList.length === 0}
                                autoComplete="off"
                                value={paymentForm.txtName}
                                onChange={handleChangePaymentForm}
                                error={paymentForm.txtName === ""}
                                helperText="Como aparece en la tarjeta"
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                id="txtEmail"
                                name="txtEmail"
                                variant="outlined"
                                label="Correo electrónico"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <MdEmail size={iconSize} className={iconClass} />
                                        </InputAdornment>
                                    ),
                                }}
                                disabled={productList.length === 0}
                                autoComplete="off"
                                value={paymentForm.txtEmail}
                                onChange={handleChangePaymentForm}
                                error={invalidEmail}
                                helperText="Las credenciales de acceso serán enviados al correo proporcionado"
                            />
                        </Grid>
                        <Grid item xs={6} className="left">
                            <FormControl
                                variant="outlined"
                                fullWidth
                                disabled={productList.length === 0 || monthlyPayments.length === 0}
                            >
                                <InputLabel id="lblModality">Modalidad de pago:</InputLabel>
                                <Select
                                    id="txtModality"
                                    name="txtModality"
                                    labelId="lblModality"
                                    label="Modalidad de pago:"
                                    displayEmpty
                                    value={paymentForm.txtModality}
                                    onChange={handleChangePaymentForm}
                                    startAdornment={
                                        <InputAdornment position="start">
                                            <MdAttachMoney size={iconSize} className={iconClass} />
                                        </InputAdornment>
                                    }
                                    error={paymentForm.txtModality === ""}
                                >
                                    <MenuItem value="1">Una sola exhibición</MenuItem>
                                    {monthlyPayments.map((diferimiento) => (
                                        <MenuItem key={diferimiento.value} value={diferimiento.value}>
                                            {diferimiento.label}
                                        </MenuItem>
                                    ))}
                                </Select>
                            </FormControl>
                        </Grid>
                        <Grid item xs={6}>
                            {entCoupon === null ? (
                                <Button
                                    variant="contained"
                                    color="secondary"
                                    size="large"
                                    fullWidth
                                    disabled={productList.length === 0}
                                    onClick={() => setCouponDialogOpen(true)}
                                >
                                    <Typography variant="caption">
                                        AGREGAR CÓDIGO <br /> DE DESCUENTO
                                    </Typography>
                                </Button>
                            ) : (
                                <Button
                                    variant="contained"
                                    color="secondary"
                                    size="large"
                                    fullWidth
                                    disabled={productList.length === 0}
                                    onClick={() => setEntCoupon(null)}
                                >
                                    <Typography variant="caption">
                                        ELIMINAR CÓDIGO
                                        <br /> DE DESCUENTO
                                    </Typography>
                                </Button>
                            )}
                        </Grid>
                        <Grid item xs={12}>
                            <Button
                                variant="contained"
                                color="primary"
                                size="large"
                                fullWidth
                                disabled={productList.length === 0}
                                onClick={handleClickSubmitPaymentForm}
                            >
                                <Typography variant="caption">PAGAR</Typography>
                            </Button>
                        </Grid>
                        <Grid item xs={12}>
                            <span className="pay-error-description">{formErrorMessage}</span>
                        </Grid>
                    </Grid>
                </div>
            </div>
            <AddCoupon
                couponDialogOpen={couponDialogOpen}
                setCouponDialogOpen={setCouponDialogOpen}
                setEntCoupon={setEntCoupon}
                funcLoader={funcLoader}
            />
        </Fragment>
    );
};

PaymentForm.propTypes = {
    entCoupon: PropTypes.object,
    funcLoader: PropTypes.func,
    monthlyPayments: PropTypes.array,
    productList: PropTypes.array,
    setEntCoupon: PropTypes.func,
    setEntOrder: PropTypes.func,
    setErrorOrder: PropTypes.func,
};

export default PaymentForm;
