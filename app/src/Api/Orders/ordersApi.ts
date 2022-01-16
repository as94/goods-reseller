import api from '../api'
import { OrderInfoContract, OrderContract, OrderListContract } from './contracts'

export default {
	Get: async (orderId: string): Promise<OrderContract> => {
		const response = await api.get(`/orders/${orderId}`)

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as OrderContract
	},

	GetOrderList: async (offset: number, count: number): Promise<OrderListContract> => {
		const response = await api.get(`/orders?offset=${offset}&count=${count}`)

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as OrderListContract
	},

	Create: async (order: OrderInfoContract): Promise<void> => {
		const response = await api.post('/public/orders', order)

		if (response.status !== 200) {
			throw new Error()
		}
	},

	Update: async (orderId: string, orderInfo: OrderInfoContract): Promise<void> => {
		const response = await api.put(`/orders/${orderId}`, orderInfo)

		if (response.status !== 200) {
			throw new Error()
		}
	},

	Delete: async (orderId: string): Promise<void> => {
		const response = await api.delete(`/orders/${orderId}`)

		if (response.status !== 200) {
			throw new Error()
		}
	},
}
