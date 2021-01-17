import React, { useCallback, useState } from 'react'
import Avatar from '@material-ui/core/Avatar'
import Button from '@material-ui/core/Button'
import CssBaseline from '@material-ui/core/CssBaseline'
import TextField from '@material-ui/core/TextField'
import Link from '@material-ui/core/Link'
import LockOutlinedIcon from '@material-ui/icons/LockOutlined'
import Typography from '@material-ui/core/Typography'
import { makeStyles } from '@material-ui/core/styles'
import Container from '@material-ui/core/Container'
import authApi from '../Api/Auth/authApi'
import { LoginUserContract } from '../Api/Auth/contracts'
import Copyright from '../Copyright/Copyright'
import { useHistory } from 'react-router-dom'

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

const Login = () => {
	const classes = useStyles()
	const history = useHistory()

	const [email, setEmail] = useState('')
	const [password, setPassword] = useState('')

	const signIn = useCallback(async () => {
		await authApi.Login({
			email,
			password,
		} as LoginUserContract)

		history.push('/')
	}, [email, password, authApi])

	return (
		<Container component="main" maxWidth="xs">
			<CssBaseline />
			<div className={classes.paper}>
				<Avatar className={classes.avatar}>
					<LockOutlinedIcon />
				</Avatar>
				<Typography component="h1" variant="h5">
					Sign In
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
					<Button fullWidth variant="contained" color="primary" className={classes.submit} onClick={signIn}>
						Sign In
					</Button>
					<Link href="#" variant="body2">
						{"Don't have an account? Sign Up"}
					</Link>
				</form>
			</div>
			<Copyright />
		</Container>
	)
}

export default Login
