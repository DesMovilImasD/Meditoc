import { Fade, Grid } from "@material-ui/core";

import React from "react";
import { imgLogoLogin } from "../../configurations/imgConfig";

const MeditocPortada = () => {
    return (
        <div>
            <Grid container spacing={0}>
                <Grid item xs={12} className="align-self-center">
                    <div className="login-logo-container">
                        <Fade in={true} timeout={4000}>
                            <img src={imgLogoLogin} alt="LOGOLOGIN" className="login-logo-img" />
                        </Fade>
                    </div>
                </Grid>
            </Grid>{" "}
        </div>
    );
};

export default MeditocPortada;
