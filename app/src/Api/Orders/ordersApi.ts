import api from '../api'
import { OrderListContract } from './contracts'

export default {
	GetOrderList: async (): Promise<OrderListContract> => {
		const response = await api.get('/orders/list')

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as OrderListContract
	},
}
