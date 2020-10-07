import { TextField, withStyles } from "@material-ui/core";

//Estilos para el input de cantidad
const InputWhite = withStyles({
    root: {
        "& label.Mui-focused": {
            borderColor: "white",
            color: "white",
        },
        "& .MuiInput-underline:before": {
            borderColor: "white",
        },
        "& .MuiInput-underline:after": {
            borderColor: "white",
        },
        "& .MuiInput-underline:hover:not(.Mui-disabled):before": {
            borderColor: "white",
        },
        "& .MuiInputBase-input": {
            color: "white",
            borderColor: "white",
        },
    },
})(TextField);

export default InputWhite;
