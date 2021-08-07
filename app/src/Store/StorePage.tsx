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
import { CardActions } from '@material-ui/core'
import { ProductListItemContract } from '../Api/Products/contracts'
import productsApi from '../Api/Products/productsApi'
import { useHistory } from 'react-router-dom'
import StoreHeader from '../StoreHeader/StoreHeader'
import StoreFooter from '../StoreFooter/StoreFooter'
import MainFeaturedPost from './MainFeaturedPost/MainFeaturedPost'
import Chip from '@material-ui/core/Chip'
import Paper from '@material-ui/core/Paper'
import Grow from '@material-ui/core/Grow'
import { Note } from './Note/Note'
import { SaleBlock } from './SaleBlock/SaleBlock'
import InfoBlock from './InfoBlock/InfoBlock'

const useStyles = makeStyles(theme => ({
	icon: {
		marginRight: theme.spacing(2),
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
	chip: {
		'& > span': {
			whiteSpace: 'break-spaces',
		},
		minHeight: '32px',
		height: 'auto',
		paddingTop: '6px',
		paddingBottom: '6px',
	},
	manSets: {
		marginTop: theme.spacing(4),
		marginBottom: theme.spacing(4),
	},
}))

const excludeProducts = ['Наполнитель для коробки', 'Подарочная коробка']

const maxProductImageShowTimeInSec = 3

const saleBlocks = [
	{
		title: 'Что подарить?',
		body: 'Иногда сложно придумать, что подарить своему знакомому мужчине на его день. Наш новый проект HAPPYBOXY решает эту проблему. Мы предлагаем интересные подарочные наборы. Все подарочные наборы составлялись мужчинами для мужчин, мы опрашивали многих парней на предмет желаемого подарка.',
		imagePath: 'url(assets/main-2.webp)',
	},
	{
		title: 'Классный подарок',
		body: 'С одной стороны, подарок должен быть уместен, полезен и доступен. С другой стороны, подарки должны вызывать эмоции, улыбку, приятное удивление и восторг. Мужчинам нравится, когда девушка обеспокоилась выбором подарка.',
		imagePath: null,
	},
	{
		title: 'Ему понравится',
		body: 'Наши подарки как раз создают атмосферу заботы. Мужчина сразу видит, что подарок составлен с любовью и со вкусом. Поверь, этот набор не оставит его равнодушным.',
		imagePath: 'url(assets/main-3.webp)',
	},
]

const infoBlocks = [
	{
		title: 'Кому мы хотим помочь?',
		body: 'Тем девушкам, которые устали думать о том, каким подарком можно удивить своего любимого мужчину. Мы полностью уверены в том, что такой подарок устроит даже самого привередливого.',
		imagePath: 'url(assets/main-4.webp)',
	},
	{
		title: 'Наши гарантии',
		body: 'Что делать, если что-то пошло не по плану? Например, тебя не устроило содержимое подарка при получении. В этом случае, мы гарантируем полный возврат денег.',
		imagePath: 'url(assets/main-5.webp)',
	},
]

interface ProductSetImages {
	[setId: string]: {
		originalSet: ProductListItemContract
		firstProduct: ProductListItemContract
		secondProduct: ProductListItemContract | null
		timeToBackToOriginalInSec: number | null
	}
}

const StorePage = () => {
	const classes = useStyles()
	const history = useHistory()
	const [products, setProducts] = useState([] as ProductListItemContract[])
	const [setList, setSetList] = useState([] as ProductListItemContract[])
	const [productSetImages, setProductSetImages] = useState({} as ProductSetImages)

	const getSetListHandler = useCallback(async () => {
		const result = await productsApi.GetProductList()
		setProducts(result.items)
	}, [setProducts, productsApi])

	const getSetComposition = useCallback(
		(setId: string) => {
			const set = products.find(x => x.id === setId)
			if (!set) {
				return []
			}
			return products
				.filter(x => set.productIds.includes(x.id))
				.filter(x => !excludeProducts.includes(x.name))
				.sort((a, b) => a.name.toLowerCase().localeCompare(b.name.toLowerCase()))
		},
		[products],
	)

	const getSetPrice = useCallback(
		(setId: string) => {
			const set = products.find(x => x.id === setId)
			if (!set) {
				return 0
			}

			const productsInSet = products.filter(x => set.productIds.includes(x.id))

			const orderItemsCost = productsInSet.reduce(
				(acc, cur) => (acc += cur.unitPrice * (1 - cur.discountPerUnit)),
				0,
			)

			return orderItemsCost + set.addedCost
		},
		[products],
	)

	const changeProductImageHandler = useCallback(
		(setId: string, product: ProductListItemContract) => {
			if (productSetImages[setId].firstProduct && productSetImages[setId].secondProduct) {
				setProductSetImages({
					...productSetImages,
					[setId]: {
						...productSetImages[setId],
						firstProduct: product,
						secondProduct: null,
						timeToBackToOriginalInSec: maxProductImageShowTimeInSec,
					},
				})
			} else {
				setProductSetImages({
					...productSetImages,
					[setId]: {
						...productSetImages[setId],
						secondProduct: product,
						timeToBackToOriginalInSec: maxProductImageShowTimeInSec,
					},
				})
			}
		},
		[productSetImages, setProductSetImages, maxProductImageShowTimeInSec],
	)

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

	useEffect(() => {
		if (Object.keys(productSetImages).length === 0) {
			let items = {}
			for (let index = 0; index < setList.length; index++) {
				const set = setList[index]
				items = {
					...items,
					[set.id]: {
						originalSet: set,
						firstProduct: set,
						secondProduct: null,
						timeToBackToOriginalInSec: null,
					},
				}
			}
			setProductSetImages(items)
		}
	}, [setList, setProductSetImages, productSetImages])

	useEffect(() => {
		const interval = setInterval(() => {
			Object.keys(productSetImages).map(key => {
				const productSetImage = productSetImages[key]
				if (productSetImage.timeToBackToOriginalInSec !== null) {
					if (productSetImage.timeToBackToOriginalInSec === 0) {
						productSetImage.firstProduct = productSetImage.originalSet
						productSetImage.secondProduct = null
						productSetImage.timeToBackToOriginalInSec = null
					} else {
						productSetImage.timeToBackToOriginalInSec--
					}

					setProductSetImages({ ...productSetImages, [key]: productSetImage })
				}
			})
		}, 1000)
		return () => clearInterval(interval)
	}, [productSetImages, setProductSetImages])

	return (
		<>
			<CssBaseline />
			<StoreHeader />
			<main>
				<MainFeaturedPost />
				<Container className={classes.cardGrid} maxWidth="lg">
					{saleBlocks.map(saleBlock => (
						<SaleBlock {...saleBlock} />
					))}
				</Container>

				<InfoBlock {...infoBlocks[0]} />
				<Container className={classes.cardGrid} maxWidth="lg" id="setList">
					<Typography
						className={classes.manSets}
						component="h2"
						align="center"
						variant="h3"
						color="inherit"
						gutterBottom
					>
						Мужские подарочные наборы
					</Typography>
					<Grid container spacing={4}>
						{setList.map(x => (
							<Grid item key={x.id} xs={12} sm={6} md={4}>
								<Card className={classes.card}>
									{productSetImages[x.id] && productSetImages[x.id].secondProduct !== null && (
										<Grow
											in={productSetImages[x.id].secondProduct !== null}
											style={{ transformOrigin: '0 0 0' }}
											{...{ timeout: 1000 }}
										>
											<Paper elevation={4}>
												<CardMedia
													className={classes.cardMedia}
													image={
														productSetImages[x.id].secondProduct?.photoPath
															? `assets/${
																	productSetImages[x.id].secondProduct?.photoPath
															  }`
															: 'assets/noImageAvailable.svg'
													}
													title={x.description}
												/>
											</Paper>
										</Grow>
									)}
									{productSetImages[x.id] &&
										productSetImages[x.id].firstProduct !== null &&
										productSetImages[x.id].secondProduct === null && (
											<Grow
												in={
													productSetImages[x.id].firstProduct !== null &&
													productSetImages[x.id].secondProduct === null
												}
												style={{ transformOrigin: '0 0 0' }}
												{...{ timeout: 1000 }}
											>
												<Paper elevation={4}>
													<CardMedia
														className={classes.cardMedia}
														image={
															productSetImages[x.id].firstProduct.photoPath
																? `assets/${
																		productSetImages[x.id].firstProduct.photoPath
																  }`
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
											{getSetComposition(x.id).map(y => (
												<Chip
													key={y.id}
													className={classes.chip}
													clickable
													label={y.name}
													variant="outlined"
													onClick={() => changeProductImageHandler(x.id, y)}
												/>
											))}
										</div>
									</CardContent>
									<CardActions
										style={{ justifyContent: 'space-between', padding: '10px 20px 10px 10px' }}
									>
										<Button
											color="primary"
											size="small"
											variant="contained"
											onClick={() => history.push(`/store/checkout/${x.id}`)}
										>
											Хочу этот
										</Button>
										<Typography variant="h5">{getSetPrice(x.id)} ₽</Typography>
									</CardActions>
								</Card>
							</Grid>
						))}
					</Grid>
				</Container>
				<InfoBlock {...infoBlocks[1]} />
			</main>
			<Note />
			<StoreFooter />
		</>
	)
}

export default StorePage
