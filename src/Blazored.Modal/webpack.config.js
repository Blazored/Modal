const path = require('path');

module.exports = (env) => {

    return {
        entry: './Interop/Blazored.Modal.ts',
        devtool: 'source-map',
        module: {
            rules: [
                {
                    test: /\.tsx?$/,
                    use: 'ts-loader',
                    exclude: /node_modules/,
                },
            ],
        },
        resolve: {
            extensions: ['.tsx', '.ts', '.js'],
        },
        output: {
            filename: 'blazored.modal.js',
            path: path.resolve(__dirname, 'wwwroot'),
        },
    };
};