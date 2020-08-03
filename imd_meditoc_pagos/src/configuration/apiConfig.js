//CONFIGURACIONES DE LAS URLS PARA LAS API'S

const apiBuy = 'ClientesService.svc/NewUniqueQuery'
const apiGetMemberships = 'ClientesService.svc/getMembership'
const apiGetServices = 'ClientesService.svc/getServices'
const apiGetPolicies = 'ClientesService.svc/getPolicies'
const apiRevalidateCoupon = 'api/promociones/validar/cupon/email'
const apiValidateCoupon = 'api/promociones/validar/cupon'

export {
  apiValidateCoupon,
  apiRevalidateCoupon,
  apiBuy,
  apiGetServices,
  apiGetMemberships,
  apiGetPolicies,
}
