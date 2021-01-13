import merge from 'webpack-merge'
import commonConfig from './webpack.common'
import { Configuration } from 'webpack-dev-server'

const productionConfig: Configuration = merge(commonConfig, {
	mode: 'production',
})

export default productionConfig
