import React, { useCallback, useEffect, useState } from 'react'
import { Box, Button, FormControl, Grid, Input, InputLabel, MenuItem, Select } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import { formIsValid, FormValidation, initialFormValidation, initialSupplyInfo } from '../utils'
import { SupplyInfoContract, SupplyItemContract } from '../../../Api/Supplies/contracts'
import suppliesApi from '../../../Api/Supplies/suppliesApi'
import { Alert } from '@material-ui/lab'
import Title from '../../Title'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import SupplyItems from '../SupplyItems/SupplyItems'

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
}))

const CreateSupply = ({ products, hide }: IOwnProps) => {
	const classes = useStyles()

	const [supply, setSupply] = useState(initialSupplyInfo as SupplyInfoContract)
	const [supplyItems, setSupplyItems] = useState([] as SupplyItemContract[])

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
			await suppliesApi.Create({ ...supply, supplyItems })
			hide()
		} else {
			setErrorText('Form is invalid')
		}
	}, [formIsValid, formValidation, supply, supplyItems, suppliesApi, hide, setErrorText])

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
				<SupplyItems
					simpleProducts={simpleProducts}
					supplyItems={supplyItems}
					setSupplyItems={setSupplyItems}
				/>

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
