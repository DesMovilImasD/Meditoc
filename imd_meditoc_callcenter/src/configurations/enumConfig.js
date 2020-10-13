const EnumPerfilesPrincipales = {
    Superadministrador: 1,
    Administrador: 2,
    DoctorCallCenter: 3,
    DoctorEspecialista: 4,
    AdministradorEspecialiesta: 5,
    DoctorAdministrador: 6,
};

const EnumListPerfilesColaboradores = [
    EnumPerfilesPrincipales.DoctorAdministrador,
    EnumPerfilesPrincipales.DoctorCallCenter,
    EnumPerfilesPrincipales.DoctorEspecialista,
    EnumPerfilesPrincipales.AdministradorEspecialiesta,
];

const EnumTipoCuenta = {
    Titular: 1,
    Administrativa: 2,
};

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
};

const EnumTipoDoctor = {
    CallCenter: 1,
    Especialista: 2,
    Administrativo: 3,
};

const EnumCuponCategoria = {
    DescuentoMonto: 1,
    DescuentoPorcentaje: 2,
};

const EnumOrigen = {
    WEB: 1,
    BaseDeDatos: 2,
    APP: 3,
    CallCenter: 4,
    PanelAdministrativo: 5,
};

const EnumIVA = {
    IVA: 0.16,
};

const EnumTipoProducto = {
    Membresia: 1,
    Servicio: 2,
};

const EnumEstatusConsulta = {
    CreadoProgramado: 1,
    Reprogramado: 2,
    EnConsulta: 3,
    Finalizado: 4,
    Cancelado: 5,
};

const EnumCatSexo = {
    Hombre: 1,
    Mujer: 2,
};

const EnumEspecialidadPrincipal = {
    MedicinaGeneral: 1,
};

const EnumStatusConekta = {
    Paid: "paid",
    Declined: "declined",
    OrderCreated: "order created",
};

const EnumTipoPago = {
    Credit: "credit",
    Debit: "debit",
};

const EnumReportesTabs = {
    Conekta: 0,
    Administrativo: 1,
};

const EnumProductos = {
    OrientacionEspecialistaID: 1,
    MembresiaVentaCalle1Mes: 2,
    MembresiaVentaCalle3Meses: 3,
    MembresiaVentaCalle6Meses: 4,
    MembresiaVentaCalle9Meses: 5,
    MembresiaVentaCalle12Meses: 6,
};

const ListProductos = [
    EnumProductos.OrientacionEspecialistaID,
    EnumProductos.MembresiaVentaCalle1Mes,
    EnumProductos.MembresiaVentaCalle3Meses,
    EnumProductos.MembresiaVentaCalle6Meses,
    EnumProductos.MembresiaVentaCalle9Meses,
    EnumProductos.MembresiaVentaCalle12Meses,
];

const EnumGrupoProducto = {
    MeditocProducts: 1,
    NutritionalProducts: 2,
    PsychologyProducts: 3,
};

const EnumEstatusCupon = {
    todos: "todos",
    activo: "activo",
    inactivo: "inactivo",
};

export {
    EnumPerfilesPrincipales,
    EnumListPerfilesColaboradores,
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
    EnumTipoPago,
    EnumReportesTabs,
    EnumProductos,
    EnumGrupoProducto,
    EnumEstatusCupon,
    ListProductos,
};
