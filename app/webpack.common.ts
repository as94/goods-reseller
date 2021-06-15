import { CleanWebpackPlugin } from 'clean-webpack-plugin'
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
	devtool: 'source-map',
	resolve: {
		extensions: ['.ts', '.tsx', '.js', '.json'],
	},
	module: {
		rules: [
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
			{ test: /\.css$/, use: ['style-loader', 'css-loader'] },
		],
	},
	output: {
		path: __dirname + '/../src/GoodsReseller.Api/wwwroot',
		filename: 'bundle.js',
		chunkFilename: 'vendor.js',
	},
	optimization: {
		splitChunks: {
			chunks: 'all',
		},
	},
	plugins: [
		new CleanWebpackPlugin(),
		new CheckerPlugin(),
		new CopyPlugin([
			{ from: 'public/assets', to: __dirname + '/../src/GoodsReseller.Api/wwwroot/assets' },
			{ from: 'public/favicon.ico', to: __dirname + '/../src/GoodsReseller.Api/wwwroot/favicon.ico' },
			{ from: 'public/robots.txt', to: __dirname + '/../src/GoodsReseller.Api/wwwroot/robots.txt' },
			{ from: 'public/sitemap.txt', to: __dirname + '/../src/GoodsReseller.Api/wwwroot/sitemap.txt' },
		]),
		new DefinePlugin(
			Object.keys(env).reduce((prev, next) => {
				prev[`process.env.${next}`] = JSON.stringify(env[next])
				return prev
			}, {}),
		)
	],
}

export default commonConfig
