import { ProductListItemContract } from './contracts'
import productsApi from './productsApi'

export const getAllProducts = async (): Promise<ProductListItemContract[]> => {
	const pageSize = 1000
	let page = 1
	let productsCount = 0

	const response = await productsApi.GetProductList(0, pageSize)
	let products = response.items
	productsCount += pageSize

	while (productsCount < response.rowsCount) {
		const response = await productsApi.GetProductList(page * pageSize, pageSize)
		products = [...products, ...response.items]
		productsCount += pageSize
		page++
	}

	return products
}
