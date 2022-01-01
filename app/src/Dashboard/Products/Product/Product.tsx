import { Box, Button, FormControl, Grid, Input, InputLabel } from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import { FileUpload, ProductContract, ProductListItemContract } from '../../../Api/Products/contracts'
import productsApi from '../../../Api/Products/productsApi'
import Title from '../../Title'
import { formIsValid, FormValidation, initialFormValidation } from '../utils'
import { Alert } from '@material-ui/lab'
import ResponsiveDialog from '../../../Dialogs/ResponsiveDialog'
import MultipleSelect from '../../../MultipleSelect/MultipleSelect'
import { useTranslation } from 'react-i18next'
import { version } from 'os'

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
	uploadBlock: {
		display: 'flex',
		flexDirection: 'column',
	},
	uploadFileName: {
		padding: '10px',
		paddingBottom: '0',
	},
	uploadImage: {
		paddingLeft: '10px',
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
	const [photoPath, setPhotoPath] = useState('')
	const [fileUpload, setFileUpload] = useState({ fileName: '' } as FileUpload)
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

	const addedCostChangeHandler = useCallback(
		(e: any) => {
			const addedCost = Number(e.target.value)
			setProduct({ ...product, addedCost })
			setFormValidation({ ...formValidation, unitPriceValid: addedCost >= 0 })
		},
		[product, setProduct, formValidation, setFormValidation],
	)

	const getProduct = useCallback(async () => {
		const response = await productsApi.GetProduct(productId)
		setProduct(response)
		setSelectedProductIds(response.productIds)
		setPhotoPath(response.photoPath)
	}, [setProduct, productId, setSelectedProductIds])

	const productPhotoChangeHandler = useCallback(
		(e: any) => {
			setFileUpload({
				fileName: e.target.files[0].name,
				fileContent: e.target.files[0],
			} as FileUpload)
		},
		[setFileUpload],
	)

	const updateProduct = useCallback(async () => {
		if (formIsValid(formValidation)) {
			await productsApi.Update(product.id, {
				...product,
				productIds: selectedProductIds,
				version: product.version + 1,
			})
			hide()
		} else {
			setErrorText(t('FormIsInvalid'))
		}
	}, [formIsValid, formValidation, product, selectedProductIds, productsApi, hide, setErrorText])

	const updatePhotoHandler = useCallback(async () => {
		if (fileUpload.fileName) {
			const formData = new FormData()
			formData.append('fileName', fileUpload.fileName)
			formData.append('fileContent', fileUpload.fileContent)
			const productPhoto = formData
			const result = await productsApi.UploadProductPhoto(product.id, product.version + 1, productPhoto)

			await getProduct()
		}
	}, [fileUpload, getProduct])

	const removeProductPhotoHandler = useCallback(async () => {
		await productsApi.RemoveProductPhoto(product.id, product.version + 1)

		setFileUpload({ fileName: '' } as FileUpload)

		await getProduct()
	}, [product, getProduct])

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
				<Title color="primary">{t('EditProduct')}</Title>
			</Box>
			{product && product.id && (
				<Grid container spacing={3}>
					<Grid item xs={12} md={12}>
						<FormControl fullWidth>
							<InputLabel htmlFor="date">{t('Date')}</InputLabel>
							<Input id="date" value={new Date(product.date).toLocaleString()} readOnly />
						</FormControl>
					</Grid>
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
					<Grid item xs={6}>
						<div className={classes.uploadBlock}>
							<input
								accept="image/*"
								style={{ display: 'none' }}
								id="raised-button-file"
								multiple
								type="file"
								onChange={productPhotoChangeHandler}
							/>
							<label htmlFor="raised-button-file">
								<Button
									variant="outlined"
									component="span"
									className={classes.button}
									disabled={Boolean(photoPath)}
								>
									{t('ChooseProductPhoto')}
								</Button>
								{photoPath && (
									<Button
										variant="outlined"
										className={classes.button}
										onClick={removeProductPhotoHandler}
										disabled={!photoPath}
									>
										{t('RemoveProductPhoto')}
									</Button>
								)}
								{fileUpload && fileUpload.fileName && (
									<Button
										variant="contained"
										color="primary"
										onClick={updatePhotoHandler}
										className={classes.button}
										disabled={Boolean(photoPath)}
									>
										{t('Save')}
									</Button>
								)}
							</label>

							<span className={classes.uploadFileName}>{fileUpload.fileName}</span>
							{photoPath && <img src={`assets/${photoPath}`} className={classes.uploadImage} />}
						</div>
					</Grid>
					<Grid item xs={12} md={12}>
						<MultipleSelect
							title={t('Products')}
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
					{t('Remove')}
				</Button>
				<Button onClick={backHandler} className={classes.button}>
					{t('Back')}
				</Button>
				<Button variant="contained" color="primary" onClick={updateProduct} className={classes.button}>
					{t('Save')}
				</Button>
			</div>
			{showDeleteDialog && (
				<ResponsiveDialog
					title={t('RemovingProductConfirmation').replace('${productName}', product.name)}
					content={t('ThisChangeCannotBeUndone')}
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
