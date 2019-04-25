<template>
  <el-container>
    <div class="container">
        <div>
        hello this is supposed to be the creation page for a collocation.
        </div>
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
        <!-- <input type="text" v-model="item.CollocName" class="form-control" required> -->
        <el-input type="text" v-model="item.CollocName" required />
      </div>

      <el-button native-type="submit">Sauvegarder</el-button>
    </form>
  </div>
  </el-container>

</template>


<script>
import {createCollocAsync,getCollocByRoomieIdAsync} from "../api/CollocationApi";

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
    console.log('qsldqlsd : ' + await getCollocByRoomieIdAsync());
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
          this.$router.replace("/roomies/collocation/" + idColloc);
        } catch (e) {
          console.error(e);
        }
      }
    }
  }
};
</script>
