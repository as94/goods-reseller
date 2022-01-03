import React, { useCallback, useEffect, useState } from 'react'
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
	setSelectedOrderId: (selectedOrderId: string) => void
	showCreateOrder: () => void
}

const OrderList = ({ setSelectedOrderId, showCreateOrder }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const [rowsState, setRowsState] = React.useState({
		page: 1,
		pageSize: 10,
		rows: [] as OrderListItemContract[],
		rowCount: 0,
		loading: false,
	})

	const columns = [
		{ field: 'date', headerName: t('Date'), width: 200 },
		{ field: 'status', type: 'string', headerName: t('Status'), width: 150 },
		{ field: 'customerPhoneNumber', headerName: t('Phone'), width: 200 },
		{ field: 'customerName', headerName: t('CustomerName'), width: 200 },
		{ field: 'addressZipCode', headerName: t('ZipCode'), width: 200 },
		{ field: 'totalCost', type: 'number', headerName: t('OrderTotalCost'), width: 150 },
	]

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
		let active = true

		;(async () => {
			setRowsState(prev => ({ ...prev, loading: true }))
			const response = await ordersApi.GetOrderList((rowsState.page - 1) * rowsState.pageSize, rowsState.pageSize)

			if (!active) {
				return
			}

			const newRows = response.items.map(x => ({
				...x,
				date: new Date(x.date).toLocaleString(),
				status: t(`${x.status}OrderStatus`),
			}))

			setRowsState(prev => ({ ...prev, loading: false, rows: newRows, rowCount: response.rowsCount }))
		})()

		return () => {
			active = false
		}
	}, [rowsState.page, rowsState.pageSize])

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
					pagination
					paginationMode="server"
					columns={columns}
					{...rowsState}
					onPageChange={pageChangeParams => setRowsState(prev => ({ ...prev, page: pageChangeParams.page }))}
					onRowClick={orderClickHandler}
				/>
			</div>
		</React.Fragment>
	)
}

export default OrderList
