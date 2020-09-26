import React, { useState, useEffect } from 'react'
import { BrowserRouter, Switch, Route, Redirect } from 'react-router-dom'
import Prices from './components/Prices/Main'
import Pays from './components/Payments/Main'
import Directories from './components/Directory/Main'
import theme from './configuration/themeConfig'
import { ThemeProvider } from '@material-ui/core'
import Loader from './components/Loader'
import {
  urlProducts,
  urlPayments,
  urlBase,
  urlDirectory,
} from './configuration/urlConfig'
import { serverMain, serverWs } from './configuration/serverConfig'
import {
  apiGetPolicies,
  apiGetServices,
  apiGetMemberships,
} from './configuration/apiConfig'
import apiKeyToken from './configuration/tokenConfig'

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
    message: 'Cargando...',
  })

  //Guardar lista de productos de orientaciones únicas
  const [lstOrientationProducts, setLstOrientationProducts] = useState([])

  //Guardar lista de productos de membresías
  const [lstMembershipProducts, setLstMembershipProducts] = useState([])

  //Guardar los links de Aviso de Privacidad y Términos y condiciones
  const [appInfo, setAppInfo] = useState({
    nMaximoDescuento: 0.9,
    sAvisoDePrivacidad: '#',
    sConektaPublicKey: apiKeyToken,
    sCorreoContacto: 'contacto@meditoc.com',
    sCorreoSoporte: 'contacto@meditoc.com',
    sDireccionEmpresa: 'Calle 17 #113, Col. Itzimná, 97100, Mérida, Yuc.',
    sTelefonoEmpresa: '5551-003021',
    sTerminosYCondiciones: '#',
    nIVA: 0.16,
    bTieneMesesSinIntereses: false,
    lstMensualidades: [
      {
        compra_minima: 300,
        descripcion: '3 meses sin intereses',
        meses: 3,
      },
      {
        compra_minima: 600,
        descripcion: '6 meses sin intereses',
        meses: 6,
      },
      {
        compra_minima: 900,
        descripcion: '9 meses sin intereses',
        meses: 9,
      },
      {
        compra_minima: 1200,
        descripcion: '12 meses sin intereses',
        meses: 12,
      },
    ],
  })

  //Mostrar/Ocultar loader y configurar mensaje
  const funcLoader = (pOpen = false, pMessage = 'Cargando...') => {
    setEntLoader({
      open: pOpen,
      message: pMessage,
    })
  }

  //Consumir servicio para obtener los links de las políticas Meditoc
  const funcGetAppInfo = async () => {
    try {
      const apiResponse = await fetch(`${serverMain}${apiGetPolicies}`)

      const response = await apiResponse.json()

      if (response.Code === 0) {
        setAppInfo(response.Result)
      }
    } catch (error) {}
  }

  //Consumir servicio para obtener las orientaciones
  const funcGetOrientations = async () => {
    funcLoader(true, 'Consultando servicios disponibles...')
    try {
      // const apiResponse = await fetch(`${serverMain}${apiGetServices}`, {
      //   method: 'GET',
      //   headers: {
      //     AppKey: 'qSVBJIQpOqtp0UfwzwX1ER6fNYR8YiPU/bw5CdEqYqk=',
      //     AppToken:
      //       'Xx3ePv63cUTg77QPATmztJ3J8cdO1riA7g+lVRzOzhfnl9FnaVT1O2YIv8YCTVRZ',
      //   },
      // })
      const apiResponse = await fetch(`${serverMain}${apiGetServices}`)

      const response = await apiResponse.json()

      if (response.Code === 0) {
        const lstGetProductMapped = response.Result.map((product) => ({
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
        }))

        setLstOrientationProducts(lstGetProductMapped)
      }
    } catch (error) {}
    funcLoader()
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
  }

  //Consumir servicio para obtener las membresias
  const funcGetMemberships = async () => {
    funcLoader(true, 'Consultando membresías disponibles...')
    try {
      // const apiResponse = await fetch(`${serverMain}${apiGetMemberships}`, {
      //   method: 'GET',
      //   headers: {
      //     AppKey: 'qSVBJIQpOqtp0UfwzwX1ER6fNYR8YiPU/bw5CdEqYqk=',
      //     AppToken:
      //       'Xx3ePv63cUTg77QPATmztJ3J8cdO1riA7g+lVRzOzhfnl9FnaVT1O2YIv8YCTVRZ',
      //   },
      // })
      const apiResponse = await fetch(`${serverMain}${apiGetMemberships}`)

      const response = await apiResponse.json()

      if (response.Code === 0) {
        const lstGetProductMapped = response.Result.map((product) => ({
          name: product.sNombre,
          shortName: product.sNombreCorto,
          price: product.fCosto,
          id: product.iIdProducto,
          productType: 1,
          qty: 1,
          icon: `&#x${product.sIcon};`,
          monthsExpiration: product.iMesVigencia,
          info: product.sDescripcion,
          selected: false,
        }))

        setLstMembershipProducts(lstGetProductMapped)
      }
    } catch (error) {}
    funcLoader()
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
  }

  const getData = async () => {
    await funcGetAppInfo()
    await funcGetOrientations()
    await funcGetMemberships()
  }

  //Obtener las orientaciones/membresías disponibles de la base al cargar el componente
  useEffect(() => {
    getData()
    // eslint-disable-next-line
  }, [])

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
              lstOrientationProducts={lstOrientationProducts}
              setLstOrientationProducts={setLstOrientationProducts}
              lstMembershipProducts={lstMembershipProducts}
              setLstMembershipProducts={setLstMembershipProducts}
            />
          </Route>
          <Route exact path={urlPayments}>
            <Pays appInfo={appInfo} funcLoader={funcLoader} />
          </Route>
          <Route exact path={urlDirectory}>
            <Directories appInfo={appInfo} funcLoader={funcLoader} />
          </Route>
        </Switch>
      </BrowserRouter>
    </ThemeProvider>
  )
}

export default App
