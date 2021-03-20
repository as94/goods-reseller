import {
	Box,
	Button,
	FormControl,
	Grid,
	Input,
	InputLabel,
	List,
	ListItem,
	ListItemText,
	MenuItem,
	Select,
} from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Title from '../../Title'
import { Operation, OrderContract, OrderInfoContract, OrderStatuses } from '../../../Api/Orders/contracts'
import ordersApi from '../../../Api/Orders/ordersApi'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import ResponsiveDialog from '../../../Dialogs/ResponsiveDialog'
import Counter from '../../../Counter/Counter'
import { formIsValid, FormValidation, initialFormValidation, initialOrder } from '../utils'
import { Alert } from '@material-ui/lab'

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
	const classes = useStyles()

	const [orderInfo, setOrderInfo] = useState(initialOrder as OrderInfoContract)
	const [formValidation, setFormValidation] = useState(initialFormValidation(true) as FormValidation)
	const [errorText, setErrorText] = useState('')

	const [allProducts, setAllProducts] = useState(
		products.sort((a, b) => {
			if (a.isSet && !b.isSet) return -1
			if (!a.isSet && b.isSet) return 1
			return 0
		}),
	)
	const [selectedProductId, setSelectedProductId] = useState('')
	const [order, setOrder] = useState({ status: OrderStatuses[0] } as OrderContract)
	const [showDeleteDialog, setShowDeleteDialog] = useState(false)

	const backHandler = useCallback(() => hide(), [hide])

	const getOrder = useCallback(async () => {
		const response = await ordersApi.Get(orderId)
		setOrder(response)
		setOrderInfo({ ...response } as OrderInfoContract)
	}, [orderId, ordersApi, setOrder, setOrderInfo])

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
			setOrderInfo({ ...orderInfo, deliveryCost: { ...order.deliveryCost, value: deliveryCost } })
			setFormValidation({ ...formValidation, deliveryCostValid: deliveryCost >= 0 })
		},
		[orderInfo, setOrder, formValidation, setFormValidation],
	)

	const saveOrderInfo = useCallback(async () => {
		if (formIsValid(formValidation)) {
			await ordersApi.Update(order.id, orderInfo)
			hide()
		} else {
			setErrorText('Form is invalid')
		}
	}, [formIsValid, formValidation, order, orderInfo, ordersApi, hide, setErrorText])

	const deleteOrder = useCallback(async () => {
		await ordersApi.Delete(orderId)
		hide()
	}, [ordersApi, orderId])

	const addOrderItem = useCallback(
		async (productId: string) => {
			if (productId) {
				await ordersApi.PatchOrderItem(order.id, {
					productId,
					op: Operation.Add,
				})
				await getOrder()
			}
		},
		[order, ordersApi, getOrder],
	)

	const removeOrderItem = useCallback(
		async (productId: string) => {
			if (productId) {
				await ordersApi.PatchOrderItem(order.id, {
					productId,
					op: Operation.Remove,
				})
				await getOrder()
			}
		},
		[order, ordersApi, getOrder],
	)

	const addSelectedProduct = useCallback(() => {
		addOrderItem(selectedProductId)
		setSelectedProductId('')
	}, [addOrderItem, selectedProductId, setSelectedProductId])

	const changeSelectedProductId = useCallback(
		(event: any) => {
			const id = event.target.value
			setSelectedProductId(id)
		},
		[setSelectedProductId],
	)

	useEffect(() => {
		getOrder()
	}, [getOrder])

	useEffect(() => {
		if (order && order.orderItems) {
			setAllProducts(products.filter(x => !order.orderItems.map(y => y.productId).includes(x.id)))
		}
	}, [order, products, setAllProducts])

	useEffect(() => {
		if (formIsValid(formValidation)) {
			setErrorText('')
		}
	}, [formValidation, formIsValid, setErrorText])

	return (
		<React.Fragment>
			<Box pt={2}>
				<Title color="primary">Edit order</Title>
			</Box>

			{order && order.id && (
				<Grid container spacing={3}>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="date">Date</InputLabel>
							<Input id="date" value={new Date(order.date).toLocaleString()} readOnly />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="status">Order status</InputLabel>
							<Select
								labelId="status"
								id="status-select"
								value={orderInfo.status || ''}
								onChange={orderStatusChangeHandler}
							>
								{OrderStatuses.map(x => (
									<MenuItem key={x} value={x}>
										{x}
									</MenuItem>
								))}
							</Select>
						</FormControl>
					</Grid>
					<Box pt={2} pl={2}>
						<Title color="secondary">Customer Info</Title>
					</Box>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.customerInfoValid.phoneNumberValid} fullWidth>
							<InputLabel htmlFor="phoneNumber">Phone number</InputLabel>
							<Input
								id="phoneNumber"
								value={orderInfo.customerInfo.phoneNumber}
								onChange={phoneNumberChangeHandler}
							/>
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="customerName">Customer name (optional)</InputLabel>
							<Input
								id="customerName"
								value={orderInfo.customerInfo.name}
								onChange={customerNameChangeHandler}
							/>
						</FormControl>
					</Grid>
					<Box pt={2} pl={2}>
						<Title color="secondary">Shipping address</Title>
					</Box>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.addressValid.cityValid} fullWidth>
							<InputLabel htmlFor="city">City</InputLabel>
							<Input id="city" value={orderInfo.address.city} onChange={cityChangeHandler} />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.addressValid.streetValid} fullWidth>
							<InputLabel htmlFor="street">Street</InputLabel>
							<Input id="street" value={orderInfo.address.street} onChange={streetChangeHandler} />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.addressValid.zipCodeValid} fullWidth>
							<InputLabel htmlFor="zipCode">Zip code</InputLabel>
							<Input id="zipCode" value={orderInfo.address.zipCode} onChange={zipCodeChangeHandler} />
						</FormControl>
					</Grid>
					<Box pt={2} pl={2}>
						<Title color="secondary">Delivery</Title>
					</Box>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.deliveryCostValid} fullWidth>
							<InputLabel htmlFor="unitPrice">Delivery cost</InputLabel>
							<Input
								required
								type="number"
								id="unitPrice"
								value={orderInfo.deliveryCost.value}
								onChange={deliveryCostChangeHandler}
							/>
						</FormControl>
					</Grid>
					{errorText && (
						<Grid item xs={12} md={12}>
							<Alert severity="error">{errorText}</Alert>
						</Grid>
					)}
					<Box pt={2} pl={2}>
						<Title color="secondary">Order items</Title>
						<FormControl fullWidth>
							<InputLabel htmlFor="totalCost">Total cost</InputLabel>
							<Input id="totalCost" value={order.totalCost.value} readOnly />
						</FormControl>
						{allProducts.length > 0 && (
							<FormControl fullWidth>
								<InputLabel htmlFor="products">Products</InputLabel>
								<Select
									labelId="products"
									id="products-select"
									value={selectedProductId}
									onChange={changeSelectedProductId}
								>
									{allProducts.map(x => (
										<MenuItem key={x.id} value={x.id}>
											{x.name} - {x.unitPrice} ({x.discountPerUnit * 100} %)
										</MenuItem>
									))}
								</Select>
								<Button
									variant="contained"
									color="primary"
									onClick={addSelectedProduct}
									className={classes.addProductButton}
									disabled={!selectedProductId}
								>
									Add product
								</Button>
							</FormControl>
						)}
						{order && order.orderItems && (
							<List>
								{order.orderItems.map((x, idx) => {
									const product = products.find(p => p.id === x.productId)
									return product ? (
										<ListItem key={x.productId}>
											<ListItemText
												primary={`${idx + 1}. ${product.name} - ${x.unitPrice.value} (${
													x.discountPerUnit * 100
												} %)`}
												className={classes.productItem}
											/>
											<Counter
												initialValue={x.quantity}
												addHandler={() => addOrderItem(x.productId)}
												removeHandler={() => removeOrderItem(x.productId)}
											/>
										</ListItem>
									) : null
								})}
							</List>
						)}
					</Box>
				</Grid>
			)}
			<div className={classes.buttons}>
				<Button variant="contained" onClick={() => setShowDeleteDialog(true)} className={classes.removeButton}>
					Remove
				</Button>
				<Button onClick={backHandler} className={classes.button}>
					Back
				</Button>
				<Button variant="contained" color="primary" onClick={saveOrderInfo} className={classes.button}>
					Save
				</Button>
			</div>
			{showDeleteDialog && (
				<ResponsiveDialog
					title={`Order for customer ${order.customerInfo.name} with phone number ${order.customerInfo.phoneNumber} will be removed. Continue?`}
					content={'This change cannot be undone'}
					cancel={() => setShowDeleteDialog(false)}
					confirm={deleteOrder}
				/>
			)}
		</React.Fragment>
	)
}

export default Order
