module.exports = {
    transpileDependencies: ["vuetify"],
    devServer: {
        proxy: {
            "^/ws": {
                target: "http://localhost:49158",
                ws: true,
                changeOrigin: true,
                pathRewrite: {
                    "^/ws": "" // rewrt path
                }
            }
        }
    }
};
