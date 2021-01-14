import { createApp } from "vue";
import App from "./App.vue";
import TriviaServer from "./plugins/trivia-server";
import './index.css'

createApp(App).use(TriviaServer).mount("#app");
