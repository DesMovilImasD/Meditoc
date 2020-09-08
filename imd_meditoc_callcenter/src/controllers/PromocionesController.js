const { serverMain } = require('../configurations/serverConfig')

class PromocionesController {
  constructor() {
    this.apiCrearCupon = 'api/promociones/guardar/cupon'
    this.apiDesactivarCupon = 'api/promociones/desactivar/cupon'
    this.apiObtenerCupones = 'api/promociones/obtener/cupones'
    this.apiAutocompletarCupon = 'api/promociones/obtener/cupon/autocompletar'
  }

  async funcCrearCupon(entCreateCupon, piIdUsuario = null) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiCrearCupon}?piIdUsuario=${piIdUsuario}`,
        {
          method: 'POST',
          body: JSON.stringify(entCreateCupon),
          headers: {
            'Content-Type': 'application/json',
          },
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar guardar el cupón'
    }
    return response
  }

  async funcDesactivarCupon(piIdCupon, piIdUsuario) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiDesactivarCupon}?piIdCupon=${piIdCupon}&piIdUsuario=${piIdUsuario}`,
        {
          method: 'POST',
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar desactivar el cupón'
    }
    return response
  }

  async funcObtenerCupones() {
    let response = { Code: 0, Message: '', Result: [] }
    try {
      const apiResponse = await fetch(`${serverMain}${this.apiObtenerCupones}`)

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar obtener los cupones'
    }
    return response
  }

  async funcAutocompletarCupon() {
    let response = { Code: 0, Message: '', Result: [] }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiAutocompletarCupon}`,
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar obtener los cupones'
    }
    return response
  }
}

export default PromocionesController
