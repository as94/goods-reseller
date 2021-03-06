import React from 'react'
import PropTypes from 'prop-types'
import { makeStyles } from '@material-ui/core/styles'
import Paper from '@material-ui/core/Paper'
import Typography from '@material-ui/core/Typography'
import Grid from '@material-ui/core/Grid'
import { Button } from '@material-ui/core'
import useMediaQuery from '@material-ui/core/useMediaQuery'

const useStyles = makeStyles(theme => ({
	mainFeaturedPost: {
		position: 'relative',
		backgroundColor: theme.palette.grey[800],
		color: theme.palette.common.white,
		backgroundImage: 'url(assets/main-1.webp)',
		backgroundSize: 'cover',
		backgroundRepeat: 'no-repeat',
		backgroundPosition: 'center',
		minHeight: '95.0vh',
	},
	mainFeaturedPostContent: {
		padding: theme.spacing(3),
		[theme.breakpoints.up('md')]: {
			padding: theme.spacing(6),
		},
		backgroundColor: 'rgba(1,87,155 ,.6)',
		minHeight: '95.0vh',
	},
	setButton: {
		color: theme.palette.common.black,
		backgroundColor: theme.palette.common.white,
		marginTop: theme.spacing(3),
	},
}))

const MainFeaturedPost = () => {
	const classes = useStyles()
	const matches = useMediaQuery('(max-width:360px)')

	return (
		<Paper className={classes.mainFeaturedPost}>
			<Grid container>
				<Grid item md={4}>
					<div className={classes.mainFeaturedPostContent}>
						<Typography component="h1" variant={matches ? 'h4' : 'h3'} color="inherit" gutterBottom>
							Оригинальный подарок мужчине
						</Typography>
						<Typography variant={matches ? 'h6' : 'h5'} color="inherit" paragraph>
							Ищешь подарочный набор для друга или близкого молодого человека? Тогда ты на правильном
							пути! У нас можно заказать подарок мужчине.
						</Typography>
						<Button
							size={matches ? 'small' : 'large'}
							variant="contained"
							className={classes.setButton}
							href="#setList"
						>
							Перейти к подарочным наборам
						</Button>
					</div>
				</Grid>
			</Grid>
		</Paper>
	)
}

MainFeaturedPost.propTypes = {
	post: PropTypes.object,
}

export default MainFeaturedPost
