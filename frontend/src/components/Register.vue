<template>
    <div class="flex justify-center p-8">
        <div class="w-full max-w-sm p-8 bg-white rounded-lg">
            <h2 class="text-2xl font-semibold text-center text-gray-800 mb-6">Register</h2>
            <el-form :model="form" label-width="auto" style="max-width: 600px">
        <el-form-item label="User name">
            <el-input v-model="form.userName" />
        </el-form-item>
        <el-form-item label="Password">
            <el-input v-model="form.password" type="password" />
        </el-form-item>
        <el-form-item label="Confirm Password">
            <el-input v-model="form.confirmPassword" type="password" />
        </el-form-item>
        <el-form-item>
            <el-button type="primary" @click="onSubmit">Submit</el-button>
        </el-form-item>
    </el-form>
        </div>
        

    </div>

</template>

<script lang="ts" setup>

import { reactive } from 'vue'
import { useRouter } from 'vue-router'
import axios from 'axios';
import { userStore } from '@/stores/userStore'
import axiosInstance from '../JwtInterceptor';

const form = reactive(
    {
        userName: "",
        password: "",
        confirmPassword: ""
    }
)
const router = useRouter();
const user = userStore();
const onSubmit = () => {
    if (form.password == form.confirmPassword) {
        axiosInstance.post(`/account/register`,
            {
                userName: form.userName,
                password: form.password,
            }, {
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