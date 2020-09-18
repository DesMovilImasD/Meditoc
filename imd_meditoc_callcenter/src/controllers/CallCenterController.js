import {
  MeditocHeaders,
  MeditocHeadersCT,
} from '../configurations/headersConfig'

const { serverMain } = require('../configurations/serverConfig')

class CallCenterController {
  constructor() {
    this.apiGetConsulta = 'Api/CallCenter/Get/Consulta/Detalle'
    this.apiNuevaConsulta = 'Api/CallCenter/Save/Folio/Especialista/Consulta'
    this.apiCancelarConsulta =
      'Api/CallCenter/Cancelar/Folio/Especialista/Consulta'
  }

  async funcGetConsulta(
    piIdConsulta = null,
    piIdPaciente = null,
    piIdColaborador = null,
    piIdEstatusConsulta = null,
    pdtFechaProgramadaInicio = null,
    pdtFechaProgramadaFin = null,
    pdtFechaConsultaInicio = null,
    pdtFechaConsultaFin = null,
  ) {
    let response = { Code: 0, Message: '', Result: [] }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiGetConsulta}?piIdConsulta=${piIdConsulta}&piIdPaciente=${piIdPaciente}&piIdColaborador=${piIdColaborador}&piIdEstatusConsulta=${piIdEstatusConsulta}&pdtFechaProgramadaInicio=${pdtFechaProgramadaInicio}&pdtFechaProgramadaFin=${pdtFechaProgramadaFin}&pdtFechaConsultaInicio=${pdtFechaConsultaInicio}&pdtFechaConsultaFin=${pdtFechaConsultaFin}`,
        {
          headers: MeditocHeaders,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar obtener las consultas'
    }
    return response
  }

  async funcNuevaConsulta(entNuevaConsulta) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(`${serverMain}${this.apiNuevaConsulta}`, {
        method: 'POST',
        body: JSON.stringify(entNuevaConsulta),
        headers: MeditocHeadersCT,
      })

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar guardar la consulta'
    }
    return response
  }

  async funcCancelarConsulta(entNuevaConsulta) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiCancelarConsulta}`,
        {
          method: 'POST',
          body: JSON.stringify(entNuevaConsulta),
          headers: MeditocHeadersCT,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar cancelar la consulta'
    }
    return response
  }
}

export default CallCenterController
