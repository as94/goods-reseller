import React from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Container from '@material-ui/core/Container'
import Typography from '@material-ui/core/Typography'

const useStyles = makeStyles(theme => ({
	cardGrid: {
		paddingTop: theme.spacing(8),
		paddingBottom: theme.spacing(8),
	},
}))

export const Note = () => {
	const classes = useStyles()

	return (
		<div className={classes.cardGrid}>
			<Container maxWidth="md">
				<Typography variant="body1" align="left" color="textSecondary" paragraph>
					Примечание: некоторые товары можно заменить аналогичными. Например: подписку на Play Station 4, из
					стандартного набора, можно заменить на Xbox One. После оформления заказа мы расскажем все
					подробности.
				</Typography>
			</Container>
		</div>
	)
}
