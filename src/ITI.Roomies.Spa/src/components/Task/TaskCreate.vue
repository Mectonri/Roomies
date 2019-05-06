<template>
  <el-container>
    <el-header>
      <h1>Tâches</h1>
    </el-header>

    <el-main v-if="roomiesList[0]">
      <el-form @submit="onSubmit($event)">
        <!-- <div class="alert alert-danger" v-if="errors.length > 0">
        <b>Les champs suivants semblent invalides :</b>

        <ul>
          <li v-for="e of errors">{{e}}</li>
        </ul>
        </div>-->

        <div>
          <label class="required">Nom</label>
          <el-input type="text" v-model="item.TaskName" required/>
        </div>

        <div>
          <label>Description</label>
          <el-input type="textarea" v-model="item.TaskDes"/>
        </div>

        <div>
          <label class="required">Echéance</label>
          <el-input type="datetime-local" id="echeance" v-model="item.TaskDate" required/>
        </div>
        <br>
        <br>
        <tr v-for="roomie of roomiesList" :key="roomie.roomieId">
          <td>
            <input type="checkbox" :id="'roomie' + roomie.roomieId" checked>
            <label :for="'roomie' + roomie.roomieId">{{ roomie.firstName }} {{ roomie.lastName }}</label>
          </td>
        </tr>
        <br>
        <br>
        <el-button @click="onSubmit">Sauvegarder</el-button>
      </el-form>
    </el-main>
    <el-main v-else>Chargement en cours</el-main>
  </el-container>
</template>

<script>
import { DateTime } from "luxon";
import AuthService from "../../services/AuthService";
import { state } from "../../state";
import { GetRoomiesIdNamesByCollocIdAsync } from "../../api/CollocationApi.js";
import { createTaskSansDescAsync } from "../../api/TaskApi.js";

export default {
  data() {
    return {
      errors: [],
      item: {},
      roomiesList: []
    };
  },
  computed: {
    auth: () => AuthService,

    isLoading() {
      return this.state.isLoading;
    }
  },
  async mounted() {
    try {
      this.roomiesList = await GetRoomiesIdNamesByCollocIdAsync(
        this.$currColloc.collocId
      );
      console.log(this.roomiesList);

      console.log("yo");
    } catch (e) {
      console.log(e);
    }
  },

  methods: {
    async onSubmit() {
      event.preventDefault();

      this.item.roomiesId = [];

      for (var i = 0; i < this.roomiesList.length; i++) {
        if (
          document.getElementById("roomie" + this.roomiesList[i].roomieId)
            .checked
        )
          this.item.roomiesId.push(this.roomiesList[i].roomieId);
      }

      var errors = [];

      if (!this.item.TaskName) errors.push("Nom");
      if (!this.item.TaskDate) errors.push("Echéance");
      if (!this.item.roomiesId[0])
        errors.push("Aucun roomie selectionné pour la tâche.");

      this.errors = errors;

      if (errors.length == 0) {
        try {
          //   var idRoomie = await createRoomieAsync(this.item);
          //   this.$router.replace("/roomies/" + idRoomie);
          this.item.collocId = this.$currColloc.collocId;
          console.log(this.item);
          await createTaskSansDescAsync(this.item);
          this.$router.replace("/task/colloc");
        } catch (e) {
          console.error(e);
        }
      }
      else{
          for( var j = 0; j < errors.length; j++){
              console.log(errors[j]);
          }
      }
    }
  }
};
</script>