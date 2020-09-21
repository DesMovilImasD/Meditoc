import React from "react";
import { Grid, TextField, Button, Fade } from "@material-ui/core";
import { imgLogoLogin, imgLogoMeditocCasa } from "../../configurations/imgConfig";
import { makeStyles, withStyles } from "@material-ui/core/styles";
import theme from "../../configurations/themeConfig";
import { useState } from "react";
import { useEffect } from "react";
import CGUController from "../../controllers/CGUController";

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

const Login = (props) => {
    const { setUsuarioSesion, setUsuarioActivo, funcLoader, funcAlert } = props;

    const classes = useStyles();

    const [imgLogoFade, setImgLogoFade] = useState(false);

    const [formLogin, setFormLogin] = useState({
        txtUsuarioMeditoc: "",
        txtPasswordMeditoc: "",
    });

    const handleChangeFormLogin = (e) => {
        setFormLogin({
            ...formLogin,
            [e.target.name]: e.target.value,
        });
    };

    const funcApiLogin = async (MeditocTkn, MeditocKey) => {
        funcLoader(true, "Validando datos de sesión..");

        const cguController = new CGUController();

        const response = await cguController.funcGetLogin(MeditocTkn, MeditocKey);
        funcLoader();
        if (response.Code === 0) {
            sessionStorage.setItem("MeditocTkn", MeditocTkn);
            sessionStorage.setItem("MeditocKey", MeditocKey);

            setUsuarioSesion(response.Result);
            setUsuarioActivo(true);
        } else {
            sessionStorage.removeItem("MeditocTkn");
            sessionStorage.removeItem("MeditocKey");
            funcAlert(response.Message);
        }
    };

    const handleSubmitFormLogin = (e) => {
        e.preventDefault();

        funcApiLogin(formLogin.txtUsuarioMeditoc, formLogin.txtPasswordMeditoc);
    };

    const funcValidarSesion = () => {
        const MeditocTkn = sessionStorage.getItem("MeditocTkn");
        const MeditocKey = sessionStorage.getItem("MeditocKey");

        if (MeditocTkn != null && MeditocKey != null) {
            funcApiLogin(MeditocTkn, MeditocKey);
        }
    };

    useEffect(() => {
        setImgLogoFade(true);
        funcValidarSesion();
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
                    <form className="login-form-container" onSubmit={handleSubmitFormLogin}>
                        <Grid container spacing={4}>
                            <Grid item xs={12}>
                                <img src={imgLogoMeditocCasa} alt="LOGOMEDITOCCASA" className="login-form-img" />
                            </Grid>
                            <Grid item xs={12}>
                                <span className="ops-nor bold size-25">ACCESO AL SISTEMA</span>
                            </Grid>
                            <Grid item xs={12}>
                                <TextFieldWhite
                                    name="txtUsuarioMeditoc"
                                    variant="outlined"
                                    fullWidth
                                    autoComplete="off"
                                    label="Usuario:"
                                    value={formLogin.txtUsuarioMeditoc}
                                    onChange={handleChangeFormLogin}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextFieldWhite
                                    name="txtPasswordMeditoc"
                                    variant="outlined"
                                    fullWidth
                                    autoComplete="new-password"
                                    label="Contraseña:"
                                    type="password"
                                    value={formLogin.txtPasswordMeditoc}
                                    onChange={handleChangeFormLogin}
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
