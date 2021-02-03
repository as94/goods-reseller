import React, { useCallback, useEffect } from 'react'
import { DataGrid, RowParams } from '@material-ui/data-grid'
import Title from '../../Title'
import productsApi from '../../../Api/Products/productsApi'
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
	{ field: 'name', headerName: 'Name', width: 200 },
	{ field: 'label', headerName: 'Label', width: 200 },
	{ field: 'unitPrice', type: 'number', headerName: 'Unit Price', width: 200 },
	{ field: 'discountPerUnit', type: 'number', headerName: 'Discount Per Unit', width: 200 },
	{ field: 'isSet', type: 'boolean', headerName: 'Is Set', width: 200 },
]

interface IOwnProps {
	products: ProductListItemContract[]
	setProducts: (products: ProductListItemContract[]) => void
	setSelectedProductId: (selectedProductId: string) => void
	showCreateProduct: () => void
}

const ProductList = ({ products, setProducts, setSelectedProductId, showCreateProduct }: IOwnProps) => {
	const classes = useStyles()

	const getProducts = useCallback(async () => {
		const response = await productsApi.GetProductList()
		setProducts(response.items)
	}, [setProducts])

	const productClickHandler = useCallback(
		(param: RowParams) => {
			setSelectedProductId(param.row.id.toString())
		},
		[setSelectedProductId],
	)

	const showCreateProductHandler = useCallback(() => showCreateProduct(), [showCreateProduct])

	useEffect(() => {
		getProducts()
	}, [getProducts])

	return (
		<React.Fragment>
			<div className={classes.header}>
				<Title>Products</Title>
				<Button variant="contained" color="primary" onClick={showCreateProductHandler}>
					Create
				</Button>
			</div>
			<div style={{ height: 650, width: '100%' }}>
				<DataGrid rows={products} columns={columns} pageSize={10} onRowClick={productClickHandler} />
			</div>
		</React.Fragment>
	)
}

export default ProductList
