<template>
    <v-simple-table fixed-header height="500px">
        <template v-slot:default>
            <thead>
                <tr>
                    <th class="text-left">
                        Name
                    </th>
                    <th class="text-left">
                        Score
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="player in players" :key="player.id">
                    <td>{{ player.name }}</td>
                    <td>{{ player.score }}</td>
                </tr>
            </tbody>
        </template>
    </v-simple-table>
</template>

<script>
export default {
    props: ["players"]
};
</script>
