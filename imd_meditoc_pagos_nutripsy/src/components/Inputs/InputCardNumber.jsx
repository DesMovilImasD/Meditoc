import MaskedInput from "react-text-mask";
import PropTypes from "prop-types";
import React from "react";

/*****************************************************
 * Descripción: Mascara de validación para número de tarjeta
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const InputCardNumber = (props) => {
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

InputCardNumber.propTypes = {
    inputRef: PropTypes.func,
};

export default InputCardNumber;
