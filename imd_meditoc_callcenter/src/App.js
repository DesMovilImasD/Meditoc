import React, { useState, createRef } from 'react'
import { ThemeProvider, Button, Typography } from '@material-ui/core'
import theme from './configurations/themeConfig'
import Loader from './components/utilidades/Loader'
import { SnackbarProvider } from 'notistack'
import Alert from './components/utilidades/Alert'
import ContentMain from './components/ContentMain'
import { HashRouter } from 'react-router-dom'
import Login from './components/login/Login'
import { MuiPickersUtilsProvider } from '@material-ui/pickers'
import MomentUtils from '@date-io/moment'
import 'moment/locale/es'

/*************************************************************
 * Descripcion: App del proyecto
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 * Invocado desde:  ---------Elemento Raíz----------
 *************************************************************/
function App() {
  const [usuarioSesion, setUsuarioSesion] = useState({
    iIdUsuario: 4,
    iIdTipoCuenta: 1,
    iIdPerfil: 1,
    sTipoCuenta: null,
    sPerfil: null,
    sUsuario: 'jperez',
    sPassword: null,
    sNombres: 'Juanito',
    sApellidoPaterno: 'Perez',
    sApellidoMaterno: null,
    dtFechaNacimiento: '1982-06-04T01:38:00',
    sTelefono: '9994450694',
    sCorreo: 'g098@live.com.mx',
    sDomicilio: '12',
    iIdUsuarioMod: 0,
    bActivo: false,
    bBaja: false,
  })
  const [usuarioActivo, setUsuarioActivo] = useState(true)

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
        <MuiPickersUtilsProvider utils={MomentUtils} locale="es">
          <HashRouter>
            <Loader entLoader={entLoader} />
            <Alert entAlert={entAlert} />
            {usuarioActivo === true ? (
              <ContentMain
                usuarioSesion={usuarioSesion}
                setUsuarioSesion={setUsuarioSesion}
                setUsuarioActivo={setUsuarioActivo}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
              />
            ) : (
              <Login
                setUsuarioSesion={setUsuarioSesion}
                setUsuarioActivo={setUsuarioActivo}
                funcLoader={funcLoader}
                funcAlert={funcAlert}
              />
            )}
          </HashRouter>
        </MuiPickersUtilsProvider>
      </SnackbarProvider>
    </ThemeProvider>
  )
}

export default App
