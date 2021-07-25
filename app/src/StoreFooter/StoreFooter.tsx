import React from 'react'
import InstagramIcon from '@material-ui/icons/Instagram'
import Copyright from '../Copyright/Copyright'
import PhoneIcon from '@material-ui/icons/Phone'
import EmailIcon from '@material-ui/icons/EmailOutlined'
import { IconButton, Typography } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'

const useStyles = makeStyles(theme => ({
	footer: {
		backgroundColor: theme.palette.background.paper,
		padding: theme.spacing(6),
	},
	contacts: {
		display: 'flex',
		justifyContent: 'center',
	},
	contact: {
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
			<div className={classes.contacts} style={{ marginBottom: '15px' }}>
				<Typography
					variant="subtitle1"
					align="center"
					color="textSecondary"
					component="p"
					className={classes.contact}
					style={{ borderBottom: '1px solid rgba(0, 0, 0, 0.54)' }}
				>
					Мы работаем по всей Москве и Московской области
				</Typography>
			</div>
			<div className={classes.contacts}>
				<PhoneIcon style={{ marginTop: '2px' }} />
				<Typography
					variant="subtitle1"
					align="center"
					color="textSecondary"
					component="p"
					className={classes.contact}
				>
					+7 (923) 123-19-99
				</Typography>
			</div>
			<div className={classes.contacts}>
				<EmailIcon style={{ marginTop: '2px' }} />
				<Typography
					variant="subtitle1"
					align="center"
					color="textSecondary"
					component="p"
					className={classes.contact}
				>
					happyboxy.feedback@yandex.ru
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
