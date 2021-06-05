import React from 'react'
import Typography from '@material-ui/core/Typography'
import Grid from '@material-ui/core/Grid'
import TextField from '@material-ui/core/TextField'
import FormControlLabel from '@material-ui/core/FormControlLabel'
import Checkbox from '@material-ui/core/Checkbox'

const CustomerInfoForm = () => {
	return (
		<>
			<Typography variant="h6" gutterBottom>
				Контактная информация
			</Typography>
			<Grid container spacing={3}>
				<Grid item xs={12} md={12}>
					<TextField id="name" label="Ваше имя" fullWidth autoComplete="name" />
				</Grid>
				<Grid item xs={12} md={12}>
					<TextField
						required
						id="phoneNumber"
						label="Ваш номер телефона"
						fullWidth
						autoComplete="phoneNumber"
					/>
				</Grid>
			</Grid>
		</>
	)
}

export default CustomerInfoForm
