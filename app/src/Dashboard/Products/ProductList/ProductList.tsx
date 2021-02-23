import React, { useCallback } from 'react'
import { DataGrid, RowParams } from '@material-ui/data-grid'
import Title from '../../Title'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import { Button } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import './ProductList.css'

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
	{ field: 'name', headerName: 'Name', width: 200 },
	{ field: 'label', headerName: 'Label', width: 200 },
	{ field: 'unitPrice', type: 'number', headerName: 'Unit Price', width: 200 },
	{ field: 'discountPerUnit', type: 'number', headerName: 'Discount Per Unit %', width: 200 },
	{ field: 'isSet', type: 'boolean', headerName: 'Is Set', width: 200 },
]

interface IOwnProps {
	products: ProductListItemContract[]
	setSelectedProductId: (selectedProductId: string) => void
	showCreateProduct: () => void
}

const ProductList = ({ products, setSelectedProductId, showCreateProduct }: IOwnProps) => {
	const classes = useStyles()

	const productClickHandler = useCallback(
		(param: RowParams) => {
			setSelectedProductId(param.row.id.toString())
		},
		[setSelectedProductId],
	)

	const showCreateProductHandler = useCallback(() => showCreateProduct(), [showCreateProduct])

	return (
		<React.Fragment>
			<div className={classes.header}>
				<Title color="primary">Products</Title>
				<Button variant="contained" color="primary" onClick={showCreateProductHandler}>
					Create
				</Button>
			</div>
			<div style={{ height: 650, width: '100%' }}>
				<DataGrid
					disableColumnMenu={true}
					rows={products.map(x => ({
						...x,
						discountPerUnit: x.discountPerUnit * 100,
						date: new Date(x.date).toLocaleString(),
					}))}
					columns={columns}
					pageSize={10}
					onRowClick={productClickHandler}
				/>
			</div>
		</React.Fragment>
	)
}

export default ProductList
