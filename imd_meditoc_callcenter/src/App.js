import { Button, ThemeProvider, Typography } from "@material-ui/core";
import React, { createRef, useState } from "react";

import { BrowserRouter } from "react-router-dom";
import DateFnsUtils from "@date-io/date-fns";
import MeditocAlert from "./components/utilidades/MeditocAlert";
import MeditocContentMain from "./components/meditoc/MeditocContentMain";
import MeditocLoader from "./components/utilidades/MeditocLoader";
import MeditocLogin from "./components/login/MeditocLogin";
import { MuiPickersUtilsProvider } from "@material-ui/pickers";
import { SnackbarProvider } from "notistack";
import es from "date-fns/locale/es";
import theme from "./configurations/themeConfig";
import { urlBase } from "./configurations/urlConfig";

/*************************************************************
 * Descripcion: App del proyecto
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde:  ---------Elemento Raíz----------
 *************************************************************/
function App() {
    const [usuarioSesion, setUsuarioSesion] = useState(null);

    const [usuarioPermisos, setUsuarioPermisos] = useState(null);
    const [usuarioActivo, setUsuarioActivo] = useState(false);
    const [entCatalogos, setEntCatalogos] = useState(null);

    //Guardar valores de estado del loader
    const [entLoader, setEntLoader] = useState({
        open: false,
        message: "Cargando...",
    });

    //Mostrar/Ocultar loader y configurar mensaje
    const funcLoader = (pOpen = false, pMessage = "Cargando...") => {
        setEntLoader({
            open: pOpen,
            message: pMessage,
        });
    };

    //Guardar valores de estado del alert
    const [entAlert, setEntAlert] = useState({
        message: "",
        variant: "error",
        state: false,
    });

    //Mostrar una alerta
    const funcAlert = (pMessage = "", pVariant = "error") => {
        setEntAlert({
            message: pMessage,
            variant: pVariant,
            state: !entAlert.state,
        });
    };

    const notistackRef = createRef();
    const onClickDismiss = (key) => () => {
        notistackRef.current.closeSnackbar(key);
    };

    return (
        <ThemeProvider theme={theme}>
            <SnackbarProvider
                maxSnack={3}
                anchorOrigin={{
                    vertical: "top",
                    horizontal: "right",
                }}
                ref={notistackRef}
                action={(key) => (
                    <Button onClick={onClickDismiss(key)} color="inherit">
                        <Typography variant="caption">CERRAR</Typography>
                    </Button>
                )}
                disableWindowBlurListener
            >
                <MuiPickersUtilsProvider utils={DateFnsUtils} locale={es}>
                    <BrowserRouter basename={urlBase}>
                        <MeditocLoader entLoader={entLoader} />
                        <MeditocAlert entAlert={entAlert} />
                        {usuarioActivo === true &&
                        usuarioSesion !== null &&
                        usuarioPermisos !== null &&
                        entCatalogos !== null ? (
                            <MeditocContentMain
                                usuarioSesion={usuarioSesion}
                                usuarioPermisos={usuarioPermisos}
                                entCatalogos={entCatalogos}
                                setUsuarioSesion={setUsuarioSesion}
                                setUsuarioActivo={setUsuarioActivo}
                                setUsuarioPermisos={setUsuarioPermisos}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        ) : (
                            <MeditocLogin
                                setUsuarioSesion={setUsuarioSesion}
                                setUsuarioActivo={setUsuarioActivo}
                                setUsuarioPermisos={setUsuarioPermisos}
                                setEntCatalogos={setEntCatalogos}
                                funcLoader={funcLoader}
                                funcAlert={funcAlert}
                            />
                        )}
                    </BrowserRouter>
                </MuiPickersUtilsProvider>
            </SnackbarProvider>
        </ThemeProvider>
    );
}

export default App;
