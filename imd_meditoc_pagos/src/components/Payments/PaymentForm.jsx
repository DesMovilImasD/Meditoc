import {
    Button,
    Checkbox,
    FormControl,
    FormControlLabel,
    Grid,
    InputAdornment,
    InputLabel,
    MenuItem,
    Select,
    TextField,
    Tooltip,
    Typography,
    makeStyles,
} from "@material-ui/core";
import { FaCcAmex, FaCcMastercard, FaCcVisa, FaCreditCard } from "react-icons/fa";
import { MdAttachMoney, MdDateRange, MdEmail, MdInfo, MdPerson, MdPhone, MdVerifiedUser } from "react-icons/md";
import React, { Fragment, useEffect, useState } from "react";
import { apiBuy, apiRevalidateCoupon } from "../../configuration/apiConfig";
import { cardEnviromentProd, lstCardTest } from "../../configuration/cardConfig";
import {
    rxAmexCard,
    rxAmexIcon,
    rxEmail,
    rxMasterCard,
    rxMasterIcon,
    rxVisaCard,
    rxVisaIcon,
} from "../../configuration/regexConfig";

import InputCVV from "../Inputs/InputCVV";
import InputCardNumber from "../Inputs/InputCardNumber";
import InputExpirationDate from "../Inputs/InputExpirationDate";
import InputPhone from "../Inputs/InputPhone";
import PropTypes from "prop-types";
import { apiKeyLanguage } from "../../configuration/tokenConfig";
import { serverMain } from "../../configuration/serverConfig";
import { useTax } from "../../configuration/taxConfig";

const useStyles = makeStyles((theme) => ({
    paylabel: {
        fontSize: 18,
        backgroundColor: "#fff",
        paddingRight: 12,
        paddingLeft: 6,
    },
}));

