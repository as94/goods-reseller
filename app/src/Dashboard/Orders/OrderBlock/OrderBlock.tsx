import { Grid, Paper } from '@material-ui/core'
import React, { useCallback, useState } from 'react'
import OrderList from '../OrderList/OrderList'
import { makeStyles } from '@material-ui/core/styles'
import { OrderListItemContract } from '../../../Api/Orders/contracts'

const useStyles = makeStyles(theme => ({
	paper: {
		padding: theme.spacing(2),
		display: 'flex',
		overflow: 'auto',
		flexDirection: 'column',
	},
}))

const OrderBlock = () => {
	const classes = useStyles()
	const [orders, setOrders] = useState([] as OrderListItemContract[])

	const [selectedOrderId, setSelectedOrderId] = useState(null as string | null)
	// const [showCreateOrder, setShowCreateOrder] = useState(false)
	// const orderHideHandler = useCallback(() => setSelectedOrderId(null), [setSelectedOrderId])
	// const createOrderShowHandler = useCallback(() => setShowCreateOrder(true), [setShowCreateOrder])
	// const createOrderHideHandler = useCallback(() => setShowCreateOrder(false), [setShowCreateOrder])

	// showCreateOrder
	return (
		<>
			{!selectedOrderId && (
				<Grid item xs={12}>
					<Paper className={classes.paper}>
						<OrderList
							orders={orders}
							setOrders={setOrders}
							setSelectedOrderId={setSelectedOrderId}
							showCreateOrder={() => null} //{createOrderShowHandler}
						/>
					</Paper>
				</Grid>
			)}
			{/* {showCreateOrder && (
				<Grid item xs={12}>
					<Paper className={classes.paper}>
						<CreateOrder orders={orders} hide={createOrderHideHandler} />
					</Paper>
				</Grid>
			)}
			{selectedOrderId && (
				<Grid item xs={12}>
					{' '}
					<Paper className={classes.paper}>
						{' '}
						<Order orders={orders} orderId={selectedOrderId} hide={orderHideHandler} />
					</Paper>
				</Grid>
			)} */}
		</>
	)
}

export default OrderBlock
