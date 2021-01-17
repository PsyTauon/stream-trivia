<template>
  <div class="home">
    <input type="button" @click="connect()" value="Click to connect" />
  </div>
</template>

<script>
export default {
  name: "Home",
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
