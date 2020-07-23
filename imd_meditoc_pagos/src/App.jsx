import React, { useState } from "react";
import { BrowserRouter, Switch, Route, Redirect } from "react-router-dom";
import PrincipalPrecios from "./components/Precios/Principal";
import PrincipalPagos from "./components/Pagos/Principal";
import theme from "./configuration/themeConfig";
import { ThemeProvider } from "@material-ui/core";
import Loader from "./components/Loader";

function App() {
    const directorio = {
        precios: "/precios",
        pagos: "/pagos",
    };

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

    return (
        <ThemeProvider theme={theme}>
            <Loader entLoader={entLoader} />
            <BrowserRouter basename="/servicios">
                <Switch>
                    <Route
                        exact
                        path="/"
                        render={() => <Redirect to={directorio.precios} />}
                    />
                    <Route exact path={directorio.precios}>
                        <PrincipalPrecios />
                    </Route>
                    <Route exact path={directorio.pagos}>
                        <PrincipalPagos funcLoader={funcLoader} />
                    </Route>
                </Switch>
            </BrowserRouter>
        </ThemeProvider>
    );
}

export default App;
