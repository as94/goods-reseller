import React, { useCallback } from 'react'
import Grid from '@material-ui/core/Grid'
import Typography from '@material-ui/core/Typography'
import TextField from '@material-ui/core/TextField'
import { InputLabel, MenuItem, Select } from '@material-ui/core'
import { AddressContract } from '../Api/Orders/contracts'
import { DeliveryType, errorsList } from './Checkout'

interface IOwnProps {
	address: AddressContract
	setAddress: (address: AddressContract) => void
	deliveryType: DeliveryType
	setDeliveryType: (deliveryType: DeliveryType) => void
	errors: string[]
}

const AddressForm = ({ address, setAddress, deliveryType, setDeliveryType, errors }: IOwnProps) => {
	const streetIsInvalid = errors.includes(errorsList.streetIsRequiredError)
	const zipCodeIsInvalid = errors.includes(errorsList.zipCodeIsRequiredError)

	const cityChangeHandler = useCallback(
		(e: any) => {
			setAddress({
				...address,
				city: e.target.value,
			})
		},
		[setAddress, address],
	)

	const streetChangeHandler = useCallback(
		(e: any) => {
			setAddress({
				...address,
				street: e.target.value,
			})
		},
		[setAddress, address],
	)

	const zipCodeChangeHandler = useCallback(
		(e: any) => {
			setAddress({
				...address,
				zipCode: e.target.value,
			})
		},
		[setAddress, address],
	)

	return (
		<>
			<Typography variant="h6" gutterBottom>
				Адрес доставки
			</Typography>
			<Grid container spacing={3}>
				<Grid item xs={12} sm={6}>
					<TextField
						disabled
						id="country"
						name="country"
						label="Страна"
						fullWidth
						value="Россия"
						autoComplete="shipping country"
					/>
				</Grid>
				<Grid item xs={12} sm={6}>
					<TextField
						id="city"
						name="city"
						label="Город"
						fullWidth
						value={address.city}
						onChange={cityChangeHandler}
						autoComplete="shipping city"
					/>
				</Grid>
				<Grid item xs={12}>
					<TextField
						id="street"
						name="street"
						label="Улица"
						value={address.street}
						onChange={streetChangeHandler}
						fullWidth
						autoComplete="shipping address-line1"
						error={streetIsInvalid}
						helperText={streetIsInvalid ? errorsList.streetIsRequiredError : undefined}
					/>
				</Grid>
				<Grid item xs={12} sm={8}>
					<InputLabel id="delivery">Способ доставки</InputLabel>
					<Select
						labelId="delivery"
						id="delivery-select"
						value={deliveryType}
						onChange={e => setDeliveryType(Number(e.target.value))}
					>
						<MenuItem value={DeliveryType.Mail}>Почта</MenuItem>
						<MenuItem value={DeliveryType.ServiceWithinMoscowRingRoad}>
							Служба доставки в пределах МКАД
						</MenuItem>
						<MenuItem value={DeliveryType.ServiceOutsideMoscowRingRoad}>Служба доставки за МКАД</MenuItem>
						{/* <MenuItem value={2}>Самовывоз</MenuItem> */}
					</Select>
				</Grid>
				<Grid item xs={12} sm={4}>
					<TextField
						id="zip"
						name="zip"
						label="Почтовый индекс"
						value={address.zipCode}
						onChange={zipCodeChangeHandler}
						fullWidth
						autoComplete="shipping postal-code"
						error={zipCodeIsInvalid}
						helperText={zipCodeIsInvalid ? errorsList.zipCodeIsRequiredError : undefined}
					/>
				</Grid>
			</Grid>
		</>
	)
}

export default AddressForm
