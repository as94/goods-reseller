import api from '../api'
import { ProductContract, ProductInfoContract, ProductListContract, UploadPhotoResult } from './contracts'

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

	Create: async (product: ProductInfoContract): Promise<void> => {
		let response = await api.post('/products', product)

		if (response.status !== 200) {
			throw new Error()
		}
	},

	Update: async (productId: string, product: ProductInfoContract): Promise<void> => {
		const response = await api.put(`/products/${productId}`, product)

		if (response.status !== 200) {
			throw new Error()
		}
	},

	Delete: async (productId: string): Promise<void> => {
		const response = await api.delete(`/products/${productId}`)

		if (response.status !== 200) {
			throw new Error()
		}
	},

	UploadProductPhoto: async (
		productId: string,
		newVersion: number,
		productPhoto: any,
	): Promise<UploadPhotoResult> => {
		const response = await api.post(`/products/${productId}/versions/${newVersion}/photos`, productPhoto)
		if (response.status !== 200) {
			throw new Error()
		}
		return response.data as UploadPhotoResult
	},

	RemoveProductPhoto: async (productId: string, newVersion: number): Promise<void> => {
		const response = await api.delete(`/products/${productId}/versions/${newVersion}/photos`)

		if (response.status !== 200) {
			throw new Error()
		}
	},
}
