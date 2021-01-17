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
    RecieveGamestate(gamestate) {
      console.log("Game State Recieved");
      this.gameState = gamestate;
    },
    QuestionAdded(gamestate) {
      console.log("Question Adeed");
      this.gameState = gamestate;
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
      this.$GameServer.GetGameState(urlGameId);
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
      this.countDown = 10;
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
