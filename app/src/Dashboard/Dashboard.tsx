import { AppBar, Box, List, ListItemText, Paper, Toolbar } from '@material-ui/core'
import { CssBaseline, makeStyles } from '@material-ui/core'
import React, { useCallback, useState } from 'react'
import clsx from 'clsx'
import { IconButton } from '@material-ui/core'
import MenuIcon from '@material-ui/icons/Menu'
import ExitToAppIcon from '@material-ui/icons/ExitToApp'
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft'
import { Typography } from '@material-ui/core'
import { Drawer } from '@material-ui/core'
import { Divider } from '@material-ui/core'
import { Container } from '@material-ui/core'
import { Grid } from '@material-ui/core'
import Copyright from '../Copyright/Copyright'
import Orders from './Orders'
import { useHistory } from 'react-router-dom'
import { useAuth } from '../Hooks/useAuth'
import { ListItem } from '@material-ui/core'
import { ListItemIcon } from '@material-ui/core'
import DashboardIcon from '@material-ui/icons/Dashboard'
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart'
import PeopleIcon from '@material-ui/icons/People'
import BarChartIcon from '@material-ui/icons/BarChart'
import LayersIcon from '@material-ui/icons/Layers'
import Products from './Products/Products'
import Product from './Products/Product/Product'
import CreateProduct from './Products/CreateProduct/CreateProduct'

const drawerWidth = 240

const useStyles = makeStyles(theme => ({
	root: {
		display: 'flex',
	},
	toolbar: {
		paddingRight: 24, // keep right padding when drawer closed
	},
	toolbarIcon: {
		display: 'flex',
		alignItems: 'center',
		justifyContent: 'flex-end',
		padding: '0 8px',
		...theme.mixins.toolbar,
	},
	appBar: {
		zIndex: theme.zIndex.drawer + 1,
		transition: theme.transitions.create(['width', 'margin'], {
			easing: theme.transitions.easing.sharp,
			duration: theme.transitions.duration.leavingScreen,
		}),
	},
	appBarShift: {
		marginLeft: drawerWidth,
		width: `calc(100% - ${drawerWidth}px)`,
		transition: theme.transitions.create(['width', 'margin'], {
			easing: theme.transitions.easing.sharp,
			duration: theme.transitions.duration.enteringScreen,
		}),
	},
	menuButton: {
		marginRight: 36,
	},
	menuButtonHidden: {
		display: 'none',
	},
	title: {
		flexGrow: 1,
	},
	drawerPaper: {
		position: 'relative',
		whiteSpace: 'nowrap',
		width: drawerWidth,
		transition: theme.transitions.create('width', {
			easing: theme.transitions.easing.sharp,
			duration: theme.transitions.duration.enteringScreen,
		}),
	},
	drawerPaperClose: {
		overflowX: 'hidden',
		transition: theme.transitions.create('width', {
			easing: theme.transitions.easing.sharp,
			duration: theme.transitions.duration.leavingScreen,
		}),
		width: theme.spacing(7),
		[theme.breakpoints.up('sm')]: {
			width: theme.spacing(9),
		},
	},
	appBarSpacer: theme.mixins.toolbar,
	content: {
		flexGrow: 1,
		minHeight: '100vh',
		overflow: 'auto',
	},
	container: {
		paddingTop: theme.spacing(4),
		paddingBottom: theme.spacing(4),
	},
	paper: {
		padding: theme.spacing(2),
		display: 'flex',
		overflow: 'auto',
		flexDirection: 'column',
	},
	fixedHeight: {
		height: 240,
	},
}))

