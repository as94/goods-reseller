import React, { useCallback, useEffect, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import CssBaseline from '@material-ui/core/CssBaseline'
import { v4 as uuid } from 'uuid'
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
import Copyright from '../Copyright/Copyright'
import StoreHeader from '../StoreHeader/StoreHeader'
import { ProductListItemContract } from '../Api/Products/contracts'
import productsApi from '../Api/Products/productsApi'
import {
	AddressContract,
	CustomerInfoContract,
	OrderInfoContract,
	OrderItemContract,
	OrderStatuses,
} from '../Api/Orders/contracts'
import { MoneyContract } from '../Api/contracts'
import ordersApi from '../Api/Orders/ordersApi'

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

function getStepContent(
	step: number,
	address: AddressContract,
	setAddress: (address: AddressContract) => void,
	deliveryType: number,
	setDeliveryType: (deliveryType: number) => void,
	customerInfo: CustomerInfoContract,
	setCustomerInfo: (customerInfo: CustomerInfoContract) => void,
	productSet: ProductListItemContract | null,
	productsInSet: ProductListItemContract[],
	orderInfo: OrderInfoContract | null,
) {
	switch (step) {
		case 0:
			return (
				<AddressForm
					address={address}
					setAddress={setAddress}
					deliveryType={deliveryType}
					setDeliveryType={setDeliveryType}
				/>
			)
		case 1:
			return <CustomerInfoForm customerInfo={customerInfo} setCustomerInfo={setCustomerInfo} />
		case 2:
			return (
				productSet &&
				orderInfo && <Review productSet={productSet} productsInSet={productsInSet} orderInfo={orderInfo} />
			)
		default:
			throw new Error('Unknown step')
	}
}

const Checkout = () => {
	let { setId } = useParams<{ setId: string }>()
	if (!setId) {
		return null
	}

	const classes = useStyles()
	const [activeStep, setActiveStep] = useState(0)
	const [orderInfo, setOrderInfo] = useState(null as OrderInfoContract | null)
	const [address, setAddress] = useState({ city: 'Москва', street: '', zipCode: '' } as AddressContract)
	const [deliveryType, setDeliveryType] = useState(0)
	const [customerInfo, setCustomerInfo] = useState({ name: '', phoneNumber: '' } as CustomerInfoContract)
	const [orderItems, setOrderItems] = useState([] as OrderItemContract[])
	const [productSet, setProductSet] = useState(null as ProductListItemContract | null)
	const [productsInSet, setProductsInSet] = useState([] as ProductListItemContract[])

	const handleNext = useCallback(async () => {
		setActiveStep(activeStep + 1)

		if (activeStep === steps.length - 1 && orderInfo) {
			await ordersApi.Create(orderInfo)
		}
	}, [setActiveStep, activeStep, steps])

	const handleBack = useCallback(() => {
		setActiveStep(activeStep - 1)
	}, [setActiveStep, activeStep])

	const getProducts = useCallback(async () => {
		const response = await productsApi.GetProductList()
		const products = response.items

		const setProducts = products
			.filter(x => x.isSet)
			.sort((a, b) => a.name.toLowerCase().localeCompare(b.name.toLowerCase()))
		const simpleProducts = products
			.filter(x => !x.isSet)
			.sort((a, b) => a.name.toLowerCase().localeCompare(b.name.toLowerCase()))

		const set = setProducts.find(x => x.id === setId)

		if (set) {
			setProductSet(set)
			const pInSet = simpleProducts.filter(x => set.productIds.includes(x.id))
			setProductsInSet(pInSet)
		}
	}, [setId, setProductSet, setProductsInSet])

	useEffect(() => {
		getProducts()
	}, [getProducts])

	useEffect(() => {
		setOrderItems(
			productsInSet.map(
				p =>
					({
						id: uuid(),
						productId: p.id,
						unitPrice: p.unitPrice,
						discountPerUnit: p.discountPerUnit,
						quantity: 1,
					} as OrderItemContract),
			),
		)
	}, [productsInSet, setOrderItems])

	useEffect(() => {
		if (productSet) {
			setOrderInfo({
				version: 1,
				status: OrderStatuses[0],
				address,
				deliveryCost: { value: deliveryType === 1 ? 300 : 0 } as MoneyContract,
				customerInfo,
				addedCost: { value: productSet.addedCost } as MoneyContract,
				orderItems,
			} as OrderInfoContract)
		}
	}, [productSet, address, deliveryType, customerInfo, orderItems, setOrderInfo])

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
									Мы свяжимся с вами в ближайшее время для уточнения деталей.
								</Typography>
							</>
						) : (
							<>
								{getStepContent(
									activeStep,
									address,
									setAddress,
									deliveryType,
									setDeliveryType,
									customerInfo,
									setCustomerInfo,
									productSet,
									productsInSet,
									orderInfo,
								)}
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
