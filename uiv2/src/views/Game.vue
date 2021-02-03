<template>
    <div>
        <!-- is host -->
        <div v-if="me.isHost">
            <v-row>
                <v-col cols="12" sm="12">
                    <v-chip class="ma-2" color="dark">
                        {{ countDown }} seconds
                        <v-icon right>
                            mdi-timer
                        </v-icon>
                    </v-chip>
                </v-col>
            </v-row>

            <v-row>
                <v-col cols="12" sm="3">
                    <v-textarea v-model="questionText" filled label="Question"></v-textarea>
                    <v-textarea v-model="answer" filled label="Answer"></v-textarea>
                    <v-divider class="mb-7"></v-divider>
                    <v-btn block @click="sendQuestion()">
                        Add Question
                    </v-btn>

                    <v-divider class="my-7"></v-divider>

                    <div>
                        <ScoreTable :players="gameState.players.slice(1)"></ScoreTable>
                    </div>
                </v-col>
                <v-col cols="12" sm="9">
                    <v-row>
                        <v-col
                            cols="12"
                            sm="2"
                            v-for="player in gameState.players.slice(1)"
                            :key="player.id"
                        >
                            <v-card>
                                <v-card-title>{{ player.name }}</v-card-title>
                                <v-card-text>
                                    <v-textarea :value="player.currentAnswer" filled></v-textarea>
                                </v-card-text>

                                <v-card-actions>
                                    <v-spacer></v-spacer>
                                    <v-btn
                                        icon
                                        color="green"
                                        @click="updateScore(1, player.connectionId)"
                                    >
                                        <v-icon>mdi-check</v-icon>
                                    </v-btn>

                                    <v-btn
                                        icon
                                        color="red"
                                        @click="updateScore(-1, player.connectionId)"
                                    >
                                        <v-icon>mdi-close</v-icon>
                                    </v-btn>
                                </v-card-actions>
                            </v-card>
                        </v-col></v-row
                    >
                </v-col>
            </v-row>
        </div>
        <!-- is player -->

        <div v-else>
            <v-row
                ><v-col cols="12" sm="12">{{ gameState.status }}</v-col>
            </v-row>
            <v-row v-if="gameState.currentQuestion">
                <v-col cols="12" sm="5">
                    <v-textarea
                        v-model="gameState.currentQuestion.questionText"
                        filled
                        rows="1"
                        disabled
                    ></v-textarea>
                </v-col>
                <v-col cols="12" sm="2">
                    <v-chip class="ma-2" color="dark">
                        {{ countDown }} seconds
                        <v-icon right>
                            mdi-timer
                        </v-icon>
                    </v-chip>
                </v-col>
                <v-col cols="12" sm="5"
                    ><v-textarea
                        v-model="gameState.currentQuestion.answer"
                        filled
                        rows="1"
                        disabled
                    ></v-textarea>
                </v-col>
            </v-row>

            <v-row
                ><v-col cols="12" sm="4">
                    <v-card>
                        <v-card-title>{{ me.name }}</v-card-title>
                        <v-card-title>{{ me.score }}</v-card-title>
                        <v-card-text>
                            <v-textarea v-model="playerAnswer" filled></v-textarea>
                        </v-card-text>

                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="primary" depressed @click="submitAnswer()">
                                Submit Answer
                            </v-btn></v-card-actions
                        >
                    </v-card>
                </v-col>
                <v-col cols="12" sm="4">
                    <iframe
                        src="https://www.twitch.tv/embed/birdwizard_/chat?parent=localhost"
                        height="500px"
                        width="100%"
                    ></iframe>
                </v-col>
                <v-col cols="12" sm="4" v-if="gameState.players"
                    ><ScoreTable :players="gameState.players.slice(1)"></ScoreTable>
                </v-col>
            </v-row>
        </div>
    </div>
</template>

<script>
import ScoreTable from "../components/ScoreTable.vue";
export default {
    name: "Game",
    data: () => ({
        questionText: "",
        gameState: {},
        answer: "",
        playerAnswer: "",
        error: "",
        countDown: 0,
        me: {}
    }),
    components: {
        ScoreTable
    },
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
        }
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
            this.$GameServer.AddQuestion(this.gameState.id, this.questionText, this.answer);
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
            this.$GameServer.UpdatePlayerScore(this.gameState.id, scoreChange, connectionId);
        },
        submitAnswer() {
            this.$GameServer.SubmitAnswer(
                this.gameState.id,
                this.playerAnswer,
                this.me.connectionId
            );
        }
    }
};
</script>
