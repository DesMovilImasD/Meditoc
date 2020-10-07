import { Button, Grid, Hidden, Link } from "@material-ui/core";

import { MdAccountBox } from "react-icons/md";
import PropTypes from "prop-types";
import React from "react";
import { rxUrl } from "../../configuration/regexConfig";

/*****************************************************
 * Descripción: Ficha informativa del médico colaborador
 * Autor: Cristopher Noh
 * Fecha: 07/09/2020
 * Modificaciones:
 *****************************************************/
const MedicInfo = (props) => {
    const { entColaborador } = props;

    return (
        <Grid item xs={12}>
            <Grid container spacing={2} style={{ marginBottom: 50 }}>
                <Hidden only={["xs"]}>
                    <Grid item sm={4} xs={12}>
                        <div className="directory-doctor-img-container">
                            {entColaborador.sFoto !== null && entColaborador.sFoto !== "" ? (
                                <img
                                    src={`data:image/png;base64,${entColaborador.sFoto}`}
                                    alt="MEDITOCDOCTOR"
                                    className="directory-doctor-img"
                                />
                            ) : (
                                <MdAccountBox style={{ fontSize: 220, color: "#ccc" }} />
                            )}
                        </div>
                    </Grid>
                </Hidden>
                <Grid item sm={8} xs={12}>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <div className="directory-doctor-name-container">
                                <span className="directory-doctor-name">{entColaborador.sNombre}</span>
                            </div>
                        </Grid>
                        <Hidden only={["xl", "lg", "md", "sm"]}>
                            <Grid item xs={12}>
                                {entColaborador.sFoto !== null && entColaborador.sFoto !== "" ? (
                                    <img
                                        src={`data:image/png;base64,${entColaborador.sFoto}`}
                                        alt="MEDITOCDOCTOR"
                                        className="directory-doctor-img"
                                    />
                                ) : (
                                    <MdAccountBox style={{ fontSize: 220, color: "#ccc" }} />
                                )}
                            </Grid>
                        </Hidden>
                        <Grid item xs={6}>
                            <span className="directory-doctor-label">Especialidad:</span>
                            <br />
                            <span className="directory-doctor-value">{entColaborador.sEspecialidad}</span>
                        </Grid>
                        <Grid item xs={6}>
                            <span className="directory-doctor-label">Céd. Prof:</span>
                            <br />
                            <span className="directory-doctor-value">{entColaborador.sCedulaProfecional}</span>
                        </Grid>
                        <Grid item xs={6}>
                            <span className="directory-doctor-label">Teléfono:</span>
                            <br />
                            <span className="directory-doctor-value">{entColaborador.sTelefono}</span>
                        </Grid>
                        <Grid item xs={6}>
                            <span className="directory-doctor-label">Whatsapp:</span>
                            <br />
                            <span className="directory-doctor-value">{entColaborador.sWhatsApp}</span>
                        </Grid>
                        <Grid item sm={6} xs={12}>
                            <span className="directory-doctor-label">Correo:</span>
                            <br />
                            <span className="directory-doctor-value">{entColaborador.sCorreo}</span>
                        </Grid>
                        <Grid item sm={6} xs={12}>
                            <span className="directory-doctor-label">Consultorio:</span>
                            <br />
                            <span className="directory-doctor-value">{entColaborador.sNombreConsultorio}</span>
                        </Grid>
                        <Grid item sm={entColaborador.sMaps !== null ? 6 : 12} xs={12}>
                            <span className="directory-doctor-label">Dirección:</span>
                            <br />
                            <span className="directory-doctor-value">{entColaborador.sDireccionConsultorio}</span>
                        </Grid>
                        {entColaborador.sMaps !== null && (
                            <Grid item sm={6} xs={12}>
                                <Button
                                    variant="contained"
                                    color="primary"
                                    target="_blank"
                                    href={entColaborador.sMaps}
                                    rel="noopener noreferrer"
                                >
                                    Ver en google maps
                                </Button>
                            </Grid>
                        )}
                        <Grid item xs={12}>
                            <span className="directory-doctor-label">URL:</span>
                            <br />
                            <span className="directory-doctor-value">
                                {rxUrl.test(entColaborador.sURL) ? (
                                    <Link
                                        href={
                                            entColaborador.sURL.startsWith("http://")
                                                ? entColaborador.sURL
                                                : entColaborador.sURL.startsWith("https://")
                                                ? entColaborador.sURL
                                                : "//" + entColaborador.sURL
                                        }
                                        target="_target"
                                        rel="noreferrer"
                                    >
                                        {entColaborador.sURL}
                                    </Link>
                                ) : (
                                    entColaborador.sURL
                                )}
                            </span>
                        </Grid>
                    </Grid>
                </Grid>
            </Grid>
        </Grid>
    );
};

MedicInfo.propTypes = {
    entColaborador: PropTypes.shape({
        sCedulaProfecional: PropTypes.string,
        sCorreo: PropTypes.string,
        sDireccionConsultorio: PropTypes.string,
        sEspecialidad: PropTypes.string,
        sFoto: PropTypes.string,
        sMaps: PropTypes.string,
        sNombre: PropTypes.string,
        sNombreConsultorio: PropTypes.string,
        sTelefono: PropTypes.string,
        sURL: PropTypes.string,
        sWhatsApp: PropTypes.string,
    }),
};

export default MedicInfo;
