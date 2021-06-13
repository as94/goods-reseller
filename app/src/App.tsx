import React from 'react'
import Login from './Login/Login'
import { Route, BrowserRouter as Router, Switch } from 'react-router-dom'
import Register from './Register/Register'
import { AuthProvider } from './Hooks/useAuth'
import PrivateRoute from './PrivateRoute'
import Dashboard from './Dashboard/Dashboard'
import StorePage from './Store/StorePage'
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles'
import SetInfo from './SetInfo/SetInfo'
import Checkout from './Checkout/Checkout'
import { blue, lightBlue } from '@material-ui/core/colors'

const theme = createMuiTheme({
	palette: {
		// type: 'dark',
		// primary: {
		// 	main: '#fff',
		// },
		// secondary: {
		// 	main: '#000',
		// },
		// contrastThreshold: 3,
		// tonalOffset: 0.2,
		primary: lightBlue,
		secondary: blue,
	},
	typography: {
		fontFamily: [
			'-apple-system',
			'BlinkMacSystemFont',
			'"Segoe UI"',
			'Roboto',
			'"Helvetica Neue"',
			'Arial',
			'sans-serif',
			'"Apple Color Emoji"',
			'"Segoe UI Emoji"',
			'"Segoe UI Symbol"',
		].join(','),
	},
})

const App = () => (
	<MuiThemeProvider theme={theme}>
		<AuthProvider>
			<Router>
				<Switch>
					<PrivateRoute exact path={['/admin']}>
						<Dashboard />
					</PrivateRoute>
					<Route exact path="/admin/login">
						<Login />
					</Route>
					<Route exact path="/admin/register">
						<Register />
					</Route>
					{/* <Route path="/sets/:setId">
						<SetInfo />
					</Route> */}
					<Route path="/store/checkout/:setId">
						<Checkout />
					</Route>
					<Route path={['/', '/store']}>
						<StorePage />
					</Route>
				</Switch>
			</Router>
		</AuthProvider>
	</MuiThemeProvider>
)

export default App
