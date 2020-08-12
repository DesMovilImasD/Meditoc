import PropTypes from "prop-types";
import React, { useState, useEffect } from "react";
import { Grid } from "@material-ui/core";
import Memberships from "./Memberships";
import Orientations from "./Orientations";
// eslint-disable-next-line
import { serverWs } from "../../configuration/serverConfig";
// eslint-disable-next-line
import { apiGetServices, apiGetMemberships } from "../../configuration/apiConfig";

/*****************************************************
 * Descripción: Contiene la estructura para mostrar
 * los precios de servicios Meditoc
 * Autor: Cristopher Noh
 * Fecha: 22/07/2020
 * Modificaciones:
 *****************************************************/
const Content = (props) => {
    // eslint-disable-next-line
    const { funcLoader } = props;

    //Guardar lista de productos de orientaciones únicas
    const [lstOrientationProducts, setLstOrientationProducts] = useState([]);

    //Guardar lista de productos de membresías
    const [lstMembershipProducts, setLstMembershipProducts] = useState([]);

    //Consumir servicio para obtener las orientaciones
    const funcGetOrientations = async () => {
        funcLoader(true, "Consultando servicios disponibles...");
        try {
            const apiResponse = await fetch(`${serverWs}${apiGetServices}`, {
                method: "POST",
            });

            const response = await apiResponse.json();

            if (response.bRespuesta === true) {
                const lstGetProducts = JSON.parse(response.sParameter1);

                if (lstGetProducts !== null && lstGetProducts !== undefined) {
                    const lstGetProductMapped = lstGetProducts.map((product) => ({
                        name: product.sNombre,
                        shortName: product.sNombreCorto,
                        price: product.fPrecio,
                        id: product.iIdProducto,
                        productType: 2,
                        qty: 1,
                        icon: `&#x${product.sIcon};`,
                        monthsExpiration: 0,
                        info: product.sResumen,
                        selected: false,
                    }));

                    setLstOrientationProducts(lstGetProductMapped);
                }
            }
        } catch (error) {}
        funcLoader();
        /**
         * SIMULACIÓN
         */
        // const lstGetProducts = [
        //     {
        //         lstProducto: [],
        //         iIdProducto: 294,
        //         sNombre: "Orientación Médica",
        //         sNombreCorto: "Médica",
        //         sDescripcion: "Podrá realizar una orientación médica con nuestros especialistas.",
        //         sResumen:
        //             "Contará con un servicio de orientación médica válido por 24 horas, el cual le dará acceso a tener una llamada telefónica, chat y videollamada.",
        //         fPrecio: 55.0,
        //         iTiempoVigencia: 0,
        //         sIcon: "f469",
        //     },
        //     {
        //         lstProducto: [],
        //         iIdProducto: 295,
        //         sNombre: "Orientación Psicológica ",
        //         sNombreCorto: "Psicológica",
        //         sDescripcion: "Podrá realizar una orientación priscologica con nuestros especialistas.",
        //         sResumen:
        //             "Contará con un servicio de orientación psicológica válido por 24 horas, el cual le dará acceso a tener una llamada telefónica, chat y videollamada.",
        //         fPrecio: 55.0,
        //         iTiempoVigencia: 0,
        //         sIcon: "f7f5",
        //     },
        //     {
        //         lstProducto: [],
        //         iIdProducto: 297,
        //         sNombre: "Orientación Nutricional",
        //         sNombreCorto: "Nutricional",
        //         sDescripcion: "Podrá realizar una orientación nutricional con nuestros especialistas",
        //         sResumen:
        //             "Contará con un servicio de orientación nutricional válido por 24 horas, el cual le dará acceso a tener una llamada telefónica, chat y videollamada.",
        //         fPrecio: 55.0,
        //         iTiempoVigencia: 0,
        //         sIcon: "f481",
        //     },
        // ];
        // const lstGetProductMapped = lstGetProducts.map((product) => ({
        //     name: product.sNombre,
        //     shortName: product.sNombreCorto,
        //     price: product.fPrecio,
        //     id: product.iIdProducto,
        //     productType: 2,
        //     qty: 1,
        //     icon: `&#x${product.sIcon};`,
        //     monthsExpiration: 0,
        //     info: product.sResumen,
        //     selected: false,
        // }));
        // setLstOrientationProducts(lstGetProductMapped);
    };

    //Consumir servicio para obtener las membresias
    const funcGetMemberships = async () => {
        funcLoader(true, "Consultando membresías disponibles...");
        try {
            const apiResponse = await fetch(`${serverWs}${apiGetMemberships}`, {
                method: "POST",
            });

            const response = await apiResponse.json();

            if (response.bRespuesta === true) {
                const lstGetProducts = JSON.parse(response.sParameter1);
                if (lstGetProducts !== null && lstGetProducts !== undefined) {
                    const lstGetProductMapped = lstGetProducts.map((product) => ({
                        name: product.sNombre,
                        shortName: product.sNombreCorto,
                        price: product.fPrecio,
                        id: product.iIdProducto,
                        productType: 1,
                        qty: 1,
                        icon: `&#x${product.sIcon};`,
                        monthsExpiration: product.iTiempoVigencia,
                        info: product.sResumen,
                        selected: false,
                    }));

                    setLstMembershipProducts(lstGetProductMapped);
                }
            }
        } catch (error) {}
        funcLoader();
        /**
         * SIMULACIÓN
         */
        // const lstGetProducts = [
        //     {
        //         lstProducto: [],
        //         iIdProducto: 292,
        //         sNombre: "Membresía 6 meses",
        //         sNombreCorto: "6 meses",
        //         sDescripcion: "Membresía por 6  meses",
        //         sResumen:
        //             "Desde el primer día contará con servicio de orientación médica, nutricional y psicológica durante seis meses para todos los miembros de su familia, empleados de empresas u hogar y sus familias, el cual es simple de usar, tendrá respuesta inmediata y llamadas ilimitadas de orientación médica.",
        //         fPrecio: 800.0,
        //         iTiempoVigencia: 6,
        //         sIcon: "f7e6",
        //     },
        //     {
        //         lstProducto: [],
        //         iIdProducto: 296,
        //         sNombre: "Membresía 12 meses",
        //         sNombreCorto: "1 año",
        //         sDescripcion: "Membresía por 1 año",
        //         sResumen:
        //             "Desde el primer día contará con servicio de orientación médica, nutricional y psicológica los 365 días del año para todos los miembros de su familia, empleados de empresas u hogar y sus familias, el cual es simple de usar, tendrá respuesta inmediata y llamadas ilimitadas de orientación médica.",
        //         fPrecio: 1450.0,
        //         iTiempoVigencia: 12,
        //         sIcon: "f7e6",
        //     },
        // ];
        // const lstGetProductMapped = lstGetProducts.map((product) => ({
        //     name: product.sNombre,
        //     shortName: product.sNombreCorto,
        //     price: product.fPrecio,
        //     id: product.iIdProducto,
        //     productType: 1,
        //     qty: 1,
        //     icon: `&#x${product.sIcon};`,
        //     monthsExpiration: product.iTiempoVigencia,
        //     info: product.sResumen,
        //     selected: false,
        // }));
        // setLstMembershipProducts(lstGetProductMapped);
    };

    //Obtener las orientaciones/membresías disponibles de la base al cargar el componente
    useEffect(() => {
        funcGetOrientations();
        funcGetMemberships();

        // eslint-disable-next-line
    }, []);

    return (
        <div className="price-content-container">
            <Grid container className="center" spacing={4}>
                <Grid item xs={12}>
                    <div className="price-content-title">
                        <span className="primary-blue">Meditoc</span> <span className="primary-gray">360</span>
                    </div>
                </Grid>
                <Grid item xs={12}>
                    <span className="price-content-description">
                        Meditoc 360 ofrece orientación médica, nutricional y psicológica a distancia los 365 días del
                        año,
                        <br /> brindando acceso a servicio de salud de calidad.
                    </span>
                </Grid>
                <Grid item xs={12}>
                    <span className="price-content-description-normal">
                        Podrá adquirir membresías para los siguientes servicios.
                    </span>
                </Grid>
                <Grid item md={6} xs={12}>
                    {lstMembershipProducts.length > 0 ? (
                        <Memberships lstMembershipProducts={lstMembershipProducts} />
                    ) : null}
                </Grid>
                <Grid item md={6} xs={12}>
                    {lstOrientationProducts.length > 0 ? (
                        <Orientations
                            lstOrientationProducts={lstOrientationProducts}
                            setLstOrientationProducts={setLstOrientationProducts}
                        />
                    ) : null}
                </Grid>
            </Grid>
        </div>
    );
};

Content.propTypes = {
    funcLoader: PropTypes.func.isRequired,
};

export default Content;
