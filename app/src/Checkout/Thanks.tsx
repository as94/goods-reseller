import React from 'react'
import CssBaseline from '@material-ui/core/CssBaseline'
import StoreHeader from '../StoreHeader/StoreHeader'
import StoreFooter from '../StoreFooter/StoreFooter'
import Typography from '@material-ui/core/Typography'
import Paper from '@material-ui/core/Paper'
import { makeStyles } from '@material-ui/core/styles'

const useStyles = makeStyles(theme => ({
	layout: {
		width: 'auto',
		marginLeft: theme.spacing(2),
		marginRight: theme.spacing(2),
		[theme.breakpoints.up(600 + theme.spacing(2) * 2)]: {
			width: 600,
			marginLeft: 'auto',
			marginRight: 'auto',
		},
	},
	paper: {
		marginTop: theme.spacing(3),
		marginBottom: theme.spacing(3),
		padding: theme.spacing(2),
		[theme.breakpoints.up(600 + theme.spacing(3) * 2)]: {
			marginTop: theme.spacing(6),
			marginBottom: theme.spacing(6),
			padding: theme.spacing(3),
		},
	},
}))

export const Thanks = () => {
	const classes = useStyles()
	return (
		<>
			<CssBaseline />
			<StoreHeader />
			<main className={classes.layout}>
				<Paper className={classes.paper}>
					<Typography variant="h5" gutterBottom>
						Спасибо за ваш заказ.
					</Typography>
					<Typography variant="subtitle1">
						Мы свяжимся с вами в ближайшее время для уточнения деталей.
					</Typography>
				</Paper>
			</main>
			<StoreFooter />
		</>
	)
}
