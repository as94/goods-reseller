import api from '../api'
import { SupplyListContract } from './contracts'

export default {
	GetSupplyList: async (): Promise<SupplyListContract> => {
		const response = await api.get('/supplies')

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as SupplyListContract
	},
}
