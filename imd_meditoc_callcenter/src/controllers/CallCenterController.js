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
    this.apiColaboradorOnline = 'Api/CallCenter/Set/Colaborador/Online'
    this.apiCrearConsultaFolio = 'Api/CallCenter/Start/Service/WithFolio'
    this.apiIniciarConsulta = 'Api/CallCenter/Iniciar/Consulta'
    this.apiFinalizarConsulta = 'Api/CallCenter/Finalizar/Consulta'
    this.apiSavePaciente = 'Api/CallCenter/Guardar/Datos/Paciente'
    this.apiSaveHistorial = 'Api/CallCenter/Guardar/Historial/Clinico'
    this.apiGetHistorial = 'Api/CallCenter/Get/Historial/Clinico'
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

  async funcColaboradorOnline(entOnlineMod) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiColaboradorOnline}`,
        {
          method: 'POST',
          body: JSON.stringify(entOnlineMod),
          headers: MeditocHeadersCT,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar cambiar el estatus'
    }
    return response
  }

  async funcCrearConsultaFolio(
    iIdColaborador = 0,
    sFolio = '',
    iIdUsuarioMod = 0,
  ) {
    let response = { Code: 0, Message: '', Result: {} }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiCrearConsultaFolio}?iIdColaborador=${iIdColaborador}&sFolio=${sFolio}&iIdUsuarioMod=${iIdUsuarioMod}`,
        {
          method: 'POST',
          headers: MeditocHeaders,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar crear la consulta'
    }
    return response
  }
  async funcIniciarConsulta(
    iIdConsulta = 0,
    iIdColaborador = 0,
    iIdUsuarioMod = 0,
  ) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiIniciarConsulta}?iIdConsulta=${iIdConsulta}&iIdColaborador=${iIdColaborador}&iIdUsuarioMod=${iIdUsuarioMod}`,
        {
          method: 'POST',
          headers: MeditocHeaders,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar crear la consulta'
    }
    return response
  }

  async funcFinalizarConsulta(
    iIdConsulta = 0,
    iIdColaborador = 0,
    iIdUsuarioMod = 0,
  ) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiFinalizarConsulta}?iIdConsulta=${iIdConsulta}&iIdColaborador=${iIdColaborador}&iIdUsuarioMod=${iIdUsuarioMod}`,
        {
          method: 'POST',
          headers: MeditocHeaders,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar crear la consulta'
    }
    return response
  }

  async funcSavePaciente(entPaciente) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(`${serverMain}${this.apiSavePaciente}`, {
        method: 'POST',
        body: JSON.stringify(entPaciente),
        headers: MeditocHeadersCT,
      })

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar guardar el paciente'
    }
    return response
  }

  async funcSaveHistorialClinico(entHistorialClinico) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(`${serverMain}${this.apiSaveHistorial}`, {
        method: 'POST',
        body: JSON.stringify(entHistorialClinico),
        headers: MeditocHeadersCT,
      })

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message =
        'Ocurrió un error al intentar guardar el historial clinico'
    }
    return response
  }

  async funcGetHistorial(
    piIdHistorialClinico = null,
    piIdConsulta = null,
    piIdPaciente = null,
    piIdColaborador = null,
    piIdFolio = null,
  ) {
    let response = { Code: 0, Message: '', Result: [] }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiGetHistorial}?piIdHistorialClinico=${piIdHistorialClinico}&piIdConsulta=${piIdConsulta}&piIdPaciente=${piIdPaciente}&piIdColaborador=${piIdColaborador}&piIdFolio=${piIdFolio}`,
        {
          headers: MeditocHeaders,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar obtener el historial'
    }
    return response
  }
}

export default CallCenterController
