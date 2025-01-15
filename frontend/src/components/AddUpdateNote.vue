<template>
    <div class="flex items-center justify-center">
        <div class="w-full max-w-sm p-8 bg-white">
            <el-form label-width="auto" style="max-width: 600px">
                <el-form-item label="Title">
                    <el-input v-model="title" />
                </el-form-item>
                <el-form-item label="Content">
                    <el-input v-model="content" />
                </el-form-item>

                <el-form-item>
                    <el-button v-f="!isView" type="primary" @click="onSubmit">Submit</el-button>
                    <el-button type="primary" @click="onBackClick">Back</el-button>
                </el-form-item>
            </el-form>
        </div>
    </div>

</template>

<script>
import { reactive } from 'vue'
import axios from 'axios';
import { useRouter } from 'vue-router'
import { userStore } from '@/stores/userStore'
import axiosInstance from '../JwtInterceptor';

export default {

    data() {

        //const user = userStore();
        const userId = localStorage.getItem('id');
        this.isView = this.$router.currentRoute._value.query.isView;
        return {
            id: "",
            title: "",
            content: "",
            isView: true
        }
    },
    mounted() {
        this.isView = this.$router.currentRoute._value.query.isView;
        console.log(this.$router.currentRoute._value.query.isView)
        this.getUser();
        const userId = localStorage.getItem('id');
        console.log("userid="+userId)
    },
    updated() {
        this.isView = this.$router.currentRoute._value.query.isView;
    },
    methods: {
        onSubmit: function ($event) {
            const userId = localStorage.getItem('id');
         
            let noteId = this.$router.currentRoute._value.query.noteId;
            if (noteId != undefined && noteId != "") {
                console.log("update")
                axiosInstance.put(`/note/${this.$router.currentRoute._value.query.noteId}`, {
                    title: this.title,
                    content: this.content,
                    userId: userId,
                }, {
                    headers: {
                        "Content-Type": "application/json"
                    }
                })
                    .then(function (response) {
                        alert("Save successfully");
                    })
                    .catch((error) => { console.log(error) })

            } else {
                axiosInstance.post(`/note`, {
                    title: this.title,
                    content: this.content,
                    userId: this.userId
                }, {
                    headers: {
                        "Content-Type": "application/json"
                    }
                })
                    .then(function (response) {
                        alert("Save successfully");
                    })
                    .catch((error) => { console.log(error) })
            }


        },
        getUser: function () {
            const userId = localStorage.getItem('id');
            console.log(userId)
            if (this.$router.currentRoute._value.query.noteId) {
                axiosInstance.get(`/note/getOne/${this.$router.currentRoute._value.query.noteId}`)
                    .then(response => {
                        const note = response.data;
                        this.id = note.id;
                        this.title = note.title;
                        this.content = note.content
                    })
                    .catch(error => {
                        console.log(error);
                    });
            }

        },
        onBackClick: function ($event) {
            this.$router.push({ name: 'notelist' })
        }
    }
}



</script>