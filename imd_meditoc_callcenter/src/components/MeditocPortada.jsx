import React from "react";
import { Grid, Fade } from "@material-ui/core";
import { imgLogoLogin } from "../configurations/imgConfig";

const MeditocPortada = () => {
    return (
        <div className="login-container">
            <Grid container spacing={0}>
                <Grid item xs={12} className="align-self-center">
                    <div className="login-logo-container">
                        <Fade in={true} timeout={5000}>
                            <img src={imgLogoLogin} alt="LOGOLOGIN" className="login-logo-img" />
                        </Fade>
                    </div>
                </Grid>
            </Grid>{" "}
        </div>
    );
};

export default MeditocPortada;
