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
    <textarea class="input" v-model="questionText" placeholder="Question" />
    <br />
    <br />
    <textarea class="input" v-model="answer" placeholder="Answer" />
    <br />
    <br />
    <Button class="button" @click="SendQuestion()" title="Add Question" />
  </div>
</template>

<script>
import Button from '../components/Button'
export default {
  name: "Game",
  components: {
    Button
  },
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
