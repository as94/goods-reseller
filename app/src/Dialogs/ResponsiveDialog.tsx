import React, { useState } from 'react'
import Button from '@material-ui/core/Button'
import Dialog from '@material-ui/core/Dialog'
import DialogActions from '@material-ui/core/DialogActions'
import DialogContent from '@material-ui/core/DialogContent'
import DialogContentText from '@material-ui/core/DialogContentText'
import DialogTitle from '@material-ui/core/DialogTitle'
import useMediaQuery from '@material-ui/core/useMediaQuery'
import { useTheme } from '@material-ui/core/styles'

interface IOwnProps {
	title: string
	content: string
	cancelText: string
	okText: string
	cancel: () => void
	confirm: () => void
}

const ResponsiveDialog = ({ title, content, cancelText, okText, cancel, confirm }: IOwnProps) => {
	const [open, setOpen] = useState(true)
	const theme = useTheme()
	const fullScreen = useMediaQuery(theme.breakpoints.down('sm'))

	const handleClose = () => {
		setOpen(false)
		cancel()
	}

	const handleConfirm = () => {
		setOpen(false)
		confirm()
	}

	return (
		<div>
			<Dialog fullScreen={fullScreen} open={open} onClose={handleClose} aria-labelledby="responsive-dialog-title">
				<DialogTitle id="responsive-dialog-title">{title}</DialogTitle>
				<DialogContent>
					<DialogContentText>{content}</DialogContentText>
				</DialogContent>
				<DialogActions>
					<Button autoFocus onClick={handleClose} color="primary">
						{cancelText}
					</Button>
					<Button onClick={handleConfirm} color="primary" autoFocus>
						{okText}
					</Button>
				</DialogActions>
			</Dialog>
		</div>
	)
}

export default ResponsiveDialog
