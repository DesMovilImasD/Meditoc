import { Button, Grid, Hidden } from "@material-ui/core";

import React from "react";
import { logoMeditocDoctores } from "../../configuration/imgConfig";
import { urlDirectory } from "../../configuration/urlConfig";
import { useHistory } from "react-router-dom";

const Directory = () => {
    const history = useHistory();

    const handleClickDirectory = () => {
        history.push(urlDirectory);
    };

    return (
        <div className="price-directory-container">
            <Grid container spacing={2}>
                <Hidden only={["xl", "lg", "md", "sm"]}>
                    <Grid item md={4} sm={6} xs={12} className="center">
                        <img src={logoMeditocDoctores} alt="LOGOMEDITOCDOCTORFONDO" className="price-directory-logo" />
                    </Grid>
                </Hidden>
                <Grid item md={8} sm={6} xs={12} className="price-directory-description-container">
                    <p>
                        <span className="price-directory-description">
                            Consulta nuestro directorio de médicos especializados
                        </span>
                    </p>
                    <p>
                        <Button variant="contained" color="primary" onClick={handleClickDirectory}>
                            Directorio
                        </Button>
                    </p>
                </Grid>
                <Hidden only={["xs"]}>
                    <Grid item md={4} sm={6} xs={12} className="center">
                        <img src={logoMeditocDoctores} alt="LOGOMEDITOCDOCTORFONDO" className="price-directory-logo" />
                    </Grid>
                </Hidden>
            </Grid>
        </div>
    );
};

export default Directory;
