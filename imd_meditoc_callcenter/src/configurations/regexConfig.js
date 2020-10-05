const rxNumeroEntero = /^\d+$/;
const rxNumedoDecimal2 = /^\d+(?:\.\d{1,2})?$/;
const rxCorreo = /^[^@]+@[^@]+\.[a-zA-Z]{2,}$/;
const rxUrl = /^https?:\/\/(www\.)?[-a-zA-Z0-9@:%._+~#=]{1,256}\.[a-z]{2,4}\b([-a-zA-Z0-9@:%_+.~#?&//=]*)$/;

export { rxNumeroEntero, rxNumedoDecimal2, rxCorreo, rxUrl };
