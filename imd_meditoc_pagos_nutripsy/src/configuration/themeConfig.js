//CONFIGURACIONES DEL TEMA PARA MEDITOC

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
                fontSize: 13,
                bottom: 6,
            },
        },
    },
});

export default theme;
