import { MoneyContract } from '../contracts'

export interface SupplyListContract {
	items: SupplyListItemContract[]
	rowsCount: number
}

export interface SupplyListItemContract {
	id: string
	date: string
	supplierName: string
	totalCost: number
}

export interface SupplierInfoContract {
	name: string
}

export interface SupplyItemContract {
	id: string
	productId: string
	unitPrice: number
	quantity: number
	discountPerUnit: number
}

export interface SupplyInfoContract {
	id: string
	version: number
	supplierInfo: SupplierInfoContract
	supplyItems: SupplyItemContract[]
}

export interface SupplyContract {
	id: string
	version: number
	date: string
	supplierInfo: SupplierInfoContract
	supplyItems: SupplyItemContract[]
	totalCost: MoneyContract
}
