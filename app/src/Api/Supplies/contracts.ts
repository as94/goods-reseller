import { MoneyContract } from '../contracts'

export interface SupplyListContract {
	items: SupplyListItemContract[]
}

export interface SupplyListItemContract {
	id: string
	date: Date
	supplierName: string
	totalCost: number
}

export interface SupplierInfoContract {
	name: string
}

export interface SupplyItemContract {
	id: string
	productId: string
	unitPrice: MoneyContract
	quantity: number
	discountPerUnit: number
}

export interface SupplyInfoContract {
	supplierInfo: SupplierInfoContract
	supplyItems: SupplyItemContract[]
}

export interface SupplyContract {
	id: string
	date: Date
	supplierInfo: SupplierInfoContract
	supplyItems: SupplyItemContract[]
	totalCost: MoneyContract
}
