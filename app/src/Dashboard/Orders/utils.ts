import { OrderInfoContract } from '../../Api/Orders/contracts'

export interface AddressValid {
	cityValid: boolean
	streetValid: boolean
	zipCodeValid: boolean
}

export interface CustomerInfoValid {
	phoneNumberValid: boolean
}

export interface FormValidation {
	addressValid: AddressValid
	customerInfoValid: CustomerInfoValid
	deliveryCostValid: boolean
	addedCostValid: boolean
}

export const initialFormValidation = (formIsValid: boolean) =>
	({
		addressValid: addressIsValid(formIsValid),
		customerInfoValid: customerInfoIsValid(formIsValid),
		deliveryCostValid: true,
		addedCostValid: true,
	} as FormValidation)

export const formIsValid = (formValidation: FormValidation) =>
	formValidation.addressValid.cityValid &&
	formValidation.addressValid.streetValid &&
	formValidation.addressValid.zipCodeValid &&
	formValidation.customerInfoValid.phoneNumberValid &&
	formValidation.deliveryCostValid &&
	formValidation.addedCostValid

export const addressIsValid = (addressIsValid: boolean) =>
	({
		cityValid: addressIsValid,
		streetValid: addressIsValid,
		zipCodeValid: addressIsValid,
	} as AddressValid)

export const customerInfoIsValid = (customerInfoIsValid: boolean) =>
	({
		phoneNumberValid: customerInfoIsValid,
	} as CustomerInfoValid)

export const initialOrder = {
	address: {
		city: '',
		street: '',
		zipCode: '',
	},
	customerInfo: {
		phoneNumber: '',
	},
	deliveryCost: {
		value: 0,
	},
	addedCost: {
		value: 0
	}
} as OrderInfoContract
