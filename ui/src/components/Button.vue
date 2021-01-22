<template>
      <input class="flex-1 md:inline max-w-sm button w-52" type="button" v-bind:value="title" />
</template>

<script>
export default {
    props: ['title']
}
</script>