import React from 'react'
import { ProductListItemContract } from '../../../Api/Products/contracts'

interface IOwnProps {
	supplyId: string
	products: ProductListItemContract[]
	hide: () => void
}

const Supply = ({ supplyId, products, hide }: IOwnProps) => <div></div>

export default Supply
