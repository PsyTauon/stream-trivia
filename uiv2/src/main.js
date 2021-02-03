import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import vuetify from "./plugins/vuetify";
import TriviaServer from "./plugins/trivia-server";

Vue.use(TriviaServer);

Vue.config.productionTip = false;

new Vue({
    router,
    store,
    vuetify,
    render: h => h(App),
    data: {
        $socket_connection: true
    }
}).$mount("#app");
