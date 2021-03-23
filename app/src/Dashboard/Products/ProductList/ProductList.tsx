import React, { useCallback } from 'react'
import { DataGrid, RowParams } from '@material-ui/data-grid'
import Title from '../../Title'
import { ProductListItemContract } from '../../../Api/Products/contracts'
import { Button } from '@material-ui/core'
import { makeStyles } from '@material-ui/core/styles'
import './ProductList.css'
import { useTranslation } from 'react-i18next'

const useStyles = makeStyles(theme => ({
	header: {
		display: 'flex',
		justifyContent: 'space-between',
		marginBottom: theme.spacing(2),
		marginLeft: theme.spacing(1),
	},
}))

interface IOwnProps {
	products: ProductListItemContract[]
	setSelectedProductId: (selectedProductId: string) => void
	showCreateProduct: () => void
}

const ProductList = ({ products, setSelectedProductId, showCreateProduct }: IOwnProps) => {
	const { t } = useTranslation()
	const classes = useStyles()

	const columns = [
		{ field: 'date', headerName: t('Date'), width: 200 },
		{ field: 'name', headerName: t('ProductName'), width: 200 },
		{ field: 'label', headerName: t('Label'), width: 200 },
		{ field: 'unitPrice', type: 'number', headerName: t('UnitPrice'), width: 200 },
		{ field: 'discountPerUnit', type: 'number', headerName: t('DiscountPerUnit'), width: 200 },
		{ field: 'isSet', type: 'string', headerName: t('IsSet'), width: 200 },
	]

	const productClickHandler = useCallback(
		(param: RowParams) => {
			setSelectedProductId(param.row.id.toString())
		},
		[setSelectedProductId],
	)

	const showCreateProductHandler = useCallback(() => showCreateProduct(), [showCreateProduct])

	return (
		<React.Fragment>
			<div className={classes.header}>
				<Title color="primary">{t('Products')}</Title>
				<Button variant="contained" color="primary" onClick={showCreateProductHandler}>
					{t('Create')}
				</Button>
			</div>
			<div style={{ height: 650, width: '100%' }}>
				<DataGrid
					disableColumnMenu={true}
					rows={products.map(x => ({
						...x,
						discountPerUnit: x.discountPerUnit * 100,
						date: new Date(x.date).toLocaleString(),
						isSet: x.isSet ? t('true') : t('false'),
					}))}
					columns={columns}
					pageSize={10}
					onRowClick={productClickHandler}
				/>
			</div>
		</React.Fragment>
	)
}

export default ProductList
