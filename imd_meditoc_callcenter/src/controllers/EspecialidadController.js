import {
  MeditocHeadersCT,
  MeditocHeaders,
} from '../configurations/headersConfig'

const { serverMain } = require('../configurations/serverConfig')

class EspecialidadController {
  constructor() {
    this.apiSaveEspecialidad = 'Api/Especialidad/Create/Resgistro'
    this.apiGetEspecialidad = 'Api/Especialidad/Get/Registros'
  }

  async funcSaveEspecialidad(entEspecialidad) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiSaveEspecialidad}`,
        {
          method: 'POST',
          body: JSON.stringify(entEspecialidad),
          headers: MeditocHeadersCT,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar guardar la especialidad'
    }
    return response
  }

  async funcGetEspecialidad(piIdEspecialidad = null) {
    let response = { Code: 0, Message: '', Result: [] }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiGetEspecialidad}?piIdEspecialidad=${piIdEspecialidad}`,
        {
          headers: MeditocHeaders,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message =
        'Ocurrió un error al intentar obtener las especialidades'
    }
    return response
  }
}

export default EspecialidadController
