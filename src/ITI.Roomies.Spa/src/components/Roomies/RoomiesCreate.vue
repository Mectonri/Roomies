<template>
  <div id="app">
    <el-steps class="el-steps_header" space="50%" :active="0">
      <el-step title="Informations" icon="el-icon-edit"></el-step>
      <el-step title="Photo" icon="el-icon-picture"></el-step>
      <el-step class="flex_basis_0" title="Colocation"></el-step>
    </el-steps>
    
    
    <header class="header_steps">
      <h2>S'inscrire</h2>
    </header>
    <main>
      <form>
        <div class="alert alert-danger" v-if="errors.length > 0">
          <b>Les champs suivants semblent invalides :</b>

          <ul>
            <li v-for="e of errors">{{e}}</li>
          </ul>
        </div>

        <div>
          <label class="required">Prénom</label>
          <input class="form-control" type="text" v-model="item.firstName" required>
        </div>

        <div>
          <label class="required">Nom</label>
          <input class="form-control" type="text" v-model="item.lastName" required>
        </div>

        <div>
          <label class="required">Date de naissance</label>
          <input class="form-control" type="date" v-model="item.birthDate" required>
        </div>

        <div>
          <label class="required">Téléphone</label>
          <input class="form-control" type="text" v-model="item.phone" required>
        </div>
        <br>
        <br>
        <button class="btn btn-dark" @click="onSubmit">Sauvegarder</button>
      </form>
    </main>
  </div>
</template>

<script>
import { createRoomieAsync } from "../../api/RoomiesApi";
import { DateTime } from "luxon";
import AuthService from "../../services/AuthService";

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
    async onSubmit() {
      event.preventDefault();

      var errors = [];

      if (!this.item.lastName) errors.push("Nom");
      if (!this.item.firstName) errors.push("Prénom");
      if (!this.item.birthDate) errors.push("Date de naissance");
      if (!this.item.phone) errors.push("Téléphone");

      this.errors = errors;

      if (errors.length == 0) {
        try {
          var idRoomie = await createRoomieAsync(this.item);
          this.$router.replace("/upload/roomie/create/"+idRoomie);
        } catch (e) {
          console.error(e);
        }
      }
    }
  }
};
</script>