const Dashboard = () => {
	const classes = useStyles()
	const [open, setOpen] = React.useState(true)
	const handleDrawerOpen = () => {
		setOpen(true)
	}
	const handleDrawerClose = () => {
		setOpen(false)
	}

	const history = useHistory()
	const auth = useAuth()

	const signOut = useCallback(async () => {
		await auth.signOut()
		history.push('/')
	}, [auth.signOut])
	// const fixedHeightPaper = clsx(classes.paper, classes.fixedHeight)

	const [selectedProductId, setSelectedProductId] = useState(null as string | null)
	const [showCreateProduct, setShowCreateProduct] = useState(false)
	const productHideHandler = useCallback(() => setSelectedProductId(null), [setSelectedProductId])
	const createProductShowHandler = useCallback(() => setShowCreateProduct(true), [setShowCreateProduct])
	const createProductHideHandler = useCallback(() => setShowCreateProduct(false), [setShowCreateProduct])

	return (
		auth.user && (
			<div className={classes.root}>
				<CssBaseline />
				<AppBar position="absolute" className={clsx(classes.appBar, open && classes.appBarShift)}>
					<Toolbar className={classes.toolbar}>
						<IconButton
							edge="start"
							color="inherit"
							aria-label="open drawer"
							onClick={handleDrawerOpen}
							className={clsx(classes.menuButton, open && classes.menuButtonHidden)}
						>
							<MenuIcon />
						</IconButton>
						<Typography component="h1" variant="h6" color="inherit" noWrap className={classes.title}>
							Dashboard
						</Typography>
						<Typography component="span" color="inherit" noWrap>
							{auth.user.email}
						</Typography>
						<IconButton color="inherit" onClick={signOut}>
							<ExitToAppIcon />
						</IconButton>
					</Toolbar>
				</AppBar>
				<Drawer
					variant="permanent"
					classes={{
						paper: clsx(classes.drawerPaper, !open && classes.drawerPaperClose),
					}}
					open={open}
				>
					<div className={classes.toolbarIcon}>
						<IconButton onClick={handleDrawerClose}>
							<ChevronLeftIcon />
						</IconButton>
					</div>
					<Divider />
					<List>
						<div>
							<ListItem button>
								<ListItemIcon>
									<DashboardIcon />
								</ListItemIcon>
								<ListItemText primary="Dashboard" />
							</ListItem>
							<ListItem button>
								<ListItemIcon>
									<ShoppingCartIcon />
								</ListItemIcon>
								<ListItemText primary="Orders" />
							</ListItem>
							<ListItem button>
								<ListItemIcon>
									<PeopleIcon />
								</ListItemIcon>
								<ListItemText primary="Customers" />
							</ListItem>
							{/* <ListItem button>
								<ListItemIcon>
									<BarChartIcon />
								</ListItemIcon>
								<ListItemText primary="Reports" />
							</ListItem> */}
						</div>
					</List>
				</Drawer>
				<main className={classes.content}>
					<div className={classes.appBarSpacer} />
					<Container maxWidth="lg" className={classes.container}>
						<Grid container spacing={3}>
							{/* Chart */}
							{/* <Grid item xs={12} md={8} lg={9}>
							<Paper className={fixedHeightPaper}>
								<Chart />
							</Paper>
						</Grid> */}
							{/* Recent Deposits */}
							{/* <Grid item xs={12} md={4} lg={3}>
							<Paper className={fixedHeightPaper}>
								<Deposits />
							</Paper>
						</Grid> */}
							{/* Recent Orders */}
							{/* <Grid item xs={12}>
								<Paper className={classes.paper}>
									<Orders />
								</Paper>
							</Grid> */}

							{!selectedProductId && !showCreateProduct && (
								<Grid item xs={12}>
									<Paper className={classes.paper}>
										<Products
											setSelectedProductId={setSelectedProductId}
											showCreateProduct={createProductShowHandler}
										/>
									</Paper>
								</Grid>
							)}

							{showCreateProduct && (
								<Grid item xs={12}>
									<Paper className={classes.paper}>
										<CreateProduct hide={createProductHideHandler} />
									</Paper>
								</Grid>
							)}

							{selectedProductId && (
								<Grid item xs={12}>
									{' '}
									<Paper className={classes.paper}>
										{' '}
										<Product productId={selectedProductId} hide={productHideHandler} />
									</Paper>
								</Grid>
							)}
						</Grid>
						<Box pt={4}>
							<Copyright />
						</Box>
					</Container>
				</main>
			</div>
		)
	)
}

export default Dashboard
