import React, { useCallback, useState } from 'react'
import { Button, FormControl, Grid, Input, InputLabel, MenuItem, Select } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import Counter from '../../../../Counter/Counter'
import { ProductListItemContract } from '../../../../Api/Products/contracts'
import { v4 as uuid } from 'uuid'
import { useTranslation } from 'react-i18next'
import { OrderItemContract } from '../../../../Api/Orders/contracts'

export interface IOwnProps {
	simpleProducts: ProductListItemContract[]
	addOrderItem: (orderItem: OrderItemContract) => void
}

const useStyles = makeStyles(theme => ({
	root: {
		flexGrow: 1,
	},
	addOrderItemButton: {
		marginTop: theme.spacing(2),
	},
}))

const OrderItem = ({ simpleProducts, addOrderItem }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const [selectedProductId, setSelectedProductId] = useState('')
	const [count, setCount] = useState(0)

	const selectedProductIdChangeHandler = useCallback(
		(event: any) => {
			const id = event.target.value
			setSelectedProductId(id)
		},
		[setSelectedProductId],
	)

	const reset = useCallback(() => {
		setSelectedProductId('')
		setCount(0)
	}, [setSelectedProductId, setCount])

	const addOrderItemHandler = useCallback(() => {
		const product = simpleProducts.find(x => x.id === selectedProductId)

		if (product) {
			const orderItem = {
				id: uuid(),
				productId: selectedProductId,
				unitPrice: product.unitPrice,
				discountPerUnit: product.discountPerUnit,
				quantity: count,
			} as OrderItemContract

			addOrderItem(orderItem)
			reset()
		}
	}, [selectedProductId, simpleProducts, selectedProductId, count, uuid, addOrderItem, reset])

	return (
		<Grid item xs={12} md={12} className={classes.root}>
			<Grid container spacing={3}>
				<Grid item xs={3}>
					<FormControl fullWidth>
						<InputLabel htmlFor="productId">{t('Product')}</InputLabel>
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
				<Grid item xs={3} style={{ marginTop: '10px' }}>
					<Counter
						initialValue={count}
						addHandler={() => setCount(prev => prev + 1)}
						removeHandler={() => setCount(prev => prev - 1)}
						isReset={count === 0}
					/>
				</Grid>
			</Grid>
			<Button
				variant="contained"
				color="primary"
				onClick={addOrderItemHandler}
				className={classes.addOrderItemButton}
				disabled={!selectedProductId}
			>
				{t('AddOrderItem')}
			</Button>
		</Grid>
	)
}

export default OrderItem
