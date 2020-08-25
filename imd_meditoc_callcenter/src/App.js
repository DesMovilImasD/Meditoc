import React, { useState, createRef } from 'react'
import { ThemeProvider, Button, Typography } from '@material-ui/core'
import theme from './configurations/themeConfig'
import Loader from './components/Loader'
import { SnackbarProvider } from 'notistack'
import Alert from './components/Alert'
import ContentMain from './components/ContentMain'
import { HashRouter } from 'react-router-dom'
import { urlBase } from './configurations/urlConfig'

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

  //Guardar valores de estado del alert
  const [entAlert, setEntAlert] = useState({
    message: '',
    variant: 'error',
    state: false,
  })

  //Mostrar una alerta
  const funcAlert = (pMessage = '', pVariant = 'error') => {
    setEntAlert({
      message: pMessage,
      variant: pVariant,
      state: !entAlert.state,
    })
  }

  const notistackRef = createRef()
  const onClickDismiss = (key) => () => {
    notistackRef.current.closeSnackbar(key)
  }

  return (
    <ThemeProvider theme={theme}>
      <SnackbarProvider
        maxSnack={3}
        anchorOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
        ref={notistackRef}
        action={(key) => (
          <Button onClick={onClickDismiss(key)} color="inherit">
            <Typography variant="caption">CERRAR</Typography>
          </Button>
        )}
      >
        <HashRouter>
          <Loader entLoader={entLoader} />
          <Alert entAlert={entAlert} />
          <ContentMain />
        </HashRouter>
      </SnackbarProvider>
    </ThemeProvider>
  )
}

export default App