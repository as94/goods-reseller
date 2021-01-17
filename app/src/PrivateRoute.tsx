import React from 'react'
import { Redirect, Route, RouteProps } from 'react-router-dom'
import { useAuth } from './Hooks/useAuth'

interface IOwnProps extends RouteProps {
	children?: React.ReactNode | React.ReactNode[]
}

const PrivateRoute = ({ children, ...rest }: IOwnProps) => {
	const auth = useAuth()

	return (
		<Route
			{...rest}
			render={({ location }) =>
				auth.user ? (
					children
				) : (
					<Redirect
						to={{
							pathname: '/admin/login',
							state: { from: location },
						}}
					/>
				)
			}
		/>
	)
}

export default PrivateRoute
