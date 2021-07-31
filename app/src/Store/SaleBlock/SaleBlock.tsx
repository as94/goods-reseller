import React from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Paper from '@material-ui/core/Paper/Paper'
import Grid from '@material-ui/core/Grid/Grid'
import Typography from '@material-ui/core/Typography/Typography'

const useStyles = makeStyles(theme => ({
	saleBlockWithImage: {
		position: 'relative',
		color: theme.palette.common.white,
		marginBottom: theme.spacing(4),
		backgroundSize: 'cover',
		backgroundRepeat: 'no-repeat',
		backgroundPosition: 'center',
	},
	saleBlockWithoutImage: {
		position: 'relative',
		color: theme.palette.primary.main,
		marginBottom: theme.spacing(4),
		backgroundColor: 'white',
	},
	overlay: {
		position: 'absolute',
		top: 0,
		bottom: 0,
		right: 0,
		left: 0,
	},
	mainFeaturedPostContent: {
		position: 'relative',
		padding: theme.spacing(3),
		[theme.breakpoints.up('md')]: {
			padding: theme.spacing(6),
		},
	},
}))

interface IOwnProps {
	title: string
	body: string
	imagePath: string | null
}

export const SaleBlock = ({ title, body, imagePath }: IOwnProps) => {
	const classes = useStyles()

	return (
		<Paper
			className={imagePath ? classes.saleBlockWithImage : classes.saleBlockWithoutImage}
			style={{ backgroundImage: imagePath ? imagePath : undefined }}
		>
			<div className={classes.overlay} />
			<Grid container>
				<Grid item>
					<div className={classes.mainFeaturedPostContent}>
						<Typography component="h2" align="center" variant="h3" color="inherit" gutterBottom>
							{title}
						</Typography>
						<Typography variant="h6" align="center" color="inherit" paragraph>
							{body}
						</Typography>
					</div>
				</Grid>
			</Grid>
		</Paper>
	)
}
