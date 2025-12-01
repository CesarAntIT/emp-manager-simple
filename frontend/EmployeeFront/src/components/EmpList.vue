<script setup>
import {onMounted, ref} from "vue";
import EmpForm from "./EmpForm.vue";
import EmpRemove from "./EmpRemove.vue";
import notificationBox from "./notificationBox.vue";

    let arr = ref([]);
    const show_add = ref(false);
    const show_edit = ref(false);
    const remove = ref(false);
    const show_notif = ref(false);

    const notif_info = ref({
        message: "",
        success: false
    })

    const currentEmployee = ref({});
    const currentID = ref("");
    const deps = ref([]);
    const currentDep = ref("");

    const listFind = ref({
        message: "texxt",
        success: false
    });

    onMounted(() => {
        GetEmpList();
        getDeps();
    });

    async function GetEmpList(){
       await fetch("http://localhost:5160/api/Employee")
            .then (res => res.json())
            .then(data => {
                console.log(data);
                arr.value = data.value;
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
    async function filterByDeps(dep){
        if (dep == "all"){
            await GetEmpList();
            return;
        }else{
            await GetEmpList();
            arr.value = arr.value.filter(emp => emp.department == dep);
        }
        
    }

function ShowNotif(message, success){
    notif_info.value.message = message;
    notif_info.value.success = success;
    show_notif.value = true;
}

function CloseNotif(){
  show_notif.value = false;
  notif_info.value.message = "";
  notif_info.value.success = false;
}


</script>

<template>
    <div>
        <div style="margin-top: 1rem;">
            <button class="btn" v-on:click="GetEmpList();
                                            ShowNotif('Showing found employees',true)">Refresh</button>
            <button disabled>Search by Department
                <select v-model="currentDep" v-on:change="filterByDeps(currentDep)">
                <option value="all">All</option>
                <option v-for="val in deps" :value="val">{{ val }}</option>
            </select>
            </button>
            
        </div>

        <!--Notifications for Testing-->
        <notificationBox class="notif" v-if="show_notif" v-on:close="CloseNotif()" :message="notif_info.message" :success="notif_info.success"/>

        <!--Edit and Add Form-->
        <EmpForm  v-if="show_add" 
                    v-on:close="show_add=false; GetEmpList()" 
                    v-on:halted="ShowNotif"
                    v-on:completed=""/>

        <EmpForm  v-if="show_edit" :id="currentID" :edit="show_edit" 
                  v-on:close="show_edit=false; GetEmpList()"
                  v-on:halted="ShowNotif"/>

        <!--Remove Form-->
        <EmpRemove v-if="remove" :employee="currentEmployee" 
                v-on:close="remove=false; GetEmpList()"
                v-on:completed="ShowNotif"
                />

        <button class="btn" style="background-color: lightgreen;" v-on:click="show_add = true">Add Employee</button>
        <table v-if="arr.length > 0">
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
        <div v-else class="card">
            <h3>No se encuentran empleados en el sistema</h3>
        </div>
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
.card{
  border-width: 2px;
  border-style: dashed;
  background-color: lightgray;
  border-radius: 10px;
  padding: 20px;
  margin: 1rem;
}

.notif{
    position: absolute;
    top: 10%;
    left: 20rem;
    
}
div{
    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif
}

</style>
