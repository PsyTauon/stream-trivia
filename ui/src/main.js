import { createApp } from "vue";
import App from "./App.vue";
import TriviaServer from "./plugins/trivia-server";
import './index.css'
import router from './router'

createApp(App).use(router).use(TriviaServer).mount("#app");
