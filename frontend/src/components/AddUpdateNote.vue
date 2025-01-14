<template>
    <el-form label-width="auto" style="max-width: 600px">
        <el-form-item label="Title">
            <el-input v-model="title" />
        </el-form-item>
        <el-form-item label="Content">
            <el-input v-model="content" />
        </el-form-item>

        <el-form-item>
            <el-button type="primary" @click="onSubmit">Submit</el-button>
            <el-button type="primary" @click="onBackClick">Back</el-button>
        </el-form-item>
    </el-form>
</template>

<script>
import { reactive } from 'vue'
import axios from 'axios';
import { useRouter } from 'vue-router'
import { userStore } from '@/stores/userStore'

export default {

    data() {

        const user = userStore();

        return {
            id: "",
            title: "",
            content: "",
            userId: user.id,
            //nodeId:this.$router.currentRoute._value.query.noteId
        }
    },
    mounted() {
        console.log(this.$router.currentRoute._value.query.noteId)
        this.getUser();
    },
    methods: {
        onSubmit: function ($event) {
            console.log("onSubmit");
            if (this.$router.currentRoute._value.query.noteId != "") {
                console.log("update")
                axios.put(`https://localhost:59916/api/note/${this.$router.currentRoute._value.query.noteId}`, {
                    title: this.title,
                    content: this.content,
                    userId: this.userId,
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
                axios.post(`https://localhost:59916/api/note`, {
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

            axios.get(`https://localhost:59916/api/note/getOne/${this.$router.currentRoute._value.query.noteId}`)
                .then(response => {
                    const note = response.data;
                    this.id = note.id;
                    this.title = note.title;
                    this.content = note.content
                })
                .catch(error => {
                    console.log(error);
                });
        },
        onBackClick: function ($event) {
            this.$router.push({ name: 'notelist' })
        }
    }
}



</script>