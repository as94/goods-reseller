import { Grid, Paper } from '@material-ui/core'
import React, { useCallback, useState } from 'react'
import SupplyList from '../SupplyList/SupplyList'
import { makeStyles } from '@material-ui/core/styles'
import { SupplyListItemContract } from '../../../Api/Supplies/contracts'
import CreateSupply from '../CreateSupply/CreateSupply'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import Supply from '../Supply/Supply'

interface IOwnProps {
	products: ProductListItemContract[]
}

const useStyles = makeStyles(theme => ({
	paper: {
		padding: theme.spacing(2),
		display: 'flex',
		overflow: 'auto',
		flexDirection: 'column',
	},
}))

const SupplyBlock = ({ products }: IOwnProps) => {
	const classes = useStyles()
	const [supplies, setSupplies] = useState([] as SupplyListItemContract[])

	const [selectedSupplyId, setSelectedSupplyId] = useState(null as string | null)
	const [showCreateSupply, setShowCreateSupply] = useState(false)
	const supplyHideHandler = useCallback(() => setSelectedSupplyId(null), [setSelectedSupplyId])
	const createSupplyShowHandler = useCallback(() => setShowCreateSupply(true), [setShowCreateSupply])
	const createSupplyHideHandler = useCallback(() => setShowCreateSupply(false), [setShowCreateSupply])

	return (
		<>
			{!selectedSupplyId && !showCreateSupply && (
				<Grid item xs={12}>
					<Paper className={classes.paper}>
						<SupplyList
							supplies={supplies}
							setSupplies={setSupplies}
							setSelectedSupplyId={setSelectedSupplyId}
							showCreateSupply={createSupplyShowHandler}
						/>
					</Paper>
				</Grid>
			)}
			{showCreateSupply && (
				<Grid item xs={12}>
					<Paper className={classes.paper}>
						<CreateSupply hide={createSupplyHideHandler} />
					</Paper>
				</Grid>
			)}

			{selectedSupplyId && (
				<Grid item xs={12}>
					{' '}
					<Paper className={classes.paper}>
						{' '}
						<Supply supplyId={selectedSupplyId} products={products} hide={supplyHideHandler} />
					</Paper>
				</Grid>
			)}
		</>
	)
}

export default SupplyBlock
