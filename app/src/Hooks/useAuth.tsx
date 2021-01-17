import React, { useCallback, useState } from 'react'
import { createContext, useContext } from 'react'
import authApi from '../Api/Auth/authApi'
import { LoginUserContract } from '../Api/Auth/contracts'

export interface IUser {
	email: string
}

export interface IAuthContext {
	user: IUser | null
	signIn: (email: string, password: string) => Promise<void>
	signOut: () => Promise<void>
	signUp: (email: string, password: string) => Promise<void>
}

const defaultContext = {
	user: {} as IUser,
	signIn: (email: string, password: string) => {},
	signOut: () => {},
	signUp: (email: string, password: string) => {},
} as IAuthContext

export const AuthContext = createContext(defaultContext)

const useAuthProvider = () => {
	const [user, setUser] = useState(null as IUser | null)

	const signIn = useCallback(
		async (email: string, password: string) => {
			try {
				await authApi.Login({
					email,
					password,
				} as LoginUserContract)

				setUser({ email } as IUser)
			} catch (e) {
				setUser(null)
			}
		},
		[authApi, setUser],
	)

	const signOut = useCallback(async () => {
		await authApi.Logout()
		setUser(null)
	}, [authApi, setUser])

	const signUp = useCallback(
		async (email: string, password: string) => {
			try {
				await authApi.Register({
					email,
					password,
				} as LoginUserContract)

				setUser({ email } as IUser)
			} catch (e) {
				setUser(null)
			}
		},
		[authApi, setUser],
	)

	return { user, signIn, signOut, signUp }
}

export const AuthProvider = ({ children }: { children?: React.ReactNode | React.ReactNode[] }) => {
	const auth = useAuthProvider()
	return <AuthContext.Provider value={auth}>{children}</AuthContext.Provider>
}

export const useAuth = () => {
	return useContext(AuthContext)
}
