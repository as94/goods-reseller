import React from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Typography from '@material-ui/core/Typography'
import List from '@material-ui/core/List'
import ListItem from '@material-ui/core/ListItem'
import ListItemText from '@material-ui/core/ListItemText'
import Grid from '@material-ui/core/Grid'

const products = [
	{
		name: 'Мужской набор "Стандартный"',
		desc: 'Для тех парней, которые любят вспомнить молодость! 🎮',
		price: '3700 ₽',
	},
	{ name: 'Доставка', desc: '', price: '300 ₽' },
]

const useStyles = makeStyles(theme => ({
	listItem: {
		padding: theme.spacing(1, 0),
	},
	total: {
		fontWeight: 700,
	},
	title: {
		marginTop: theme.spacing(2),
	},
}))

const Review = () => {
	const classes = useStyles()

	return (
		<>
			<Typography variant="h6" gutterBottom>
				Просмотр заказа
			</Typography>
			<List disablePadding>
				{products.map(product => (
					<ListItem className={classes.listItem} key={product.name}>
						<ListItemText primary={product.name} secondary={product.desc} />
						<Typography variant="body2">{product.price}</Typography>
					</ListItem>
				))}
				<ListItem className={classes.listItem}>
					<ListItemText primary="Итого" />
					<Typography variant="subtitle1" className={classes.total}>
						4000 ₽
					</Typography>
				</ListItem>
			</List>
			<Grid container spacing={2}>
				<Grid item xs={12} sm={6}>
					<Typography variant="h6" gutterBottom className={classes.title}>
						Адрес доставки
					</Typography>
					<Typography gutterBottom>Красная 94, 23а</Typography>
					<Typography gutterBottom>119154</Typography>
				</Grid>
				<Grid item container direction="column" xs={12} sm={6}>
					<Typography variant="h6" gutterBottom className={classes.title}>
						Контактная информация
					</Typography>
					<Grid container>
						<Grid item xs={6}>
							<Typography gutterBottom>Василий</Typography>
						</Grid>
						<Grid item xs={6}>
							<Typography gutterBottom>+7 922 111 56 78</Typography>
						</Grid>
					</Grid>
				</Grid>
			</Grid>
		</>
	)
}

export default Review
