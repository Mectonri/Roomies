<template>
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
        <input type="text" v-model="item.CollocName" class="form-control" required>
      </div>

      <el-button native-type="submit" class="btn btn-primary">Sauvegarder</el-button>
    </form>
  </div>

</template>


<script>

import {
  CreateColloc
} from "../api/RoomiesApi";
import {
    AddColRoom
} from "../api/RoomiesApi";

export default {
  data() {
    return {
      item: {},
      id: null,
      errors: []
    };
  },

  async mounted() {
    this.id = this.$route.params.id;

  },

  methods: {
    async onSubmit(event) {
      event.preventDefault();

      var errors = [];

      if (!this.item.CollocName) errors.push("CollocName");

      this.errors = errors;

      if (errors.length == 0) {
        try {
          var idColloc = await CreateColloc(this.item);
          this.$router.replace("/Home/");
        } catch (e) {
          console.error(e);
        }
      }
    }
  }
};
</script>
