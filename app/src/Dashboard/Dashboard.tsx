import { AppBar, Box, List, ListItemText, Paper, Toolbar } from '@material-ui/core'
import { CssBaseline, makeStyles } from '@material-ui/core'
import React, { useCallback, useEffect, useState } from 'react'
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
import { useHistory } from 'react-router-dom'
import { useAuth } from '../Hooks/useAuth'
import { ListItem } from '@material-ui/core'
import { ListItemIcon } from '@material-ui/core'
import DashboardIcon from '@material-ui/icons/Dashboard'
import ShoppingCartIcon from '@material-ui/icons/ShoppingCart'
import LocalMallIcon from '@material-ui/icons/LocalMall'
import ProductBlock from './Products/ProductBlock/ProductBlock'
import OrderBlock from './Orders/OrderBlock/OrderBlock'
import { ProductListItemContract } from '../Api/Products/contracts'
import productsApi from '../Api/Products/productsApi'

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
}))

const menuItems = {
	dashboard: 'dashboard',
	orders: 'orders',
	products: 'products',
}

const Dashboard = () => {
	const classes = useStyles()
	const [open, setOpen] = React.useState(true)
	const handleDrawerOpen = () => {
		setOpen(true)
	}
	const handleDrawerClose = () => {
		setOpen(false)
	}

	const [products, setProducts] = useState([] as ProductListItemContract[])
	const [selectedProductId, setSelectedProductId] = useState(null as string | null)
	const [showCreateProduct, setShowCreateProduct] = useState(false)

	const history = useHistory()
	const auth = useAuth()

	const signOut = useCallback(async () => {
		await auth.signOut()
		history.push('/')
	}, [auth.signOut])

	const getProducts = useCallback(async () => {
		if (!showCreateProduct && !selectedProductId) {
			const response = await productsApi.GetProductList()
			setProducts(response.items)
		}
	}, [setProducts, showCreateProduct, selectedProductId])

	const [selectedMenuItem, setSelectedMenuItem] = useState(menuItems.dashboard)

	useEffect(() => {
		getProducts()
	}, [getProducts])

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
							<ListItem
								button
								selected={selectedMenuItem === menuItems.dashboard}
								onClick={() => setSelectedMenuItem(menuItems.dashboard)}
							>
								<ListItemIcon>
									<DashboardIcon />
								</ListItemIcon>
								<ListItemText primary="Dashboard" />
							</ListItem>
							<ListItem
								button
								selected={selectedMenuItem === menuItems.orders}
								onClick={() => setSelectedMenuItem(menuItems.orders)}
							>
								<ListItemIcon>
									<ShoppingCartIcon />
								</ListItemIcon>
								<ListItemText primary="Orders" />
							</ListItem>
							<ListItem
								button
								selected={selectedMenuItem === menuItems.products}
								onClick={() => setSelectedMenuItem(menuItems.products)}
							>
								<ListItemIcon>
									<LocalMallIcon />
								</ListItemIcon>
								<ListItemText primary="Products" />
							</ListItem>
						</div>
					</List>
				</Drawer>
				<main className={classes.content}>
					<div className={classes.appBarSpacer} />
					<Container maxWidth="lg" className={classes.container}>
						<Grid container spacing={3}>
							{selectedMenuItem === menuItems.dashboard && (
								<div>
									<h1>Dashboard</h1> <p>Not implemented yet</p>
								</div>
							)}

							{selectedMenuItem === menuItems.orders && <OrderBlock products={products} />}
							{selectedMenuItem === menuItems.products && (
								<ProductBlock
									products={products}
									showCreateProduct={showCreateProduct}
									setShowCreateProduct={setShowCreateProduct}
									selectedProductId={selectedProductId}
									setSelectedProductId={setSelectedProductId}
								/>
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
