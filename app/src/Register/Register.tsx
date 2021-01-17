import React, { useCallback, useEffect, useState } from 'react'
import Avatar from '@material-ui/core/Avatar'
import Button from '@material-ui/core/Button'
import CssBaseline from '@material-ui/core/CssBaseline'
import TextField from '@material-ui/core/TextField'
import LockOpenOutlinedIcon from '@material-ui/icons/LockOpenOutlined'
import Typography from '@material-ui/core/Typography'
import { makeStyles } from '@material-ui/core/styles'
import Container from '@material-ui/core/Container'
import Copyright from '../Copyright/Copyright'
import { useHistory, useLocation } from 'react-router-dom'
import { useAuth } from '../Hooks/useAuth'

const useStyles = makeStyles(theme => ({
	paper: {
		marginTop: theme.spacing(8),
		display: 'flex',
		flexDirection: 'column',
		alignItems: 'center',
	},
	avatar: {
		margin: theme.spacing(1),
		backgroundColor: theme.palette.secondary.main,
	},
	form: {
		width: '100%', // Fix IE 11 issue.
		marginTop: theme.spacing(1),
	},
	submit: {
		margin: theme.spacing(3, 0, 2),
	},
}))

const Register = () => {
	const classes = useStyles()

	const history = useHistory()
	const location = useLocation()
	const auth = useAuth()

	const { from } = (location.state as any) || { from: { pathname: '/' } }

	const [email, setEmail] = useState('')
	const [password, setPassword] = useState('')

	const signUp = useCallback(async () => {
		await auth.signUp(email, password)

		history.replace(from)
	}, [email, password, auth.signUp, history])

	return (
		<Container component="main" maxWidth="xs">
			<CssBaseline />
			<div className={classes.paper}>
				<Avatar className={classes.avatar}>
					<LockOpenOutlinedIcon />
				</Avatar>
				<Typography component="h1" variant="h5">
					Sign Up
				</Typography>
				<form className={classes.form} noValidate>
					<TextField
						variant="outlined"
						margin="normal"
						required
						fullWidth
						id="email"
						label="Email Address"
						name="email"
						autoComplete="email"
						autoFocus
						value={email}
						onChange={e => setEmail(e.target.value)}
					/>
					<TextField
						variant="outlined"
						margin="normal"
						required
						fullWidth
						name="password"
						label="Password"
						type="password"
						id="password"
						autoComplete="current-password"
						value={password}
						onChange={e => setPassword(e.target.value)}
					/>
					<Button fullWidth variant="contained" color="primary" className={classes.submit} onClick={signUp}>
						Sign Up
					</Button>
				</form>
			</div>
			<Copyright />
		</Container>
	)
}

export default Register
