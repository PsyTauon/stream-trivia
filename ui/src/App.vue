<template>
  <div>
    <img alt="Vue logo" src="./assets/logo.png" />
    <HelloWorld msg="Welcome to Your Vue.js App" />
    <input type="button" @click="connect()" value="Click to connect" />
  </div>
</template>

<script>
import HelloWorld from "./components/HelloWorld.vue";

export default {
  name: "App",

  components: {
    HelloWorld,
  },
  gameServer: {
    PlayerJoined(playerName) {
      console.log(playerName + " connected");
    },
  },
  methods: {
    connect() {
      this.$GameServer.JoinGame("1234", "Colby");
    },
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
