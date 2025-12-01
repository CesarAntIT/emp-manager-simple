<script setup>
import EmpList from './components/EmpList.vue';
import Topbar from './components/Topbar.vue';
import LogIn from './components/LogIn.vue';
</script>

<template>
  <div>
    <Topbar/>
      <RouterView></RouterView>
  </div>
</template>

<style scoped>
div{
  font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif
}
</style>
