import i18n from 'i18next'
import enJson from './localizedresources/en.json'
import ruJson from './localizedresources/ru.json'
import { initReactI18next } from 'react-i18next'

i18n.use(initReactI18next).init({
	lng: 'ru',
	fallbackLng: 'en',
	keySeparator: false,
	interpolation: {
		escapeValue: false,
		formatSeparator: ',',
	},
	resources: {
		en: enJson,
		ru: ruJson,
	},
})

export default i18n
