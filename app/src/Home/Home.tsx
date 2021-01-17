import React from 'react'
import Container from '@material-ui/core/Container'
import CssBaseline from '@material-ui/core/CssBaseline'
import Typography from '@material-ui/core/Typography'
import Copyright from '../Copyright/Copyright'
import { makeStyles } from '@material-ui/core/styles'
import { Link } from 'react-router-dom'

const useStyles = makeStyles(theme => ({
	paper: {
		marginTop: theme.spacing(8),
		display: 'flex',
		flexDirection: 'column',
		alignItems: 'center',
	},
}))

const Home = () => {
	const classes = useStyles()

	return (
		<Container component="main" maxWidth="xs">
			<CssBaseline />
			<div className={classes.paper}>
				<Typography component="h1" variant="h5">
					You are in the house
				</Typography>
			</div>

			<ul>
				<li>
					<Link to="/admin/">Home</Link>
				</li>
				<li>
					<Link to="/admin/login">Login</Link>
				</li>
				<li>
					<Link to="/admin/register">Register</Link>
				</li>
			</ul>

			<Copyright />
		</Container>
	)
}

export default Home
