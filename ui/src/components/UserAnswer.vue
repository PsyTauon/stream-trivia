<template>
    <div class="border rounded-lg h-32 flex flex-col px-4">
        <div class="flex-1">
            {{ username }}
        </div>
        <div class="flex-grow">
            {{ answer }}
        </div>
        <div class="flex-1 flex">
            <i class="full flex-1">❌</i>
            <i class="full flex-1">✔️</i>
        </div>
    </div>
</template>

<script>
export default {
    props: ['username', 'answer']
}
</script>