import merge from 'webpack-merge'
import commonConfig from './webpack.common'
import { Configuration } from 'webpack-dev-server'

const UglifyJsPlugin = require('uglifyjs-webpack-plugin');

const productionConfig: Configuration = merge(commonConfig, {
	devtool: false,
	mode: 'production',
	plugins: [
		new UglifyJsPlugin({
			cache: true,
			parallel: true,
			sourceMap: true
		})
	]
})

export default productionConfig
