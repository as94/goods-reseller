import React, { useCallback, useEffect } from 'react'
import { DataGrid, RowParams } from '@material-ui/data-grid'
import Title from '../../Title'
import { SupplyListItemContract } from '../../../Api/Supplies/contracts'
import { Button } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import './SupplyList.css'
import suppliesApi from '../../../Api/Supplies/suppliesApi'
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
	setSelectedSupplyId: (selectedSupplyId: string) => void
	showCreateSupply: () => void
}

const SupplyList = ({ setSelectedSupplyId, showCreateSupply }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const [rowsState, setRowsState] = React.useState({
		page: 1,
		pageSize: 10,
		rows: [] as SupplyListItemContract[],
		rowCount: 0,
		loading: false,
	})

	const columns = [
		{ field: 'date', headerName: t('Date'), width: 300 },
		{ field: 'supplierName', type: 'string', headerName: t('SupplierName'), width: 300 },
		{ field: 'totalCost', type: 'number', headerName: t('SupplyTotalCost'), width: 300 },
	]

	const supplyClickHandler = useCallback(
		(param: RowParams) => {
			setSelectedSupplyId(param.row.id.toString())
		},
		[setSelectedSupplyId],
	)

	const showCreateSupplyHandler = useCallback(() => {
		showCreateSupply()
	}, [showCreateSupply])

	useEffect(() => {
		let active = true

		;(async () => {
			setRowsState(prev => ({ ...prev, loading: true }))
			const response = await suppliesApi.GetSupplyList(
				(rowsState.page - 1) * rowsState.pageSize,
				rowsState.pageSize,
			)

			if (!active) {
				return
			}

			const newRows = response.items.map(x => ({ ...x, date: new Date(x.date).toLocaleString() }))

			setRowsState(prev => ({ ...prev, loading: false, rows: newRows, rowCount: response.rowsCount }))
		})()

		return () => {
			active = false
		}
	}, [rowsState.page, rowsState.pageSize])

	return (
		<React.Fragment>
			<div className={classes.header}>
				<Title color="primary">{t('Supplies')}</Title>
				<Button variant="contained" color="primary" onClick={showCreateSupplyHandler}>
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
					onRowClick={supplyClickHandler}
				/>
			</div>
		</React.Fragment>
	)
}

export default SupplyList
