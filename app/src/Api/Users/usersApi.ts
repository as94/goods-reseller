import api from '../api'
import { UserContract } from './contracts'

export default {
	GetMyUserInfo: async (): Promise<UserContract> => {
		const response = await api.get('users/me')

		if (response.status !== 200) {
			throw new Error()
		}

		return response.data as UserContract
	},
}
