import { Box, Button, FormControl, FormHelperText, Grid, Input, InputLabel, TextField } from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import { ProductInfoContract } from '../../../Api/Products/contracts'
import productsApi from '../../../Api/Products/productsApi'
import Title from '../../Title'
import { Alert } from '@material-ui/lab'

interface IOwnProps {
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
}))

const initialProduct = {
	label: '',
	name: '',
	description: '',
	unitPrice: 0,
	discountPerUnit: 0,
	productIds: [],
} as ProductInfoContract

interface FormValidation {
	labelValid: boolean
	nameValid: boolean
	unitPriceValid: boolean
	discountPerUnitValid: boolean
}

const initialFormValidation = {
	labelValid: false,
	nameValid: false,
	unitPriceValid: false,
	discountPerUnitValid: false,
} as FormValidation

const formIsValid = (formValidation: FormValidation) =>
	formValidation.labelValid &&
	formValidation.nameValid &&
	formValidation.unitPriceValid &&
	formValidation.discountPerUnitValid

const CreateProduct = ({ hide }: IOwnProps) => {
	const classes = useStyles()

	const [product, setProduct] = useState(initialProduct as ProductInfoContract)
	const [formValidation, setFormValidation] = useState(initialFormValidation as FormValidation)
	const [errorText, setErrorText] = useState('')

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
			setFormValidation({ ...formValidation, discountPerUnitValid: discountPerUnit >= 0 })
		},
		[product, setProduct, formValidation, setFormValidation],
	)

	const createProduct = useCallback(async () => {
		if (formIsValid(formValidation)) {
			await productsApi.Create(product)
			hide()
		} else {
			setErrorText('Form is invalid')
		}
	}, [formIsValid, formValidation, product, productsApi, hide, setErrorText])

	useEffect(() => {
		if (formIsValid(formValidation)) {
			setErrorText('')
		}
	}, [formValidation, formIsValid, setErrorText])

	return (
		<React.Fragment>
			<Box pt={2}>
				<Title>Create product</Title>
			</Box>

			<Grid container spacing={3}>
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
				{errorText && (
					<Grid item xs={12} md={12}>
						<Alert severity="error">{errorText}</Alert>
					</Grid>
				)}
			</Grid>
			<div className={classes.buttons}>
				<Button onClick={backHandler} className={classes.button}>
					Back
				</Button>
				<Button variant="contained" color="primary" onClick={createProduct} className={classes.button}>
					Save
				</Button>
			</div>
		</React.Fragment>
	)
}

export default CreateProduct
