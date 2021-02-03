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
