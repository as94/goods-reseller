import React, { useCallback, useEffect, useState } from 'react'
import Button from '@material-ui/core/Button'
import Card from '@material-ui/core/Card'
import CardContent from '@material-ui/core/CardContent'
import CardMedia from '@material-ui/core/CardMedia'
import CssBaseline from '@material-ui/core/CssBaseline'
import Grid from '@material-ui/core/Grid'
import Typography from '@material-ui/core/Typography'
import { makeStyles } from '@material-ui/core/styles'
import Container from '@material-ui/core/Container'
import { CardActions, IconButton, List, ListItem, ListItemText, Popover } from '@material-ui/core'
import { ProductListItemContract } from '../Api/Products/contracts'
import productsApi from '../Api/Products/productsApi'
import { useHistory } from 'react-router-dom'
import StoreHeader from '../StoreHeader/StoreHeader'
import StoreFooter from '../StoreFooter/StoreFooter'
import MainFeaturedPost from './MainFeaturedPost/MainFeaturedPost'
import Chip from '@material-ui/core/Chip'
import Paper from '@material-ui/core/Paper'
import Grow from '@material-ui/core/Grow'

const useStyles = makeStyles(theme => ({
	icon: {
		marginRight: theme.spacing(2),
	},
	heroContent: {
		backgroundColor: theme.palette.background.paper,
		padding: theme.spacing(12, 0, 12),
	},
	heroContentNote: {
		backgroundColor: theme.palette.background.paper,
		padding: theme.spacing(6, 0, 6),
	},
	heroButtons: {
		marginTop: theme.spacing(4),
	},
	cardGrid: {
		paddingTop: theme.spacing(8),
		paddingBottom: theme.spacing(8),
	},
	card: {
		height: '100%',
		display: 'flex',
		flexDirection: 'column',
	},
	cardMedia: {
		paddingTop: '100%',
		fill: theme.palette.common.white,
		stroke: theme.palette.divider,
		strokeWidth: 1,
	},
	cardContent: {
		flexGrow: 1,
	},
	footer: {
		backgroundColor: theme.palette.background.paper,
		padding: theme.spacing(6),
	},
	phoneContacts: {
		display: 'flex',
		justifyContent: 'center',
	},
	phoneNumber: {
		paddingLeft: '10px',
	},
	setComposition: {
		display: 'flex',
		justifyContent: 'left',
		flexWrap: 'wrap',
		'& > *': {
			margin: theme.spacing(0.5),
			cursor: 'pointer',
		},
	},
}))

const mainFeaturedPost = {
	title: 'Оригинальный подарок для мужчин',
	description:
		'Ищете подарочный набор для друга или парня? Тогда вы на правильном пути! Здесь вы сможете выбрать подходящий вариант подарочного набора и купить подарок мужчине.',
	image: 'assets/main-1.jpg',
	imgText: 'Подходящие варианты подарочных наборов для мужчины, которые можно купить в Москве',
}

const excludeProducts = ['Наполнитель для коробки', 'Подарочная коробка']

const StorePage = () => {
	const classes = useStyles()
	const history = useHistory()
	const [products, setProducts] = useState([] as ProductListItemContract[])
	const [setList, setSetList] = useState([] as ProductListItemContract[])
	const [selectedProductImageForSet, setSelectedProductImageForSet] = useState(
		null as { setId: string; productImage: string } | null,
	)

	const getSetListHandler = useCallback(async () => {
		const result = await productsApi.GetProductList()
		setProducts(result.items)
	}, [setProducts, productsApi])

	useEffect(() => {
		setSetList(
			products
				.filter(x => x.isSet)
				.sort((a, b) => {
					if (a.date > b.date) {
						return 1
					} else if (a.date < b.date) {
						return -1
					} else {
						return 0
					}
				}),
		)
	}, [products, setSetList])

	useEffect(() => {
		getSetListHandler()
	}, [getSetListHandler])

	const getSetComposition = useCallback(
		(setId: string) => {
			const set = products.find(x => x.id === setId)
			if (!set) {
				return null
			}
			return products
				.filter(x => set.productIds.includes(x.id))
				.filter(x => !excludeProducts.includes(x.name))
				.sort((a, b) => a.name.toLowerCase().localeCompare(b.name.toLowerCase()))
		},
		[products],
	)

	console.log('selectedProductImageForSet', selectedProductImageForSet)

	return (
		<>
			<CssBaseline />
			<StoreHeader />
			<main>
				<MainFeaturedPost post={mainFeaturedPost} />
				<Container className={classes.cardGrid} maxWidth="md" id="sets">
					<Grid container spacing={4}>
						{setList.map(x => (
							<Grid item key={x.id} xs={12} sm={6} md={4}>
								<Card className={classes.card}>
									{selectedProductImageForSet && selectedProductImageForSet.setId === x.id && (
										<Grow
											in={
												selectedProductImageForSet !== null &&
												selectedProductImageForSet.setId === x.id
											}
											style={{ transformOrigin: '0 0 0' }}
											{...{ timeout: 2000 }}
										>
											<Paper elevation={4}>
												<CardMedia
													className={classes.cardMedia}
													image={
														selectedProductImageForSet.productImage
															? `assets/${selectedProductImageForSet.productImage}`
															: 'assets/noImageAvailable.svg'
													}
													title={x.description}
												/>
											</Paper>
										</Grow>
									)}
									{(!selectedProductImageForSet || selectedProductImageForSet.setId !== x.id) && (
										<Grow
											in={
												!selectedProductImageForSet || selectedProductImageForSet.setId !== x.id
											}
											style={{ transformOrigin: '0 0 0' }}
											{...{ timeout: 2000 }}
										>
											<Paper elevation={4}>
												<CardMedia
													className={classes.cardMedia}
													image={
														x.photoPath
															? `assets/${x.photoPath}`
															: 'assets/noImageAvailable.svg'
													}
													title={x.description}
												/>
											</Paper>
										</Grow>
									)}
									<CardContent className={classes.cardContent}>
										<Typography gutterBottom variant="h5" component="h2">
											{x.name}
										</Typography>
										<Typography>{x.description}</Typography>
										<div className={classes.setComposition}>
											{getSetComposition(x.id)?.map(y => (
												<Chip
													clickable
													label={y.name}
													variant="outlined"
													onMouseEnter={() =>
														setSelectedProductImageForSet({
															setId: x.id,
															productImage: y.photoPath,
														})
													}
													onMouseLeave={() => setSelectedProductImageForSet(null)}
												/>
											))}
										</div>
									</CardContent>
									<CardActions>
										<Button
											color="primary"
											size="small"
											variant="contained"
											onClick={() => history.push(`/store/checkout/${x.id}`)}
										>
											Хочу этот
										</Button>
									</CardActions>
								</Card>
							</Grid>
						))}
					</Grid>
				</Container>
			</main>
			<div className={classes.cardGrid}>
				<Container maxWidth="md">
					<Typography variant="body1" align="left" color="textSecondary" paragraph>
						Примечание: некоторые товары можно заменить аналогичными. Например: подписку на Play Station 4,
						из стандартного набора, можно заменить на Xbox One. После оформления заказа мы расскажем все
						подробности.
					</Typography>
				</Container>
			</div>
			<StoreFooter />
		</>
	)
}

export default StorePage
