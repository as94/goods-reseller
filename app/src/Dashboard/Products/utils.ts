import { ProductInfoContract } from '../../Api/Products/contracts'

export interface FormValidation {
	labelValid: boolean
	nameValid: boolean
	unitPriceValid: boolean
	discountPerUnitValid: boolean
}

export const initialFormValidation = (formIsValid: boolean) =>
	({
		labelValid: formIsValid,
		nameValid: formIsValid,
		unitPriceValid: true,
		discountPerUnitValid: true,
	} as FormValidation)

export const formIsValid = (formValidation: FormValidation) =>
	formValidation.labelValid &&
	formValidation.nameValid &&
	formValidation.unitPriceValid &&
	formValidation.discountPerUnitValid

export const initialProduct = {
	label: '',
	name: '',
	description: '',
	unitPrice: 0,
	discountPerUnit: 0,
	productIds: [],
} as ProductInfoContract