/*****************************************************
 * Descripción: Formulario de pago
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const PaymentForm = (props) => {
    const {
        appInfo,
        productList,
        monthlyPayments,
        totalPayment,
        entCoupon,
        formErrorMessage,
        setFormErrorMessage,
        setEntOrder,
        setErrorOrder,
        funcLoader,
    } = props;

    //Tamaño del icono para los inputs del formulario
    const iconSize = 25;

    const classes = useStyles();

    //Datos del formulario de pagos
    const [paymentForm, setPaymentForm] = useState({
        txtCardNumber: "",
        txtExpirationDate: "",
        txtCVV: "",
        txtName: "",
        txtEmail: "",
        txtPhone: "",
        txtModality: "1",
    });

    //Guardar validaciones al dar clic en pagar
    const [paymentFormInvalidInputs, setPaymentFormInvalidInputs] = useState({
        txtCardNumber: false,
        txtExpirationDate: false,
        txtCVV: false,
        txtName: false,
        txtEmail: false,
        txtPhone: false,
        txtModality: false,
    });

    //Color de los iconos del formulario (blue - habilitado / gray - deshabilitado)
    const [iconClass, setIconClass] = useState("secondary-blue");

    //Año actual (formato completo)
    const fullYearNow = new Date().getFullYear();

    //Año actual (primeros 2 dígitos)
    const yearNow = fullYearNow.toString().substring(0, 2);

    //Máximo de años permitidos para la fecha de expiración a partir del año actual
    const maxYearExpirationDate = 15;

    //Mes de expiración ingresado
    const [monthExpirationDate, setMonthExpirationDate] = useState(0);

    //Año de expiración ingresado
    const [yearExpirationDate, setYearExpirationDate] = useState(0);

    //Número de tarjeta inválido
    const [invalidCardNumber, setInvalidCardNumber] = useState(true);

    //Fecha de expiración inválida
    const [invalidExpirationDate, setInvalidExpirationDate] = useState(true);

    //Email proporcionado inválido
    const [invalidEmail, setInvalidEmail] = useState(true);

    //Telefono proporcionado debe ser completo
    const [invalidPhone, setInvalidPhone] = useState(false);

    //Guardar consentimiento del cliente de aceptar las políticas de Meditoc
    const [acceptedPolicies, setAcceptedPolicies] = useState(false);

    //Evento Change para el check de aceptar Términos y Condiciones
    const handleChangeAcceptPolicies = () => {
        setAcceptedPolicies(!acceptedPolicies);
    };

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

            case "txtPhone":
                funcValidatePhone(e.target.value);
                break;

            default:
                setPaymentForm({
                    ...paymentForm,
                    [e.target.name]: e.target.value,
                });
                break;
        }
    };

    //Evento submit del formulario de pagos
    const handleClickSubmitPaymentForm = (e) => {
        e.preventDefault();

        setFormErrorMessage("");

        window.Conekta.setPublicKey(appInfo.sConektaPublicKey);
        window.Conekta.setLanguage(apiKeyLanguage);

        let formErrorMessage = "";
        let errorForm = false;

        let paymentFormInvalidInputsTemp = {
            txtCardNumber: false,
            txtExpirationDate: false,
            txtCVV: false,
            txtName: false,
            txtEmail: false,
            txtPhone: false,
            txtModality: false,
        };

        if (invalidPhone) {
            //formErrorMessage = "Proporcione un número de teléfono válido";
            errorForm = true;
            paymentFormInvalidInputsTemp.txtPhone = true;
        }

        if (invalidEmail) {
            //formErrorMessage = "Correo electrónico no válido";
            errorForm = true;
            paymentFormInvalidInputsTemp.txtEmail = true;
        }
        if (paymentForm.txtName === "") {
            //formErrorMessage = "Ingrese el nombre de tarjetahabiente";
            errorForm = true;
            paymentFormInvalidInputsTemp.txtName = true;
        }
        if (paymentForm.txtCVV.length < 3) {
            //formErrorMessage = "Código de verificación no válido";
            errorForm = true;
            paymentFormInvalidInputsTemp.txtCVV = true;
        }
        if (invalidExpirationDate) {
            //formErrorMessage = "Fecha de vencimiento no válido";
            errorForm = true;
            paymentFormInvalidInputsTemp.txtExpirationDate = true;
        }
        if (paymentForm.txtModality === "") {
            formErrorMessage = "Seleccione modalidad de pago";
            errorForm = true;
            paymentFormInvalidInputsTemp.txtModality = true;
        }

        if (invalidCardNumber) {
            //formErrorMessage = "Número de tarjeta no válido";
            errorForm = true;
            paymentFormInvalidInputsTemp.txtCardNumber = true;
        }

        if (!acceptedPolicies) {
            formErrorMessage = "Es necesario aceptar los términos y condiciones para continuar";
            errorForm = true;
        }

        setPaymentFormInvalidInputs(paymentFormInvalidInputsTemp);

        if (errorForm) {
            setFormErrorMessage(formErrorMessage);
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

        let validateCardNetWork = funcValidateCardNetwork(cardNumberCompare);

        if (cardEnviromentProd) {
            if (lstCardTest.includes(cardNumberCompare)) {
                validateCardNetWork = false;
            }
        }

        setInvalidCardNumber(!validateCardNetWork);

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
    const funcValidateCardNetworkIcon = (cardNumber = "") => {
        cardNumber = cardNumber.replace(/ /g, "");
        if (rxVisaIcon.test(cardNumber)) {
            return <FaCcVisa size={iconSize} className={iconClass} />;
        }

        if (rxMasterIcon.test(cardNumber)) {
            return <FaCcMastercard size={iconSize} className={iconClass} />;
        }

        if (rxAmexIcon.test(cardNumber)) {
            return <FaCcAmex size={iconSize} className={iconClass} />;
        }

        return <FaCreditCard size={iconSize} className={iconClass} />;
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

    //Validar formato de telefono
    const funcValidatePhone = (phone) => {
        const phoneCompare = phone.replace(/ /g, "");
        if (phoneCompare !== "") {
            if (phoneCompare.length !== 10) {
                setInvalidPhone(true);
            } else {
                setInvalidPhone(false);
            }
        } else {
            setInvalidPhone(false);
        }
        setPaymentForm({
            ...paymentForm,
            txtPhone: phone,
        });
    };

    //Revalidar los meses sin interes al modificar el monto total a pagar
    const funcValidateAmounthMonthlyPayments = () => {
        if (paymentForm.txtModality === "12" && totalPayment < 1200) {
            setPaymentForm({ ...paymentForm, txtModality: "1" });
            return;
        }

        if (paymentForm.txtModality === "9" && totalPayment < 900) {
            setPaymentForm({ ...paymentForm, txtModality: "1" });
            return;
        }

        if (paymentForm.txtModality === "6" && totalPayment < 600) {
            setPaymentForm({ ...paymentForm, txtModality: "1" });
            return;
        }

        if (paymentForm.txtModality === "3" && totalPayment < 300) {
            setPaymentForm({ ...paymentForm, txtModality: "1" });
            return;
        }
    };

    //Evento para tokenización exitosa de la tarjeta ingresada
    const successResponseHandler = (token) => {
        const entCreateOrder = {
            currency: "MXN",
            coupon: entCoupon === null ? null : entCoupon.fiIdCupon,
            iIdOrigen: 1,
            tax: useTax,
            customer_info: {
                email: paymentForm.txtEmail,
                name: paymentForm.txtName,
                phone: paymentForm.txtPhone,
            },
            line_items: productList.map((product) => ({
                product_id: product.id,
                quantity: product.qty,
            })),
            charges: [
                {
                    payment_method: {
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
                    entCreateOrder.customer_info.email
                );

                if (!responseRevalidate) {
                    return;
                }
            }

            funcLoader(true, "Realizando compra...");

            const apiResponse = await fetch(`${serverMain}${apiBuy}`, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(entCreateOrder),
            });

            const response = await apiResponse.json();

            if (response.Code === 0 && response.Result !== null) {
                setEntOrder(response.Result);
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
                `${serverMain}${apiRevalidateCoupon}?piIdCupon=${piIdCupon}&psEmail=${psEmail}`
            );

            const response = await apiResponse.json();

            if (response.Code === 0) {
                if (response.Result === true) {
                    responseMethod = true;
                } else {
                    setFormErrorMessage("Tu pago no se puede procesar porque ya has aplicado este cupón");
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

    //Cambiar los iconos del fomulario a deshabilitados cuando no se tengan productos en el resumen de compra
    useEffect(() => {
        setIconClass(productList.length === 0 ? "secondary-gray" : "secondary-blue");
    }, [productList]);

    //Ejecutar funcValidateAmounthMonthlyPayments cada vez que cambien los valores de monthlyPayments
    useEffect(() => {
        funcValidateAmounthMonthlyPayments();

        // eslint-disable-next-line
    }, [monthlyPayments]);

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
                                placeholder="1111 2222 3333 4444"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            {funcValidateCardNetworkIcon(paymentForm.txtCardNumber)}
                                        </InputAdornment>
                                    ),
                                    inputComponent: InputCardNumber,
                                }}
                                InputLabelProps={{
                                    className: classes.paylabel,
                                }}
                                disabled={productList.length === 0}
                                value={paymentForm.txtCardNumber}
                                onChange={handleChangePaymentForm}
                                error={
                                    invalidCardNumber &&
                                    productList.length > 0 &&
                                    paymentFormInvalidInputs.txtCardNumber
                                }
                                autoComplete="off"
                                helperText={
                                    invalidCardNumber &&
                                    productList.length > 0 &&
                                    paymentFormInvalidInputs.txtCardNumber
                                        ? "Número de tarjeta no válido"
                                        : ""
                                }
                            />
                        </Grid>
                        <Grid item sm={6} xs={12}>
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
                                InputLabelProps={{
                                    className: classes.paylabel,
                                }}
                                disabled={productList.length === 0}
                                value={paymentForm.txtExpirationDate}
                                onChange={handleChangePaymentForm}
                                error={
                                    invalidExpirationDate &&
                                    productList.length > 0 &&
                                    paymentFormInvalidInputs.txtExpirationDate
                                }
                                autoComplete="off"
                                helperText={
                                    invalidExpirationDate &&
                                    productList.length > 0 &&
                                    paymentFormInvalidInputs.txtExpirationDate
                                        ? "Fecha de vencimiento no válido"
                                        : ""
                                }
                            />
                        </Grid>
                        <Grid item sm={6} xs={12}>
                            <TextField
                                id="txtCVV"
                                name="txtCVV"
                                variant="outlined"
                                label="Código de verificación (CVC):"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <MdVerifiedUser size={iconSize} className={iconClass} />
                                        </InputAdornment>
                                    ),
                                    inputComponent: InputCVV,
                                }}
                                InputLabelProps={{
                                    className: classes.paylabel,
                                }}
                                disabled={productList.length === 0}
                                type="password"
                                autoComplete="new-password"
                                value={paymentForm.txtCVV}
                                onChange={handleChangePaymentForm}
                                error={
                                    paymentForm.txtCVV.length < 3 &&
                                    productList.length > 0 &&
                                    paymentFormInvalidInputs.txtCVV
                                }
                                helperText={
                                    paymentForm.txtCVV.length < 3 &&
                                    productList.length > 0 &&
                                    paymentFormInvalidInputs.txtCVV
                                        ? "Código de verificación no válido"
                                        : ""
                                }
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                id="txtName"
                                name="txtName"
                                variant="outlined"
                                label="Nombre tarjetahabiente:"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <MdPerson size={iconSize} className={iconClass} />
                                        </InputAdornment>
                                    ),
                                    endAdornment: (
                                        <Tooltip title="Como aparece en la tarjeta" placement="top-end" arrow>
                                            <InputAdornment position="end" style={{ cursor: "pointer", left: 30 }}>
                                                <MdInfo size={iconSize} className="secondary-gray" />
                                            </InputAdornment>
                                        </Tooltip>
                                    ),
                                }}
                                InputLabelProps={{
                                    className: classes.paylabel,
                                }}
                                disabled={productList.length === 0}
                                autoComplete="off"
                                value={paymentForm.txtName}
                                onChange={handleChangePaymentForm}
                                error={
                                    paymentForm.txtName === "" &&
                                    productList.length > 0 &&
                                    paymentFormInvalidInputs.txtName
                                }
                                helperText={
                                    paymentForm.txtName === "" &&
                                    productList.length > 0 &&
                                    paymentFormInvalidInputs.txtName
                                        ? "Nombre de tarjetahabiente es requerido"
                                        : ""
                                }
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                id="txtEmail"
                                name="txtEmail"
                                variant="outlined"
                                label="Correo electrónico:"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <MdEmail size={iconSize} className={iconClass} />
                                        </InputAdornment>
                                    ),
                                    endAdornment: (
                                        <Tooltip
                                            title="Las credenciales de acceso serán enviados al correo proporcionado"
                                            placement="top-end"
                                            arrow
                                        >
                                            <InputAdornment position="end" style={{ cursor: "pointer" }}>
                                                <MdInfo size={iconSize} className="secondary-gray" />
                                            </InputAdornment>
                                        </Tooltip>
                                    ),
                                }}
                                InputLabelProps={{
                                    className: classes.paylabel,
                                }}
                                disabled={productList.length === 0}
                                autoComplete="off"
                                value={paymentForm.txtEmail}
                                onChange={handleChangePaymentForm}
                                error={invalidEmail && productList.length > 0 && paymentFormInvalidInputs.txtEmail}
                                helperText={
                                    invalidEmail && productList.length > 0 && paymentFormInvalidInputs.txtEmail
                                        ? "Correo electrónico no válido"
                                        : ""
                                }
                            />
                        </Grid>
                        <Grid item sm={appInfo.bTieneMesesSinIntereses === true ? 6 : 12} xs={12}>
                            <TextField
                                id="txtPhone"
                                name="txtPhone"
                                variant="outlined"
                                label="Teléfono:"
                                placeholder="000 000 0000"
                                fullWidth
                                InputProps={{
                                    startAdornment: (
                                        <InputAdornment position="start">
                                            <MdPhone size={iconSize} className={iconClass} />
                                        </InputAdornment>
                                    ),
                                    inputComponent: InputPhone,
                                }}
                                InputLabelProps={{
                                    className: classes.paylabel,
                                }}
                                disabled={productList.length === 0}
                                autoComplete="off"
                                value={paymentForm.txtPhone}
                                onChange={handleChangePaymentForm}
                                error={invalidPhone && productList.length > 0 && paymentFormInvalidInputs.txtPhone}
                                helperText={
                                    invalidPhone && productList.length > 0 && paymentFormInvalidInputs.txtPhone
                                        ? "Teléfono no válido"
                                        : ""
                                }
                            />
                        </Grid>
                        {appInfo.bTieneMesesSinIntereses === true ? (
                            <Grid item sm={6} xs={12} className="left">
                                <FormControl
                                    variant="outlined"
                                    fullWidth
                                    disabled={productList.length === 0 || monthlyPayments.length === 0}
                                >
                                    <InputLabel id="lblModality" className={classes.paylabel}>
                                        Modalidad de pago:
                                    </InputLabel>
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
                                        error={
                                            paymentForm.txtModality === "" &&
                                            productList.length > 0 &&
                                            paymentFormInvalidInputs.txtModality
                                        }
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
                        ) : null}
                        <Grid item xs={12}>
                            <FormControlLabel
                                control={
                                    <Checkbox
                                        checked={acceptedPolicies}
                                        onChange={handleChangeAcceptPolicies}
                                        name="chkPolicies"
                                        disabled={productList.length === 0}
                                    />
                                }
                                label={
                                    <Typography variant="body2">
                                        Acepto los{" "}
                                        <a
                                            href={appInfo.sTerminosYCondiciones}
                                            target="_blank"
                                            className="pay-form-policies-link"
                                            rel="noopener noreferrer"
                                        >
                                            términos y condiciones
                                        </a>{" "}
                                        y el{" "}
                                        <a
                                            href={appInfo.sAvisoDePrivacidad}
                                            target="_blank"
                                            className="pay-form-policies-link"
                                            rel="noopener noreferrer"
                                        >
                                            aviso de privacidad
                                        </a>
                                        .
                                    </Typography>
                                }
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <Button
                                variant="contained"
                                color="secondary"
                                size="large"
                                fullWidth
                                disabled={productList.length === 0 || !acceptedPolicies}
                                onClick={handleClickSubmitPaymentForm}
                            >
                                PAGAR
                            </Button>
                        </Grid>
                        <Grid item xs={12}>
                            <Typography className="pay-error-description">{formErrorMessage}</Typography>
                        </Grid>
                    </Grid>
                </div>
            </div>
        </Fragment>
    );
};

PaymentForm.propTypes = {
    appInfo: PropTypes.shape({
        bTieneMesesSinIntereses: PropTypes.bool,
        sAvisoDePrivacidad: PropTypes.string,
        sConektaPublicKey: PropTypes.string,
        sTerminosYCondiciones: PropTypes.string,
    }),
    entCoupon: PropTypes.shape({
        fiIdCupon: PropTypes.number,
    }),
    formErrorMessage: PropTypes.string,
    funcLoader: PropTypes.func,
    monthlyPayments: PropTypes.array,
    productList: PropTypes.array,
    setEntOrder: PropTypes.func,
    setErrorOrder: PropTypes.func,
    setFormErrorMessage: PropTypes.func,
    totalPayment: PropTypes.number,
};

export default PaymentForm;
