

<template>
    <el-form ref="dynamicFormRef" :model="formData" :rules="validationRules" label-width="100px"
        @submit.prevent="onSubmit">
        <el-form-item label="Title" prop="title">
            <el-input v-model="formData.title" placeholder="Enter your title" />
        </el-form-item>

        <el-form-item label="Content" prop="content">
            <el-input v-model="formData.content" placeholder="Enter your content" />
        </el-form-item>

        <el-form-item>
            <el-button v-if="!isView" id="submit-id" type="primary" @click="onSubmit">Submit</el-button>
            <!-- <el-button type="primary" @click="onBackClick">Back</el-button> -->
        </el-form-item>
    </el-form>
</template>

<script>
import { reactive, onMounted, ref, watch } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import { getNote, createNote, updateNote } from '@/services/apiService';

export default {
    setup() {
        const router = useRouter();
        const route = useRoute();
        const isView = ref(false);
        isView.value = route.query.isView === 'true';
       
        // Form data
        const formData = reactive({
            title: "",
            content: "",
        });

        const isTitleRequired = ref(false);

        // Validation rules
        const validationRules = reactive({
            title: [
                { required: true, message: "Title is required", trigger: "blur" },
            ]
        });

        watch(isTitleRequired, (newValue) => {
            if (newValue) {
                validationRules.title.unshift({
                    required: true,
                    message: "Title is required",
                    trigger: "blur",
                });
            } else {
                validationRules.title.shift(); // Remove the "required" rule
            }
        });

        // Form reference
        const dynamicFormRef = ref(null);

        // Form submission
        const onSubmit = async () => {

            dynamicFormRef.value.validate((valid) => {
                if (valid) {
                    save();
                } else {
                    console.error("Form validation failed");
                }
            });
        };

        const onBackClick = () => {
            router.push({ name: 'notelist' });
        };
        const save = async () => {
            if (formData.id) {
                await updateNote(formData.id, formData);
            } else {
                await createNote({
                    ...formData
                });
            }
            router.push({ name: 'notelist' });
        }

        onMounted(() => {
            getOneNote();
        });

        const getOneNote = async () => {
            const noteId = route.query.noteId;
            if (noteId) {
                try {
                    const response = await getNote(noteId);
                    formData.id = response.data.id;
                    formData.title = response.data.title;
                    formData.content = response.data.content;
                } catch (error) {
                    console.log(error);
                }
            }
        };

        return {
            formData,
            validationRules,
            isTitleRequired,
            dynamicFormRef,
            onSubmit,
            onBackClick,
            getOneNote,
            isView
        };
    },
};
</script>