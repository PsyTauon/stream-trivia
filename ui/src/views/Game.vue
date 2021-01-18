<template>
  <div>
    <br />
    <br />
    <label>{{ gameState }}</label>
    <br />
    <br />
    <label>{{ me }}</label>
    <br />
    <br />
    {{ countDown }}

    <div v-if="me.isHost">
      YOU ARE HOST
      <br />
      <br />
      <textarea v-model="questionText" placeholder="Question" />
      <br />
      <br />
      <textarea v-model="answer" placeholder="Answer" />
      <br />
      <br />
      <input type="button" @click="sendQuestion()" value="Add Question" />

      <div v-for="player in gameState.players" :key="player.id">
        <!-- <div v-if="!player.isHost"> -->
        {{ player.name }}
        <input
          type="button"
          @click="updateScore(1, player.connectionId)"
          value="RIGHT"
        />
        <input
          type="button"
          @click="updateScore(-1, player.connectionId)"
          value="WRONG"
        />
        <!-- </div> -->
      </div>
    </div>
    <div v-else>
      YOU ARE PLAYER
      <br />
      <br />
      <textarea v-model="playerAnswer" placeholder="Answer" />
      <br />
      <br />
      <input type="button" @click="submitAnswer()" value="Submit Answer" />
    </div>
  </div>
</template>

<script>
export default {
  name: "Game",
  data: () => ({
    questionText: "",
    gameState: {},
    answer: "",
    playerAnswer: "",
    error: "",
    countDown: 0,
    me: {},
  }),
  gameServer: {
    RoomEnterred(gamestate) {
      if (this.gamestate) {
        this.gameState.players = gamestate.players;
      } else {
        this.gamestate = this.gameState = gamestate;
      }
    },
    SetMe(player) {
      this.me = player;
    },
    QuestionAdded(gamestate) {
      this.gameState = gamestate;
      this.countDown = 10;
      this.playerAnswer = "";
      this.countDownTimer();
    },
    SetCountPhase(gamestate) {
      this.gameState = gamestate;
    },
    ScoreUpdated(gamestate) {
      this.gameState = gamestate;
    },
    AnswerAdded(gamestate) {
      this.gameState = gamestate;
    },
  },
  created() {
    let urlGameId = this.$route.query.id;
    if (urlGameId) {
      this.$GameServer.EnterRoom(urlGameId);
    } else {
      // TODO ERROR
      //this.$router.push("/");
    }
  },
  methods: {
    sendQuestion() {
      this.$GameServer.AddQuestion(
        this.gameState.id,
        this.questionText,
        this.answer
      );
    },
    countDownTimer() {
      if (this.countDown > 0) {
        setTimeout(() => {
          this.countDown -= 1;
          this.countDownTimer();
        }, 1000);
      }
    },
    updateScore(scoreChange, connectionId) {
      this.$GameServer.UpdatePlayerScore(
        this.gameState.id,
        scoreChange,
        connectionId
      );
    },
    submitAnswer() {
      this.$GameServer.SubmitAnswer(
        this.gameState.id,
        this.playerAnswer,
        this.me.connectionId
      );
    },
  },
};
</script>
