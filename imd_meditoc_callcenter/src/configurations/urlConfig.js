/*************************************************************
 * Descripcion: Contiene las urls para el router del portal Meditoc
 * Creado: Cristopher Noh
 * Fecha: 26/08/2020
 *************************************************************/

const urlBase = '/call_center'

const urlSystem = {
  configuracion: {
    usuarios: '/usuarios',
    perfiles: '/perfiles',
    sistema: '/sistema',
  },
  administracion: {
    colaboradores: '/colaboradores',
    institucion: '/empresas',
    productos: '/productos',
    cupones: '/cupones',
    especialidades: '/especialidades',
  },
  folios: {
    folios: '/folios',
  },
  callcenter: {
    consultas: '/consultas',
    administrarConsultas: '/administrar_consultas',
  },
  reportes: {
    ordenes: '/ordenes',
    doctores: '/doctores',
  },
}

export { urlBase, urlSystem }
