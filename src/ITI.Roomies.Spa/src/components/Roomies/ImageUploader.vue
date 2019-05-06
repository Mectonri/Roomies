<template>
  <div>
    <div>
      <form action="upload" id="uploader">
        <input type="file" name="pic" accept="image/*">
        <button  @click="upload"> submit </button>
      </form>
    </div>
  </div>
</template>

<script>

import { uploadImage } from "../../api/RoomiesApi";

export default {
  data() {
    return {
      image: {},

    }
  },

  async mounted() {

  },

  methods:{
    async upload(){
      this.image = document.getElementById("uploader");
      console.log(this.image);

      await uploadImage( this.image );

    },

  },
};
</script>
