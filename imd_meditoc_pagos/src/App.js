import React, { useState, useEffect } from 'react'
import { BrowserRouter, Switch, Route, Redirect } from 'react-router-dom'
import Prices from './components/Prices/Main'
import Pays from './components/Payments/Main'
import theme from './configuration/themeConfig'
import { ThemeProvider } from '@material-ui/core'
import Loader from './components/Loader'
import { urlProducts, urlPayments, urlBase } from './configuration/urlConfig'
import { serverWs } from './configuration/serverConfig'
import { apiGetPolicies } from './configuration/apiConfig'
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
      const apiResponse = await fetch(`${serverWs}${apiGetPolicies}`)

      const response = await apiResponse.json()

      if (response.bRespuesta === true) {
        setAppInfo(response.sParameter1)
      }
    } catch (error) {}
  }

  //Ejecutar funcGetPolicies al cargar el componente
  useEffect(() => {
    funcGetAppInfo()

    // eslint-disable-next-line
  }, [])

  return (
    <ThemeProvider theme={theme}>
      <Loader entLoader={entLoader} />
      <BrowserRouter basename={urlBase}>
        <Switch>
          <Route exact path="/" render={() => <Redirect to={urlProducts} />} />
          <Route exact path={urlProducts}>
            <Prices appInfo={appInfo} funcLoader={funcLoader} />
          </Route>
          <Route exact path={urlPayments}>
            <Pays appInfo={appInfo} funcLoader={funcLoader} />
          </Route>
        </Switch>
      </BrowserRouter>
    </ThemeProvider>
  )
}

export default App
