import PropTypes from "prop-types";
import React from "react";
import MaskedInput from "react-text-mask";

/*****************************************************
 * Descripción: Mascara de validación para fecha de vencimiento
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const InputFechaVencimiento = (props) => {
    const { inputRef, ...other } = props;

    const year = new Date().getFullYear();

    const yearNumber1 = year.toString().substring(2, 3);
    const yearNumber2 = (year + 10).toString().substring(2, 3);

    return (
        <MaskedInput
            {...other}
            ref={(ref) => {
                inputRef(ref ? ref.inputElement : null);
            }}
            mask={[
                /[0-1]/,
                /\d/,
                "/",
                new RegExp(`[${yearNumber1}-${yearNumber2}]`),
                /\d/,
            ]}
            guide={false}
        />
    );
};

InputFechaVencimiento.propTypes = {
    inputRef: PropTypes.func,
};

export default InputFechaVencimiento;
