<template>
  <div class="flex flex-col md:grid md:grid-cols-2 md:gap-4">
    <div class="order-3 md:order-none md:col-span-2">
      {{ countDown }}
    </div>
    <div class="order-1 md:order-none">
      <div>
        <div class="flex flex-col space-y-4 items-center mb-4">
          <textarea class="input max-w-xl" v-model="questionText" placeholder="Question" />
          <textarea class="input max-w-xl" v-model="answer" placeholder="Answer" />
        </div>
        <Button class="button" @click="SendQuestion()" title="Add Question" />
      </div>
    </div>
    <div class="order-4 md:order-none md:flex gap-2">
      <UserAnswer username="Psy" answer="This is an answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
    </div>
    <div class="order-3 md:order-none">
      Player list
    </div>
    <div class="order-5 md:order-none">
      <label>{{ gameState }}</label>
    </div>
  </div>
</template>

<script>
import Button from '../components/Button'
import UserAnswer from '../components/UserAnswer'
export default {
  name: "Game",
  components: {
    Button,
    UserAnswer
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
