<template>
  <el-container>
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
        <el-input type="text" v-model="item.CollocName" required />
      </div>

      <el-button native-type="submit">Sauvegarder</el-button>
    </form>
  </div>
  </el-container>

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

          // Ajout à la colloc en utilisation
          this.$currColloc.setCollocId(idColloc);
          this.$currColloc.setCollocName(this.item.CollocName);
     
          this.$router.replace("/roomies/collocation/" + idColloc);
        } catch (e) {
          console.error(e);
        }
      }
    }
  }
};
</script>
