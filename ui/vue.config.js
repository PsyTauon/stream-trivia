// vue.config.js
module.exports = {
  devServer: {
    proxy: {
      "^/hub": {
        // SET TO YOUR API PORT
        target: "http://localhost:49164",
      },
    },
  },
};
