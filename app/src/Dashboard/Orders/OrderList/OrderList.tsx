import React, { useCallback, useEffect } from 'react'
import { DataGrid, RowParams } from '@material-ui/data-grid'
import Title from '../../Title'
import { OrderListItemContract } from '../../../Api/Orders/contracts'
import { Button } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import './OrderList.css'
import ordersApi from '../../../Api/Orders/ordersApi'

const useStyles = makeStyles(theme => ({
	header: {
		display: 'flex',
		justifyContent: 'space-between',
		marginBottom: theme.spacing(2),
		marginLeft: theme.spacing(1),
	},
}))

const columns = [
	{ field: 'date', headerName: 'Date', width: 200 },
	{ field: 'status', type: 'string', headerName: 'Status', width: 150 },
	{ field: 'customerPhoneNumber', headerName: 'Phone', width: 200 },
	{ field: 'customerName', headerName: 'Name', width: 150 },
	{ field: 'addressCity', headerName: 'City', width: 150 },
	{ field: 'addressStreet', headerName: 'Street', width: 150 },
	{ field: 'addressZipCode', headerName: 'Zip Code', width: 150 },
	{ field: 'totalCost', type: 'number', headerName: 'Total Cost', width: 150 },
]

interface IOwnProps {
	orders: OrderListItemContract[]
	setOrders: (orders: OrderListItemContract[]) => void
	setSelectedOrderId: (selectedOrderId: string) => void
	showCreateOrder: () => void
}

const OrderList = ({ orders, setOrders, setSelectedOrderId, showCreateOrder }: IOwnProps) => {
	const classes = useStyles()

	const getOrders = useCallback(async () => {
		const response = await ordersApi.GetOrderList()
		setOrders(response.items)
	}, [setOrders])

	const orderClickHandler = useCallback(
		(param: RowParams) => {
			setSelectedOrderId(param.row.id.toString())
		},
		[setSelectedOrderId],
	)

	const showCreateOrderHandler = useCallback(() => {
		showCreateOrder()
	}, [showCreateOrder])

	useEffect(() => {
		getOrders()
	}, [getOrders])

	return (
		<React.Fragment>
			<div className={classes.header}>
				<Title color="primary">Orders</Title>
				<Button variant="contained" color="primary" onClick={showCreateOrderHandler}>
					Create
				</Button>
			</div>
			<div style={{ height: 650, width: '100%' }}>
				<DataGrid
					disableColumnMenu={true}
					rows={orders.map(x => ({ ...x, date: new Date(x.date).toLocaleString() }))}
					columns={columns}
					pageSize={10}
					onRowClick={orderClickHandler}
				/>
			</div>
		</React.Fragment>
	)
}

export default OrderList
