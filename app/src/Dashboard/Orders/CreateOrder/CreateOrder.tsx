import { Box, Button, FormControl, Grid, Input, InputLabel } from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Title from '../../Title'
import { Alert } from '@material-ui/lab'
import { formIsValid, FormValidation, initialFormValidation, initialOrder } from '../utils'
import { OrderInfoContract, OrderItemContract, OrderStatuses } from '../../../Api/Orders/contracts'
import ordersApi from '../../../Api/Orders/ordersApi'
import { useTranslation } from 'react-i18next'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import OrderItems from '../OrderItems/OrderItems'

interface IOwnProps {
	products: ProductListItemContract[]
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
	addProductButton: {
		marginTop: theme.spacing(1),
	},
	productItem: {
		paddingRight: theme.spacing(3),
	},
}))

const CreateOrder = ({ products, hide }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const setProducts = products
		.filter(x => x.isSet)
		.sort((a, b) => a.name.toLowerCase().localeCompare(b.name.toLowerCase()))
	const simpleProducts = products
		.filter(x => !x.isSet)
		.sort((a, b) => a.name.toLowerCase().localeCompare(b.name.toLowerCase()))

	const [order, setOrder] = useState(initialOrder as OrderInfoContract)
	const [orderItems, setOrderItems] = useState([] as OrderItemContract[])
	const [formValidation, setFormValidation] = useState(initialFormValidation(false) as FormValidation)
	const [totalCost, setTotalCost] = useState(0)
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
			if (deliveryCost < 0) {
				return
			}
			setOrder({ ...order, deliveryCost: { ...order.deliveryCost, value: deliveryCost } })
			setFormValidation({ ...formValidation, deliveryCostValid: deliveryCost >= 0 })
		},
		[order, setOrder, formValidation, setFormValidation],
	)

	const setOrderAddedCost = useCallback(
		(addedCost: number) => {
			setOrder({ ...order, addedCost: { ...order.addedCost, value: addedCost } })
		},
		[order, setOrder],
	)

	const addedCostChangeHandler = useCallback(
		(e: any) => {
			const addedCost = Number(e.target.value)
			if (addedCost < 0) {
				return
			}
			setOrderAddedCost(addedCost)
			setFormValidation({ ...formValidation, addedCostValid: addedCost >= 0 })
		},
		[setOrderAddedCost, formValidation, setFormValidation],
	)

	const createOrder = useCallback(async () => {
		if (formIsValid(formValidation)) {
			await ordersApi.Create({ ...order, orderItems, status: OrderStatuses[0], version: 1 })
			hide()
		} else {
			setErrorText(t('FormIsInvalid'))
		}
	}, [formIsValid, formValidation, order, orderItems, ordersApi, hide, setErrorText, t])

	useEffect(() => {
		const orderItemsCost = orderItems.reduce(
			(acc, cur) => (acc += cur.unitPrice * (1 - cur.discountPerUnit) * cur.quantity),
			0,
		)
		setTotalCost(orderItemsCost + order.deliveryCost.value + order.addedCost.value)
	}, [orderItems, order.deliveryCost, order.addedCost])

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
				<Box pt={2} pl={2}>
					<Title color="secondary">{t('AdditionalCosts')}</Title>
				</Box>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.deliveryCostValid} fullWidth>
						<InputLabel htmlFor="deliveryCost">{t('DeliveryCost')}</InputLabel>
						<Input
							required
							type="number"
							id="deliveryCost"
							value={order.deliveryCost.value}
							onChange={deliveryCostChangeHandler}
						/>
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.addedCostValid} fullWidth>
						<InputLabel htmlFor="addedCost">{t('AddedCost')}</InputLabel>
						<Input
							required
							type="number"
							id="addedCost"
							value={order.addedCost.value}
							onChange={addedCostChangeHandler}
						/>
					</FormControl>
				</Grid>
				<OrderItems
					setProducts={setProducts}
					simpleProducts={simpleProducts}
					orderItems={orderItems}
					setOrderItems={setOrderItems}
					setOrderAddedCost={setOrderAddedCost}
				/>
				<Grid item xs={12} md={12}>
					<FormControl fullWidth>
						<InputLabel htmlFor="totalCost">{t('TotalCost')}</InputLabel>
						<Input type="number" id="totalCost" value={totalCost} readOnly />
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
