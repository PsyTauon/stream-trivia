<template>
  <div class="dark:bg-black pt-10 text-center">
    <div>
      <div class="font-title capitalize text-5xl text-black">TRIVIA</div>
      <div class="font-subtitle -m-3 text-black">on stream</div>
    </div>
    <div class="p-10 flex flex-col items-center space-y-4">
      <input class="input w-60 text-xl px-4 py-2" type="text" v-model="gameId" placeholder="Game ID" />
      <input class="input w-60 text-xl px-4 py-2" type="text" v-model="playerName" placeholder="Player Name" />
    </div>
    <div class="flex flex-col md:flex-row items-center justify-center md:space-x-4 md:space-y-0 space-y-4 mx-4">
      <Button @click="join()" title="Join Game" />
      <Button @click="create()" title="Create Game" />
    </div>
  </div>
</template>

<script>
import Button from '../components/Button'
import toastMe from 'toast-me';

const toast = (message) => toastMe(message, {
  position: 'bottom',
  duration: 3000,
  closeable: false,
})

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
      toast(this.error);
    },
  },
  methods: {
    join() {
      if (this.gameId && this.playerName) {
        this.$GameServer.JoinGame(this.gameId, this.playerName);
      } else {
        this.error = "Game ID and Player Name Required";
        toast(this.error);
      }
    },
    create() {
      if (this.playerName) {
        this.$GameServer.CreateGame(this.getRandomString(6), this.playerName);
      } else {
        this.error = "Player Name Required";
        toast(this.error);
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
