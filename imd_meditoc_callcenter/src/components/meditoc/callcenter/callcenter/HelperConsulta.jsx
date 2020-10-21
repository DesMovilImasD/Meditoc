import { Button, Popover, Typography } from "@material-ui/core";

import InfoIcon from "@material-ui/icons/Info";
import PropTypes from "prop-types";
import React from "react";
import theme from "../../../../configurations/themeConfig";

const HelperConsulta = (props) => {
    const {
        popoverConsulta,
        handleClosePopoverConsulta,
        handleClickPopoverConsultaReiniciar,
        handleClickIniciarConsulta,
    } = props;

    return (
        <Popover
            anchorReference="anchorPosition"
            anchorPosition={{ top: 140, left: 20 }}
            anchorOrigin={{
                vertical: "top",
                horizontal: "left",
            }}
            transformOrigin={{
                vertical: "top",
                horizontal: "left",
            }}
            open={popoverConsulta}
            onClose={handleClosePopoverConsulta}
        >
            <div style={{ padding: 30, textAlign: "center" }}>
                <div>
                    <InfoIcon style={{ fontSize: 80, color: theme.palette.primary.main }} />
                </div>
                <div>
                    <Typography variant="body2" paragraph>
                        SE HA CAMBIADO TU ESTATUS A OCUPADO.
                    </Typography>
                    <Typography variant="body2" paragraph>
                        Un paciente ha entrado a tu sala pero <br />
                        aún no has iniciado la consulta.
                    </Typography>
                    <Button color="default" style={{ color: "#888" }} onClick={handleClickPopoverConsultaReiniciar}>
                        <Typography variant="body2">REINICIAR CHAT</Typography>
                    </Button>
                    <Button color="primary" onClick={handleClickIniciarConsulta}>
                        <Typography variant="body2">INICIAR CONSULTA</Typography>
                    </Button>
                </div>
            </div>
        </Popover>
    );
};

HelperConsulta.propTypes = {
    handleClickIniciarConsulta: PropTypes.func,
    handleClickPopoverConsultaReiniciar: PropTypes.func,
    handleClosePopoverConsulta: PropTypes.func,
    popoverConsulta: PropTypes.bool,
};

export default HelperConsulta;
