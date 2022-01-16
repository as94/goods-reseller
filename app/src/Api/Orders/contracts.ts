export interface OrderListContract {
	items: OrderListItemContract[]
	rowsCount: number
}

export interface OrderListItemContract {
	id: string
	version: number
	status: string
	date: string
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
	version: number
	status: string
	date: string
	address: AddressContract
	customerInfo: CustomerInfoContract
	orderItems: OrderItemContract[]
	deliveryCost: number
	addedCost: number
	totalCost: number
}

export interface OrderInfoContract {
	id: string
	version: number
	status: string
	address: AddressContract
	customerInfo: CustomerInfoContract
	deliveryCost: number
	addedCost: number
	orderItems: OrderItemContract[]
}

export const OrderStatuses = ['Accepted', 'Packed', 'Shipped', 'Canceled'] as const
type OrderStatusTuple = typeof OrderStatuses
export type OrderStatus = OrderStatusTuple[number]

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
	unitPrice: number
	quantity: number
	discountPerUnit: number
}
