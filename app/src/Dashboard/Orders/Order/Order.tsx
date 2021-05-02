import { Box, Button, FormControl, Grid, Input, InputLabel, MenuItem, Select } from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Title from '../../Title'
import { OrderContract, OrderInfoContract, OrderItemContract, OrderStatuses } from '../../../Api/Orders/contracts'
import ordersApi from '../../../Api/Orders/ordersApi'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import ResponsiveDialog from '../../../Dialogs/ResponsiveDialog'
import Counter from '../../../Counter/Counter'
import { formIsValid, FormValidation, initialFormValidation, initialOrder } from '../utils'
import { Alert } from '@material-ui/lab'
import { useTranslation } from 'react-i18next'
import OrderItems from '../OrderItems/OrderItems'

interface IOwnProps {
	orderId: string
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
	removeButton: {
		marginTop: theme.spacing(3),
		marginRight: 'auto',
		backgroundColor: '#e53935',
		'&:hover': {
			background: '#d32f2f',
		},
	},
	productItem: {
		paddingRight: theme.spacing(3),
	},
}))

const Order = ({ orderId, products, hide }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const setProducts = products
		.filter(x => x.isSet)
		.sort((a, b) => a.name.toLowerCase().localeCompare(b.name.toLowerCase()))
	const simpleProducts = products
		.filter(x => !x.isSet)
		.sort((a, b) => a.name.toLowerCase().localeCompare(b.name.toLowerCase()))

	const [orderInfo, setOrderInfo] = useState(initialOrder as OrderInfoContract)
	const [orderItems, setOrderItems] = useState([] as OrderItemContract[])
	const [orderDate, setOrderDate] = useState(null as Date | null)
	const [showDeleteDialog, setShowDeleteDialog] = useState(false)

	const [formValidation, setFormValidation] = useState(initialFormValidation(true) as FormValidation)
	const [errorText, setErrorText] = useState('')
	const [totalCost, setTotalCost] = useState(0)

	const backHandler = useCallback(() => hide(), [hide])

	const getOrder = useCallback(async () => {
		const response = await ordersApi.Get(orderId)
		setOrderInfo({ ...response } as OrderInfoContract)
		setOrderItems(response.orderItems)
		setOrderDate(new Date(response.date))
	}, [orderId, ordersApi, setOrderItems, setOrderInfo])

	const orderStatusChangeHandler = useCallback(
		(e: any) => {
			const orderStatus = e.target.value
			setOrderInfo({ ...orderInfo, status: orderStatus })
		},
		[orderInfo, setOrderInfo],
	)

	const phoneNumberChangeHandler = useCallback(
		(e: any) => {
			const phoneNumber = e.target.value
			setOrderInfo({ ...orderInfo, customerInfo: { ...orderInfo.customerInfo, phoneNumber } })
			setFormValidation({
				...formValidation,
				customerInfoValid: { ...formValidation.customerInfoValid, phoneNumberValid: phoneNumber },
			})
		},
		[orderInfo, setOrderInfo, formValidation, setFormValidation],
	)

	const customerNameChangeHandler = useCallback(
		(e: any) => {
			const customerName = e.target.value
			if (customerName) {
				setOrderInfo({ ...orderInfo, customerInfo: { ...orderInfo.customerInfo, name: customerName } })
			}
		},
		[orderInfo, setOrderInfo],
	)

	const cityChangeHandler = useCallback(
		(e: any) => {
			const city = e.target.value
			setOrderInfo({ ...orderInfo, address: { ...orderInfo.address, city } })
			setFormValidation({ ...formValidation, addressValid: { ...formValidation.addressValid, cityValid: city } })
		},
		[orderInfo, setOrderInfo, formValidation, setFormValidation],
	)

	const streetChangeHandler = useCallback(
		(e: any) => {
			const street = e.target.value
			setOrderInfo({ ...orderInfo, address: { ...orderInfo.address, street } })
			setFormValidation({
				...formValidation,
				addressValid: { ...formValidation.addressValid, streetValid: street },
			})
		},
		[orderInfo, setOrderInfo, formValidation, setFormValidation],
	)

	const zipCodeChangeHandler = useCallback(
		(e: any) => {
			const zipCode = e.target.value
			setOrderInfo({ ...orderInfo, address: { ...orderInfo.address, zipCode } })
			setFormValidation({
				...formValidation,
				addressValid: { ...formValidation.addressValid, zipCodeValid: zipCode },
			})
		},
		[orderInfo, setOrderInfo, formValidation, setFormValidation],
	)

	const deliveryCostChangeHandler = useCallback(
		(e: any) => {
			const deliveryCost = Number(e.target.value)
			if (deliveryCost < 0) {
				return
			}
			setOrderInfo({ ...orderInfo, deliveryCost: { ...orderInfo.deliveryCost, value: deliveryCost } })
			setFormValidation({ ...formValidation, deliveryCostValid: deliveryCost >= 0 })
		},
		[orderInfo, formValidation, setFormValidation],
	)

	const setOrderAddedCost = useCallback(
		(addedCost: number) => {
			setOrderInfo({ ...orderInfo, addedCost: { ...orderInfo.addedCost, value: addedCost } })
		},
		[orderInfo, setOrderInfo],
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

	const saveOrderInfo = useCallback(async () => {
		if (formIsValid(formValidation)) {
			await ordersApi.Update(orderId, { ...orderInfo, orderItems, version: orderInfo.version + 1 })
			hide()
		} else {
			setErrorText(t('FormIsInvalid'))
		}
	}, [formIsValid, formValidation, orderId, orderInfo, orderItems, ordersApi, hide, setErrorText, t])

	const deleteOrder = useCallback(async () => {
		await ordersApi.Delete(orderId)
		hide()
	}, [ordersApi, orderId])

	useEffect(() => {
		getOrder()
	}, [getOrder])

	useEffect(() => {
		const orderItemsCost = orderItems.reduce(
			(acc, cur) => (acc += cur.unitPrice * (1 - cur.discountPerUnit) * cur.quantity),
			0,
		)
		setTotalCost(orderItemsCost + orderInfo.deliveryCost.value + orderInfo.addedCost.value)
	}, [orderItems, orderInfo.deliveryCost, orderInfo.addedCost])

	useEffect(() => {
		if (formIsValid(formValidation)) {
			setErrorText('')
		}
	}, [formValidation, formIsValid, setErrorText])

	return (
		<React.Fragment>
			<Box pt={2}>
				<Title color="primary">{t('EditOrder')}</Title>
			</Box>

			{orderInfo && orderDate && (
				<Grid container spacing={3}>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="date">{t('Date')}</InputLabel>
							<Input id="date" value={orderDate.toLocaleString()} readOnly />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="status">{t('Status')}</InputLabel>
							<Select
								labelId="status"
								id="status-select"
								value={orderInfo.status || ''}
								onChange={orderStatusChangeHandler}
							>
								{OrderStatuses.map(x => (
									<MenuItem key={x} value={x}>
										{t(`${x}OrderStatus`)}
									</MenuItem>
								))}
							</Select>
						</FormControl>
					</Grid>
					<Box pt={2} pl={2}>
						<Title color="secondary">{t('CustomerInfo')}</Title>
					</Box>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.customerInfoValid.phoneNumberValid} fullWidth>
							<InputLabel htmlFor="phoneNumber">{t('PhoneNumber')}</InputLabel>
							<Input
								id="phoneNumber"
								value={orderInfo.customerInfo.phoneNumber}
								onChange={phoneNumberChangeHandler}
							/>
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="customerName">{t('CustomerName')}</InputLabel>
							<Input
								id="customerName"
								value={orderInfo.customerInfo.name}
								onChange={customerNameChangeHandler}
							/>
						</FormControl>
					</Grid>
					<Box pt={2} pl={2}>
						<Title color="secondary">{t('ShippingAddress')}</Title>
					</Box>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.addressValid.cityValid} fullWidth>
							<InputLabel htmlFor="city">{t('City')}</InputLabel>
							<Input id="city" value={orderInfo.address.city} onChange={cityChangeHandler} />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.addressValid.streetValid} fullWidth>
							<InputLabel htmlFor="street">{t('Street')}</InputLabel>
							<Input id="street" value={orderInfo.address.street} onChange={streetChangeHandler} />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.addressValid.zipCodeValid} fullWidth>
							<InputLabel htmlFor="zipCode">{t('ZipCode')}</InputLabel>
							<Input id="zipCode" value={orderInfo.address.zipCode} onChange={zipCodeChangeHandler} />
						</FormControl>
					</Grid>
					<Box pt={2} pl={2}>
						<Title color="secondary">{t('AdditionalCosts')}</Title>
					</Box>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.deliveryCostValid} fullWidth>
							<InputLabel htmlFor="unitPrice">{t('DeliveryCost')}</InputLabel>
							<Input
								required
								type="number"
								id="unitPrice"
								value={orderInfo.deliveryCost.value}
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
								value={orderInfo.addedCost.value}
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
			)}
			<div className={classes.buttons}>
				<Button variant="contained" onClick={() => setShowDeleteDialog(true)} className={classes.removeButton}>
					{t('Remove')}
				</Button>
				<Button onClick={backHandler} className={classes.button}>
					{t('Back')}
				</Button>
				<Button variant="contained" color="primary" onClick={saveOrderInfo} className={classes.button}>
					{t('Save')}
				</Button>
			</div>
			{showDeleteDialog && (
				<ResponsiveDialog
					title={t('RemovingOrderConfirmation')
						.replace('${customerName}', orderInfo.customerInfo.name ?? '')
						.replace('${customerPhoneNumber}', orderInfo.customerInfo.phoneNumber)}
					content={t('ThisChangeCannotBeUndone')}
					cancelText={t('Cancel')}
					okText={t('Ok')}
					cancel={() => setShowDeleteDialog(false)}
					confirm={deleteOrder}
				/>
			)}
		</React.Fragment>
	)
}

export default Order
