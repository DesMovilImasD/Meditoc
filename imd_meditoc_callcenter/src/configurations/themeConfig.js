/*************************************************************
 * Descripcion: Contiene los colores del tema principal del portal
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 *************************************************************/

import { createMuiTheme } from "@material-ui/core/styles";

const theme = createMuiTheme({
    palette: {
        primary: {
            main: "#12B6CB",
            contrastText: "#FFFFFF",
        },
        secondary: {
            main: "#115C8A",
            contrastText: "#FFFFFF",
        },
    },
    overrides: {
        MuiTooltip: {
            tooltip: {
                fontSize: 15,
                fontWeight: "normal",
            },
        },
    },
});

export default theme;
