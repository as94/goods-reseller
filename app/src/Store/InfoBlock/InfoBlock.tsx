import React from 'react'
import PropTypes from 'prop-types'
import { makeStyles } from '@material-ui/core/styles'
import Paper from '@material-ui/core/Paper'
import Typography from '@material-ui/core/Typography'
import Grid from '@material-ui/core/Grid'

const useStyles = makeStyles(theme => ({
	infoBlockWithImage: {
		position: 'relative',
		color: theme.palette.common.black,
		marginBottom: theme.spacing(4),
		backgroundSize: 'cover',
		backgroundRepeat: 'no-repeat',
		backgroundPosition: 'center',
	},
	infoBlockWithoutImage: {
		position: 'relative',
		color: theme.palette.primary.main,
		marginBottom: theme.spacing(4),
		backgroundColor: 'white',
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
	body: string | null
	textItems: any[] | null
	imagePath: string | null
}

const InfoBlock = ({ title, body, textItems, imagePath }: IOwnProps) => {
	const classes = useStyles()

	return (
		<Paper
			className={imagePath ? classes.infoBlockWithImage : classes.infoBlockWithoutImage}
			style={{ backgroundImage: imagePath ? imagePath : undefined }}
		>
			<Grid container>
				<Grid item>
					<div className={classes.mainFeaturedPostContent}>
						<Typography component="h1" align="center" variant="h3" color="inherit" gutterBottom>
							{title}
						</Typography>
						{body && (
							<Typography variant="h5" align="center" color="inherit" paragraph>
								{body}
							</Typography>
						)}
						{textItems && (
							<div style={{ display: 'flex', paddingTop: '20px' }}>
								{textItems.map(textItem => (
									<div
										key={textItem.title}
										style={{ flexGrow: 1, flexBasis: '0', padding: '0 10px' }}
									>
										<Typography variant="h5" align="center" color="inherit" paragraph>
											{textItem.title}
										</Typography>
										<Typography variant="body1" align="center" color="inherit" paragraph>
											{textItem.text}
										</Typography>
									</div>
								))}
							</div>
						)}
					</div>
				</Grid>
			</Grid>
		</Paper>
	)
}

InfoBlock.propTypes = {
	post: PropTypes.object,
}

export default InfoBlock
