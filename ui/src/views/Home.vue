<template>
  <div class="dark:bg-black pt-10">
    <div>
      <div class="font-title capitalize text-5xl text-black">TRIVIA</div>
      <div class="font-subtitle -m-3 text-black">on stream</div>
    </div>
    <div class="p-10">
      <input class="input text-xl px-4 py-2" type="text" v-model="playerName" placeholder="Player Name" />
    </div>
    <div class="flex justify-center space-x-4 mx-4">
      <Button @click="join()" title="Join Game" />
      <Button @click="create()" title="Create Game" />
    </div>
    <label>{{ error }}</label>
  </div>
</template>

<script>
import Button from '../components/Button'

export default {
  name: "Home",
  components: {
    Button
  },
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
