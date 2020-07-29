import PropTypes from "prop-types";
import React from "react";
import MaskedInput from "react-text-mask";

/*****************************************************
 * Descripción: Mascara de validación para fecha de vencimiento
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const InputExpirationDate = (props) => {
    const { inputRef, ...other } = props;

    return (
        <MaskedInput
            {...other}
            ref={(ref) => {
                inputRef(ref ? ref.inputElement : null);
            }}
            mask={[/[0-1]/, /\d/, "/", /\d/, /\d/]}
            guide={false}
        />
    );
};

InputExpirationDate.propTypes = {
    inputRef: PropTypes.func,
};

export default InputExpirationDate;
