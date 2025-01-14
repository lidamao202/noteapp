<template>
    <el-form :model="form" label-width="auto" style="max-width: 600px">
        <el-form-item label="User name">
            <el-input v-model="form.userName" />
        </el-form-item>
        <el-form-item label="Password" >
            <el-input v-model="form.password" type="password"/>
        </el-form-item>
        <el-form-item label="Confirm Password" >
            <el-input v-model="form.confirmPassword" type="password"/>
        </el-form-item>
        <el-form-item>
            <el-button type="primary" @click="onSubmit">Submit</el-button>
        </el-form-item>
    </el-form>
</template>

<script lang="ts" setup>

import { reactive } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios';
import { userStore } from '@/stores/userStore'

const form = reactive(
    {
        userName: "",
        password: "",
        confirmPassword:""
    }
)
const router = useRouter();
const user = userStore();
const onSubmit = () => {
    if(form.password == form.confirmPassword){
        axios.post(`https://localhost:59916/api/account/register`,
        {
            userName: form.userName,
            password: form.password,
        },
        {
            headers: {
                "Content-Type": "application/json"
            }
        }
    )
        .then(response => {
            user.id = response.data.id;
            user.userName = response.data.userName;

            router.push({ path: '/' })
        })
        .catch(error => {
            console.log(error);
        });
    }

}
</script>