import { CheckerPlugin } from 'awesome-typescript-loader'
import { Configuration } from 'webpack'
import CopyPlugin from 'copy-webpack-plugin'

const commonConfig: Configuration = {
	entry: ['./src/index.tsx'],
	output: {
		filename: 'bundle.js',
		path: __dirname + '/../src/GoodsReseller.Api/wwwroot',
		chunkFilename: 'vendor.js',
	},
	optimization: {
		splitChunks: {
			chunks: 'all',
		},
	},
	devtool: 'eval-source-map',
	resolve: {
		extensions: ['.ts', '.tsx', '.js', '.json'],
	},

	module: {
		rules: [
			// {
			// 	test: /\.js$/,
			// 	use: [
			// 		{
			// 			loader: 'cache-loader',
			// 			options: {
			// 				cacheDirectory: __dirname + '/node_modules/.cache/cache-loader',
			// 			},
			// 		},
			// 		'babel-loader',
			// 	],
			// },
			{ enforce: 'pre', test: /\.js$/, loader: 'source-map-loader' },
			{
				test: /\.tsx?$/,
				loader: 'awesome-typescript-loader',
				options: {
					reportFiles: ['./src/**/*.{ts,tsx}'],
					useBabel: true,
					babelCore: '@babel/core',
				},
			},
		],
	},
	plugins: [
		new CheckerPlugin(),
		new CopyPlugin([
			{ from: 'public/assets', to: __dirname + '/../src/GoodsReseller.Api/wwwroot/assets' },
			{ from: 'public/favicon.ico', to: __dirname + '/../src/GoodsReseller.Api/wwwroot/favicon.ico' },
		]),
	],
}

export default commonConfig
