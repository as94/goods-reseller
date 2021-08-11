import React, { useCallback, useState } from 'react'
import { makeStyles } from '@material-ui/core/styles'
import Modal from '@material-ui/core/Modal'

const useStyles = makeStyles(theme => ({
	paper: {
		position: 'absolute',
		width: 400,
		backgroundColor: theme.palette.background.paper,
		border: '2px solid #000',
		boxShadow: theme.shadows[5],
		padding: theme.spacing(2, 4, 3),
	},
}))

const getModalStyle = () => {
	const top = 50
	const left = 50

	return {
		top: `${top}%`,
		left: `${left}%`,
		transform: `translate(-${top}%, -${left}%)`,
	}
}

export interface IOwnProps {
	open: boolean
	setOpen: (open: boolean) => void
	title: string
	message: string
}

export const MessageModal = ({ open, setOpen, title, message }: IOwnProps) => {
	const classes = useStyles()

	const handleClose = useCallback(() => {
		setOpen(false)
	}, [])

	return (
		<Modal open={open} onClose={handleClose}>
			<div style={getModalStyle()} className={classes.paper}>
				<h2 id="simple-modal-title">{title}</h2>
				<p id="simple-modal-description">{message}</p>
				{/* <MessageModal title={title} message={message} /> */}
			</div>
		</Modal>
	)
}
