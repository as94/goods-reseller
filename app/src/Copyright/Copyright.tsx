import React from 'react'
import Typography from '@material-ui/core/Typography'
import Box from '@material-ui/core/Box'

const Copyright = () => {
	return (
		<Box mt={8}>
			<Typography variant="body2" color="textSecondary" align="center">
				{'© '}
				<span color="inherit">Happy Boxy</span> {new Date().getFullYear()}
				{'. Все права защищены'}
			</Typography>
		</Box>
	)
}

export default Copyright
