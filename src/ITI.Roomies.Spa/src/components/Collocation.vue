<template>
    <div class="container">

        <div class="form-group">

      </div>

    <form @submit="onSubmit($event)">
      <div class="alert alert-danger" v-if="errors.length > 0">
        <b>Les champs suivants semblent invalides :</b>

        <ul>
          <li v-for="e of errors">{{e}}</li>
        </ul>
      </div>

      <div class="form-group">
        <label class="required">Nom de collocation</label>
        <input type="text" v-model="item.CollocName" class="form-control" required>
      </div>

      <el-button native-type="submit" class="btn btn-primary">Sauvegarder</el-button>
    </form>
  </div>

</template>


<script>
import {createCollocAsync} from "../api/CollocationApi";

export default {
  data() {
    return {
      item: {},
      UserId: null,
      errors: [],
      idColloc : 0
    };
  },
  

  async mounted() {
    this.idColloc = this.$route.params.id;

  },

  methods: {
    
    async onSubmit(event) {
      event.preventDefault();

      var errors = [];

      var idColloc = null;
      if (!this.item.CollocName) errors.push("CollocName");

      this.errors = errors;
      if (errors.length == 0) {
        try {
          idColloc = await createCollocAsync(this.item);
          this.$router.replace("/");
        } catch (e) {
          console.error(e);
        }
      }
    }
  }
};
</script>
