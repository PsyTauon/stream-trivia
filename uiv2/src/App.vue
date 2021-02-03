<template>
    <v-app>
        <v-app-bar app color="primary" dark>
            <div class="d-flex align-center">
                <v-btn text to="/">
                    <h2>Stream Trivia</h2>
                </v-btn>
            </div>
        </v-app-bar>

        <v-main>
            <v-container>
                <router-view />
            </v-container>
        </v-main>
    </v-app>
</template>

<script>
export default {
    name: "App"
};
</script>
