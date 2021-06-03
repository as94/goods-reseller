import React from 'react'
import { makeStyles } from '@material-ui/core/styles'
import { faGift } from '@fortawesome/free-solid-svg-icons'
import AppBar from '@material-ui/core/AppBar'
import Toolbar from '@material-ui/core/Toolbar'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import Typography from '@material-ui/core/Typography'

const useStyles = makeStyles(theme => ({
	icon: {
		marginRight: theme.spacing(2),
	},
}))

const StoreHeader = () => {
	const classes = useStyles()
	return (
		<AppBar position="relative">
			<Toolbar>
				<FontAwesomeIcon className={classes.icon} icon={faGift} />
				<Typography variant="h6" color="inherit" noWrap>
					Подарки для мужчин
				</Typography>
			</Toolbar>
		</AppBar>
	)
}

export default StoreHeader
