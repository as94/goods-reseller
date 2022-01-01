import React, { useCallback, useState } from 'react'
import { Button, FormControl, Grid, Input, InputLabel, MenuItem, Select } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import Counter from '../../../../Counter/Counter'
import { ProductListItemContract } from '../../../../Api/Products/contracts'
import { v4 as uuid } from 'uuid'
import { useTranslation } from 'react-i18next'
import { OrderItemContract } from '../../../../Api/Orders/contracts'

export interface IOwnProps {
	setProducts: ProductListItemContract[]
	simpleProducts: ProductListItemContract[]
	addOrderItem: (orderItem: OrderItemContract) => void
	addOrderItems: (newOrderItems: OrderItemContract[]) => void
	setOrderAddedCost: (addedCost: number) => void
}

const useStyles = makeStyles(theme => ({
	root: {
		flexGrow: 1,
	},
	addButton: {
		marginTop: theme.spacing(2),
	},
}))

const OrderItem = ({ setProducts, simpleProducts, addOrderItem, addOrderItems, setOrderAddedCost }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const [selectedProductId, setSelectedProductId] = useState('')
	const [count, setCount] = useState(0)
	const [selectedSetId, setSelectedSetId] = useState('')

	const selectedProductIdChangeHandler = useCallback(
		(event: any) => {
			const id = event.target.value
			setSelectedProductId(id)
		},
		[setSelectedProductId],
	)

	const selectedSetIdChangeHandler = useCallback(
		(event: any) => {
			const id = event.target.value
			setSelectedSetId(id)
		},
		[setSelectedSetId],
	)

	const resetProduct = useCallback(() => {
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
			resetProduct()
		}
	}, [selectedProductId, simpleProducts, selectedProductId, count, uuid, addOrderItem, resetProduct])

	const addSetHandler = useCallback(() => {
		const set = setProducts.find(x => x.id === selectedSetId)
		if (set) {
			const products = simpleProducts.filter(x => set.productIds.includes(x.id))
			if (products.length) {
				const orderItems = products.map(
					p =>
						({
							id: uuid(),
							productId: p.id,
							unitPrice: p.unitPrice,
							discountPerUnit: p.discountPerUnit,
							quantity: 1,
						} as OrderItemContract),
				)
				addOrderItems(orderItems)
				setSelectedSetId('')
			}
			setOrderAddedCost(set.addedCost)
		}
	}, [
		selectedSetId,
		setProducts,
		simpleProducts,
		selectedSetId,
		uuid,
		addOrderItems,
		setSelectedSetId,
		setOrderAddedCost,
	])

	return (
		<Grid item xs={12} md={12} className={classes.root}>
			<Grid container spacing={3}>
				<Grid item xs={3}>
					<FormControl fullWidth>
						<InputLabel htmlFor="setId">{t('Set')}</InputLabel>
						<Select
							labelId="setId"
							id="set-id-select"
							style={{ width: '100%' }}
							value={selectedSetId}
							onChange={selectedSetIdChangeHandler}
						>
							{setProducts.map(x => (
								<MenuItem key={x.id} value={x.id}>
									{x.name}
								</MenuItem>
							))}
						</Select>
					</FormControl>
				</Grid>
			</Grid>
			<Button
				variant="contained"
				color="primary"
				onClick={addSetHandler}
				className={classes.addButton}
				disabled={!selectedSetId}
			>
				{t('AddSet')}
			</Button>
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
				className={classes.addButton}
				disabled={!selectedProductId}
			>
				{t('AddOrderItem')}
			</Button>
		</Grid>
	)
}

export default OrderItem
