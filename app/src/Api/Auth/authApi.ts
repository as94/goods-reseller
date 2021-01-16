import api from '../api'
import { LoginUserContract } from './contracts'

export default {
	Login: async (loginUser: LoginUserContract): Promise<void> => {
		var response = await api.post('/auth/login', loginUser)

		if (response.status !== 200) {
			throw new Error()
		}
	},

	Logout: async (): Promise<void> => {
		var response = await api.post('/auth/logout')

		if (response.status !== 200) {
			throw new Error()
		}
	},
}
