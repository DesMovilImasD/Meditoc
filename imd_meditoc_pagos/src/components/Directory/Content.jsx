import React from "react";
import {
    Grid,
    TextField,
    InputAdornment,
    IconButton,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    makeStyles,
    Hidden,
} from "@material-ui/core";
import { BsSearch } from "react-icons/bs";
import Pagination from "@material-ui/lab/Pagination";
import { logoMeditocDoctorSample } from "../../configuration/imgConfig";

const useStyles = makeStyles({
    ul: {
        display: "inline-flex",
        flexWrap: "initial",
    },
});

const Content = () => {
    const classes = useStyles();

    return (
        <div className="directory-content">
            <Grid container spacing={3}>
                <Grid item sm={6} xs={12}>
                    <TextField
                        variant="outlined"
                        fullWidth
                        label="Buscar..."
                        InputProps={{
                            endAdornment: (
                                <InputAdornment edge="start">
                                    <IconButton>
                                        <BsSearch />
                                    </IconButton>
                                </InputAdornment>
                            ),
                        }}
                    />
                </Grid>
                <Grid item sm={6} xs={12}>
                    <FormControl fullWidth variant="outlined">
                        <InputLabel id="slcEspecialidad">Especialidad:</InputLabel>
                        <Select labelId="slcEspecialidad" label="Especialidad:">
                            <MenuItem value="">
                                <em>Todas las especialidades</em>
                            </MenuItem>
                            <MenuItem value="10">Cardiología</MenuItem>
                            <MenuItem value="20">Cirugía General</MenuItem>
                            <MenuItem value="30">Dermatología</MenuItem>
                            <MenuItem value="40">Geriatría</MenuItem>
                            <MenuItem value="50">Hematología</MenuItem>
                            <MenuItem value="60">Nefrología</MenuItem>
                        </Select>
                    </FormControl>
                </Grid>
                <Grid item xs={12}>
                    <Grid container spacing={2}>
                        <Hidden only={["xs"]}>
                            <Grid item sm={4} xs={12}>
                                <div className="directory-doctor-img-container">
                                    <img
                                        src={logoMeditocDoctorSample}
                                        alt="LOGOMEDITOCDOCTORSAMPLE"
                                        className="directory-doctor-img"
                                    />
                                </div>
                            </Grid>
                        </Hidden>
                        <Grid item sm={8} xs={12}>
                            <Grid container spacing={2}>
                                <Grid item xs={12}>
                                    <div className="directory-doctor-name-container">
                                        <span className="directory-doctor-name">Dra. Andrea Ortiz Pavón</span>
                                    </div>
                                </Grid>
                                <Hidden only={["xl", "lg", "md", "sm"]}>
                                    <Grid xs={12}>
                                        <img
                                            src={logoMeditocDoctorSample}
                                            alt="LOGOMEDITOCDOCTORSAMPLE"
                                            className="directory-doctor-img"
                                        />
                                    </Grid>
                                </Hidden>
                                <Grid item xs={6}>
                                    <span className="directory-doctor-label">Especialidad</span>
                                    <br />
                                    <span className="directory-doctor-value">CARDIOLOGÍA</span>
                                </Grid>
                                <Grid item xs={6}>
                                    <span className="directory-doctor-label">Céd. Prof</span>
                                    <br />
                                    <span className="directory-doctor-value">09238312</span>
                                </Grid>
                                <Grid item xs={6}>
                                    <span className="directory-doctor-label">Teléfono</span>
                                    <br />
                                    <span className="directory-doctor-value">9991066754</span>
                                </Grid>
                                <Grid item xs={6}>
                                    <span className="directory-doctor-label">Whatsapp</span>
                                    <br />
                                    <span className="directory-doctor-value">9991066754</span>
                                </Grid>
                                <Grid item xs={12}>
                                    <span className="directory-doctor-label">Correo</span>
                                    <br />
                                    <span className="directory-doctor-value">andrea@correo.com.mx</span>
                                </Grid>
                                <Grid item xs={12}>
                                    <span className="directory-doctor-label">Dirección</span>
                                    <br />
                                    <span className="directory-doctor-value">
                                        Calle 32 No. 123 Temozón Norte Fracc. Santa Gertudris Copó
                                    </span>
                                </Grid>
                                <Grid item xs={12}>
                                    <span className="directory-doctor-label">Consultorio</span>
                                    <br />
                                    <span className="directory-doctor-value">
                                        Hospital Faro del Mayab - Consultorio 241
                                    </span>
                                </Grid>
                                <Grid item xs={12}>
                                    <span className="directory-doctor-label">URL</span>
                                    <br />
                                    <span className="directory-doctor-value">www.meditoc.com</span>
                                </Grid>
                            </Grid>
                        </Grid>
                    </Grid>
                </Grid>
                <Grid item xs={12}>
                    <div className="directory-pagination-container">
                        <Pagination
                            classes={{ ul: classes.ul }}
                            count={10}
                            size="large"
                            showFirstButton
                            showLastButton
                            color="primary"
                        />
                    </div>
                </Grid>
            </Grid>
        </div>
    );
};

export default Content;
