import api from '../api'
import { CreateOrderContract, OrderContract, OrderInfoContract, OrderListContract, PatchOrderItem } from './contracts'

export default {
	Get: async (orderId: string): Promise<OrderContract> => {
		const response = await api.get(`/orders/${orderId}`)

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as OrderContract
	},

	GetOrderList: async (): Promise<OrderListContract> => {
		const response = await api.get('/orders/list')

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as OrderListContract
	},

	Create: async (order: CreateOrderContract): Promise<void> => {
		const response = await api.post('/orders', order)

		if (response.status !== 200) {
			throw new Error()
		}
	},

	Update: async (orderId: string, orderInfo: OrderInfoContract): Promise<void> => {
		const response = await api.patch(`/orders/${orderId}/orderInfo`, orderInfo)

		if (response.status !== 200) {
			throw new Error()
		}
	},

	PatchOrderItem: async (orderId: string, patchOrderItem: PatchOrderItem): Promise<void> => {
		const response = await api.patch(`orders/${orderId}/orderItems`, patchOrderItem)

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
