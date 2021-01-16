import axios from 'axios'

export default axios.create({
	withCredentials: true,
	baseURL: `${process.env.API_URL}/api`,
	responseType: 'json',
})
