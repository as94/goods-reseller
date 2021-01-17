import React from 'react'
import Login from './Login/Login'
import { Route, BrowserRouter as Router, Switch } from 'react-router-dom'
import Home from './Home/Home'
import Register from './Register/Register'
import { AuthProvider } from './Hooks/useAuth'
import PrivateRoute from './PrivateRoute'

const App = () => (
	<AuthProvider>
		<Router>
			<Switch>
				<PrivateRoute exact path={['/', '/admin']}>
					<Home />
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
