import React from 'react'
import { makeStyles } from '@material-ui/core/styles'
import CssBaseline from '@material-ui/core/CssBaseline'
import AppBar from '@material-ui/core/AppBar'
import Toolbar from '@material-ui/core/Toolbar'
import Paper from '@material-ui/core/Paper'
import Stepper from '@material-ui/core/Stepper'
import Step from '@material-ui/core/Step'
import StepLabel from '@material-ui/core/StepLabel'
import Button from '@material-ui/core/Button'
import Typography from '@material-ui/core/Typography'
import AddressForm from './AddressForm'
import CustomerInfoForm from './CustomerInfoForm'
import Review from './Review'
import { useParams } from 'react-router-dom'
import { useTranslation } from 'react-i18next'
import Copyright from '../Copyright/Copyright'
import StoreHeader from '../StoreHeader/StoreHeader'

const useStyles = makeStyles(theme => ({
	appBar: {
		position: 'relative',
	},
	layout: {
		width: 'auto',
		marginLeft: theme.spacing(2),
		marginRight: theme.spacing(2),
		[theme.breakpoints.up(600 + theme.spacing(2) * 2)]: {
			width: 600,
			marginLeft: 'auto',
			marginRight: 'auto',
		},
	},
	paper: {
		marginTop: theme.spacing(3),
		marginBottom: theme.spacing(3),
		padding: theme.spacing(2),
		[theme.breakpoints.up(600 + theme.spacing(3) * 2)]: {
			marginTop: theme.spacing(6),
			marginBottom: theme.spacing(6),
			padding: theme.spacing(3),
		},
	},
	stepper: {
		padding: theme.spacing(3, 0, 5),
	},
	buttons: {
		display: 'flex',
		justifyContent: 'flex-end',
	},
	button: {
		marginTop: theme.spacing(3),
		marginLeft: theme.spacing(1),
	},
}))

const steps = ['Адрес доставки', 'Контактная информация', 'Просмотр заказа']

function getStepContent(step: number) {
	switch (step) {
		case 0:
			return <AddressForm />
		case 1:
			return <CustomerInfoForm />
		case 2:
			return <Review />
		default:
			throw new Error('Unknown step')
	}
}

const Checkout = () => {
	let { setId } = useParams<{ setId: string }>()
	if (!setId) {
		return null
	}
	const { t } = useTranslation()

	const classes = useStyles()
	const [activeStep, setActiveStep] = React.useState(0)

	const handleNext = () => {
		setActiveStep(activeStep + 1)
	}

	const handleBack = () => {
		setActiveStep(activeStep - 1)
	}

	return (
		<>
			<CssBaseline />
			<StoreHeader />
			<main className={classes.layout}>
				<Paper className={classes.paper}>
					<Typography component="h1" variant="h4" align="center">
						Оформление заказа
					</Typography>
					<Stepper activeStep={activeStep} className={classes.stepper}>
						{steps.map(label => (
							<Step key={label}>
								<StepLabel>{label}</StepLabel>
							</Step>
						))}
					</Stepper>
					<>
						{activeStep === steps.length ? (
							<>
								<Typography variant="h5" gutterBottom>
									Спасибо за ваш заказ.
								</Typography>
								<Typography variant="subtitle1">
									Ваш заказ #2001539. Мы свяжимся с вами в ближайшее время для уточнения деталей.
								</Typography>
							</>
						) : (
							<>
								{getStepContent(activeStep)}
								<div className={classes.buttons}>
									{activeStep !== 0 && (
										<Button onClick={handleBack} className={classes.button}>
											Назад
										</Button>
									)}
									<Button
										variant="contained"
										color="primary"
										onClick={handleNext}
										className={classes.button}
									>
										{activeStep === steps.length - 1 ? 'Разместить заказ' : 'Дальше'}
									</Button>
								</div>
							</>
						)}
					</>
				</Paper>
				<Copyright />
			</main>
		</>
	)
}

export default Checkout