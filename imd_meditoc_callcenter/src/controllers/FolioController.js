const { serverMain } = require('../configurations/serverConfig')

class FolioController {
  constructor() {
    this.apiCrearFolioEmpresa = 'Api/Folio/Create/FolioEmpresa'
    this.apiObtenerFolios = 'Api/Folios/Get/Report'
    this.apiUpdFechaVencimiento = 'Api/Folio/Update/FechaVencimiento'
    this.apiDelFoliosEmpresa = 'Api/Folio/Delete/FoliosEmpresa'
  }

  async funcCrearFoliosEmpresa(entFolioEmpresa) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiCrearFolioEmpresa}`,
        {
          method: 'POST',
          body: JSON.stringify(entFolioEmpresa),
          headers: {
            'Content-Type': 'application/json',
          },
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message =
        'Ocurrió un error al intentar crear los folios de la empresa'
    }
    return response
  }

  async funcGetFolios(
    piIdFolio = null,
    piIdEmpresa = null,
    piIdProducto = null,
    piIdOrigen = null,
    psFolio = '',
    psOrdenConekta = '',
    pbTerminosYCondiciones = null,
    pbActivo = true,
    pbBaja = false,
  ) {
    let response = { Code: 0, Message: '', Result: [] }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiObtenerFolios}?piIdFolio=${piIdFolio}&piIdEmpresa=${piIdEmpresa}&piIdProducto=${piIdProducto}&piIdOrigen=${piIdOrigen}&psFolio=${psFolio}&psOrdenConekta=${psOrdenConekta}&pbTerminosYCondiciones=${pbTerminosYCondiciones}&pbActivo=${pbActivo}&pbBaja=${pbBaja}`,
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar obtener los folios'
    }
    return response
  }

  async funcUpdFechaVencimiento(entFolioFV) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiUpdFechaVencimiento}`,
        {
          method: 'POST',
          body: JSON.stringify(entFolioFV),
          headers: {
            'Content-Type': 'application/json',
          },
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message =
        'Ocurrió un error al intentar modificar la fecha de vencimiento de los folios'
    }
    return response
  }

  async funcEliminarFoliosEmpresa(entFolioFV) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiDelFoliosEmpresa}`,
        {
          method: 'POST',
          body: JSON.stringify(entFolioFV),
          headers: {
            'Content-Type': 'application/json',
          },
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar eliminar los folios'
    }
    return response
  }
}

export default FolioController
