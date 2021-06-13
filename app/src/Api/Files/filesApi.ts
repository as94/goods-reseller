import api from '../api'

export default {
	Upload: async (formData: FormData): Promise<void> => {
		const response = await api.post('/files', formData)

		if (response.status !== 200) {
			throw new Error()
		}
	},
}
