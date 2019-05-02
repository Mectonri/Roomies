<template>
  <el-container>
    <el-header>
      <h1>Tâches</h1>
    </el-header>

    <el-main v-if="taskData[0]">
      <div v-for="taskColloc of taskData" :key="taskColloc.collocId">
        <h2>{{taskColloc.collocId}}</h2>

        <table v-if="taskData !='Nada'">
          <tr>
            <td>Nom</td>
            <td>Echéance</td>
            <td>Etat</td>
          </tr>
          <tr v-for="task of taskColloc.taskArray" :key="task.taskId">
            <td>{{ task.taskName }}</td>
            <td>{{ task.taskDate }}</td>
            <td>{{ task.state }}</td>
          </tr>
        </table>
        <div v-else>Aune tâche à afficher</div>
      </div>
    </el-main>
    <el-main v-else>Chargement en cours</el-main>
  </el-container>
</template>

<script>
import { DateTime } from "luxon";
import AuthService from "../../services/AuthService";
import { GetTasksByRoomieIdAsync } from "../../api/TaskApi.js";
// import { state } from "../../state";

export default {
  data() {
    return {
      errors: [],
      taskData: [],
      taskColloc: ''
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
      this.futureTaskData = await GetTasksByRoomieIdAsync();
      if (this.taskData.length == this.futureTaskData) {
        this.taskData = "Nada";
      } else {
        var currTaskDataColloc;
        var tempArray = [];
        for( var task in this.futureTaskData){
          if(this.futureTaskData[task].collocId != currTaskDataColloc){
            if(task != 0) {
              this.taskData.push({ collocId : currTaskDataColloc, taskArray : tempArray});
              tempArray = [];
            }
            currTaskDataColloc = this.futureTaskData[task].collocId;
          }
          tempArray.push(this.futureTaskData[task]);
        }
        this.taskData.push({ collocId : this.futureTaskData[task].collocId, taskArray : tempArray});

        console.log(this.taskData);
      }
      
    } catch (e) {
      console.log(e);
    }
  }
};
</script>