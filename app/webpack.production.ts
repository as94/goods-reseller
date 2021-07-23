import merge from 'webpack-merge'
import commonConfig from './webpack.common'
import { Configuration } from 'webpack-dev-server'

const UglifyJsPlugin = require('uglifyjs-webpack-plugin');

const productionConfig: Configuration = merge(commonConfig, {
	devtool: false,
	mode: 'production',
	output: {
		path: __dirname + '/../src/GoodsReseller.Api/wwwroot',
		filename: '[name].[contenthash].js',
		chunkFilename: '[name].[contenthash].js',
	},
	optimization: {
		runtimeChunk: 'single',
		splitChunks: {
			cacheGroups: {
			  vendor: {
				test: /[\\/]node_modules[\\/]/,
				name: 'vendors',
				chunks: 'all',
			  },
			},
		  },
	},
	plugins: [
		new UglifyJsPlugin({
			cache: true,
			parallel: true,
			sourceMap: true
		})
	]
})

export default productionConfig
