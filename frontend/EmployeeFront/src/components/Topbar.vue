<script setup>

</script>

<template>
    <div class="bar">
        <h3>Employee Management</h3>
        <hr>
        <router-link
         class="link"
         :to="'/login'">Log Out</router-link>
    </div>
</template>

<style>

.bar{
    display: flex;
    background-color: lightgray;
    box-shadow: 5px 8px 5px grey;
    padding:1rem;
    border-bottom-left-radius: 20px;
}

hr{
    border:none;
}

h3{
    padding:    0.5rem;
}

.link{
  display: block;
  font-size: 1.17em;
  margin-top: 1em;
  margin-bottom: 1em;
  margin-left: 0;
  margin-right: 0;
  font-weight: bold;
    background-color: white;
    padding: 0.5rem;
    border-radius: 0.3rem;
    color: black;
}

</style>