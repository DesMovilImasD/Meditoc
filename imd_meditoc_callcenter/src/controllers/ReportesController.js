import { LiveTv } from '@material-ui/icons'

const { MeditocHeaders } = require('../configurations/headersConfig')
const { serverMain } = require('../configurations/serverConfig')

class ReportesController {
  constructor() {
    this.apiGetDoctores = 'api/reportes/doctores'
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
        `${serverMain}${this.apiGetDoctores}?psIdColaborador=${psIdColaborador}`,
        {
          headers: MeditocHeaders,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar obtener los reportes'
    }
    return response
  }
}

export default ReportesController
