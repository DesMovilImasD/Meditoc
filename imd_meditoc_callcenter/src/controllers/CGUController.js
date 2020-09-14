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
    this.apiGetPerfiles = 'Api/CGU/Get/Perfiles'
    this.apiGetUsuarios = 'Api/CGU/Get/Usuarios'
    this.apiCambiarPassword = 'Api/CGU/Create/CambiarContrasenia'
    this.apiGetLogin = 'Api/CGU/Get/Login'
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
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar obtener los permisos'
    }
    return response
  }

  async funcGetPerfiles(iIdPerfil = null, bActivo = true, bBaja = false) {
    let response = { Code: 0, Message: '', Result: [] }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiGetPerfiles}?iIdPerfil=${iIdPerfil}&bActivo=${bActivo}&bBaja=${bBaja}`,
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar obtener los perfiles'
    }
    return response
  }

  async funcGetUsuarios(
    iIdUsuario = null,
    iIdTipoCuenta = null,
    iIdPerfil = null,
    bActivo = true,
    bBaja = false,
  ) {
    let response = { Code: 0, Message: '', Result: [] }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiGetUsuarios}?iIdUsuario=${iIdUsuario}&iIdTipoCuenta=${iIdTipoCuenta}&iIdPerfil=${iIdPerfil}&bActivo=${bActivo}&bBaja=${bBaja}`,
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar obtener los usuarios'
    }
    return response
  }

  async funcCambiarPassword(
    iIdUsuario = 0,
    sPassword = '',
    iIdUsuarioUltMod = 0,
  ) {
    let response = { Code: 0, Message: '', Result: false }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiCambiarPassword}?iIdUsuario=${iIdUsuario}&sPassword=${sPassword}&iIdUsuarioUltMod=${iIdUsuarioUltMod}`,
        { method: 'POST' },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al intentar cambiar la contraseña'
    }
    return response
  }

  async funcGetLogin(sUsuario = '', sPassword = '') {
    let response = { Code: 0, Message: '', Result: {} }
    try {
      const apiResponse = await fetch(
        `${serverMain}${this.apiGetLogin}?sUsuario=${sUsuario}&sPassword=${sPassword}`,
        { method: 'POST' },
      )

      response = await apiResponse.json()
    } catch (error) {
      response.Code = -1
      response.Message = 'Ocurrió un error al validar los datos de sesión'
    }
    return response
  }
}

export default CGUController
