import api from '../api'
import { ProductContract, ProductListContract } from './contracts'

export default {
	GetProduct: async (productId: string): Promise<ProductContract> => {
		const response = await api.get(`/products/${productId}`)

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as ProductContract
	},

	GetProductList: async (): Promise<ProductListContract> => {
		const response = await api.get('/products/list')

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as ProductListContract
	},
}
