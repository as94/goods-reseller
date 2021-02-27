export interface SupplyListContract {
	items: SupplyListItemContract[]
}

export interface SupplyListItemContract {
	id: string
	date: Date
	supplierName: string
	totalCost: number
}
