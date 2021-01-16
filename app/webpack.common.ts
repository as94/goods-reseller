import { CheckerPlugin } from 'awesome-typescript-loader'
import { Configuration, DefinePlugin } from 'webpack'
import CopyPlugin from 'copy-webpack-plugin'
import dotenv from 'dotenv'
import fs from 'fs'
import path from 'path'

const currentPath = path.join(__dirname)
const basePath = currentPath + '/.env'
const envPath = basePath + '.' + process.env.ENVIRONMENT
const finalPath = fs.existsSync(envPath) ? envPath : basePath
const env = dotenv.config({ path: finalPath }).parsed

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
		new DefinePlugin(
			Object.keys(env).reduce((prev, next) => {
				prev[`process.env.${next}`] = JSON.stringify(env[next])
				return prev
			}, {}),
		),
	],
}

export default commonConfig
