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
import { useHistory, useParams } from 'react-router-dom'
import StoreHeader from '../StoreHeader/StoreHeader'
import { ProductListItemContract } from '../Api/Products/contracts'
import {
	AddressContract,
	CustomerInfoContract,
	OrderInfoContract,
	OrderItemContract,
	OrderStatuses,
} from '../Api/Orders/contracts'
import ordersApi from '../Api/Orders/ordersApi'
import StoreFooter from '../StoreFooter/StoreFooter'
import { MessageModal } from '../Modal/MessageModal'
import { getAllProducts } from '../Api/Products/productsUtils'

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
	errors: string[],
) {
	switch (step) {
		case 0:
			return (
				<AddressForm
					address={address}
					setAddress={setAddress}
					deliveryType={deliveryType}
					setDeliveryType={setDeliveryType}
					errors={errors}
				/>
			)
		case 1:
			return <CustomerInfoForm customerInfo={customerInfo} setCustomerInfo={setCustomerInfo} errors={errors} />
		case 2:
			return (
				productSet &&
				orderInfo && (
					<Review
						productSet={productSet}
						productsInSet={productsInSet}
						orderInfo={orderInfo}
						deliveryType={deliveryType}
					/>
				)
			)
		default:
			throw new Error('Unknown step')
	}
}

export const errorsList = {
	cityIsRequiredError: 'Город обязателен для заполнения',
	streetIsRequiredError: 'Улица обязательна для заполнения',
	zipCodeIsRequiredError: 'Почтовый индекс обязателен для заполнения',
	phoneNumberIsRequiredError: 'Телефон обязателен для заполнения',
	phoneNumberIsIncorrect: 'Телефонный номер не соответствует формату',
}

export enum DeliveryType {
	Mail = 0,
	ServiceWithinMoscowRingRoad = 1,
	ServiceOutsideMoscowRingRoad = 2,
}

const Checkout = () => {
	let { setId } = useParams<{ setId: string }>()
	if (!setId) {
		return null
	}

	const classes = useStyles()
	const history = useHistory()
	const [activeStep, setActiveStep] = useState(0)
	const [orderInfo, setOrderInfo] = useState(null as OrderInfoContract | null)
	const [address, setAddress] = useState({ city: '', street: '', zipCode: '' } as AddressContract)
	const [deliveryType, setDeliveryType] = useState(DeliveryType.Mail)
	const [customerInfo, setCustomerInfo] = useState({ name: '', phoneNumber: '' } as CustomerInfoContract)
	const [orderItems, setOrderItems] = useState([] as OrderItemContract[])
	const [productSet, setProductSet] = useState(null as ProductListItemContract | null)
	const [productsInSet, setProductsInSet] = useState([] as ProductListItemContract[])

	const [errors, setErrors] = useState([] as string[])
	const [touched, setTouched] = useState(false)
	const [showErrorModal, setShowErrorModal] = useState(false)

	const getValidationErrors = useCallback(() => {
		let currentErrors = [] as string[]
		if (activeStep === 0) {
			if (!address.city) {
				currentErrors = [...currentErrors, errorsList.cityIsRequiredError]
			}
			if (!address.street) {
				currentErrors = [...currentErrors, errorsList.streetIsRequiredError]
			}
			if (!address.zipCode && deliveryType === DeliveryType.Mail) {
				currentErrors = [...currentErrors, errorsList.zipCodeIsRequiredError]
			}
		}
		if (activeStep === 1) {
			if (!customerInfo.phoneNumber) {
				currentErrors = [...currentErrors, errorsList.phoneNumberIsRequiredError]
			}

			if (!customerInfo.phoneNumber.match(/^(?:\+\d{1,3}|0\d{1,3}|00\d{1,2})?(?:\s?\(\d+\))?(?:[-\/\s.]|\d)+$/)) {
				currentErrors = [...currentErrors, errorsList.phoneNumberIsIncorrect]
			}
		}
		return currentErrors
	}, [activeStep, address, customerInfo, errorsList, deliveryType])

	const moveToNextStep = useCallback(async () => {
		setTouched(false)

		if (activeStep < steps.length - 1) {
			setActiveStep(activeStep + 1)
		} else if (orderInfo) {
			try {
				await ordersApi.Create(orderInfo)
				setActiveStep(activeStep + 1)
				history.push('/store/thanks')
			} catch (e) {
				setShowErrorModal(true)
			}
		}
	}, [activeStep, setActiveStep, orderInfo, history])

	const handleNext = useCallback(async () => {
		const validationErrors = getValidationErrors()
		if (validationErrors.length === 0) {
			moveToNextStep()
		} else {
			setTouched(true)
		}
	}, [setTouched, getValidationErrors, moveToNextStep])

	const handleBack = useCallback(() => {
		setTouched(true)
		setActiveStep(activeStep - 1)
	}, [setActiveStep, activeStep, setTouched])

	const getProducts = useCallback(async () => {
		const allProducts = await getAllProducts()
		const products = allProducts

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
	}, [setId, setProductSet, setProductsInSet, getAllProducts])

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
				id: uuid(),
				version: 1,
				status: OrderStatuses[0],
				address,
				deliveryCost: deliveryType === 1 ? 300 : 0,
				customerInfo,
				addedCost: productSet.addedCost,
				orderItems,
			} as OrderInfoContract)
		}
	}, [productSet, address, deliveryType, customerInfo, orderItems, setOrderInfo])

	useEffect(() => {
		if (touched) {
			const validationErrors = getValidationErrors()
			setErrors(validationErrors)
		}
	}, [touched, setErrors, getValidationErrors])

	return (
		<>
			<CssBaseline />
			<StoreHeader />
			<MessageModal
				open={showErrorModal}
				setOpen={setShowErrorModal}
				title="Произошла ошибка при оформлении заказа"
				message="Пожалуйста, перепроверьте данные формы и попробуйте ещё раз. Или свяжитесь с нами через контакты"
			/>
			<main className={classes.layout}>
				<Paper className={classes.paper}>
					<Typography component="h1" variant="h4" align="center">
						Оформление заказа
					</Typography>
					<Stepper activeStep={activeStep} className={classes.stepper} orientation="vertical">
						{steps.map(label => (
							<Step key={label}>
								<StepLabel>{label}</StepLabel>
							</Step>
						))}
					</Stepper>
					{activeStep !== steps.length && (
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
								errors,
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
									disabled={touched && errors.length > 0}
								>
									{activeStep === steps.length - 1 ? 'Разместить заказ' : 'Дальше'}
								</Button>
							</div>
						</>
					)}
				</Paper>
			</main>
			<StoreFooter />
		</>
	)
}

export default Checkout
