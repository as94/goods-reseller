import React from 'react'
import PropTypes from 'prop-types'
import { makeStyles } from '@material-ui/core/styles'
import Paper from '@material-ui/core/Paper'
import Typography from '@material-ui/core/Typography'
import Grid from '@material-ui/core/Grid'

const useStyles = makeStyles(theme => ({
	mainFeaturedPost: {
		position: 'relative',
		backgroundColor: theme.palette.grey[800],
		color: theme.palette.common.white,
		backgroundImage: 'url(assets/main-1.jpg)',
		backgroundSize: 'cover',
		backgroundRepeat: 'no-repeat',
		backgroundPosition: 'center',
		minHeight: '50.0vw',
	},
	mainFeaturedPostContent: {
		position: 'relative',
		padding: theme.spacing(3),
		[theme.breakpoints.up('md')]: {
			padding: theme.spacing(6),
		},
		backgroundColor: 'rgba(0,0,0,.6)',
		minHeight: '50.0vw',
	},
}))

const MainFeaturedPost = (props: any) => {
	const classes = useStyles()
	const { post } = props

	return (
		<Paper square className={classes.mainFeaturedPost} style={{ backgroundImage: `url(${post.image})` }}>
			{<img style={{ display: 'none' }} src={post.image} alt={post.imageText} />}
			<Grid container>
				<Grid item md={4}>
					<div className={classes.mainFeaturedPostContent}>
						<Typography component="h1" variant="h3" color="inherit" gutterBottom>
							{post.title}
						</Typography>
						<Typography variant="h5" color="inherit" paragraph>
							{post.description}
						</Typography>
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
