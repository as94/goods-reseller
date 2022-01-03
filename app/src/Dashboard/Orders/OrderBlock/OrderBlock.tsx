import { Grid, Paper } from '@material-ui/core'
import React, { useCallback, useState } from 'react'
import OrderList from '../OrderList/OrderList'
import { makeStyles } from '@material-ui/core/styles'
import CreateOrder from '../CreateOrder/CreateOrder'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import Order from '../Order/Order'

interface IOwnProps {
	products: ProductListItemContract[]
}

const useStyles = makeStyles(theme => ({
	paper: {
		padding: theme.spacing(2),
		display: 'flex',
		overflow: 'auto',
		flexDirection: 'column',
	},
}))

const OrderBlock = ({ products }: IOwnProps) => {
	const classes = useStyles()

	const [selectedOrderId, setSelectedOrderId] = useState(null as string | null)
	const [showCreateOrder, setShowCreateOrder] = useState(false)
	const orderHideHandler = useCallback(() => setSelectedOrderId(null), [setSelectedOrderId])
	const createOrderShowHandler = useCallback(() => setShowCreateOrder(true), [setShowCreateOrder])
	const createOrderHideHandler = useCallback(() => setShowCreateOrder(false), [setShowCreateOrder])

	return (
		<>
			{!selectedOrderId && !showCreateOrder && (
				<Grid item xs={12}>
					<Paper className={classes.paper}>
						<OrderList setSelectedOrderId={setSelectedOrderId} showCreateOrder={createOrderShowHandler} />
					</Paper>
				</Grid>
			)}
			{showCreateOrder && (
				<Grid item xs={12}>
					<Paper className={classes.paper}>
						<CreateOrder products={products} hide={createOrderHideHandler} />
					</Paper>
				</Grid>
			)}

			{selectedOrderId && (
				<Grid item xs={12}>
					{' '}
					<Paper className={classes.paper}>
						{' '}
						<Order orderId={selectedOrderId} products={products} hide={orderHideHandler} />
					</Paper>
				</Grid>
			)}
		</>
	)
}

export default OrderBlock
