import React, { useCallback, useEffect, useState } from 'react'
import { Box, Button, FormControl, Grid, Input, InputLabel } from '@material-ui/core'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import { makeStyles } from '@material-ui/core/styles'
import { SupplyContract } from '../../../Api/Supplies/contracts'
import { formIsValid, FormValidation, initialFormValidation, initialSupplyInfo } from '../utils'
import suppliesApi from '../../../Api/Supplies/suppliesApi'
import { Alert } from '@material-ui/lab'
import Title from '../../Title'
import ResponsiveDialog from '../../../Dialogs/ResponsiveDialog'

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
	supplyId: string
	products: ProductListItemContract[]
	hide: () => void
}

const Supply = ({ supplyId, products, hide }: IOwnProps) => {
	if (!supplyId) {
		return null
	}

	const classes = useStyles()

	const [supply, setSupply] = useState(initialSupplyInfo as SupplyContract)
	const [formValidation, setFormValidation] = useState(initialFormValidation(false) as FormValidation)
	const [errorText, setErrorText] = useState('')

	const [showDeleteDialog, setShowDeleteDialog] = useState(false)

	const backHandler = useCallback(() => hide(), [hide])

	const supplierNameChangeHandler = useCallback(
		(e: any) => {
			const supplierName = e.target.value
			setSupply({ ...supply, supplierInfo: { ...supply.supplierInfo, name: supplierName } })
			setFormValidation({ ...formValidation, supplierNameValid: supplierName })
		},
		[supply, setSupply, formValidation, setFormValidation],
	)

	const getSupply = useCallback(async () => {
		const response = await suppliesApi.GetSupply(supplyId)
		setSupply(response)
	}, [setSupply, supplyId])

	const updateSupply = useCallback(async () => {
		if (formIsValid(formValidation)) {
			await suppliesApi.Update(supply.id, supply)
			hide()
		} else {
			setErrorText('Form is invalid')
		}
	}, [formIsValid, formValidation, supply, suppliesApi, hide, setErrorText])

	const deleteProduct = useCallback(async () => {
		await suppliesApi.Delete(supplyId)
		hide()
	}, [suppliesApi, supplyId])

	useEffect(() => {
		getSupply()
	}, [getSupply])

	useEffect(() => {
		if (formIsValid(formValidation)) {
			setErrorText('')
		}
	}, [formValidation, formIsValid, setErrorText])

	return (
		<React.Fragment>
			<Box pt={2}>
				<Title color="primary">Edit supply</Title>
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

				{errorText && (
					<Grid item xs={12} md={12}>
						<Alert severity="error">{errorText}</Alert>
					</Grid>
				)}
			</Grid>
			<div className={classes.buttons}>
				<Button variant="contained" onClick={() => setShowDeleteDialog(true)} className={classes.removeButton}>
					Remove
				</Button>
				<Button onClick={backHandler} className={classes.button}>
					Back
				</Button>
				<Button variant="contained" color="primary" onClick={updateSupply} className={classes.button}>
					Save
				</Button>
			</div>
			{showDeleteDialog && (
				<ResponsiveDialog
					title={`Supply from '${supply.supplierInfo.name}' will be removed. Continue?`}
					content={'This change cannot be undone'}
					cancel={() => setShowDeleteDialog(false)}
					confirm={deleteProduct}
				/>
			)}
		</React.Fragment>
	)
}

export default Supply
