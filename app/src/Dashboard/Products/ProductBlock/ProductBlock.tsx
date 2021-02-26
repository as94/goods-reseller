import { Grid, Paper } from '@material-ui/core'
import React, { useCallback, useState } from 'react'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import CreateProduct from '../CreateProduct/CreateProduct'
import Product from '../Product/Product'
import ProductList from '../ProductList/ProductList'
import { makeStyles } from '@material-ui/core/styles'

const useStyles = makeStyles(theme => ({
	paper: {
		padding: theme.spacing(2),
		display: 'flex',
		overflow: 'auto',
		flexDirection: 'column',
	},
}))

export interface IOwnProps {
	products: ProductListItemContract[]
	showCreateProduct: boolean
	setShowCreateProduct: (showCreateProduct: boolean) => void
	selectedProductId: string | null
	setSelectedProductId: (selectedProductId: string | null) => void
}

const ProductBlock = ({
	products,
	showCreateProduct,
	setShowCreateProduct,
	selectedProductId,
	setSelectedProductId,
}: IOwnProps) => {
	const classes = useStyles()

	const productHideHandler = useCallback(() => setSelectedProductId(null), [setSelectedProductId])
	const createProductShowHandler = useCallback(() => setShowCreateProduct(true), [setShowCreateProduct])
	const createProductHideHandler = useCallback(() => setShowCreateProduct(false), [setShowCreateProduct])

	return (
		<>
			{!selectedProductId && !showCreateProduct && (
				<Grid item xs={12}>
					<Paper className={classes.paper}>
						<ProductList
							products={products}
							setSelectedProductId={setSelectedProductId}
							showCreateProduct={createProductShowHandler}
						/>
					</Paper>
				</Grid>
			)}

			{showCreateProduct && (
				<Grid item xs={12}>
					<Paper className={classes.paper}>
						<CreateProduct products={products} hide={createProductHideHandler} />
					</Paper>
				</Grid>
			)}

			{selectedProductId && (
				<Grid item xs={12}>
					{' '}
					<Paper className={classes.paper}>
						{' '}
						<Product products={products} productId={selectedProductId} hide={productHideHandler} />
					</Paper>
				</Grid>
			)}
		</>
	)
}

export default ProductBlock
