import { SupplierInfoContract, SupplyInfoContract } from '../../Api/Supplies/contracts'

export interface FormValidation {
	supplierNameValid: boolean
}

export const initialFormValidation = (formIsValid: boolean) =>
	({
		supplierNameValid: formIsValid,
	} as FormValidation)

export const formIsValid = (formValidation: FormValidation) => formValidation.supplierNameValid

export const initialSupplyInfo = {
	supplierInfo: {
		name: '',
	} as SupplierInfoContract,
	supplyItems: [],
} as SupplyInfoContract
