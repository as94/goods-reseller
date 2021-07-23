import React from 'react'
import { Route, BrowserRouter, Switch } from 'react-router-dom'
import { AuthProvider } from './Hooks/useAuth'
import PrivateRoute from './PrivateRoute'
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles'
import { blue, lightBlue } from '@material-ui/core/colors'

const Login = React.lazy(() => import('./Login/Login'))
const Register = React.lazy(() => import('./Register/Register'))
const Dashboard = React.lazy(() => import('./Dashboard/Dashboard'))
const StorePage = React.lazy(() => import('./Store/StorePage'))
const Checkout = React.lazy(() => import('./Checkout/Checkout'))

const theme = createMuiTheme({
	palette: {
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
		<React.Suspense fallback={<span>Загрузка</span>}>
			<BrowserRouter>
				<AuthProvider>
					<Switch>
						<PrivateRoute exact path={['/admin']} render={() => <Dashboard />} />
						<Route exact path="/admin/login" render={() => <Login />} />
						<Route exact path="/admin/register" render={() => <Register />} />
						<Route path="/store/checkout/:setId" render={() => <Checkout />} />
						<Route path={['/', '/store']} render={() => <StorePage />} />
					</Switch>
				</AuthProvider>
			</BrowserRouter>
		</React.Suspense>
	</MuiThemeProvider>
)

export default App
