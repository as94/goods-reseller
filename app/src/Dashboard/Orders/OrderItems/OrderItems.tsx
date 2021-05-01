import React, { useCallback, useEffect } from 'react'
import Table from '@material-ui/core/Table'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import Paper from '@material-ui/core/Paper'
import { makeStyles } from '@material-ui/core/styles'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import { Box, FormControl, Grid, Input, InputLabel } from '@material-ui/core'
import DeleteIcon from '@material-ui/icons/Delete'
import Title from '../../Title'
import { useTranslation } from 'react-i18next'
import { OrderItemContract } from '../../../Api/Orders/contracts'
import OrderItem from './OrderItem/OrderItem'

export interface IOwnProps {
	setProducts: ProductListItemContract[]
	simpleProducts: ProductListItemContract[]
	orderItems: OrderItemContract[]
	setOrderItems: (orderItems: OrderItemContract[]) => void
}

const useStyles = makeStyles(theme => ({
	table: {
		minWidth: 650,
	},
	removeButton: {
		cursor: 'pointer',
	},
}))

const OrderItems = ({ setProducts, simpleProducts, orderItems, setOrderItems }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const addOrderItem = useCallback(
		(orderItem: OrderItemContract) => {
			setOrderItems([...orderItems, orderItem])
		},
		[orderItems, setOrderItems],
	)

	const addOrderItems = useCallback(
		(newOrderItems: OrderItemContract[]) => {
			setOrderItems([...orderItems, ...newOrderItems])
		},
		[orderItems, setOrderItems],
	)

	const removeOrderItem = useCallback(
		(orderItemId: string) => {
			setOrderItems(orderItems.filter(x => x.id !== orderItemId))
		},
		[orderItems, setOrderItems],
	)

	return (
		<>
			<Box pt={2} pl={2}>
				<Title color="secondary">{t('OrderItems')}</Title>
			</Box>
			<OrderItem
				setProducts={setProducts}
				simpleProducts={simpleProducts}
				addOrderItem={addOrderItem}
				addOrderItems={addOrderItems}
			/>
			{orderItems.length > 0 && (
				<Grid item xs={12} md={12}>
					<TableContainer component={Paper}>
						<Table className={classes.table} aria-label="simple table">
							<TableHead>
								<TableRow>
									<TableCell align="right">{t('ProductName')}</TableCell>
									<TableCell align="right">{t('UnitPrice')}</TableCell>
									<TableCell align="right">{t('DiscountPerUnit')}</TableCell>
									<TableCell align="right">{t('Quantity')}</TableCell>
									<TableCell></TableCell>
								</TableRow>
							</TableHead>
							<TableBody>
								{orderItems.map((x, idx) => {
									const product = simpleProducts.find(p => p.id === x.productId)
									return product ? (
										<TableRow key={idx}>
											<TableCell align="right">{product.name}</TableCell>
											<TableCell align="right">{x.unitPrice}</TableCell>
											<TableCell align="right">(${x.discountPerUnit * 100} %)</TableCell>
											<TableCell align="right">{x.quantity}</TableCell>
											<TableCell align="center">
												<DeleteIcon
													className={classes.removeButton}
													onClick={() => removeOrderItem(x.id)}
												/>
											</TableCell>
										</TableRow>
									) : null
								})}
							</TableBody>
						</Table>
					</TableContainer>
				</Grid>
			)}
		</>
	)
}

export default OrderItems
