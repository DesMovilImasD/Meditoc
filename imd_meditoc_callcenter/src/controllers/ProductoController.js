import {
  MeditocHeadersCT,
  MeditocHeaders,
} from '../configurations/headersConfig'

const { serverMain } = require('../configurations/serverConfig')

class ProductoController {
  constructor() {
    this.apiCreateProducto = 'Api/Producto/Create/Producto'
    this.apiObtenerProductos = 'Api/Producto/Get/ObtenerProducto'
  }

  async funcSaveProducto(entProducto) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiCreateProducto}`,
        {
          method: 'POST',
          body: JSON.stringify(entProducto),
          headers: MeditocHeadersCT,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar guardar el producto'
    }
    return response
  }

  async funcGetProductos(iIdProducto = null) {
    let response = { Code: 0, Message: '', Result: [] }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiObtenerProductos}?iIdProducto=${iIdProducto}`,
        {
          headers: MeditocHeaders,
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar obtener los productos'
    }
    return response
  }
}

export default ProductoController
