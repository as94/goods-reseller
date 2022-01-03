export interface ProductContract {
	id: string
	version: number
	date: string
	label: string
	name: string
	description: string
	unitPrice: number
	discountPerUnit: number
	addedCost: number
	productIds: string[]
	photoPath: string
}

export interface ProductListItemContract {
	id: string
	version: number
	date: string
	label: string
	name: string
	description: string
	unitPrice: number
	discountPerUnit: number
	addedCost: number
	isSet: boolean
	productIds: string[]
	photoPath: string
}

export interface ProductListContract {
	items: ProductListItemContract[]
	rowsCount: number
}

export interface ProductInfoContract {
	id: string
	version: number
	label: string
	name: string
	description: string
	unitPrice: number
	discountPerUnit: number
	addedCost: number
	productIds: string[]
}

export interface FileUpload {
	fileName: string
	fileContent: any
}

export interface UploadPhotoResult {
	photoPath: string
}
