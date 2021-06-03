import React from 'react'
import { useTranslation } from 'react-i18next'

const Checkout = () => {
	const { t } = useTranslation()
	return <>{t('Checkout')}</>
}

export default Checkout