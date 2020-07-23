import React from "react";
import MaskedInput from "react-text-mask";

const InputCodigoVerificacion = (props) => {
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

export default InputCodigoVerificacion;
