import MaskedInput from "react-text-mask";
import PropTypes from "prop-types";
import React from "react";

/*****************************************************
 * Descripción: Mascara de validación para teléfono
 * Autor: Cristopher Noh
 * Fecha: 28/08/2020
 * Invocado desde: --
 *****************************************************/
const MeditocInputPhone = (props) => {
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

MeditocInputPhone.propTypes = {
    inputRef: PropTypes.func,
};

export default MeditocInputPhone;
