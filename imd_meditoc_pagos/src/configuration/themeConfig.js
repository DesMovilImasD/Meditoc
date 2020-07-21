import { createMuiTheme } from '@material-ui/core/styles'

//Configurar solores del tema
const theme = createMuiTheme({
  palette: {
    primary: {
      main: '#12B6CB',
      contrastText: 'white',
    },
    secondary: {
      main: '#115C8A',
      contrastText: 'white',
    },
  },
})

export default theme
