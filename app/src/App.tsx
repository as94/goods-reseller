import React from 'react'
import Login from './Login/Login'
import { Route, BrowserRouter as Router, Switch } from 'react-router-dom'
import Home from './Home/Home'
import Register from './Register/Register'

const App = () => (
	<Router>
		<Switch>
			<Route exact path={['/', '/admin']}>
				<Home />
			</Route>
			<Route path="/admin/login">
				<Login />
			</Route>
			<Route path="/admin/register">
				<Register />
			</Route>
		</Switch>
	</Router>
)

export default App
