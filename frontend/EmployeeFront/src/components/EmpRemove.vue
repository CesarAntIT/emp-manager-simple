<script setup>
import {ref} from "vue";

const props = defineProps(['employee']);
const emits = defineEmits(['close','completed']);

const emp = ref({
    firstName: props.employee.firstName,
    lastName: props.employee.lastName,
    Pay: props.employee.pay,
    ID: props.employee.id,
    Department:props.employee.department,
    hireDate: props.employee.hireDate,
    BirhtDate: props.employee.birthDate,
})

async function RemoveEmployee(){

    await fetch(`http://localhost:5160/api/Employee?Id=${props.employee.id}`, {
    method: 'DELETE'
    })
    .then(res => res.json())
    .then(data => {
        emits('completed', data.message, data.success);
        emits('close');
    })
}

</script>

<template>
    <div class="main-card">
        <h2>Eliminar Empleado</h2>
        <p>Desea eliminar al empleado {{ emp.firstName.toUpperCase() }} {{ emp.lastName.toUpperCase() }}<br>
           Que ha sido empleado desde {{ new Date(emp.hireDate).toDateString() }}
        </p>
        <button v-on:click="$emit('close')">Cancel</button>
        <button style="background-color: lightcoral;" v-on:click="RemoveEmployee()">Remove</button>
    </div>
</template>

<style lang="less" scoped>
.main-card{
    position: absolute;
    background-color: white;
    box-shadow: 2px 5px 30px;
    padding: 2em;
    border-radius: 1rem;
    width: 50%;

    input{
        padding: 10px;
        margin: 1px;
    }
    button{
        padding: 0.5rem;
        border-radius: 5px;
        border-color: gray;
        margin: 5px;
    }
}
</style>