import React, { useCallback, useState } from 'react'
import Button from '@material-ui/core/Button'
import ButtonGroup from '@material-ui/core/ButtonGroup'

export interface IOwnProps {
	initialValue: number
	addHandler: () => Promise<void>
	removeHandler: () => Promise<void>
}

const Counter = ({ initialValue, addHandler, removeHandler }: IOwnProps) => {
	const [counter, setCounter] = useState(initialValue)

	const handleIncrement = useCallback(async () => {
		await addHandler()
		setCounter(prev => prev + 1)
	}, [setCounter, addHandler])

	const handleDecrement = useCallback(async () => {
		await removeHandler()
		setCounter(prev => prev - 1)
	}, [setCounter, removeHandler])

	return (
		<ButtonGroup size="small" aria-label="small outlined button group">
			<Button onClick={handleDecrement} disabled={counter === 0}>
				-
			</Button>
			<Button disabled>{counter}</Button>
			<Button onClick={handleIncrement}>+</Button>
		</ButtonGroup>
	)
}

export default Counter
