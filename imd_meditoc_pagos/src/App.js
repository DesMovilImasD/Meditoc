import React from 'react'
import { BrowserRouter, Switch, Route, Redirect } from 'react-router-dom'
import PrincipalPrecios from './components/Precios/Principal'
import PrincipalPagos from './components/Pagos/Principal'
import theme from './configuration/themeConfig'
import { ThemeProvider } from '@material-ui/core'

function App() {
  const directorio = {
    precios: '/precios',
    pagos: '/pagos',
  }

  return (
    <ThemeProvider theme={theme}>
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
            <PrincipalPagos />
          </Route>
        </Switch>
      </BrowserRouter>
    </ThemeProvider>
  )
}

export default App
