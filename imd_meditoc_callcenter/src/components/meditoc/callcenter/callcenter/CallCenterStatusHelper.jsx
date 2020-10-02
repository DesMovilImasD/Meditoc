import React from "react";
import theme from "../../../../configurations/themeConfig";
import { Button, Popover, Typography } from "@material-ui/core";
import InfoIcon from "@material-ui/icons/Info";

const CallCenterStatusHelper = (props) => {
    const { popoverOcupadoInicio, handleClosePopoverOcupado, handleClickPopoverDisponible } = props;
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
            open={popoverOcupadoInicio}
            onClose={handleClosePopoverOcupado}
        >
            <div style={{ padding: 30, textAlign: "center" }}>
                <div>
                    <InfoIcon style={{ fontSize: 80, color: theme.palette.primary.main }} />
                </div>
                <div>
                    <Typography variant="body2" paragraph>
                        TU ESTATUS ACTUAL ES OCUPADO.
                    </Typography>
                    <Typography variant="body2" paragraph>
                        Cambia tu estatus a DISPONIBLE para comenzar a
                        <br />
                        recibir videollamadas y chats de tus pacientes
                    </Typography>
                    <Button color="default" style={{ color: "#888" }} onClick={handleClosePopoverOcupado}>
                        <Typography variant="body2">CONTINUAR OCUPADO</Typography>
                    </Button>
                    <Button color="primary" onClick={handleClickPopoverDisponible}>
                        <Typography variant="body2">CAMBIAR A DISPONIBLE</Typography>
                    </Button>
                </div>
            </div>
        </Popover>
    );
};

export default CallCenterStatusHelper;
