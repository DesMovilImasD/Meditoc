const urlBase = '/call_center'

const urlSystem = {
  configuracion: {
    usuarios: '/usuarios',
    perfiles: '/perfiles',
    sistema: '/sistema',
  },
  administracion: {
    colaboradores: '/colaboradores',
    institucion: '/institucion',
    productos: '/productos',
    cupones: '/cupones',
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
