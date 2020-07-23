import React from "react";
import MaskedInput from "react-text-mask";

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

export default InputFechaVencimiento;
