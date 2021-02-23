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
import { Operation, OrderContract } from '../../../Api/Orders/contracts'
import ordersApi from '../../../Api/Orders/ordersApi'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import ResponsiveDialog from '../../../Dialogs/ResponsiveDialog'
import Counter from '../../../Counter/Counter'

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

	const [allProducts, setAllProducts] = useState(
		products.sort((a, b) => {
			if (a.isSet && !b.isSet) return -1
			if (!a.isSet && b.isSet) return 1
			return 0
		}),
	)
	const [selectedProductId, setSelectedProductId] = useState('')
	const [order, setOrder] = useState({} as OrderContract)
	const [showDeleteDialog, setShowDeleteDialog] = useState(false)

	const backHandler = useCallback(() => hide(), [hide])

	const getOrder = useCallback(async () => {
		const response = await ordersApi.Get(orderId)
		setOrder(response)
	}, [orderId, ordersApi, setOrder])

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

	return (
		<React.Fragment>
			<Box pt={2}>
				<Title color="primary">Create order</Title>
			</Box>

			{order && order.id && (
				<Grid container spacing={3}>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="date">Date</InputLabel>
							<Input id="date" value={new Date(order.date).toLocaleString()} readOnly />
						</FormControl>
					</Grid>
					<Box pt={2} pl={2}>
						<Title color="secondary">Customer Info</Title>
					</Box>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="phoneNumber">Phone number</InputLabel>
							<Input id="phoneNumber" value={order.customerInfo.phoneNumber} readOnly />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="customerName">Customer name (optional)</InputLabel>
							<Input id="customerName" value={order.customerInfo.name ?? ''} readOnly />
						</FormControl>
					</Grid>
					<Box pt={2} pl={2}>
						<Title color="secondary">Shipping address</Title>
					</Box>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="city">City</InputLabel>
							<Input id="city" value={order.address.city} readOnly />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="street">Street</InputLabel>
							<Input id="street" value={order.address.street} readOnly />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="zipCode">Zip code</InputLabel>
							<Input id="zipCode" value={order.address.zipCode} readOnly />
						</FormControl>
					</Grid>
					<Box pt={2} pl={2}>
						<Title color="secondary">Order items</Title>
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
