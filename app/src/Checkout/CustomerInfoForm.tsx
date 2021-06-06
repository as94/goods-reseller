import React, { useCallback, useEffect, useState } from 'react'
import Typography from '@material-ui/core/Typography'
import Grid from '@material-ui/core/Grid'
import TextField from '@material-ui/core/TextField'
import { CustomerInfoContract } from '../Api/Orders/contracts'
import { errorsList } from './Checkout'

interface IOwnProps {
	customerInfo: CustomerInfoContract
	setCustomerInfo: (customerInfo: CustomerInfoContract) => void
	errors: string[]
}

const CustomerInfoForm = ({ customerInfo, setCustomerInfo, errors }: IOwnProps) => {
	const phoneNumberIsInvalid = errors.includes(errorsList.phoneNumberIsRequiredError)

	const nameChangeHandler = useCallback(
		(e: any) => {
			setCustomerInfo({
				...customerInfo,
				name: e.target.value,
			})
		},
		[customerInfo, setCustomerInfo],
	)

	const phoneNumberChangeHandler = useCallback(
		(e: any) => {
			setCustomerInfo({
				...customerInfo,
				phoneNumber: e.target.value,
			})
		},
		[customerInfo, setCustomerInfo],
	)

	return (
		<>
			<Typography variant="h6" gutterBottom>
				Контактная информация
			</Typography>
			<Grid container spacing={3}>
				<Grid item xs={12} md={12}>
					<TextField
						id="name"
						label="Ваше имя"
						value={customerInfo.name}
						onChange={nameChangeHandler}
						fullWidth
						autoComplete="name"
					/>
				</Grid>
				<Grid item xs={12} md={12}>
					<TextField
						required
						id="phoneNumber"
						label="Ваш номер телефона"
						value={customerInfo.phoneNumber}
						onChange={phoneNumberChangeHandler}
						fullWidth
						autoComplete="phoneNumber"
						error={phoneNumberIsInvalid}
						helperText={phoneNumberIsInvalid ? errorsList.phoneNumberIsRequiredError : undefined}
					/>
				</Grid>
			</Grid>
		</>
	)
}

export default CustomerInfoForm
