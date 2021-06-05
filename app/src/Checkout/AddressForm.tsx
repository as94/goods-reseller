import React, { useState } from 'react'
import Grid from '@material-ui/core/Grid'
import Typography from '@material-ui/core/Typography'
import TextField from '@material-ui/core/TextField'
import { InputLabel, MenuItem, Select } from '@material-ui/core'

const AddressForm = () => {
	const [deliveryType, setDeliveryType] = useState(0)
	return (
		<>
			<Typography variant="h6" gutterBottom>
				Адрес доставки
			</Typography>
			<Grid container spacing={3}>
				<Grid item xs={12} sm={6}>
					<TextField
						required
						disabled
						id="city"
						name="city"
						label="Город"
						fullWidth
						value="Москва"
						autoComplete="shipping city"
					/>
				</Grid>
				<Grid item xs={12} sm={6}>
					<TextField
						required
						disabled
						id="country"
						name="country"
						label="Страна"
						fullWidth
						value="Россия"
						autoComplete="shipping country"
					/>
				</Grid>
				<Grid item xs={12}>
					<TextField
						required
						id="street"
						name="street"
						label="Улица"
						fullWidth
						autoComplete="shipping address-line1"
					/>
				</Grid>
				<Grid item xs={12} sm={6}>
					<InputLabel id="delivery">Способ доставки</InputLabel>
					<Select
						labelId="delivery"
						id="delivery-select"
						value={deliveryType}
						onChange={e => setDeliveryType(Number(e.target.value))}
					>
						<MenuItem value={0}>Почта</MenuItem>
						<MenuItem value={1}>Самовывоз</MenuItem>
						<MenuItem value={2}>Служба доставки</MenuItem>
					</Select>
				</Grid>
				<Grid item xs={12} sm={6}>
					<TextField
						required
						id="zip"
						name="zip"
						label="Почтовый индекс"
						fullWidth
						autoComplete="shipping postal-code"
					/>
				</Grid>
			</Grid>
		</>
	)
}

export default AddressForm
