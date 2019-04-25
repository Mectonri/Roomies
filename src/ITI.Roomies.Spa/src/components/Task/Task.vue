<template>
  <el-container>
    <el-header>
      <h1>Tâches</h1>
    </el-header>
    <el-main v-if="taskData[0]">
      <h2>{{taskData[0].collocId}}</h2>

      <table>
        <tr>
          <td>Nom</td>
          <td>Echéance</td>
          <td>Etat</td>
        </tr>
        <tr v-for="task of taskData">
          <td>{{ task.taskName }}</td>
          <td>{{ task.taskDate }}</td>
          <td>{{ task.state }}</td>
        </tr>
      </table>
    </el-main>
    <el-main v-else>Chargement en cours</el-main>
  </el-container>
</template>

<script>
import { DateTime } from "luxon";
import AuthService from "../../services/AuthService";
import { getTasksByCollocIdAsync } from "../../api/TaskApi.js";
import { state } from "../../state";

export default {
  data() {
    return {
      //   item: {},
      //   id: null,
      errors: [],
      taskData: []
    };
  },
  computed: {
    auth: () => AuthService,

    isLoading() {
      return this.state.isLoading;
    }
  },
  async mounted() {
    console.log("yo");
    //   console.log(this.item);
    try {
      this.taskData.push(await getTasksByCollocIdAsync(7));
      console.log(this.taskData);
    } catch (e) {
      console.log(e);
    }
  }
};
</script>