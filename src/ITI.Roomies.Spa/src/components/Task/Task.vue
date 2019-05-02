<template>
  <el-container>
    <el-header>
      <h1>Tâches</h1>
    </el-header>

    <el-main v-if="taskData[0]">
      <h2>{{$currColloc.collocName}}</h2>

      <table v-if="taskData !='Nada'">
        <tr>
          <td>Nom</td>
          <td>Echéance</td>
          <td>Etat</td>
        </tr>
        <tr v-for="task of taskData" :key="task.taskId">
          <td>{{ task.taskName }}</td>
          <td>{{ task.taskDate }}</td>
          <td>{{ task.state }}</td>
        </tr>
      </table>
      <div v-else>
        Aune tâche à afficher
      </div>
    </el-main>
    <el-main v-else>Chargement en cours</el-main>
  </el-container>
</template>

<script>
import { DateTime } from "luxon";
import AuthService from "../../services/AuthService";
import { getTasksByCollocIdAsync } from "../../api/TaskApi.js";
// import { state } from "../../state";

export default {
  data() {
    return {
      errors: [],
      taskData: [],
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
      // console.log("avant : " + this.taskData);
      // console.log(this.taskData.length);
      // this.taskData2 = this.taskData;
      this.futureTaskData = await getTasksByCollocIdAsync(this.$currColloc.collocId);
      if(this.taskData.length == this.futureTaskData){
        this.taskData = "Nada";
      }
      else{
        this.taskData = this.futureTaskData;
      }
      // console.log(this.taskData);
      // console.log(this.taskData.length);
      // console.log(this.taskData == this.taskData2);
    } catch (e) {
      console.log(e);
    }
  }
};
</script>