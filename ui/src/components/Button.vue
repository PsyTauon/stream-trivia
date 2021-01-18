<template>
      <input class="flex-1 max-w-sm button" type="button" v-bind:value="title" />
</template>

<script>
export default {
    props: ['title']
}
</script>