import React, { useCallback } from 'react'
import Container from '@material-ui/core/Container'
import CssBaseline from '@material-ui/core/CssBaseline'
import Typography from '@material-ui/core/Typography'
import Button from '@material-ui/core/Button'
import Copyright from '../Copyright/Copyright'
import { makeStyles } from '@material-ui/core/styles'
import { useHistory } from 'react-router-dom'
import { useAuth } from '../Hooks/useAuth'
import Box from '@material-ui/core/Box'

const useStyles = makeStyles(theme => ({
	paper: {
		marginTop: theme.spacing(8),
		display: 'flex',
		flexDirection: 'column',
		alignItems: 'center',
	},
	submit: {
		margin: theme.spacing(3, 0, 2),
	},
}))

const Home = () => {
	const classes = useStyles()

	const history = useHistory()
	const auth = useAuth()

	const signOut = useCallback(async () => {
		await auth.signOut()
		history.push('/')
	}, [auth.signOut])

	return (
		<Container component="main" maxWidth="xs">
			<CssBaseline />
			{auth.user && (
				<div className={classes.paper}>
					<Box component="div" display="inline">
						{auth.user.email}
					</Box>
					<Button fullWidth variant="contained" color="primary" className={classes.submit} onClick={signOut}>
						Sign Out
					</Button>
					<Typography component="h1" variant="h5">
						You are in the house
					</Typography>
				</div>
			)}

			<Copyright />
		</Container>
	)
}

export default Home
