import React, { useCallback, useEffect, useState } from 'react'
import { Box, Button, FormControl, Grid, Input, InputLabel, MenuItem, Select } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import { formIsValid, FormValidation, initialFormValidation, initialSupplyInfo } from '../utils'
import { SupplyInfoContract } from '../../../Api/Supplies/contracts'
import suppliesApi from '../../../Api/Supplies/suppliesApi'
import { Alert } from '@material-ui/lab'
import Title from '../../Title'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import Counter from '../../../Counter/Counter'

interface IOwnProps {
	products: ProductListItemContract[]
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
	root: {
		flexGrow: 1,
	},
	addSupplyItemButton: {
		marginTop: theme.spacing(2),
	},
}))

const SupplyItem = ({ simpleProducts }: { simpleProducts: ProductListItemContract[] }) => {
	const classes = useStyles()

	const [selectedProductId, setSelectedProductId] = useState('')
	const [unitPrice, setUnitPrice] = useState(0)
	const [discountPerUnit, setDiscountPerUnit] = useState(0)
	const [count, setCount] = useState(0)

	const selectedProductIdChangeHandler = useCallback(
		(event: any) => {
			const id = event.target.value
			setSelectedProductId(id)
		},
		[setSelectedProductId],
	)

	const unitPriceChangeHandler = useCallback(
		(e: any) => {
			const x = Number(e.target.value)
			if (x >= 0) {
				setUnitPrice(x)
			}
		},
		[setUnitPrice],
	)

	const discountPerUnitChangeHandler = useCallback(
		(e: any) => {
			const x = Number(e.target.value)
			if (x >= 0 && x <= 1) {
				setDiscountPerUnit(x)
			}
		},
		[setDiscountPerUnit],
	)

	return (
		<Grid item xs={12} md={12} className={classes.root}>
			<Grid container spacing={3}>
				<Grid item xs={3}>
					<FormControl fullWidth>
						<InputLabel htmlFor="productId">Product</InputLabel>
						<Select
							labelId="productId"
							id="product-id-select"
							style={{ width: '100%' }}
							value={selectedProductId}
							onChange={selectedProductIdChangeHandler}
						>
							{simpleProducts.map(x => (
								<MenuItem key={x.id} value={x.id}>
									{x.name}
								</MenuItem>
							))}
						</Select>
					</FormControl>
				</Grid>
				<Grid item xs={3}>
					<FormControl fullWidth>
						<InputLabel htmlFor="unitPrice">Unit Price</InputLabel>
						<Input
							required
							type="number"
							id="unitPrice"
							value={unitPrice}
							onChange={unitPriceChangeHandler}
						/>
					</FormControl>
				</Grid>
				<Grid item xs={3}>
					<FormControl fullWidth>
						<InputLabel htmlFor="discountPerUnit">Discount Per Unit</InputLabel>
						<Input
							required
							type="number"
							id="discountPerUnit"
							value={discountPerUnit}
							onChange={discountPerUnitChangeHandler}
						/>
					</FormControl>
				</Grid>
				<Grid item xs={3} style={{ marginTop: '10px' }}>
					<Counter
						initialValue={count}
						addHandler={() => Promise.resolve(setCount(prev => prev + 1))}
						removeHandler={() => Promise.resolve(setCount(prev => prev - 1))}
					/>
				</Grid>
			</Grid>
			<Button
				variant="contained"
				color="primary"
				onClick={() => null}
				className={classes.addSupplyItemButton}
				disabled={!selectedProductId}
			>
				Add supply item
			</Button>
		</Grid>
	)
}

const CreateSupply = ({ products, hide }: IOwnProps) => {
	const classes = useStyles()

	const [supply, setSupply] = useState(initialSupplyInfo as SupplyInfoContract)

	const simpleProducts = products.sort((a, b) => {
		if (a.isSet && !b.isSet) return -1
		if (!a.isSet && b.isSet) return 1
		return 0
	})

	const [formValidation, setFormValidation] = useState(initialFormValidation(false) as FormValidation)
	const [errorText, setErrorText] = useState('')

	const backHandler = useCallback(() => hide(), [hide])

	const supplierNameChangeHandler = useCallback(
		(e: any) => {
			const supplierName = e.target.value
			setSupply({ ...supply, supplierInfo: { ...supply.supplierInfo, name: supplierName } })
			setFormValidation({ ...formValidation, supplierNameValid: supplierName })
		},
		[supply, setSupply, formValidation, setFormValidation],
	)

	const createSupply = useCallback(async () => {
		if (formIsValid(formValidation)) {
			await suppliesApi.Create(supply)
			hide()
		} else {
			setErrorText('Form is invalid')
		}
	}, [formIsValid, formValidation, supply, suppliesApi, hide, setErrorText])

	useEffect(() => {
		if (formIsValid(formValidation)) {
			setErrorText('')
		}
	}, [formValidation, formIsValid, setErrorText])

	return (
		<React.Fragment>
			<Box pt={2}>
				<Title color="primary">Create supply</Title>
			</Box>

			<Grid container spacing={3}>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.supplierNameValid} fullWidth>
						<InputLabel htmlFor="supplierName">Supplier name</InputLabel>
						<Input
							id="supplierName"
							value={supply.supplierInfo.name}
							onChange={supplierNameChangeHandler}
						/>
					</FormControl>
				</Grid>
				<Box pt={2} pl={2}>
					<Title color="secondary">Supply items</Title>
				</Box>
				<SupplyItem simpleProducts={simpleProducts} />

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
				<Button variant="contained" color="primary" onClick={createSupply} className={classes.button}>
					Save
				</Button>
			</div>
		</React.Fragment>
	)
}

export default CreateSupply
