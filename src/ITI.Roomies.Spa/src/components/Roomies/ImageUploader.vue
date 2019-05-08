<template>
    <div>
<br>
        <form enctype="multipart/form-data">
            <input type="file" name="image" accept="image/x-png,image/jpg,image/jpeg" @change="handleImageUpload($event.target.files)" multiple/>
        <el-button type="button" @click="submitImage()">Importer une/des photo(s)</el-button>
        </form>
        
        <el-button icon="el-icon-download" @click="DownloadImage()">Telecharger toutes les photos</el-button>
    </div>
</template>

<script>

import axios from "axios"


export default {
    data(){
        return {
            file : new FormData(),
            env : process.env.VUE_APP_BACKEND,
        }
    },

    methods:{
        async submitImage() {
            const file = this.file;
            const endpoint = process.env.VUE_APP_BACKEND + "/api/image";
            let data = await axios.post(`${endpoint}/uploadImage`, file,
            {
                headers: {
                            'Content-type' : "multipart/form-data",
                        },
                        responseType: 'application/json'
            });
        }
    },
}
</script>