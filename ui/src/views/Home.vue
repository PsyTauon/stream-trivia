<template>
  <div class="home">
    <br />
    <label>{{ error }}</label>
    <br />
    <br />
    <input type="text" v-model="gameId" placeholder="Game Id" />
    <br />
    <br />
    <input type="text" v-model="playerName" placeholder="Player Name" />
    <br />
    <br />
    <input type="button" @click="join()" value="Join Game" />
    <br />
    <input type="button" @click="create()" value="Create Game" />
  </div>
</template>

<script>
export default {
  name: "Home",
  data: () => ({
    gameId: "",
    playerName: "",
    error: "",
  }),
  gameServer: {
    JoinedSuccessful(gameId) {
      this.$router.push("game?id=" + gameId);
    },
    JoinError(error) {
      this.error = error;
    },
  },
  methods: {
    join() {
      if (this.gameId && this.playerName) {
        this.$GameServer.JoinGame(this.gameId, this.playerName);
      } else {
        this.error = "Game Id and Player Name Required";
      }
    },
    create() {
      if (this.playerName) {
        this.$GameServer.CreateGame(this.getRandomString(6), this.playerName);
      } else {
        this.error = "Player Name Required";
      }
    },
    getRandomString(length) {
      var randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      var result = "";
      for (var i = 0; i < length; i++) {
        result += randomChars.charAt(
          Math.floor(Math.random() * randomChars.length)
        );
      }
      return result;
    },
  },
};
</script>
