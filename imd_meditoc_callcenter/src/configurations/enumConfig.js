const EnumPerfilesPrincipales = {
  Superadministrador: 1,
  Administrador: 2,
  DoctorCallCenter: 3,
  DoctorEspecialista: 4,
  AdministradorEspecialiesta: 5,
}

const EnumTipoCuenta = {
  Titular: 1,
  Administrativa: 2,
}

const EnumSistema = {
  Configuracion: 1,
  ConfiguracionSM: {
    Usuarios: 1,
    Perfiles: 2,
    Sistema: 3,
  },
  Administracion: 2,
  AdministracionSM: {
    Colaboradores: 1,
    Empresa: 2,
    Productos: 3,
    Cupones: 4,
    Especialidades: 5,
  },
  Folios: 3,
  FoliosSM: {
    Folios: 1,
  },
  CallCenter: 4,
  CallCenterSM: {
    Consultas: 1,
    AdministrarConsultas: 2,
  },
  Reportes: 5,
  ReportesSM: {
    Ventas: 1,
    Doctores: 2,
  },
}

const EnumTipoDoctor = {
  CallCenter: 1,
  Especialista: 2,
}

const EnumCuponCategoria = {
  DescuentoMonto: 1,
  DescuentoPorcentaje: 2,
}

const EnumOrigen = {
  WEB: 1,
  BaseDeDatos: 2,
  APP: 3,
  CallCenter: 4,
  PanelAdministrativo: 5,
}

const EnumIVA = {
  IVA: 0.16,
}

const EnumTipoProducto = {
  Membresia: 1,
  Servicio: 2,
}

const EnumEstatusConsulta = {
  CreadoProgramado: 1,
  Reprogramado: 2,
  EnConsulta: 3,
  Finalizado: 4,
  Cancelado: 5,
}

const EnumCatSexo = {
  Hombre: 1,
  Mujer: 2,
}

const EnumEspecialidadPrincipal = {
  MedicinaGeneral: 1,
}

const EnumStatusConekta = {
  Paid: 'paid',
  Declined: 'declined',
  OrderCreated: 'order created',
}

export {
  EnumPerfilesPrincipales,
  EnumTipoCuenta,
  EnumSistema,
  EnumTipoDoctor,
  EnumCuponCategoria,
  EnumOrigen,
  EnumIVA,
  EnumTipoProducto,
  EnumEstatusConsulta,
  EnumCatSexo,
  EnumEspecialidadPrincipal,
  EnumStatusConekta,
}
