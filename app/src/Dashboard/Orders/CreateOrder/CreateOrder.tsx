import { Box, Button, FormControl, Grid, Input, InputLabel } from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Title from '../../Title'
import { Alert } from '@material-ui/lab'
import { formIsValid, FormValidation, initialFormValidation, initialOrder } from '../utils'
import { CreateOrderContract } from '../../../Api/Orders/contracts'
import ordersApi from '../../../Api/Orders/ordersApi'
import { useTranslation } from 'react-i18next'

interface IOwnProps {
	hide: () => void
}

const useStyles = makeStyles(theme => ({
	buttons: {
		display: 'flex',
		justifyContent: 'flex-end',
	},
	button: {
		marginTop: theme.spacing(3),
		marginLeft: theme.spacing(1),
	},
}))

const CreateOrder = ({ hide }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const [order, setOrder] = useState(initialOrder as CreateOrderContract)
	const [formValidation, setFormValidation] = useState(initialFormValidation(false) as FormValidation)
	const [errorText, setErrorText] = useState('')

	const backHandler = useCallback(() => hide(), [hide])

	const phoneNumberChangeHandler = useCallback(
		(e: any) => {
			const phoneNumber = e.target.value
			setOrder({ ...order, customerInfo: { ...order.customerInfo, phoneNumber } })
			setFormValidation({
				...formValidation,
				customerInfoValid: { ...formValidation.customerInfoValid, phoneNumberValid: phoneNumber },
			})
		},
		[order, setOrder, formValidation, setFormValidation],
	)

	const customerNameChangeHandler = useCallback(
		(e: any) => {
			const customerName = e.target.value
			setOrder({ ...order, customerInfo: { ...order.customerInfo, name: customerName } })
		},
		[order, setOrder],
	)

	const cityChangeHandler = useCallback(
		(e: any) => {
			const city = e.target.value
			setOrder({ ...order, address: { ...order.address, city } })
			setFormValidation({ ...formValidation, addressValid: { ...formValidation.addressValid, cityValid: city } })
		},
		[order, setOrder, formValidation, setFormValidation],
	)

	const streetChangeHandler = useCallback(
		(e: any) => {
			const street = e.target.value
			setOrder({ ...order, address: { ...order.address, street } })
			setFormValidation({
				...formValidation,
				addressValid: { ...formValidation.addressValid, streetValid: street },
			})
		},
		[order, setOrder, formValidation, setFormValidation],
	)

	const zipCodeChangeHandler = useCallback(
		(e: any) => {
			const zipCode = e.target.value
			setOrder({ ...order, address: { ...order.address, zipCode } })
			setFormValidation({
				...formValidation,
				addressValid: { ...formValidation.addressValid, zipCodeValid: zipCode },
			})
		},
		[order, setOrder, formValidation, setFormValidation],
	)

	const deliveryCostChangeHandler = useCallback(
		(e: any) => {
			const deliveryCost = Number(e.target.value)
			setOrder({ ...order, deliveryCost: { ...order.deliveryCost, value: deliveryCost } })
			setFormValidation({ ...formValidation, deliveryCostValid: deliveryCost >= 0 })
		},
		[order, setOrder, formValidation, setFormValidation],
	)

	const createOrder = useCallback(async () => {
		if (formIsValid(formValidation)) {
			await ordersApi.Create({ ...order })
			hide()
		} else {
			setErrorText(t('FormIsInvalid'))
		}
	}, [formIsValid, formValidation, order, ordersApi, hide, setErrorText, t])

	useEffect(() => {
		if (formIsValid(formValidation)) {
			setErrorText('')
		}
	}, [formValidation, formIsValid, setErrorText])

	return (
		<React.Fragment>
			<Box pt={2}>
				<Title color="primary">{t('CreateOrder')}</Title>
			</Box>

			<Grid container spacing={3}>
				<Box pt={2} pl={2}>
					<Title color="secondary">{t('CustomerInfo')}</Title>
				</Box>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.customerInfoValid.phoneNumberValid} fullWidth>
						<InputLabel htmlFor="phoneNumber">{t('PhoneNumber')}</InputLabel>
						<Input
							id="phoneNumber"
							value={order.customerInfo.phoneNumber}
							onChange={phoneNumberChangeHandler}
						/>
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<FormControl fullWidth>
						<InputLabel htmlFor="customerName">{t('CustomerName')}</InputLabel>
						<Input id="customerName" value={order.customerInfo.name} onChange={customerNameChangeHandler} />
					</FormControl>
				</Grid>
				<Box pt={2} pl={2}>
					<Title color="secondary">{t('ShippingAddress')}</Title>
				</Box>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.addressValid.cityValid} fullWidth>
						<InputLabel htmlFor="city">{t('City')}</InputLabel>
						<Input id="city" value={order.address.city} onChange={cityChangeHandler} />
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.addressValid.streetValid} fullWidth>
						<InputLabel htmlFor="street">{t('Street')}</InputLabel>
						<Input id="street" value={order.address.street} onChange={streetChangeHandler} />
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.addressValid.zipCodeValid} fullWidth>
						<InputLabel htmlFor="zipCode">{t('ZipCode')}</InputLabel>
						<Input id="zipCode" value={order.address.zipCode} onChange={zipCodeChangeHandler} />
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.deliveryCostValid} fullWidth>
						<InputLabel htmlFor="unitPrice">{t('DeliveryCost')}</InputLabel>
						<Input
							required
							type="number"
							id="unitPrice"
							value={order.deliveryCost.value}
							onChange={deliveryCostChangeHandler}
						/>
					</FormControl>
				</Grid>
				{errorText && (
					<Grid item xs={12} md={12}>
						<Alert severity="error">{errorText}</Alert>
					</Grid>
				)}
			</Grid>
			<div className={classes.buttons}>
				<Button onClick={backHandler} className={classes.button}>
					{t('Back')}
				</Button>
				<Button variant="contained" color="primary" onClick={createOrder} className={classes.button}>
					{t('Save')}
				</Button>
			</div>
		</React.Fragment>
	)
}

export default CreateOrder
