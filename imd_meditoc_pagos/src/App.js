import React, { useState } from 'react'
import { BrowserRouter, Switch, Route, Redirect } from 'react-router-dom'
import Prices from './components/Prices/Main'
import Pays from './components/Payments/Main'
import theme from './configuration/themeConfig'
import { ThemeProvider } from '@material-ui/core'
import Loader from './components/Loader'
import { urlProducts, urlPayments, urlBase } from './configuration/urlConfig'

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

  //Mostrar/Ocultar loader y configurar mensaje
  const funcLoader = (pOpen = false, pMessage = 'Cargando...') => {
    setEntLoader({
      open: pOpen,
      message: pMessage,
    })
  }

  return (
    <ThemeProvider theme={theme}>
      <Loader entLoader={entLoader} />
      <BrowserRouter basename={urlBase}>
        <Switch>
          <Route exact path="/" render={() => <Redirect to={urlProducts} />} />
          <Route exact path={urlProducts}>
            <Prices />
          </Route>
          <Route exact path={urlPayments}>
            <Pays funcLoader={funcLoader} />
          </Route>
        </Switch>
      </BrowserRouter>
    </ThemeProvider>
  )
}

export default App
