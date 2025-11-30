<script setup>
import {onMounted, ref} from "vue";
import EmpForm from "./EmpForm.vue";
import EmpRemove from "./EmpRemove.vue";

    let arr = ref([]);
    const show_add = ref(false);
    const show_edit = ref(false);
    const remove = ref(false);
    const currentEmployee = ref({});
    const currentID = ref("");
    const deps = ref([]);
    const currentDep = ref("");

    onMounted(() => {
        GetEmpList();
        getDeps();
    });

    function GetEmpList(){
        fetch("http://localhost:5160/api/Employee")
            .then (res => {
                if (!res.ok){
                    throw new Error("Could not fetch data")
                }
                return res.json();
            })
            .then(data => {
                console.log(data);
                arr.value = data;
            });
    }
    
    function getDeps(){
        fetch('http://localhost:5160/api/Employee/deps')
        .then(res => res.json())
        .then(data => deps.value = data);
    }
    function SetToDelete(emp){
        remove.value = true;
        currentEmployee.value = emp;
    }

    function filterByDeps(dep){
        if (dep == "all"){
            GetEmpList()
            return;
        }else{
            arr.value = arr.value.filter(emp => emp.department == dep);
        }
        
    }


</script>

<template>
    <div class="card">
        <div style="margin-top: 1rem;">
            <button class="btn" v-on:click="GetEmpList()">Refresh</button>
            <button disabled>Search by Department
                <select v-model="currentDep" v-on:change="filterByDeps(currentDep)">
                <option value="all">All</option>
                <option v-for="val in deps" :value="val">{{ val }}</option>
            </select>
            </button>
            
        </div>

        <EmpForm  v-if="show_add" v-on:close="show_add=false; GetEmpList()"/>
        <EmpForm  v-if="show_edit" :id="currentID" :edit="show_edit" v-on:close="show_edit=false; GetEmpList()"/>
        <EmpRemove v-if="remove" :employee="currentEmployee" v-on:close="remove=false; GetEmpList()"/>

        <button class="btn" style="background-color: lightgreen;" v-on:click="show_add = true">Add Employee</button>
        <table>
            <tr>
                <th>Firstname</th>
                <th>Lastname</th>
                <th>Hire Date</th>
                <th>Department</th>
                <th>Wage</th>
                <th></th>
            </tr>
            <tr v-for="val in arr">
                <td>{{ val.firstName }}</td>
                <td>{{ val.lastName }}</td>
                <td>{{new Date(val.hireDate).toDateString() }}</td>
                <td>{{ val.department }}</td>
                <td>USD$ {{ val.pay }}</td>
                <td>
                    <button class="btn" style="background-color:lightgoldenrodyellow;" 
                            v-on:click="show_edit= true; currentID=val.id"
                            >Edit</button>
                    <button class="btn" style="background-color:lightcoral;" v-on:click="SetToDelete(val)">Elim</button>
                </td>
            </tr>
        </table>
    </div>
    
</template>

<style scoped>

table{
  border-width: 2px;
  border-style: dashed;
  background-color: lightgray;
  border-radius: 10px;
  padding: 20px;
  margin: 1rem;
}

tr{
  background-color: grey;
}

td,th{
  padding:5px;
  border-style: hidden;
  border-radius: 5px;
}

th{
    border-bottom-left-radius: 0px;
    border-bottom-right-radius: 0px;
}

button{
    padding: 0.5rem;
    border-radius: 5px;
    border-color: gray;
    margin: 5px;
}
select{
    padding: 0.5rem;
    border-radius: 5px;
    border-color: gray;
    margin: 5px;
}
input{
    padding: 0.5rem;
    border-radius: 5px;
    border-color: gray;
    margin: 5px;
}


</style>
