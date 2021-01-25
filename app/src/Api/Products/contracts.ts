export interface ProductContract {
	id: string
	version: number
	label: string
	name: string
	description: string
	unitPrice: number
	discountPerUnit: number
	products: ProductContract[]
}

export interface ProductListItemContract {
	id: string
	version: number
	label: string
	name: string
	unitPrice: number
	discountPerUnit: number
	isSet: boolean
}

export interface ProductListContract {
	items: ProductListItemContract[]
}
