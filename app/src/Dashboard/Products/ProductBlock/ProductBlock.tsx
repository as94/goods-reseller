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

const ProductBlock = () => {
	const classes = useStyles()
	const [products, setProducts] = useState([] as ProductListItemContract[])

	const [selectedProductId, setSelectedProductId] = useState(null as string | null)
	const [showCreateProduct, setShowCreateProduct] = useState(false)
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
							setProducts={setProducts}
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
