<template>
  <div>
    Test
  </div>
</template>

<script>
export default {
  name: "Game",
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
