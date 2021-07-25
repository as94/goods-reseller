import React from 'react'
import { makeStyles } from '@material-ui/core/styles'
import { faGift } from '@fortawesome/free-solid-svg-icons'
import AppBar from '@material-ui/core/AppBar'
import Toolbar from '@material-ui/core/Toolbar'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import Typography from '@material-ui/core/Typography'
import { useHistory } from 'react-router-dom'

const useStyles = makeStyles(theme => ({
	icon: {
		marginRight: theme.spacing(2),
	},
	appBar: {
		cursor: 'pointer',
	},
}))

const StoreHeader = () => {
	const classes = useStyles()
	const history = useHistory()
	return (
		<AppBar position="relative" className={classes.appBar} onClick={() => history.push(`/`)}>
			<Toolbar>
				<FontAwesomeIcon className={classes.icon} icon={faGift} />
				<Typography variant="h6" color="inherit" noWrap>
					Happyboxy
				</Typography>
			</Toolbar>
		</AppBar>
	)
}

export default StoreHeader
