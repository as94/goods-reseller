export interface ProductContract {
	id: string
	version: number
	date: string
	label: string
	name: string
	description: string
	unitPrice: number
	discountPerUnit: number
	productIds: string[]
}

export interface ProductListItemContract {
	id: string
	version: number
	date: string
	label: string
	name: string
	unitPrice: number
	discountPerUnit: number
	isSet: boolean
	productIds: string[]
}

export interface ProductListContract {
	items: ProductListItemContract[]
}

export interface ProductInfoContract {
	label: string
	name: string
	description: string
	unitPrice: number
	discountPerUnit: number
	productIds: string[]
}
