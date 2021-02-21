import React from 'react'
import Typography from '@material-ui/core/Typography'

const Title = (props: any) => {
	return (
		<Typography component="h2" variant="h6" color={props.color} gutterBottom>
			{props.children}
		</Typography>
	)
}

export default Title
