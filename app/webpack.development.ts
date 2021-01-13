import path from 'path'
import merge from 'webpack-merge'
import commonConfig from './webpack.common'
import { Configuration } from 'webpack-dev-server'

const developmentConfig: Configuration = merge(commonConfig, {
	mode: 'development',
	devtool: 'inline-source-map',
	devServer: {
		contentBase: [path.join(__dirname, 'public'), path.join(__dirname, 'dist')],
		compress: true,
		port: 9000,
	},
})

export default developmentConfig
