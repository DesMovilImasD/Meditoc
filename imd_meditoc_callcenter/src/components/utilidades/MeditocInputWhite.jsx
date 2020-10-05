const { TextField, withStyles } = require("@material-ui/core");

const MeditocInputWhite = withStyles({
    root: {
        "& label.Mui-focused": {
            color: "#fff",
        },
        "& .MuiInput-underline:after": {
            borderBottomColor: "#fff",
        },
        "& .MuiInputBase-input": {
            color: "#fff",
            borderColor: "#fff",
        },
        "& .MuiFormLabel-root": {
            color: "#fff",
        },
        "& .MuiOutlinedInput-root": {
            "& fieldset": {
                borderColor: "#fff",
            },
            "&:hover fieldset": {
                borderColor: "#fff",
            },
            "&.Mui-focused fieldset": {
                borderColor: "#fff",
            },
        },
    },
})(TextField);

export default MeditocInputWhite;
