const { serverMain } = require('../configurations/serverConfig')

/*************************************************************
 * Descripcion: Contiene las llamadas a los servicios del CGU
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 *************************************************************/
class CGUController {
  constructor() {
    this.apiSaveModulo = 'Api/CGU/Create/Modulo'
    this.apiSaveSubmodulo = 'Api/CGU/Create/SubModulo'
    this.apiSaveBoton = 'Api/CGU/Create/Boton'
    this.apiSavePerfil = 'Api/CGU/Create/Perfil'
    this.apiSaveUsuario = 'Api/CGU/Create/Usuario'
    this.apiSavePermiso = 'Api/CGU/Create/Permiso'
    this.apiGetPermisosXPerfil = 'Api/CGU/GET/PermisoXPerfil'
  }

  async funcSaveModulo(entCreateModulo) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(`${serverMain}${this.apiSaveModulo}`, {
        method: 'POST',
        body: JSON.stringify(entCreateModulo),
        headers: {
          'Content-Type': 'application/json',
        },
      })

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar guardar el módulo'
    }
    return response
  }

  async funcSaveSubmodulo(entCreateSubmodulo) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(`${serverMain}${this.apiSaveSubmodulo}`, {
        method: 'POST',
        body: JSON.stringify(entCreateSubmodulo),
        headers: {
          'Content-Type': 'application/json',
        },
      })

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar guardar el submódulo'
    }
    return response
  }

  async funcSaveBoton(entBoton) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(`${serverMain}${this.apiSaveBoton}`, {
        method: 'POST',
        body: JSON.stringify(entBoton),
        headers: {
          'Content-Type': 'application/json',
        },
      })

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar guardar el botón'
    }
    return response
  }

  async funcSavePerfil(entPerfil) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(`${serverMain}${this.apiSavePerfil}`, {
        method: 'POST',
        body: JSON.stringify(entPerfil),
        headers: {
          'Content-Type': 'application/json',
        },
      })

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar guardar el perfil'
    }
    return response
  }

  async funcSaveUsuario(entUsuario) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(`${serverMain}${this.apiSaveUsuario}`, {
        method: 'POST',
        body: JSON.stringify(entUsuario),
        headers: {
          'Content-Type': 'application/json',
        },
      })

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar guardar el usuario'
    }
    return response
  }

  async funcSavePermiso(entPermisos) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(`${serverMain}${this.apiSavePermiso}`, {
        method: 'POST',
        body: JSON.stringify(entPermisos),
        headers: {
          'Content-Type': 'application/json',
        },
      })

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar guardar el permiso'
    }
    return response
  }

  async funcGetPermisosXPeril(iIdPerfil = null) {
    let response = { Code: 0, Message: '', Result: [] }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiGetPermisosXPerfil}?iIdPerfil=${iIdPerfil}`,
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
        },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar obtener los permisos'
    }
    return response
  }
}

export default CGUController
