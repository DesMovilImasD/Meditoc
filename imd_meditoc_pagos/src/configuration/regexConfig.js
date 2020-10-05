//Validaciones de tarjetas
const rxVisaCard = /^4[0-9]{12}(?:[0-9]{3})?$/;
const rxMasterCard = /^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$/;
const rxAmexCard = /^3[47][0-9]{13}$/;
const rxDinersCard = /^3(?:0[0-5]|[68][0-9])[0-9]{11}$/;
const rxDiscoverCard = /^6(?:011|5[0-9]{2})[0-9]{12}$/;
const rxJcbCard = /^(?:2131|1800|35\d{3})\d{11}$/;

export { rxVisaCard, rxMasterCard, rxAmexCard, rxDinersCard, rxDiscoverCard, rxJcbCard };

//Validaciones de iconos de tarjeta
const rxVisaIcon = /^4\d+$/;
const rxMasterIcon = /^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)\d+$/;
const rxAmexIcon = /^3[47]\d+$/;
const rxDinersIcon = /^3(?:0[0-5]|[68][0-9])\d+$/;
const rxDiscoverIcon = /^6(?:011|5[0-9]{2})\d+$/;
const rxJcbIcon = /^(?:2131|1800|35\d{3})\d+$/;

export { rxVisaIcon, rxMasterIcon, rxAmexIcon, rxDinersIcon, rxDiscoverIcon, rxJcbIcon };

const rxEmail = /^[^@]+@[^@]+\.[a-zA-Z]{2,}$/;
const rxUrl = /^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._+~#=]{1,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_+.~#?&//=]*)$/;

export { rxEmail, rxUrl };
