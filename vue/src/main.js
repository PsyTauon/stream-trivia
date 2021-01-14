import { createApp } from "vue";
import App from "./App.vue";
import TriviaServer from "./plugins/trivia-server";

createApp(App).use(TriviaServer).mount("#app");
