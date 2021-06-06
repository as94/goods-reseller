import React from 'react'
import InstagramIcon from '@material-ui/icons/Instagram'
import Copyright from '../Copyright/Copyright'
import PhoneIcon from '@material-ui/icons/Phone'
import { IconButton, Typography } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'

const useStyles = makeStyles(theme => ({
	footer: {
		backgroundColor: theme.palette.background.paper,
		padding: theme.spacing(6),
	},
	phoneContacts: {
		display: 'flex',
		justifyContent: 'center',
	},
	phoneNumber: {
		paddingLeft: '10px',
	},
}))

const StoreFooter = () => {
	const classes = useStyles()
	return (
		<footer className={classes.footer}>
			<Typography variant="h6" align="center" gutterBottom>
				Наши контакты
			</Typography>
			<div className={classes.phoneContacts}>
				<PhoneIcon />
				<Typography
					variant="subtitle1"
					align="center"
					color="textSecondary"
					component="p"
					className={classes.phoneNumber}
				>
					+7 923 123 19 99
				</Typography>
			</div>
			<Typography variant="subtitle1" align="center" color="textSecondary" component="p">
				<IconButton href="https://www.instagram.com/happyboxy.msk/">
					<InstagramIcon />
				</IconButton>
			</Typography>

			<Copyright />
		</footer>
	)
}

export default StoreFooter
