import api from '../api'
import { FinancialStatisticContract } from './contracts'

export default {
	GetStatisticByYear: async (year: number): Promise<FinancialStatisticContract> => {
		const response = await api.get(`/statistics?year=${year}`)

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as FinancialStatisticContract
	},

	GetStatisticByMonth: async (year: number, month: number): Promise<FinancialStatisticContract> => {
		const response = await api.get(`/statistics?year=${year}&month=${month}`)

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as FinancialStatisticContract
	},
}
