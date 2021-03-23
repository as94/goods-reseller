import React, { useCallback } from 'react'
import Table from '@material-ui/core/Table'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import Paper from '@material-ui/core/Paper'
import { makeStyles } from '@material-ui/core/styles'
import SupplyItem from './SupplyItem/SupplyItem'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import { SupplyItemContract } from '../../../Api/Supplies/contracts'
import { Box, Grid } from '@material-ui/core'
import DeleteIcon from '@material-ui/icons/Delete'
import Title from '../../Title'
import { useTranslation } from 'react-i18next'

export interface IOwnProps {
	simpleProducts: ProductListItemContract[]
	supplyItems: SupplyItemContract[]
	setSupplyItems: (supplyItems: SupplyItemContract[]) => void
}

const useStyles = makeStyles(theme => ({
	table: {
		minWidth: 650,
	},
	removeButton: {
		cursor: 'pointer',
	},
}))

const SupplyItems = ({ simpleProducts, supplyItems, setSupplyItems }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const addSupplyItem = useCallback(
		(supplyItem: SupplyItemContract) => {
			setSupplyItems([...supplyItems, supplyItem])
		},
		[supplyItems, setSupplyItems],
	)

	const removeSupplyItem = useCallback(
		(supplyItemId: string) => {
			setSupplyItems(supplyItems.filter(x => x.id !== supplyItemId))
		},
		[supplyItems, setSupplyItems],
	)

	return (
		<>
			<Box pt={2} pl={2}>
				<Title color="secondary">{t('SupplyItems')}</Title>
			</Box>
			<SupplyItem simpleProducts={simpleProducts} addSupplyItem={addSupplyItem} />
			{supplyItems.length > 0 && (
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
								{supplyItems.map((x, idx) => {
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
													onClick={() => removeSupplyItem(x.id)}
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

export default SupplyItems
