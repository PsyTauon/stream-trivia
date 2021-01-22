<template>
  <div class="grid grid-cols-game gap-4">
    <div class="text-center col-span-2">
      <div class="inline-block m-10">
        <span class="bg-pink-400 p-5 rounded-full">{{ countdown }}</span>
      </div>
    </div>
    <div>
      <div class="flex flex-col items-center">
        <div class="flex flex-col space-y-4 items-center mb-6">
          <textarea class="input max-w-xl w-52" v-model="questionText" placeholder="Question" />
          <textarea class="input max-w-xl w-52" v-model="answer" placeholder="Answer" />
        </div>
        <Button class="button" @click="SendQuestion()" title="Send Question" />
      </div>
      <div class="mt-10 items-cetner">
        Leaderboard
      </div>
    </div>
    <div class="flex flex-wrap gap-8">
      <UserAnswer username="Psy" answer="This is an answer. It is really long, and probably too wordy. No one wants to read it." />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
      <UserAnswer username="birdwizard_" answer="Another answer" />
    </div>
    <div>
      <label>{{ gameState }}</label>
    </div>
  </div>
</template>

<script>
import Button from '../../components/Button'
import UserAnswer from '../../components/UserAnswer'

export default {
  props: {
    countdown: Number
  },
  components: {
    Button,
    UserAnswer
  }
}
</script>