import api from '../api'
import { ProductContract, ProductInfoContract, ProductListContract } from './contracts'

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

	Create: async (product: ProductInfoContract, productPhoto: any | null): Promise<void> => {
		let response = await api.post('/products', product)

		if (response.status !== 200) {
			throw new Error()
		}

		if (productPhoto !== null) {
			response = await api.post(`/products/${response.data.productId}/photos`, productPhoto)
			if (response.status !== 200) {
				throw new Error()
			}
		}
	},

	Update: async (productId: string, product: ProductInfoContract, productPhoto: any | null): Promise<void> => {
		let response = await api.put(`/products/${productId}`, product)

		if (response.status !== 200) {
			throw new Error()
		}

		if (productPhoto !== null) {
			response = await api.post(`/products/${productId}/photos`, productPhoto)
			if (response.status !== 200) {
				throw new Error()
			}
		}
	},

	Delete: async (productId: string): Promise<void> => {
		const response = await api.delete(`/products/${productId}`)

		if (response.status !== 200) {
			throw new Error()
		}
	},

	RemoveProductPhoto: async (productId: string) : Promise<void> => {
		const response = await api.delete(`/products/${productId}/photos`)

		if (response.status !== 200) {
			throw new Error()
		}
	}
}
