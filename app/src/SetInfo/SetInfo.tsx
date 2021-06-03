import React from 'react'
import { useTranslation } from 'react-i18next'

const SetInfo = () => {
	const { t } = useTranslation()
	return <>{t('SetInfo')}</>
}

export default SetInfo
