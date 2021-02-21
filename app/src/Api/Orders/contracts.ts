export interface OrderListContract {
	items: OrderListItemContract[]
}

export interface OrderListItemContract {
	id: string
	version: number
	orderStatus: number
	customerPhoneNumber: string
	customerName: string
	addressCity: string
	addressStreet: string
	addressZipCode: string
	totalCost: number
}

export interface OrderContract {
	id: string
	version: string
	address: AddressContract
	customerInfo: CustomerInfoContract
	orderItems: OrderItemContract[]
	totalCost: MoneyContract
}

export interface CreateOrderContract {
	address: AddressContract
	customerInfo: CustomerInfoContract
}

export enum Operation {
	Add = 'add',
	Remove = 'remove',
}

export interface PatchOrderItem {
	orderId: string
	op: Operation
	productId: string
}

export interface AddressContract {
	city: string
	street: string
	zipCode: string

	houseNumber?: string
	apartmentNumber?: string
	entranceNumber?: string
	floor?: string
	intercom?: string
}

export interface CustomerInfoContract {
	phoneNumber: string
	name?: string
}

export interface OrderItemContract {
	id: string
	productId: string
	unitPrice: MoneyContract
	quantity: number
	discountPerUnit: number
}

export interface MoneyContract {
	value: number
}
