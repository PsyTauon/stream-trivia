<template>
      <input class="flex-1 max-w-sm border-brand bg-primary-dark text-white" type="button" v-bind:value="title" />
</template>

<script>
export default {
    props: ['title']
}
</script>