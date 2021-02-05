module.exports = {
    transpileDependencies: ["vuetify"],
    devServer: {
        proxy: {
            "^/hub": {
                // SET TO YOUR API PORT
                target: "http://localhost:49156"
            }
        }
    }
};
