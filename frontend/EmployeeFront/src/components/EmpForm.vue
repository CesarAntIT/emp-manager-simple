<script setup>
import {computed, onMounted, ref} from "vue";
    const disabled = ref(0);
    const props = defineProps({
        edit: Boolean,
        id: String,
    })
    const emits = defineEmits(['close','completed','halted']);

    const Emp = ref({
        ID: '',
        firstName: '',
        lastName: '',
        BirthDate: '',
        Department: '',
        hireDate: '',
        Pay: ''
    });

    function getEditEmployee(id, edit){
        if (edit){
            fetch(`http://localhost:5160/api/Employee/byId?id=${id}`)
            .then(res => res.json())
            .then(data => {
                Emp.value.ID = data.id;
                Emp.value.firstName = data.firstName;
                Emp.value.lastName = data.lastName;
                Emp.value.BirthDate = data.birthDate.split('T')[0];
                Emp.value.hireDate = data.hireDate.split('T')[0];;
                Emp.value.Department = data.department;
                Emp.value.Pay = data.pay;
            })
        }
        return;
    }

    function AddEmployee(){
        
        Emp.value.ID = "7d1b5e35-4e7a-4fb8-ad99-805898ffe3b5";

        if (!Emp.value.firstName||!Emp.value.lastName||!Emp.value.Pay||!Emp.value.BirthDate||!Emp.value.Department)
        {
            emits('halted',"All text fields must be full",false);
            return;
        }
        if (isNaN(Emp.value.Pay)){
            emits('halted',"The pay for an employee must be a number",false)
            return;
        }
        if (Emp.value.Pay < 0){
            emits('halted',"The Pay Amount cannot be below 0",false);
            return;
        }

        Emp.value.hireDate = new Date().toISOString();
        const bDate = new Date(Emp.value.BirthDate).toISOString();

        fetch('http://localhost:5160/api/Employee', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
              body: JSON.stringify({
                id: Emp.value.ID,
                firstName: Emp.value.firstName,
                lastName: Emp.value.lastName,
                pay: Emp.value.Pay,
                birthDate: bDate,
                hireDate: Emp.value.hireDate,
                department: Emp.value.Department
            })
        }).then(res => {
            if (!res.ok){
                alert("Los campos no están correctos y no se realizó la operación, tener en cuenta:\nDepartament solo 2 Letras\nFecha de Nacimiento anterior a 2008\nNo dejar espacios en blanco")
                return;

            }else{
                emits('close');
            }
        })
        
    }

    function EditEmployee(){

        if (!Emp.value.firstName||!Emp.value.lastName||!Emp.value.Pay||!Emp.value.BirthDate||!Emp.value.Department)
        {
            emits('halted',"Todos los campos deben estar llenos",false)
            return;
        }
        if (isNaN(Emp.value.Pay)){
            emits('halted',"The pay for an employee must be a number",false)
            return;
        }
        if (Emp.value.Pay < 0){
            emits('halted',"The Pay Amount cannot be below 0",false)
            return;
        }


        const bDate = new Date(Emp.value.BirthDate).toISOString();
        const hDate = new Date(Emp.value.hireDate).toISOString();

        fetch('http://localhost:5160/api/Employee', {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id: Emp.value.ID,
                firstName: Emp.value.firstName,
                lastName: Emp.value.lastName,
                pay: Emp.value.Pay,
                birthDate: bDate,
                hireDate: hDate,
                department: Emp.value.Department
            })
        })
        .then(res => res.json()) 
        .then(data => {

            emits('halted',data.message,data.success);
            if (data.success){
                emits('close');
            }
        })
    }

    getEditEmployee(props.id, props.edit);

</script>

<template>
    <div class="main-card">
        <h2 v-if="!edit">Add an Employee</h2>
        <h2 v-if="edit">Edit an Employee</h2>

            <div style="display: flex;">
            <div>
                <input type="text" hidden>
                <div>
                    <label for="">Name</label><br>
                    <input type="text" placeholder="Name" v-model="Emp.firstName">
                </div>
                <div>
                    <label for="">Lastname</label><br>
                    <input type="text" placeholder="Lastname" v-model="Emp.lastName">
                </div>
                <div>
                    <label for="">Birth Date</label><br>
                    <input type="date" v-model="Emp.BirthDate">
                </div>
                </div>

                <div>
                    <div>
                        <label for="">Department</label><br>
                        <input type="text" placeholder="Dep" v-model="Emp.Department">
                    </div>

                    <div>
                        <label for="">Pay</label><br>
                        <input type="number" step="0.10" placeholder="USD$" required v-model="Emp.Pay">
                    </div>

                    <div v-if="edit">
                        <label for="">Hire Date</label><br>
                        <input type="date" v-model="Emp.hireDate">
                    </div>

                    <button v-on:click="$emit('close')">Cancel</button>

                    <button v-if="!edit" style="background-color: lightgreen;"
                            type="submit"
                            v-on:click="AddEmployee()">Add</button>
                    <button v-if="edit" style="background-color: lightgreen;" 
                            v-on:click="EditEmployee()">Edit</button>
                </div>
            </div>
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
    min-width: 30rem;

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

