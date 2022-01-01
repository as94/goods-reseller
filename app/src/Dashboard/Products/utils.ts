import { ProductInfoContract } from '../../Api/Products/contracts'
import { v4 as uuid } from 'uuid'

export interface FormValidation {
	labelValid: boolean
	nameValid: boolean
	unitPriceValid: boolean
	discountPerUnitValid: boolean
	addedCostValid: boolean
}

export const initialFormValidation = (formIsValid: boolean) =>
	({
		labelValid: formIsValid,
		nameValid: formIsValid,
		unitPriceValid: true,
		discountPerUnitValid: true,
		addedCostValid: true,
	} as FormValidation)

export const formIsValid = (formValidation: FormValidation) =>
	formValidation.labelValid &&
	formValidation.nameValid &&
	formValidation.unitPriceValid &&
	formValidation.discountPerUnitValid &&
	formValidation.addedCostValid

export const initialProduct = {
	id: uuid(),
	version: 1,
	label: '',
	name: '',
	description: '',
	unitPrice: 0,
	discountPerUnit: 0,
	addedCost: 0,
	productIds: [],
} as ProductInfoContract
