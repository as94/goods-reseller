import React, { useCallback, useEffect, useState } from 'react'
import Avatar from '@material-ui/core/Avatar'
import Button from '@material-ui/core/Button'
import CssBaseline from '@material-ui/core/CssBaseline'
import TextField from '@material-ui/core/TextField'
import MaterialLink from '@material-ui/core/Link'
import LockOutlinedIcon from '@material-ui/icons/LockOutlined'
import Typography from '@material-ui/core/Typography'
import { makeStyles } from '@material-ui/core/styles'
import Container from '@material-ui/core/Container'
import Copyright from '../Copyright/Copyright'
import { Link, useHistory, useLocation } from 'react-router-dom'
import { useAuth } from '../Hooks/useAuth'
import { useTranslation } from 'react-i18next'

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
	const { t } = useTranslation()
	const classes = useStyles()

	const history = useHistory()
	const location = useLocation()
	const auth = useAuth()

	const { from } = (location.state as any) || { from: { pathname: '/' } }

	const [email, setEmail] = useState('')
	const [password, setPassword] = useState('')

	const signIn = useCallback(async () => {
		await auth.signIn(email, password)
		history.replace(from)
	}, [email, password, auth.signIn, history])

	useEffect(() => {
		if (auth.user) {
			history.replace(from)
		}
	}, [auth.user, history])

	return (
		<Container component="main" maxWidth="xs">
			<CssBaseline />
			<div className={classes.paper}>
				<Avatar className={classes.avatar}>
					<LockOutlinedIcon />
				</Avatar>
				<Typography component="h1" variant="h5">
					{t('SignIn')}
				</Typography>
				<form className={classes.form} noValidate>
					<TextField
						variant="outlined"
						margin="normal"
						required
						fullWidth
						id="email"
						label={t('EmailAddress')}
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
						label={t('Password')}
						type="password"
						id="password"
						autoComplete="current-password"
						value={password}
						onChange={e => setPassword(e.target.value)}
					/>
					<Button fullWidth variant="contained" color="primary" className={classes.submit} onClick={signIn}>
						{t('SignIn')}
					</Button>
					<MaterialLink variant="body2" component={Link} to="/admin/register">
						{t('SignUpQuestion')}
					</MaterialLink>
				</form>
			</div>
			<Copyright />
		</Container>
	)
}

export default Login
