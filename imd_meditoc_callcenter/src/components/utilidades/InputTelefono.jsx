import PropTypes from "prop-types";
import React from "react";
import MaskedInput from "react-text-mask";

/*****************************************************
 * Descripción: Mascara de validación para teléfono
 * Autor: Cristopher Noh
 * Fecha: 28/08/2020
 * Invocado desde: --
 *****************************************************/
const InputTelefono = (props) => {
    const { inputRef, ...other } = props;

    return (
        <MaskedInput
            {...other}
            ref={(ref) => {
                inputRef(ref ? ref.inputElement : null);
            }}
            mask={[/[1-9]/, /\d/, /\d/, " ", /\d/, /\d/, /\d/, " ", /\d/, /\d/, /\d/, /\d/]}
            guide={false}
        />
    );
};

InputTelefono.propTypes = {
    inputRef: PropTypes.func,
};

export default InputTelefono;
