<template>
  <div>
    <br />
    <br />
    <label>{{ gameState }}</label>
    <br />
    <br />
    {{ countDown }}
    <br />
    <br />
    <textarea v-model="questionText" placeholder="Question" />
    <br />
    <br />
    <textarea v-model="answer" placeholder="Answer" />
    <br />
    <br />
    <input type="button" @click="SendQuestion()" value="Add Question" />
  </div>
</template>

<script>
export default {
  name: "Game",
  data: () => ({
    questionText: "",
    gameState: {},
    answer: "",
    error: "",
    countDown: 0,
  }),
  gameServer: {
    RoomEnterred(gamestate) {
      console.log("Game State Recieved");
      if (this.gamestate) {
        this.gameState.players = gamestate.players;
      } else {
        this.gamestate = this.gameState = gamestate;
      }
    },
    QuestionAdded(gamestate) {
      console.log("Question Adeed");
      this.gameState = gamestate;
      this.countDown = 10;
      this.countDownTimer();
    },
    SetCountPhase(gamestate) {
      console.log("COUNT PHASE");
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
    SendQuestion() {
      this.$GameServer.AddQuestion(
        this.gameState.gameId,
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
  },
};
</script>
