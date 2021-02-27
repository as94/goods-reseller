import React, { useCallback, useEffect } from 'react'
import { DataGrid, RowParams } from '@material-ui/data-grid'
import Title from '../../Title'
import { SupplyListItemContract } from '../../../Api/Supplies/contracts'
import { Button } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import './SupplyList.css'
import suppliesApi from '../../../Api/Supplies/suppliesApi'

const useStyles = makeStyles(theme => ({
	header: {
		display: 'flex',
		justifyContent: 'space-between',
		marginBottom: theme.spacing(2),
		marginLeft: theme.spacing(1),
	},
}))

const columns = [
	{ field: 'date', headerName: 'Date', width: 300 },
	{ field: 'supplierName', type: 'string', headerName: 'Supplier name', width: 300 },
	{ field: 'totalCost', type: 'number', headerName: 'Total Cost', width: 300 },
]

interface IOwnProps {
	supplies: SupplyListItemContract[]
	setSupplies: (supplies: SupplyListItemContract[]) => void
	setSelectedSupplyId: (selectedSupplyId: string) => void
	showCreateSupply: () => void
}

const SupplyList = ({ supplies, setSupplies, setSelectedSupplyId, showCreateSupply }: IOwnProps) => {
	const classes = useStyles()

	const getSupplies = useCallback(async () => {
		const response = await suppliesApi.GetSupplyList()
		setSupplies(response.items)
	}, [setSupplies])

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
		getSupplies()
	}, [getSupplies])

	return (
		<React.Fragment>
			<div className={classes.header}>
				<Title color="primary">Supplies</Title>
				<Button variant="contained" color="primary" onClick={showCreateSupplyHandler}>
					Create
				</Button>
			</div>
			<div style={{ height: 650, width: '100%' }}>
				<DataGrid
					disableColumnMenu={true}
					rows={supplies.map(x => ({ ...x, date: new Date(x.date).toLocaleString() }))}
					columns={columns}
					pageSize={10}
					onRowClick={supplyClickHandler}
				/>
			</div>
		</React.Fragment>
	)
}

export default SupplyList
