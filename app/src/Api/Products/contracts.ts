export interface ProductContract {
	Id: string
	Version: number
	Label: string
	Name: string
	Description: string
	UnitPrice: number
	DiscountPerUnit: number
	Products: ProductContract[]
}

export interface ProductListItemContract {
	Id: string
	Version: number
	Label: string
	Name: string
}

export interface ProductListContract {
	items: ProductListItemContract[]
}
