<template>
  <div>
    <GridGame :countdown=countdown v-if="displayComponent == 'grid'" />
  </div>
</template>

<script>
import GridGame from './game/GridGame'

export default {
  name: "Game",
  components: {
    GridGame
  },
  data: () => ({
    flexWidth: 640,
    displayComponent: 'grid',
    questionText: "",
    gameState: {},
    answer: "",
    playerAnswer: "",
    error: "",
    countdown: 0,
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
      this.countdown = 10;
      this.playerAnswer = "";
      this.countdownTimer();
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
    if (window.innerWidth <= this.flexWidth) {
      this.displayComponent = 'flex'
    } else {
      this.displayComponent = 'grid'
    }

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
    countdownTimer() {
      if (this.countdown > 0) {
        setTimeout(() => {
          this.countdown -= 1;
          this.countdownTimer();
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
