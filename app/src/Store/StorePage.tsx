import React, { useCallback, useEffect, useState } from 'react'
import Button from '@material-ui/core/Button'
import InstagramIcon from '@material-ui/icons/Instagram'
import Card from '@material-ui/core/Card'
import CardContent from '@material-ui/core/CardContent'
import CardMedia from '@material-ui/core/CardMedia'
import CssBaseline from '@material-ui/core/CssBaseline'
import Grid from '@material-ui/core/Grid'
import Typography from '@material-ui/core/Typography'
import { makeStyles } from '@material-ui/core/styles'
import Container from '@material-ui/core/Container'
import Copyright from '../Copyright/Copyright'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faGift } from '@fortawesome/free-solid-svg-icons'
import { CardActions, IconButton, List, ListItem, ListItemText, Popover } from '@material-ui/core'
import PhoneIcon from '@material-ui/icons/Phone'
import { ProductListItemContract } from '../Api/Products/contracts'
import productsApi from '../Api/Products/productsApi'
import { useHistory } from 'react-router-dom'
import StoreHeader from '../StoreHeader/StoreHeader'

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
		paddingTop: '56.25%', // 16:9
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
}))

const StorePage = () => {
	const classes = useStyles()
	const history = useHistory()
	const [products, setProducts] = useState([] as ProductListItemContract[])
	const [setList, setSetList] = useState([] as ProductListItemContract[])
	const [anchorEl, setAnchorEl] = useState(null)
	const [selectedSetId, setSelectedSetId] = useState(null as string | null)

	const open = Boolean(anchorEl)

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

	const showSetCompositionHandler = useCallback(
		(event: any, setId: string) => {
			setAnchorEl(event.currentTarget)
			setSelectedSetId(setId)
		},
		[setSelectedSetId],
	)

	const hideSetCompositionHandler = useCallback(event => {
		setAnchorEl(null)
	}, [])

	const getPopoverContent = useCallback(() => {
		if (!selectedSetId) {
			return null
		}
		const set = products.find(x => x.id === selectedSetId)
		if (!set) {
			return null
		}
		const setProducts = products
			.filter(x => set.productIds.includes(x.id))
			.filter(x => x.name !== 'Наполнитель для коробки' && x.name !== 'Подарочная коробка')
			.sort((a, b) => a.name.toLowerCase().localeCompare(b.name.toLowerCase()))
		return (
			<List dense>
				{setProducts.map(x => (
					<ListItem>
						<ListItemText primary={x.name} />
					</ListItem>
				))}
			</List>
		)
	}, [selectedSetId])

	return (
		<>
			<CssBaseline />
			<StoreHeader />
			<main>
				<div className={classes.heroContent}>
					<Container maxWidth="sm">
						<Typography component="h1" variant="h2" align="center" color="textPrimary" gutterBottom>
							Мужские подарочные наборы
						</Typography>
						<Typography variant="h6" align="center" color="textSecondary" paragraph>
							Если вам самим надоело думать, что подарить своёму знакомому мужчине, тогда...
						</Typography>
						<Typography variant="h6" align="center" color="textSecondary" paragraph>
							Перестаньте думать об этом!
						</Typography>
						<Typography variant="h6" align="center" color="textSecondary" paragraph>
							Мы всё придумали. Осталось выбрать подходящий вариант подарочного набора и сделать заказ.
						</Typography>
						<div className={classes.heroButtons}>
							<Grid container spacing={2} justify="center">
								<Grid item>
									<Button variant="contained" color="primary" href="#sets">
										К наборам
									</Button>
								</Grid>
								{/* <Grid item>
									<Button variant="outlined" color="primary">
										Сделать заказ
									</Button>
								</Grid> */}
							</Grid>
						</div>
					</Container>
				</div>
				<Container className={classes.cardGrid} maxWidth="md" id="sets">
					<Grid container spacing={4}>
						{setList.map((x, idx) => (
							<Grid item key={x.id} xs={12} sm={6} md={4}>
								<Card className={classes.card}>
									<CardMedia
										className={classes.cardMedia}
										image={`assets/set-${idx + 1}.jpg`}
										title="Image title"
									/>
									<CardContent className={classes.cardContent}>
										<Typography gutterBottom variant="h5" component="h2">
											{x.name}
										</Typography>
										<Typography>{x.description}</Typography>
									</CardContent>
									<CardActions>
										<Button
											size="small"
											variant="outlined"
											color="primary"
											onClick={e => showSetCompositionHandler(e, x.id)}
										>
											Состав
										</Button>
										<Popover
											id={open ? `${x.id}-popover` : undefined}
											open={open}
											anchorEl={anchorEl}
											onClose={hideSetCompositionHandler}
											anchorOrigin={{
												vertical: 'bottom',
												horizontal: 'center',
											}}
											transformOrigin={{
												vertical: 'top',
												horizontal: 'left',
											}}
										>
											{getPopoverContent()}
										</Popover>
										<Button
											size="small"
											variant="outlined"
											color="primary"
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
						Примичание: некоторые товары можно заменить аналогичными. Например: подписку на Play Station 4,
						из стандартного набора, можно заменить на Xbox One. После оформления заказа, мы расскажем все
						подробности.
					</Typography>
				</Container>
			</div>
			<footer className={classes.footer}>
				<Typography variant="h6" align="center" gutterBottom>
					Наши контакты
				</Typography>
				<div className={classes.phoneContacts}>
					<PhoneIcon />
					<Typography
						variant="subtitle1"
						align="center"
						color="textSecondary"
						component="p"
						className={classes.phoneNumber}
					>
						+7 923 123 19 99
					</Typography>
				</div>
				<Typography variant="subtitle1" align="center" color="textSecondary" component="p">
					<IconButton href="https://www.instagram.com/happyboxy.msk/">
						<InstagramIcon />
					</IconButton>
				</Typography>

				<Copyright />
			</footer>
		</>
	)
}

export default StorePage
