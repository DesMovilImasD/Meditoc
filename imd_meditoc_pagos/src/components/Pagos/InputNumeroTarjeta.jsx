import PropTypes from "prop-types";
import React from "react";
import MaskedInput from "react-text-mask";

/*****************************************************
 * Descripción: Mascara de validación para número de tarjeta
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const InputNumeroTarjeta = (props) => {
    const { inputRef, ...other } = props;

    return (
        <MaskedInput
            {...other}
            ref={(ref) => {
                inputRef(ref ? ref.inputElement : null);
            }}
            mask={[
                /[1-9]/,
                /\d/,
                /\d/,
                /\d/,
                " ",
                /\d/,
                /\d/,
                /\d/,
                /\d/,
                " ",
                /\d/,
                /\d/,
                /\d/,
                /\d/,
                " ",
                /\d/,
                /\d/,
                /\d/,
                /\d/,
            ]}
            guide={false}
        />
    );
};

InputNumeroTarjeta.propTypes = {
    inputRef: PropTypes.func,
};

export default InputNumeroTarjeta;
