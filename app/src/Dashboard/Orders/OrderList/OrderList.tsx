import React, { useCallback, useEffect } from 'react'
import { DataGrid, RowParams } from '@material-ui/data-grid'
import Title from '../../Title'
import { OrderListItemContract } from '../../../Api/Orders/contracts'
import { Button } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import './OrderList.css'
import ordersApi from '../../../Api/Orders/ordersApi'
import { useTranslation } from 'react-i18next'

const useStyles = makeStyles(theme => ({
	header: {
		display: 'flex',
		justifyContent: 'space-between',
		marginBottom: theme.spacing(2),
		marginLeft: theme.spacing(1),
	},
}))

interface IOwnProps {
	orders: OrderListItemContract[]
	setOrders: (orders: OrderListItemContract[]) => void
	setSelectedOrderId: (selectedOrderId: string) => void
	showCreateOrder: () => void
}

const OrderList = ({ orders, setOrders, setSelectedOrderId, showCreateOrder }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const columns = [
		{ field: 'date', headerName: t('Date'), width: 200 },
		{ field: 'status', type: 'string', headerName: t('Status'), width: 150 },
		{ field: 'customerPhoneNumber', headerName: t('Phone'), width: 200 },
		{ field: 'customerName', headerName: t('CustomerName'), width: 200 },
		{ field: 'addressZipCode', headerName: t('ZipCode'), width: 200 },
		{ field: 'totalCost', type: 'number', headerName: t('OrderTotalCost'), width: 150 },
	]

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
				<Title color="primary">{t('Orders')}</Title>
				<Button variant="contained" color="primary" onClick={showCreateOrderHandler}>
					{t('Create')}
				</Button>
			</div>
			<div style={{ height: 650, width: '100%' }}>
				<DataGrid
					disableColumnMenu={true}
					rows={orders.map(x => ({
						...x,
						date: new Date(x.date).toLocaleString(),
						status: t(`${x.status}OrderStatus`),
					}))}
					columns={columns}
					pageSize={10}
					onRowClick={orderClickHandler}
				/>
			</div>
		</React.Fragment>
	)
}

export default OrderList
