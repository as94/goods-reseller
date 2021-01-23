import React from 'react'
import Login from './Login/Login'
import { Route, BrowserRouter as Router, Switch } from 'react-router-dom'
import Register from './Register/Register'
import { AuthProvider } from './Hooks/useAuth'
import PrivateRoute from './PrivateRoute'
import Dashboard from './Dashboard/Dashboard'

const App = () => (
	<AuthProvider>
		<Router>
			<Switch>
				<PrivateRoute exact path={['/', '/admin']}>
					<Dashboard />
				</PrivateRoute>
				<Route path="/admin/login">
					<Login />
				</Route>
				<Route path="/admin/register">
					<Register />
				</Route>
			</Switch>
		</Router>
	</AuthProvider>
)

export default App
