const { MeditocHeaders } = require('../configurations/headersConfig')
const { serverMain } = require('../configurations/serverConfig')

class ReportesController {
  constructor() {
    this.apiGetDoctores = 'api/reportes/doctores'
    this.apiDescargarReporteDoctores = 'api/reportes/doctores/descargar'
    this.apiGetVentas = 'api/reportes/ventas'
    this.apiDescargarReporteVentas = 'api/reportes/ventas/descargar'
    this.apiGetReporteGlobal = 'api/reportes/ventas/meditoc'
  }

  async funcObtenerReporteDoctores(
    psIdColaborador = '',
    psColaborador = '',
    psIdTipoDoctor = '',
    psIdEspecialidad = '',
    psIdEstatusConsulta = '',
    psRFC = '',
    psNumSala = '',
    pdtFechaProgramadaInicio = null,
    pdtFechaProgramadaFinal = null,
    pdtFechaConsultaInicio = null,
    pdtFechaConsultaFin = null,
  ) {
    let response = { Code: 0, Message: '', Result: {} }

    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiGetDoctores}?psIdColaborador=${psIdColaborador}&psColaborador=${psColaborador}&psIdTipoDoctor=${psIdTipoDoctor}&psIdEspecialidad=${psIdEspecialidad}&psIdEstatusConsulta=${psIdEstatusConsulta}&psRFC=${psRFC}&psNumSala=${psNumSala}&pdtFechaProgramadaInicio=${pdtFechaProgramadaInicio}&pdtFechaProgramadaFinal=${pdtFechaProgramadaFinal}&pdtFechaConsultaInicio=${pdtFechaConsultaInicio}&pdtFechaConsultaFin=${pdtFechaConsultaFin}`,
        {
          headers: MeditocHeaders,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message =
        'Ocurrió un error al intentar obtener la información de los doctores'
    }
    return response
  }

  async funcDescargarReporteDoctores(
    psIdColaborador = '',
    psColaborador = '',
    psIdTipoDoctor = '',
    psIdEspecialidad = '',
    psIdEstatusConsulta = '',
    psRFC = '',
    psNumSala = '',
    pdtFechaProgramadaInicio = null,
    pdtFechaProgramadaFinal = null,
    pdtFechaConsultaInicio = null,
    pdtFechaConsultaFin = null,
  ) {
    const apiResponse = await fetch(
      `${serverMain}${this.apiDescargarReporteDoctores}?psIdColaborador=${psIdColaborador}&psColaborador=${psColaborador}&psIdTipoDoctor=${psIdTipoDoctor}&psIdEspecialidad=${psIdEspecialidad}&psIdEstatusConsulta=${psIdEstatusConsulta}&psRFC=${psRFC}&psNumSala=${psNumSala}&pdtFechaProgramadaInicio=${pdtFechaProgramadaInicio}&pdtFechaProgramadaFinal=${pdtFechaProgramadaFinal}&pdtFechaConsultaInicio=${pdtFechaConsultaInicio}&pdtFechaConsultaFin=${pdtFechaConsultaFin}`,
      {
        headers: MeditocHeaders,
      },
    )
    return apiResponse
  }

  async funcObtenerReporteVentas(
    psFolio = '',
    psIdEmpresa = '',
    psIdProducto = '',
    psIdTipoProducto = '',
    psIdOrigen = '',
    psOrderId = '',
    psStatus = '',
    psCupon = '',
    psTipoPago = '',
    pdtFechaInicio = null,
    pdtFechaFinal = null,
    pdtFechaVencimiento = null,
  ) {
    let response = { Code: 0, Message: '', Result: {} }

    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiGetVentas}?psFolio=${psFolio}&psIdEmpresa=${psIdEmpresa}&psIdProducto=${psIdProducto}&psIdTipoProducto=${psIdTipoProducto}&psIdOrigen=${psIdOrigen}&psOrderId=${psOrderId}&psStatus=${psStatus}&psCupon=${psCupon}&psTipoPago=${psTipoPago}&pdtFechaInicio=${pdtFechaInicio}&pdtFechaFinal=${pdtFechaFinal}&pdtFechaVencimiento=${pdtFechaVencimiento}`,
        {
          headers: MeditocHeaders,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message =
        'Ocurrió un error al intentar obtener la información de ventas'
    }
    return response
  }

  async funcObtenerReporteGlobal(
    psFolio = '',
    psIdEmpresa = '',
    psIdProducto = '',
    psIdTipoProducto = '',
    psIdOrigen = '',
    psOrderId = '',
    psStatus = '',
    psCupon = '',
    psTipoPago = '',
    pdtFechaInicio = null,
    pdtFechaFinal = null,
    pdtFechaVencimiento = null,
  ) {
    let response = { Code: 0, Message: '', Result: {} }

    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiGetReporteGlobal}?psFolio=${psFolio}&psIdEmpresa=${psIdEmpresa}&psIdProducto=${psIdProducto}&psIdTipoProducto=${psIdTipoProducto}&psIdOrigen=${psIdOrigen}&psOrderId=${psOrderId}&psStatus=${psStatus}&psCupon=${psCupon}&psTipoPago=${psTipoPago}&pdtFechaInicio=${pdtFechaInicio}&pdtFechaFinal=${pdtFechaFinal}&pdtFechaVencimiento=${pdtFechaVencimiento}`,
        {
          headers: MeditocHeaders,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message =
        'Ocurrió un error al intentar obtener la información de ventas'
    }
    return response
  }

  async funcDescargarReporteVentas(
    psFolio = '',
    psIdEmpresa = '',
    psIdProducto = '',
    psIdTipoProducto = '',
    psIdOrigen = '',
    psOrderId = '',
    psStatus = '',
    psCupon = '',
    psTipoPago = '',
    pdtFechaInicio = null,
    pdtFechaFinal = null,
    pdtFechaVencimiento = null,
  ) {
    const apiResponse = await fetch(
      `${serverMain}${this.apiDescargarReporteVentas}?psFolio=${psFolio}&psIdEmpresa=${psIdEmpresa}&psIdProducto=${psIdProducto}&psIdTipoProducto=${psIdTipoProducto}&psIdOrigen=${psIdOrigen}&psOrderId=${psOrderId}&psStatus=${psStatus}&psCupon=${psCupon}&psTipoPago=${psTipoPago}&pdtFechaInicio=${pdtFechaInicio}&pdtFechaFinal=${pdtFechaFinal}&pdtFechaVencimiento=${pdtFechaVencimiento}`,
      {
        headers: MeditocHeaders,
      },
    )
    return apiResponse
  }
}

export default ReportesController
