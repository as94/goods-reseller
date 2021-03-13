import React, { useCallback, useEffect, useState } from 'react'
import { Box, Grid, makeStyles } from '@material-ui/core'
import Table from '@material-ui/core/Table'
import TableBody from '@material-ui/core/TableBody'
import TableCell from '@material-ui/core/TableCell'
import TableContainer from '@material-ui/core/TableContainer'
import TableHead from '@material-ui/core/TableHead'
import TableRow from '@material-ui/core/TableRow'
import Paper from '@material-ui/core/Paper'
import Title from '../Title'
import { FinancialStatisticContract } from '../../Api/Statistics/contracts'
import statisticsApi from '../../Api/Statistics/statisticsApi'

const useStyles = makeStyles(theme => ({
	paper: {
		padding: theme.spacing(2),
		display: 'flex',
		overflow: 'auto',
		flexDirection: 'column',
	},
	table: {
		minWidth: 650,
	},
}))

const StatisticBlock = () => {
	const classes = useStyles()

	const [monthStatistic, setMonthStatistic] = useState({} as FinancialStatisticContract)
	const [yearStatistic, setYearStatistic] = useState({} as FinancialStatisticContract)

	const getMonthStatistic = useCallback(async () => {
		const now = new Date()
		const result = await statisticsApi.GetStatisticByMonth(now.getFullYear(), now.getMonth() + 1)
		setMonthStatistic(result)
	}, [statisticsApi])

	const getYearStatistic = useCallback(async () => {
		const now = new Date()
		const result = await statisticsApi.GetStatisticByYear(now.getFullYear())
		setYearStatistic(result)
	}, [statisticsApi])

	useEffect(() => {
		getMonthStatistic()
		getYearStatistic()
	}, [getMonthStatistic, getYearStatistic])

	return (
		<Grid item xs={12} md={12}>
			<Paper className={classes.paper}>
				<Box pt={2} pl={2}>
					<Title color="primary">Statistics</Title>
				</Box>
				<Box pt={2} pl={2}>
					<Title color="secondary">Month statistic</Title>
				</Box>
				<TableContainer component={Paper}>
					<Table className={classes.table} aria-label="simple table">
						<TableBody>
							<TableRow>
								<TableCell align="left">Revenue</TableCell>
								<TableCell align="right">{monthStatistic.revenue}</TableCell>
							</TableRow>
							<TableRow>
								<TableCell align="left">Costs</TableCell>
								<TableCell align="right">{monthStatistic.costs}</TableCell>
							</TableRow>
							<TableRow>
								<TableCell align="left">Gross profit</TableCell>
								<TableCell align="right">{monthStatistic.grossProfit}</TableCell>
							</TableRow>
							<TableRow>
								<TableCell align="left">Net profit</TableCell>
								<TableCell align="right">{monthStatistic.netProfit}</TableCell>
							</TableRow>
						</TableBody>
					</Table>
				</TableContainer>
				<Box pt={2} pl={2}>
					<Title color="secondary">Year statistic</Title>
				</Box>
				<TableContainer component={Paper}>
					<Table className={classes.table} aria-label="simple table">
						<TableBody>
							<TableRow>
								<TableCell align="left">Revenue</TableCell>
								<TableCell align="right">{yearStatistic.revenue}</TableCell>
							</TableRow>
							<TableRow>
								<TableCell align="left">Costs</TableCell>
								<TableCell align="right">{yearStatistic.costs}</TableCell>
							</TableRow>
							<TableRow>
								<TableCell align="left">Gross profit</TableCell>
								<TableCell align="right">{yearStatistic.grossProfit}</TableCell>
							</TableRow>
							<TableRow>
								<TableCell align="left">Net profit</TableCell>
								<TableCell align="right">{yearStatistic.netProfit}</TableCell>
							</TableRow>
						</TableBody>
					</Table>
				</TableContainer>
			</Paper>
		</Grid>
	)
}

export default StatisticBlock
