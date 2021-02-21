import { Box, Button, FormControl, Grid, Input, InputLabel } from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Title from '../../Title'
import { Alert } from '@material-ui/lab'
import { formIsValid, FormValidation, initialFormValidation, initialOrder } from '../utils'
import { CreateOrderContract } from '../../../Api/Orders/contracts'
import ordersApi from '../../../Api/Orders/ordersApi'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import ResponsiveDialog from '../../../Dialogs/ResponsiveDialog'

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
	removeButton: {
		marginTop: theme.spacing(3),
		marginRight: 'auto',
		backgroundColor: '#e53935',
		'&:hover': {
			background: '#d32f2f',
		},
	},
}))

const Order = ({ orderId, products, hide }: IOwnProps) => {
	const classes = useStyles()

	const [order, setOrder] = useState(initialOrder as CreateOrderContract)
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

	useEffect(() => {
		getOrder()
	}, [getOrder])

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
			</Grid>
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
