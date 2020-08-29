import React from "react";
import { Grid, TextField, Button, Fade } from "@material-ui/core";
import { imgLogoLogin, imgLogoMeditocCasa } from "../../configurations/imgConfig";
import { makeStyles, withStyles } from "@material-ui/core/styles";
import theme from "../../configurations/themeConfig";
import { useState } from "react";
import { useEffect } from "react";

const useStyles = makeStyles(() => ({
    button: {
        color: theme.palette.primary.main,
        backgroundColor: "#fff",
    },
}));

const TextFieldWhite = withStyles({
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

const Login = () => {
    const classes = useStyles();

    const [imgLogoFade, setImgLogoFade] = useState(false);

    useEffect(() => {
        setImgLogoFade(true);

        // eslint-disable-next-line
    }, []);

    return (
        <div className="login-container">
            <Grid container spacing={0} className="login-page">
                <Grid item md={8} xs={12} className="align-self-center">
                    <div className="login-logo-container">
                        <Fade in={imgLogoFade} timeout={1500}>
                            <img src={imgLogoLogin} alt="LOGOLOGIN" className="login-logo-img" />
                        </Fade>
                    </div>
                </Grid>
                <Grid item md={4} xs={12} className="login-form-back">
                    <form className="login-form-container">
                        <Grid container spacing={4}>
                            <Grid item xs={12}>
                                <img src={imgLogoMeditocCasa} alt="LOGOMEDITOCCASA" className="login-form-img" />
                            </Grid>
                            <Grid item xs={12}>
                                <span className="ops-nor bold size-25">ACCESO AL SISTEMA</span>
                            </Grid>
                            <Grid item xs={12}>
                                <TextFieldWhite
                                    variant="outlined"
                                    fullWidth
                                    autoComplete="new-password"
                                    label="Usuario:"
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextFieldWhite
                                    variant="outlined"
                                    fullWidth
                                    autoComplete="new-password"
                                    label="Contraseña:"
                                    type="password"
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <Button variant="contained" type="submit" className={classes.button} fullWidth>
                                    ENTRAR
                                </Button>
                            </Grid>
                            <Grid item xs={12}>
                                {" "}
                            </Grid>
                        </Grid>
                    </form>
                </Grid>
            </Grid>
        </div>
    );
};

export default Login;
