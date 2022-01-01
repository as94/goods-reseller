import { SupplierInfoContract, SupplyInfoContract } from '../../Api/Supplies/contracts'
import { v4 as uuid } from 'uuid'

export interface FormValidation {
	supplierNameValid: boolean
}

export const initialFormValidation = (formIsValid: boolean) =>
	({
		supplierNameValid: formIsValid,
	} as FormValidation)

export const formIsValid = (formValidation: FormValidation) => formValidation.supplierNameValid

export const initialSupplyInfo = {
	id: uuid(),
	version: 1,
	supplierInfo: {
		name: '',
	} as SupplierInfoContract,
	supplyItems: [],
} as SupplyInfoContract
