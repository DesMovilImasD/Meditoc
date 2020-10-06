import MaskedInput from "react-text-mask";
import PropTypes from "prop-types";
import React from "react";

/*****************************************************
 * Descripción: Mascara de validación para código de verificación
 * Autor: Cristopher Noh
 * Fecha: 23/07/2020
 * Modificaciones:
 *****************************************************/
const InputCVV = (props) => {
    const { inputRef, ...other } = props;

    return (
        <MaskedInput
            {...other}
            ref={(ref) => {
                inputRef(ref ? ref.inputElement : null);
            }}
            mask={[/\d/, /\d/, /\d/, /\d/]}
            guide={false}
        />
    );
};

InputCVV.propTypes = {
    inputRef: PropTypes.func,
};

export default InputCVV;
