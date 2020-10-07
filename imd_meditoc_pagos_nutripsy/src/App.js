import { BrowserRouter, Redirect, Route, Switch } from "react-router-dom";
import React, { useEffect, useState } from "react";
import { apiGetPolicies, apiGetProducts } from "./configuration/apiConfig";
import { urlBase, urlPayments, urlProducts } from "./configuration/urlConfig";

import Loader from "./components/Loader";
import Pays from "./components/Payments/Main";
import Prices from "./components/Prices/Main";
import { ThemeProvider } from "@material-ui/core";
import defaultAppInfo from "./configuration/appinfoConfig";
import { serverMain } from "./configuration/serverConfig";
import theme from "./configuration/themeConfig";

/*****************************************************
 * Descripción: App principal web
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
function App() {
    //Guardar valores de estado del loader
    const [entLoader, setEntLoader] = useState({
        open: false,
        message: "Cargando...",
    });

    //Guardar lista de productos de orientaciones únicas
    const [lstNutritionalProducts, setLstNutritionalProducts] = useState([]);

    //Guardar lista de productos de membresías
    const [lstPsychologyProducts, setLstPsychologyProducts] = useState([]);

    //Guardar los links de Aviso de Privacidad y Términos y condiciones
    const [appInfo, setAppInfo] = useState(defaultAppInfo);

    //Mostrar/Ocultar loader y configurar mensaje
    const funcLoader = (pOpen = false, pMessage = "Cargando...") => {
        setEntLoader({
            open: pOpen,
            message: pMessage,
        });
    };

    //Consumir servicio para obtener los links de las políticas Meditoc
    const funcGetAppInfo = async () => {
        try {
            const apiResponse = await fetch(`${serverMain}${apiGetPolicies}`);

            const response = await apiResponse.json();

            if (response.Code === 0) {
                setAppInfo(response.Result);
            }
        } catch (error) {}
    };

    //Consumir servicio para obtener las orientaciones
    const funcGetProducts = async () => {
        funcLoader(true, "Consultando productos disponibles...");
        try {
            const apiResponse = await fetch(`${serverMain}${apiGetProducts}`);

            const response = await apiResponse.json();

            if (response.Code === 0) {
                if (Array.isArray(response.Result.lstNutritionalProducts)) {
                    const lstNutritional = response.Result.lstNutritionalProducts.map((product) => ({
                        name: product.sNombre,
                        shortName: product.sNombreCorto,
                        price: product.fCosto,
                        id: product.iIdProducto,
                        productType: 2,
                        qty: 1,
                        icon: `&#x${product.sIcon};`,
                        monthsExpiration: 0,
                        info: product.sDescripcion,
                        selected: false,
                    }));
                    setLstNutritionalProducts(lstNutritional);
                }
                if (Array.isArray(response.Result.lstPsychologyProducts)) {
                    const lstPsychology = response.Result.lstPsychologyProducts.map((product) => ({
                        name: product.sNombre,
                        shortName: product.sNombreCorto,
                        price: product.fCosto,
                        id: product.iIdProducto,
                        productType: 2,
                        qty: 1,
                        icon: `&#x${product.sIcon};`,
                        monthsExpiration: 0,
                        info: product.sDescripcion,
                        selected: false,
                    }));
                    setLstPsychologyProducts(lstPsychology);
                }
            }
        } catch (error) {}
        funcLoader();
    };

    const getData = async () => {
        await funcGetAppInfo();
        await funcGetProducts();
    };

    //Obtener las orientaciones/membresías disponibles de la base al cargar el componente
    useEffect(() => {
        getData();
        // eslint-disable-next-line
    }, []);

    return (
        <ThemeProvider theme={theme}>
            <Loader entLoader={entLoader} />
            <BrowserRouter basename={urlBase}>
                <Switch>
                    <Route exact path="/" render={() => <Redirect to={urlProducts} />} />
                    <Route exact path={urlProducts}>
                        <Prices
                            appInfo={appInfo}
                            funcLoader={funcLoader}
                            lstNutritionalProducts={lstNutritionalProducts}
                            lstPsychologyProducts={lstPsychologyProducts}
                        />
                    </Route>
                    <Route exact path={urlPayments}>
                        <Pays appInfo={appInfo} funcLoader={funcLoader} />
                    </Route>
                </Switch>
            </BrowserRouter>
        </ThemeProvider>
    );
}

export default App;
