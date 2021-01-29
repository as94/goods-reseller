import { Box, Button, FormControl, FormHelperText, Grid, Input, InputLabel, TextField } from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import { ProductContract } from '../../../Api/Products/contracts'
import productsApi from '../../../Api/Products/productsApi'
import Title from '../../Title'

interface IOwnProps {
	productId: string | null
	hide: () => void
}

const useStyles = makeStyles(theme => ({
	buttons: {
		display: 'flex',
		justifyContent: 'flex-end',
	},
	button: {
		marginTop: theme.spacing(3),
		marginLeft: theme.spacing(1),
	},
	removeButton: {
		marginTop: theme.spacing(3),
		marginRight: 'auto',
		backgroundColor: '#e53935',
		'&:hover': {
			background: '#d32f2f',
		},
	},
}))

const Product = ({ productId, hide }: IOwnProps) => {
	if (!productId) {
		return null
	}

	const classes = useStyles()

	const [product, setProduct] = useState(null as ProductContract | null)

	const getProduct = useCallback(async () => {
		const response = await productsApi.GetProduct(productId)
		setProduct(response)
	}, [setProduct, productId])

	useEffect(() => {
		getProduct()
	}, [getProduct])

	const backHandler = useCallback(() => hide(), [hide])

	return (
		product && (
			<React.Fragment>
				<Box pt={2}>
					<Title>Edit product</Title>
				</Box>

				<Grid container spacing={3}>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="name">Name</InputLabel>
							<Input id="name" value={product.name} />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="label">Label</InputLabel>
							<Input id="label" value={product.label} />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="description">Description</InputLabel>
							<Input id="description" value={product.description} />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="unitPrice">Unit Price</InputLabel>
							<Input required type="number" id="unitPrice" value={product.unitPrice} />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="discountPerUnit">Discount Per Unit</InputLabel>
							<Input required type="number" id="discountPerUnit" value={product.discountPerUnit} />
						</FormControl>
					</Grid>
				</Grid>
				<div className={classes.buttons}>
					<Button variant="contained" onClick={() => null} className={classes.removeButton}>
						Remove
					</Button>
					<Button onClick={backHandler} className={classes.button}>
						Back
					</Button>
					<Button variant="contained" color="primary" onClick={() => null} className={classes.button}>
						Save
					</Button>
				</div>
			</React.Fragment>
		)
	)
}

export default Product
