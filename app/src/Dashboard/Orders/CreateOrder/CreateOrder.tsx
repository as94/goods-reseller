import { Box, Button, FormControl, Grid, Input, InputLabel } from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Title from '../../Title'
import { Alert } from '@material-ui/lab'
import { formIsValid, FormValidation, initialFormValidation, initialOrder } from '../utils'
import { CreateOrderContract } from '../../../Api/Orders/contracts'
import ordersApi from '../../../Api/Orders/ordersApi'

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

	const createOrder = useCallback(async () => {
		if (formIsValid(formValidation)) {
			await ordersApi.Create({ ...order })
			hide()
		} else {
			setErrorText('Form is invalid')
		}
	}, [formIsValid, formValidation, order, ordersApi, hide, setErrorText])

	useEffect(() => {
		if (formIsValid(formValidation)) {
			setErrorText('')
		}
	}, [formValidation, formIsValid, setErrorText])

	return (
		<React.Fragment>
			<Box pt={2}>
				<Title color="primary">Create order</Title>
			</Box>

			<Grid container spacing={3}>
				<Box pt={2} pl={2}>
					<Title color="secondary">Customer Info</Title>
				</Box>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.customerInfoValid.phoneNumberValid} fullWidth>
						<InputLabel htmlFor="phoneNumber">Phone number</InputLabel>
						<Input
							id="phoneNumber"
							value={order.customerInfo.phoneNumber}
							onChange={phoneNumberChangeHandler}
						/>
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<FormControl fullWidth>
						<InputLabel htmlFor="customerName">Customer name (optional)</InputLabel>
						<Input id="customerName" value={order.customerInfo.name} onChange={customerNameChangeHandler} />
					</FormControl>
				</Grid>
				<Box pt={2} pl={2}>
					<Title color="secondary">Shipping address</Title>
				</Box>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.addressValid.cityValid} fullWidth>
						<InputLabel htmlFor="city">City</InputLabel>
						<Input id="city" value={order.address.city} onChange={cityChangeHandler} />
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.addressValid.streetValid} fullWidth>
						<InputLabel htmlFor="street">Street</InputLabel>
						<Input id="street" value={order.address.street} onChange={streetChangeHandler} />
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.addressValid.zipCodeValid} fullWidth>
						<InputLabel htmlFor="zipCode">Zip code</InputLabel>
						<Input id="zipCode" value={order.address.zipCode} onChange={zipCodeChangeHandler} />
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
					Back
				</Button>
				<Button variant="contained" color="primary" onClick={createOrder} className={classes.button}>
					Save
				</Button>
			</div>
		</React.Fragment>
	)
}

export default CreateOrder
