//CONFIGURACIONES DE DATOS DE ARTICULOS
import React from 'react'
import {
  FaBookMedical,
  FaBriefcaseMedical,
  FaCommentMedical,
  FaNotesMedical,
} from 'react-icons/fa'

const membershipTypeProduct = 1
const orientationTypeProduct = 2

const productOrientationPrice = 55

const productSixMonthMembership = {
  name: 'Membresía 6 meses',
  shortName: '6 meses',
  price: 800,
  id: 289,
  productType: membershipTypeProduct,
  qty: 1,
  icon: <FaBookMedical />,
  monthsExpiration: 6,
}

const productOneYearMembership = {
  name: 'Membresía 12 meses',
  shortName: '1 año',
  price: 1450,
  id: 290,
  productType: membershipTypeProduct,
  qty: 1,
  icon: <FaBookMedical />,
  monthsExpiration: 12,
}

const productMedicalOrientation = {
  name: 'Orientación Médica',
  shortName: 'Médica',
  price: productOrientationPrice,
  id: 294,
  productType: orientationTypeProduct,
  qty: 1,
  icon: <FaBriefcaseMedical />,
  monthsExpiration: 0,
}

const productPsychologicalOrientation = {
  name: 'Orientación Psicológica',
  shortName: 'Psicológica',
  price: productOrientationPrice,
  id: 295,
  productType: orientationTypeProduct,
  qty: 1,
  icon: <FaCommentMedical />,
  monthsExpiration: 0,
}

const productNutritionalOrientation = {
  name: 'Orientación Nutricional',
  shortName: 'Nutricional',
  price: productOrientationPrice,
  id: 296,
  productType: orientationTypeProduct,
  qty: 1,
  icon: <FaNotesMedical />,
  monthsExpiration: 0,
}

export {
  productSixMonthMembership,
  productOneYearMembership,
  productOrientationPrice,
  productMedicalOrientation,
  productPsychologicalOrientation,
  productNutritionalOrientation,
}
