import React, { useCallback, useEffect, useState } from 'react'
import { DataGrid } from '@material-ui/data-grid'
import Title from '../Title'
import productsApi from '../../Api/Products/productsApi'
import { ProductListItemContract } from '../../Api/Products/contracts'
import './Products.css'

const columns = [
	{ field: 'name', headerName: 'Name', width: 200 },
	{ field: 'label', headerName: 'Label', width: 200 },
	{ field: 'unitPrice', type: 'number', headerName: 'Unit Price', width: 200 },
	{ field: 'discountPerUnit', type: 'number', headerName: 'Discount Per Unit', width: 200 },
	{ field: 'isSet', type: 'boolean', headerName: 'Is Set', width: 200 },
]

const Products = () => {
	const [products, setProducts] = useState([] as ProductListItemContract[])

	const getProducts = useCallback(async () => {
		const response = await productsApi.GetProductList()
		setProducts(response.items)
	}, [setProducts])

	useEffect(() => {
		getProducts()
	}, [getProducts])

	return (
		<React.Fragment>
			<Title>Products</Title>
			<div style={{ height: 700, width: '100%' }}>
				<DataGrid rows={products} columns={columns} pageSize={10} />
			</div>
		</React.Fragment>
	)
}

export default Products
