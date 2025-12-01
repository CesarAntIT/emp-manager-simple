<script setup>
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';
import notificationBox from './notificationBox.vue';

const show_notif = ref(false);
const notif_info = ref({
    message: "mango",
    success: false
});
const router = useRouter();
const User = ref({
    ID: "",
    Name: "",
    Password: "",
    Role: "Admin"
});

onMounted(() => {
    sessionStorage.removeItem('User');
})

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


async function LogIn(){
    if (User.value.Name == ""){
        ShowNotif("Username cannot be empty value",false);
        setTimeout(() => {
            CloseNotif();
            }, 2000);  
        return;
    }

    if (User.value.Password == ""){
        ShowNotif("Password cannot be empty value",false);
        setTimeout(() => {
            CloseNotif();
            }, 2000);  
        return;
    }
    
    await fetch(`http://localhost:5160/api/LogIn?username=${User.value.Name}&password=${User.value.Password}`)
        .then(res => res.json())
        .then(data => {                
            ShowNotif(data.message,data.success)

            if (data.success){
                sessionStorage.setItem('User',JSON.stringify(data.value))                
                setTimeout(() => {
                    router.push({path: '/'});       
                }, 3000);    
            }else{
                setTimeout(() => {
                CloseNotif();
                }, 2000);   
            }
        })
}

</script>

<template>
    <notificationBox class="notif" v-if="show_notif" 
        :message="notif_info.message" 
        :success="notif_info.success"
        v-on:close="CloseNotif"/>

    <div class="main-card">
        <p>Welcome to the Small Employee Manager</p>
        <h3>Log In</h3>
        <input type="text" placeholder="Username" v-model="User.Name">
        <input type="password" placeholder="Password" v-model="User.Password">
        <div>
            <button v-on:click="LogIn()">Log In</button>
            <button disabled>Register</button>
        </div>
    </div>
</template>

<style lang="less" scoped>
.main-card{
    
    position: absolute;
    background-color: white;
    box-shadow: 2px 5px 10px;
    padding: 2em;
    border-radius: 1rem;
    width: 50%;
    min-width: 30rem;
    margin: 2rem;

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

.notif{
    position: absolute;
    top: 10%;
    left: 20rem;
    z-index: 9;
    
}

</style>