import api from '../api'
import { SupplyContract, SupplyInfoContract, SupplyListContract } from './contracts'

export default {
	GetSupply: async (supplyId: string): Promise<SupplyContract> => {
		const response = await api.get(`/supplies/${supplyId}`)

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as SupplyContract
	},

	GetSupplyList: async (offset: number, count: number): Promise<SupplyListContract> => {
		const response = await api.get(`/supplies?offset=${offset}&count=${count}`)

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as SupplyListContract
	},

	Create: async (supplyInfo: SupplyInfoContract): Promise<void> => {
		const response = await api.post('/supplies', supplyInfo)

		if (response.status !== 200) {
			throw new Error()
		}
	},

	Update: async (supplyId: string, supplyInfo: SupplyInfoContract): Promise<void> => {
		const response = await api.put(`/supplies/${supplyId}`, supplyInfo)

		if (response.status !== 200) {
			throw new Error()
		}
	},

	Delete: async (supplyId: string): Promise<void> => {
		const response = await api.delete(`/supplies/${supplyId}`)

		if (response.status !== 200) {
			throw new Error()
		}
	},
}
