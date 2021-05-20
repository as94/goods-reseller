import React from 'react'
import AppBar from '@material-ui/core/AppBar'
import Button from '@material-ui/core/Button'
import InstagramIcon from '@material-ui/icons/Instagram'
import Card from '@material-ui/core/Card'
import CardActions from '@material-ui/core/CardActions'
import CardContent from '@material-ui/core/CardContent'
import CardMedia from '@material-ui/core/CardMedia'
import CssBaseline from '@material-ui/core/CssBaseline'
import Grid from '@material-ui/core/Grid'
import Toolbar from '@material-ui/core/Toolbar'
import Typography from '@material-ui/core/Typography'
import { makeStyles } from '@material-ui/core/styles'
import Container from '@material-ui/core/Container'
import Copyright from '../Copyright/Copyright'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faGift } from '@fortawesome/free-solid-svg-icons'
import { IconButton } from '@material-ui/core'
import PhoneIcon from '@material-ui/icons/Phone'

const useStyles = makeStyles(theme => ({
	icon: {
		marginRight: theme.spacing(2),
	},
	heroContent: {
		backgroundColor: theme.palette.background.paper,
		padding: theme.spacing(12, 0, 12),
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

	return (
		<>
			<CssBaseline />
			<AppBar position="relative">
				<Toolbar>
					<FontAwesomeIcon className={classes.icon} icon={faGift} />
					<Typography variant="h6" color="inherit" noWrap>
						Подарки для мужчин
					</Typography>
				</Toolbar>
			</AppBar>
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
				<div className={classes.heroContent}></div>
				<Container className={classes.cardGrid} maxWidth="md" id="sets">
					<Grid container spacing={4}>
						<Grid item key={1} xs={12} sm={6} md={4}>
							<Card className={classes.card}>
								<CardMedia className={classes.cardMedia} image="assets/set-1.jpg" title="Image title" />
								<CardContent className={classes.cardContent}>
									<Typography gutterBottom variant="h5" component="h2">
										Базовый мужской набор
									</Typography>
									<Typography>Для настоящих чистюль! 🧼</Typography>
								</CardContent>
								{/* <CardActions>
									<Button size="small" color="primary">
										Выбрать
									</Button>
								</CardActions> */}
							</Card>
						</Grid>
						<Grid item key={2} xs={12} sm={6} md={4}>
							<Card className={classes.card}>
								<CardMedia className={classes.cardMedia} image="assets/set-2.jpg" title="Image title" />
								<CardContent className={classes.cardContent}>
									<Typography gutterBottom variant="h5" component="h2">
										Стандартный мужской набор
									</Typography>
									<Typography>Для тех парней, которые любят вспомнить молодость! 🎮</Typography>
								</CardContent>
								{/* <CardActions>
									<Button size="small" color="primary">
										Выбрать
									</Button>
								</CardActions> */}
							</Card>
						</Grid>
						<Grid item key={3} xs={12} sm={6} md={4}>
							<Card className={classes.card}>
								<CardMedia className={classes.cardMedia} image="assets/set-3.jpg" title="Image title" />
								<CardContent className={classes.cardContent}>
									<Typography gutterBottom variant="h5" component="h2">
										Премиальный мужской набор
									</Typography>
									<Typography>Для стильных ребят! 🕶</Typography>
								</CardContent>
								{/* <CardActions>
									<Button size="small" color="primary">
										Выбрать
									</Button>
								</CardActions> */}
							</Card>
						</Grid>
					</Grid>
				</Container>
			</main>
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
