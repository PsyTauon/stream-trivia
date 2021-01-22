<template>
    <div class="shadow-brand border border-brand min-h-32 w-60 flex flex-col">
        <div class="h-10 bg-primary text-center py-2 text-white rounded-t-lg">
            {{ username }}
        </div>
        <div class="flex-grow px-4 pt-2">
            {{ answer }}
        </div>
        <div class="h-15 flex text-center py-4">
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