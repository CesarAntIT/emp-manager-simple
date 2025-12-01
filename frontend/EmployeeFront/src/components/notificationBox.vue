<script setup>
    const prop = defineProps({
        message: String,
        success: Boolean
    })
    const emits = defineEmits(['close'])

</script>

<template>
    <div v-if="success" class="notif-good" v-on:click="$emit('close')">
        <p><b>{{ message }}</b><br>notification</p>
    </div>
    <div v-else class="notif-bad" v-on:click="$emit('close')">
        <p><b>{{message}}</b><br>notification</p>
    </div>
</template>

<style lang="less" scoped>
.notif-good{
    color: green;
    background-color: rgba(144, 238, 144, 0.712);
    padding-left: 1rem;
    margin: 20px;
    width: 10rem;
    max-width: 25rem;
    border-radius: 0.5rem;
    border-color: rgba(0, 128, 0, 0.298);
    border-style: solid;
    display: flex;
    align-items:center;

        &:hover{
            background-color: rgba(144, 238, 144, 1);
    }
}

.notif-bad{
    color: darkred;
    background-color: rgba(240, 128, 128, 0.712);
    padding-left: 1rem;
    margin: 20px;
    width: 10rem;
    max-width: 25rem;
    border-radius: 0.5rem;
    border-color: rgba(255, 0, 0, 0.298);
    border-style: solid;
    display: flex;
    align-items:center;

    &:hover{
            background-color: rgba(240, 128, 128, 1);
    }
}
</style>