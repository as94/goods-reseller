import React, { useCallback, useEffect, useState } from 'react'
import Button from '@material-ui/core/Button'
import ButtonGroup from '@material-ui/core/ButtonGroup'

export interface IOwnProps {
	initialValue: number
	addHandler: () => void
	removeHandler: () => void
	isReset?: boolean
}

const Counter = ({ initialValue, addHandler, removeHandler, isReset }: IOwnProps) => {
	const [counter, setCounter] = useState(initialValue)

	const handleIncrement = useCallback(async () => {
		addHandler()
		setCounter(prev => prev + 1)
	}, [setCounter, addHandler])

	const handleDecrement = useCallback(async () => {
		removeHandler()
		setCounter(prev => prev - 1)
	}, [setCounter, removeHandler])

	useEffect(() => {
		if (isReset) {
			setCounter(0)
		}
	}, [isReset])

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
