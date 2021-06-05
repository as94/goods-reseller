import React from 'react'
import Login from './Login/Login'
import { Route, BrowserRouter as Router, Switch } from 'react-router-dom'
import Register from './Register/Register'
import { AuthProvider } from './Hooks/useAuth'
import PrivateRoute from './PrivateRoute'
import Dashboard from './Dashboard/Dashboard'
import StorePage from './Store/StorePage'
import { MuiThemeProvider, createMuiTheme } from '@material-ui/core/styles'
import { orange, yellow } from '@material-ui/core/colors'
import SetInfo from './SetInfo/SetInfo'
import Checkout from './Checkout/Checkout'

const theme = createMuiTheme({
	palette: {
		primary: orange,
		secondary: yellow,
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
