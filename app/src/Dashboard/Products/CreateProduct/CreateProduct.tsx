import { Box, Button, FormControl, Grid, Input, InputLabel } from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import { FileUpload, ProductInfoContract, ProductListItemContract } from '../../../Api/Products/contracts'
import productsApi from '../../../Api/Products/productsApi'
import Title from '../../Title'
import { Alert } from '@material-ui/lab'
import { formIsValid, FormValidation, initialFormValidation, initialProduct } from '../utils'
import MultipleSelect from '../../../MultipleSelect/MultipleSelect'
import { useTranslation } from 'react-i18next'

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
	uploadBlock: {
		display: 'flex',
		flexDirection: 'column',
	},
	uploadFileName: {
		padding: '10px',
		paddingBottom: '0',
	},
}))

const CreateProduct = ({ products, hide }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const [simpleProducts] = useState(products.filter(p => !p.isSet))
	const [product, setProduct] = useState(initialProduct as ProductInfoContract)
	const [selectedProductIds, setSelectedProductIds] = useState<string[]>([])
	const [fileUpload, setFileUpload] = useState({ fileName: '' } as FileUpload)
	const [formValidation, setFormValidation] = useState(initialFormValidation(false) as FormValidation)
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
			setFormValidation({ ...formValidation, discountPerUnitValid: discountPerUnit >= 0 && discountPerUnit <= 1 })
		},
		[product, setProduct, formValidation, setFormValidation],
	)

	const addedCostChangeHandler = useCallback(
		(e: any) => {
			const addedCost = Number(e.target.value)
			setProduct({ ...product, addedCost })
			setFormValidation({ ...formValidation, unitPriceValid: addedCost >= 0 })
		},
		[product, setProduct, formValidation, setFormValidation],
	)

	const createProduct = useCallback(async () => {
		if (formIsValid(formValidation)) {
			await productsApi.Create({ ...product, productIds: selectedProductIds })
			hide()
		} else {
			setErrorText(t('FormIsInvalid'))
		}
	}, [formIsValid, formValidation, product, selectedProductIds, productsApi, fileUpload, hide, setErrorText])

	useEffect(() => {
		if (formIsValid(formValidation)) {
			setErrorText('')
		}
	}, [formValidation, formIsValid, setErrorText])

	return (
		<React.Fragment>
			<Box pt={2}>
				<Title color="primary">{t('CreateProduct')}</Title>
			</Box>

			<Grid container spacing={3}>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.nameValid} fullWidth>
						<InputLabel htmlFor="name">{t('ProductName')}</InputLabel>
						<Input id="name" value={product.name} onChange={nameChangeHandler} />
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.labelValid} fullWidth>
						<InputLabel htmlFor="label">{t('Label')}</InputLabel>
						<Input id="label" value={product.label} onChange={labelChangeHandler} />
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<FormControl fullWidth>
						<InputLabel htmlFor="description">{t('Description')}</InputLabel>
						<Input id="description" value={product.description} onChange={descriptionChangeHandler} />
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<FormControl error={!formValidation.unitPriceValid} fullWidth>
						<InputLabel htmlFor="unitPrice">{t('UnitPrice')}</InputLabel>
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
						<InputLabel htmlFor="discountPerUnit">{t('DiscountPerUnit')}</InputLabel>
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
					<FormControl error={!formValidation.addedCostValid} fullWidth>
						<InputLabel htmlFor="addedCost">{t('AddedCost')}</InputLabel>
						<Input
							required
							type="number"
							id="addedCost"
							value={product.addedCost}
							onChange={addedCostChangeHandler}
						/>
					</FormControl>
				</Grid>
				<Grid item xs={12} md={12}>
					<MultipleSelect
						title={t('Products')}
						items={simpleProducts}
						selectedIds={selectedProductIds}
						setSelectedIds={setSelectedProductIds}
					/>
				</Grid>
				{errorText && (
					<Grid item xs={12} md={12}>
						<Alert severity="error">{errorText}</Alert>
					</Grid>
				)}
			</Grid>
			<div className={classes.buttons}>
				<Button onClick={backHandler} className={classes.button}>
					{t('Back')}
				</Button>
				<Button variant="contained" color="primary" onClick={createProduct} className={classes.button}>
					{t('Save')}
				</Button>
			</div>
		</React.Fragment>
	)
}

export default CreateProduct
