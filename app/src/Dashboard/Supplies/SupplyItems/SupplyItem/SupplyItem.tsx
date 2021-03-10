import React, { useCallback, useState } from 'react'
import { Button, FormControl, Grid, Input, InputLabel, MenuItem, Select } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import Counter from '../../../../Counter/Counter'
import { ProductListItemContract } from '../../../../Api/Products/contracts'
import { SupplyItemContract } from '../../../../Api/Supplies/contracts'
import { v4 as uuid } from 'uuid'

export interface IOwnProps {
	simpleProducts: ProductListItemContract[]
	addSupplyItem: (supplyItem: SupplyItemContract) => void
	removeSupplyItem: (supplyItemId: string) => void
}

const useStyles = makeStyles(theme => ({
	root: {
		flexGrow: 1,
	},
	addSupplyItemButton: {
		marginTop: theme.spacing(2),
	},
}))

const SupplyItem = ({ simpleProducts, addSupplyItem, removeSupplyItem }: IOwnProps) => {
	const classes = useStyles()

	const [selectedProductId, setSelectedProductId] = useState('')
	const [unitPrice, setUnitPrice] = useState(0)
	const [discountPerUnit, setDiscountPerUnit] = useState(0)
	const [count, setCount] = useState(0)

	const selectedProductIdChangeHandler = useCallback(
		(event: any) => {
			const id = event.target.value
			setSelectedProductId(id)
		},
		[setSelectedProductId],
	)

	const unitPriceChangeHandler = useCallback(
		(e: any) => {
			const x = Number(e.target.value)
			if (x >= 0) {
				setUnitPrice(x)
			}
		},
		[setUnitPrice],
	)

	const discountPerUnitChangeHandler = useCallback(
		(e: any) => {
			const x = Number(e.target.value)
			if (x >= 0 && x <= 1) {
				setDiscountPerUnit(x)
			}
		},
		[setDiscountPerUnit],
	)

	const reset = useCallback(() => {
		setSelectedProductId('')
		setUnitPrice(0)
		setDiscountPerUnit(0)
		setCount(0)
	}, [setSelectedProductId, setUnitPrice, setDiscountPerUnit, setCount])

	const addSupplyItemHandler = useCallback(() => {
		const supplyItem = {
			id: uuid(),
			productId: selectedProductId,
			unitPrice,
			discountPerUnit,
			quantity: count,
		} as SupplyItemContract

		addSupplyItem(supplyItem)
		reset()
	}, [selectedProductId, unitPrice, discountPerUnit, count, uuid, addSupplyItem, reset])

	return (
		<Grid item xs={12} md={12} className={classes.root}>
			<Grid container spacing={3}>
				<Grid item xs={3}>
					<FormControl fullWidth>
						<InputLabel htmlFor="productId">Product</InputLabel>
						<Select
							labelId="productId"
							id="product-id-select"
							style={{ width: '100%' }}
							value={selectedProductId}
							onChange={selectedProductIdChangeHandler}
						>
							{simpleProducts.map(x => (
								<MenuItem key={x.id} value={x.id}>
									{x.name}
								</MenuItem>
							))}
						</Select>
					</FormControl>
				</Grid>
				<Grid item xs={3}>
					<FormControl fullWidth>
						<InputLabel htmlFor="unitPrice">Unit Price</InputLabel>
						<Input
							required
							type="number"
							id="unitPrice"
							value={unitPrice}
							onChange={unitPriceChangeHandler}
						/>
					</FormControl>
				</Grid>
				<Grid item xs={3}>
					<FormControl fullWidth>
						<InputLabel htmlFor="discountPerUnit">Discount Per Unit</InputLabel>
						<Input
							required
							type="number"
							id="discountPerUnit"
							value={discountPerUnit}
							onChange={discountPerUnitChangeHandler}
						/>
					</FormControl>
				</Grid>
				<Grid item xs={3} style={{ marginTop: '10px' }}>
					<Counter
						initialValue={count}
						addHandler={() => Promise.resolve(setCount(prev => prev + 1))}
						removeHandler={() => Promise.resolve(setCount(prev => prev - 1))}
						isReset={count === 0}
					/>
				</Grid>
			</Grid>
			<Button
				variant="contained"
				color="primary"
				onClick={addSupplyItemHandler}
				className={classes.addSupplyItemButton}
				disabled={!selectedProductId}
			>
				Add supply item
			</Button>
		</Grid>
	)
}

export default SupplyItem
