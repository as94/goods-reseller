import { Box, Button, FormControl, FormHelperText, Grid, Input, InputLabel, TextField } from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import { ProductContract, ProductListItemContract } from '../../../Api/Products/contracts'
import productsApi from '../../../Api/Products/productsApi'
import Title from '../../Title'
import { formIsValid, FormValidation, initialFormValidation, initialProduct } from '../utils'
import { Alert } from '@material-ui/lab'
import ResponsiveDialog from '../../../Dialogs/ResponsiveDialog'
import MultipleSelect from '../../../MultipleSelect/MultipleSelect'
import { useTranslation } from 'react-i18next'

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

interface IOwnProps {
	products: ProductListItemContract[]
	productId: string | null
	hide: () => void
}

const Product = ({ products, productId, hide }: IOwnProps) => {
	if (!productId) {
		return null
	}

	const { t } = useTranslation()
	const classes = useStyles()

	const [simpleProducts] = useState(products.filter(p => !p.isSet && p.id !== productId))
	const [product, setProduct] = useState({} as ProductContract)
	const [selectedProductIds, setSelectedProductIds] = useState<string[]>([])
	const [formValidation, setFormValidation] = useState(initialFormValidation(true) as FormValidation)
	const [errorText, setErrorText] = useState('')

	const [showDeleteDialog, setShowDeleteDialog] = useState(false)

	const backHandler = useCallback(() => hide(), [hide])

	const labelChangeHandler = useCallback(
		(e: any) => {
			const label = e.target.value
			setProduct({ ...product, label })
			setFormValidation({ ...formValidation, labelValid: label })
		},
		[product, setProduct, formValidation, setFormValidation],
	)

	const nameChangeHandler = useCallback(
		(e: any) => {
			const name = e.target.value
			setProduct({ ...product, name })
			setFormValidation({ ...formValidation, nameValid: name })
		},
		[product, setProduct, formValidation, setFormValidation],
	)

	const descriptionChangeHandler = useCallback(
		(e: any) => {
			setProduct({ ...product, description: e.target.value })
		},
		[product, setProduct],
	)

	const unitPriceChangeHandler = useCallback(
		(e: any) => {
			const unitPrice = Number(e.target.value)
			setProduct({ ...product, unitPrice })
			setFormValidation({ ...formValidation, unitPriceValid: unitPrice >= 0 })
		},
		[product, setProduct, formValidation, setFormValidation],
	)

	const discountPerUnitChangeHandler = useCallback(
		(e: any) => {
			const discountPerUnit = Number(e.target.value)
			setProduct({ ...product, discountPerUnit })
			setFormValidation({ ...formValidation, discountPerUnitValid: discountPerUnit >= 0 && discountPerUnit <= 1 })
		},
		[product, setProduct, formValidation, setFormValidation],
	)

	const getProduct = useCallback(async () => {
		const response = await productsApi.GetProduct(productId)
		setProduct(response)
	}, [setProduct, productId])

	const updateProduct = useCallback(async () => {
		if (formIsValid(formValidation)) {
			await productsApi.Update(product.id, { ...product, productIds: selectedProductIds })
			hide()
		} else {
			setErrorText('Form is invalid')
		}
	}, [formIsValid, formValidation, product, selectedProductIds, productsApi, hide, setErrorText])

	const deleteProduct = useCallback(async () => {
		await productsApi.Delete(productId)
		hide()
	}, [productsApi, productId])

	useEffect(() => {
		getProduct()
	}, [getProduct])

	useEffect(() => {
		if (formIsValid(formValidation)) {
			setErrorText('')
		}
	}, [formValidation, formIsValid, setErrorText])

	return (
		<React.Fragment>
			<Box pt={2}>
				<Title color="primary">Edit product</Title>
			</Box>
			{product && product.id && (
				<Grid container spacing={3}>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="date">Date</InputLabel>
							<Input id="date" value={new Date(product.date).toLocaleString()} readOnly />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.nameValid} fullWidth>
							<InputLabel htmlFor="name">Name</InputLabel>
							<Input id="name" value={product.name} onChange={nameChangeHandler} />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.labelValid} fullWidth>
							<InputLabel htmlFor="label">Label</InputLabel>
							<Input id="label" value={product.label} onChange={labelChangeHandler} />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="description">Description</InputLabel>
							<Input id="description" value={product.description} onChange={descriptionChangeHandler} />
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.unitPriceValid} fullWidth>
							<InputLabel htmlFor="unitPrice">Unit Price</InputLabel>
							<Input
								required
								type="number"
								id="unitPrice"
								value={product.unitPrice}
								onChange={unitPriceChangeHandler}
							/>
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<FormControl error={!formValidation.discountPerUnitValid} fullWidth>
							<InputLabel htmlFor="discountPerUnit">Discount Per Unit</InputLabel>
							<Input
								required
								type="number"
								id="discountPerUnit"
								value={product.discountPerUnit}
								onChange={discountPerUnitChangeHandler}
							/>
						</FormControl>
					</Grid>
					<Grid item xs={12} md={12}>
						<MultipleSelect
							title={'Products'}
							items={simpleProducts}
							selectedIds={product.productIds}
							setSelectedIds={setSelectedProductIds}
						/>
					</Grid>
					{errorText && (
						<Grid item xs={12} md={12}>
							<Alert severity="error">{errorText}</Alert>
						</Grid>
					)}
				</Grid>
			)}
			<div className={classes.buttons}>
				<Button variant="contained" onClick={() => setShowDeleteDialog(true)} className={classes.removeButton}>
					Remove
				</Button>
				<Button onClick={backHandler} className={classes.button}>
					Back
				</Button>
				<Button variant="contained" color="primary" onClick={updateProduct} className={classes.button}>
					Save
				</Button>
			</div>
			{showDeleteDialog && (
				<ResponsiveDialog
					title={`'${product.name}' will be removed. Continue?`}
					content={'This change cannot be undone'}
					cancel={() => setShowDeleteDialog(false)}
					cancelText={t('Cancel')}
					okText={t('Ok')}
					confirm={deleteProduct}
				/>
			)}
		</React.Fragment>
	)
}

export default Product
