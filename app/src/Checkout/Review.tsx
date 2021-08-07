import React from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Typography from '@material-ui/core/Typography'
import List from '@material-ui/core/List'
import ListItem from '@material-ui/core/ListItem'
import ListItemText from '@material-ui/core/ListItemText'
import Grid from '@material-ui/core/Grid'
import { OrderInfoContract } from '../Api/Orders/contracts'
import { ProductListItemContract } from '../Api/Products/contracts'
import { DeliveryType } from './Checkout'

interface IOwnProps {
	productSet: ProductListItemContract
	productsInSet: ProductListItemContract[]
	orderInfo: OrderInfoContract
	deliveryType: DeliveryType
}

const useStyles = makeStyles(theme => ({
	listItem: {
		padding: theme.spacing(1, 0),
	},
	compositionItem: {
		padding: theme.spacing(1, 1),
	},
	total: {
		fontWeight: 700,
	},
	title: {
		marginTop: theme.spacing(2),
	},
}))

const Review = ({ productSet, productsInSet, orderInfo, deliveryType }: IOwnProps) => {
	const classes = useStyles()

	const orderItemsCost = orderInfo.orderItems.reduce(
		(acc, cur) => (acc += cur.unitPrice * (1 - cur.discountPerUnit) * cur.quantity),
		0,
	)

	const setCost = orderItemsCost + orderInfo.addedCost.value
	const deliveryCost = orderInfo.deliveryCost.value
	const totalCost = setCost + deliveryCost

	let deliveryInfo = 'Бесплатно'
	if (deliveryType === DeliveryType.ServiceWithinMoscowRingRoad) {
		deliveryInfo = `${deliveryCost} ₽`
	} else if (deliveryType === DeliveryType.ServiceOutsideMoscowRingRoad) {
		deliveryInfo = 'Обсуждается отдельно'
	}

	return (
		<>
			<Typography variant="h6" gutterBottom>
				Просмотр заказа
			</Typography>
			<List disablePadding>
				<ListItem className={classes.listItem} key={productSet.id}>
					<ListItemText primary={productSet.name} secondary={productSet.description} />
					<Typography variant="body2">{`${setCost} ₽`}</Typography>
				</ListItem>
				{productsInSet.map(product => (
					<ListItem className={classes.compositionItem} key={product.id}>
						<ListItemText primary={product.name} secondary={product.description} />
					</ListItem>
				))}
				<ListItem className={classes.listItem}>
					<ListItemText primary="Доставка" />
					<Typography variant="body2">{deliveryInfo}</Typography>
				</ListItem>
				<ListItem className={classes.listItem}>
					<ListItemText primary="Итого" />
					<Typography variant="subtitle1" className={classes.total}>
						{`${totalCost} ₽`}
					</Typography>
				</ListItem>
			</List>
			<Grid container spacing={2}>
				<Grid item xs={12} sm={6}>
					<Typography variant="h6" gutterBottom className={classes.title}>
						Адрес доставки
					</Typography>
					<Typography gutterBottom>{orderInfo.address.street}</Typography>
					<Typography gutterBottom>{orderInfo.address.zipCode}</Typography>
				</Grid>
				<Grid item container direction="column" xs={12} sm={6}>
					<Typography variant="h6" gutterBottom className={classes.title}>
						Контактная информация
					</Typography>
					<Grid container>
						<Grid item xs={6}>
							<Typography gutterBottom>{orderInfo.customerInfo.name}</Typography>
						</Grid>
						<Grid item xs={6}>
							<Typography gutterBottom>{orderInfo.customerInfo.phoneNumber}</Typography>
						</Grid>
					</Grid>
				</Grid>
			</Grid>
		</>
	)
}

export default Review
