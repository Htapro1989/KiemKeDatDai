const { when, whenDev, whenProd, whenCI, whenTest, ESLINT_MODES, POSTCSS_MODES } = require('@craco/craco');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const CracoAntDesignPlugin = require('craco-antd');

module.exports = {
  plugins: [
    {
      plugin: CracoAntDesignPlugin,
      options: {
        customizeTheme: {
          '@primary-color': '#F4600C',
          '@link-color': '#F4600C',
          '@border-radius-base': '6px',

          //input
          '@input-placeholder-color': '#A6AEBB',
          '@border-color-base': '#DEE5EF',
          '@input-color': '#091E42',

          // //Text
          '@text-color': '#091E42',

          //Error 
          '@error-color': 'red',
          '@error-color-active': 'red',

          //menu item
          '@menu-item-height': '30px',
          '@menu-item-padding-horizontal': '10px'

        },
      },
    },
  ],
  webpack: {
    alias: {},
    plugins: [],
    configure: (webpackConfig, { env, paths }) => {
      if (!webpackConfig.plugins) {
        config.plugins = [];
      }

      webpackConfig.plugins.push(
        process.env.NODE_ENV === 'production'
          ? new CopyWebpackPlugin([
            {
              from: 'node_modules/@aspnet/signalr/dist/browser/signalr.min.js',
            },
            {
              from: 'node_modules/abp-web-resources/Abp/Framework/scripts/libs/abp.signalr-client.js',
            },
            {
              from: 'src/lib/abp.js',
            },
          ])
          : new CopyWebpackPlugin([
            {
              from: 'node_modules/@aspnet/signalr/dist/browser/signalr.min.js',
            },
            {
              from: 'node_modules/abp-web-resources/Abp/Framework/scripts/libs/abp.signalr-client.js',
              to: 'dist/abp.signalr-client.js'
            },
            {
              from: 'src/lib/abp.js',
            },
          ])
      );

      return webpackConfig;
    },
  },
};
