import React, { useCallback, useEffect, useState } from 'react'
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles'
import Input from '@material-ui/core/Input'
import InputLabel from '@material-ui/core/InputLabel'
import MenuItem from '@material-ui/core/MenuItem'
import FormControl from '@material-ui/core/FormControl'
import Select from '@material-ui/core/Select'
import { Chip, useTheme } from '@material-ui/core'

export interface IItem {
	id: string
	name: string
}

interface IOwnProps {
	title: string
	items: IItem[]
	selectedIds: string[]
	setSelectedIds: (selectedIds: string[]) => void
}

const useStyles = makeStyles((theme: Theme) =>
	createStyles({
		formControl: {
			margin: theme.spacing(1),
			minWidth: 120,
			maxWidth: 300,
		},
		chips: {
			display: 'flex',
			flexWrap: 'wrap',
		},
		chip: {
			margin: 2,
		},
	}),
)

const getStyles = (name: string, names: string[], theme: Theme) => {
	return {
		fontWeight: names.indexOf(name) === -1 ? theme.typography.fontWeightRegular : theme.typography.fontWeightMedium,
	}
}

const ITEM_HEIGHT = 50
const ITEM_PADDING_TOP = 8
const MenuProps = {
	PaperProps: {
		style: {
			maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
			width: 250,
		},
	},
}

const MultipleSelect = ({ title, items, selectedIds, setSelectedIds }: IOwnProps) => {
	const classes = useStyles()
	const theme = useTheme()
	const [selectedNames, setSelectedNames] = useState<string[]>([])

	const handleChange = useCallback(
		(event: React.ChangeEvent<{ value: unknown }>) => {
			const names = event.target.value as string[]
			setSelectedNames(names)
			setSelectedIds(items.filter(i => names.includes(i.name)).map(i => i.id))
		},
		[items, setSelectedNames, setSelectedIds],
	)

	useEffect(() => {
		setSelectedNames(items.filter(i => selectedIds.includes(i.id)).map(i => i.name))
	}, [items, selectedIds, setSelectedNames])

	return (
		<div>
			<FormControl className={classes.formControl}>
				<InputLabel id="mutiple-chip-label">{title}</InputLabel>
				<Select
					labelId="mutiple-chip-label"
					id="mutiple-chip"
					multiple
					value={selectedNames}
					onChange={handleChange}
					input={<Input id="select-multiple-chip" />}
					renderValue={selected => (
						<div className={classes.chips}>
							{(selected as string[]).map(value => (
								<Chip key={value} label={value} className={classes.chip} />
							))}
						</div>
					)}
					MenuProps={MenuProps}
				>
					{items.map(item => (
						<MenuItem key={item.name} value={item.name} style={getStyles(item.name, selectedNames, theme)}>
							{item.name}
						</MenuItem>
					))}
				</Select>
			</FormControl>
		</div>
	)
}

export default MultipleSelect
