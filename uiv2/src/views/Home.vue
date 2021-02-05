<template>
    <div class="home">
        <v-row>
            <v-col cols="12" sm="12">
                <v-alert dense outlined type="error" v-if="error">
                    {{ error }}
                </v-alert>
            </v-col>
        </v-row>
        <v-row>
            <v-col cols="12" sm="4">
                <v-card>
                    <v-card-title>Join Game</v-card-title>
                    <v-card-text>
                        <v-text-field v-model="gameId" label="Game Id" outlined></v-text-field>
                        <v-text-field
                            v-model="playerNameJoin"
                            label="Player Name"
                            outlined
                        ></v-text-field>
                    </v-card-text>

                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="primary" depressed @click="join()">
                            Join
                        </v-btn>
                    </v-card-actions>
                </v-card></v-col
            >
            <v-col cols="12" sm="4">
                <v-card>
                    <v-card-title>Create Game</v-card-title>
                    <v-card-text>
                        <v-text-field
                            v-model="playerNameCreate"
                            label="Player Name"
                            outlined
                        ></v-text-field>
                        <v-text-field
                            v-model="twitch"
                            label="Twitch Useranem (Eg. birdwizard_)"
                            outlined
                        ></v-text-field>
                        <v-select
                            :items="playerCounts"
                            v-model="playerCount"
                            filled
                            label="Player Count"
                        ></v-select
                    ></v-card-text>

                    <v-card-actions>
                        <v-spacer></v-spacer>
                        <v-btn color="primary" depressed @click="create()">
                            Create
                        </v-btn>
                    </v-card-actions>
                </v-card></v-col
            >
        </v-row>
    </div>
</template>

<script>
// @ is an alias to /src

export default {
    name: "Home",
    data: () => ({
        gameId: "",
        playerNameJoin: "",
        playerNameCreate: "",
        error: "",
        twitch: "birdwizard_",
        playerCount: 12,
        playerCounts: [12, 24, 36, 48, 64, 128]
    }),
    gameServer: {
        JoinedSuccessful(gameId) {
            this.$router.push("game?id=" + gameId);
        },
        JoinError(error) {
            this.error = error;
        }
    },
    methods: {
        join() {
            this.error = "";
            if (this.gameId && this.playerNameJoin) {
                this.$GameServer.JoinGame(this.gameId, this.playerNameJoin);
            } else {
                this.error = "Game Id and Player Name Required";
            }
        },
        create() {
            this.error = "";
            if (this.playerNameCreate) {
                this.$GameServer.CreateGame(
                    this.getRandomString(6),
                    this.playerNameCreate,
                    this.playerCount,
                    this.twitch
                );
            } else {
                this.error = "Player Name Required";
            }
        },
        getRandomString(length) {
            var randomChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = "";
            for (var i = 0; i < length; i++) {
                result += randomChars.charAt(Math.floor(Math.random() * randomChars.length));
            }
            return result;
        }
    }
};
</script>
