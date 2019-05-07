<template>
  <el-container>
    <el-header>
      <h1>Tâches</h1>
    </el-header>

    <el-main v-if="taskData[0]">
      <div v-if="taskData !='Nada'">
        <div v-for="taskColloc of taskData" :key="taskColloc.collocId">
          <h2>{{taskColloc.collocId}}</h2>

          <table v-if="taskData !='Nada'">
            <tr>
              <td>Nom</td>
              <td>Description</td>
              <td>Echéance</td>
              <td>Etat</td>
              <td></td>
            </tr>
            <tr v-for="task of taskColloc.taskArray" :key="task.taskId">
              <td>{{ task.taskName }}</td>
              <td>{{ task.taskDes }}</td>
              <td>{{ task.taskDate }}</td>
              <td v-if="!task.state">
                <el-button @click="updateState(task.taskId, true)">{{ task.state }}</el-button>
              </td>
              <td v-else>
                <el-button @click="updateState(task.taskId, false)">{{ task.state }}</el-button>
              </td>
          <td>
            <el-button @click="deleteTask(task.taskId)">X</el-button>
          </td>
            </tr>
          </table>
        </div>
      </div>
      <div v-else>Aucune tâche à afficher</div>
    </el-main>
    <el-main v-else>Chargement en cours</el-main>
  </el-container>
</template>

<script>
import { DateTime } from "luxon";
import AuthService from "../../services/AuthService";
import { GetTasksByRoomieIdAsync, UpdateTaskStateAsync, DeleteTaskByIdAsync } from "../../api/TaskApi.js";
// import { state } from "../../state";

export default {
  data() {
    return {
      errors: [],
      taskData: [],
      taskColloc: ""
    };
  },
  computed: {
    auth: () => AuthService,

    isLoading() {
      return this.state.isLoading;
    }
  },

  async mounted() {
    this.getTasks();
  },
  
  methods: {
    async updateState(taskIdToUpdate, taskNewState){
      await UpdateTaskStateAsync(taskIdToUpdate, taskNewState);
      // TO DO : Changer la ligne suivante en actualisation des données affichées.
      document.location.reload(true);
    },

    async deleteTask(taskId) {
      await DeleteTaskByIdAsync(taskId);
      // TO DO : Changer la ligne suivante en actualisation des données affichées.
      document.location.reload(true);
    },

    async getTasks(){
      try {
        this.futureTaskData = await GetTasksByRoomieIdAsync();
        if (this.taskData.length == this.futureTaskData) {
          this.taskData = 'Nada';
          console.log(this.taskData);
        } else {
          var currTaskDataColloc;
          var tempArray = [];
          for (var task in this.futureTaskData) {
            if (this.futureTaskData[task].collocId != currTaskDataColloc) {
              if (task != 0) {
                this.taskData.push({
                  collocId: currTaskDataColloc,
                  taskArray: tempArray
                });
                tempArray = [];
              }
              currTaskDataColloc = this.futureTaskData[task].collocId;
            }
            tempArray.push(this.futureTaskData[task]);
          }
          this.taskData.push({
            collocId: this.futureTaskData[task].collocId,
            taskArray: tempArray
          });

          console.log(this.taskData);
        }
      } catch (e) {
        console.log(e);
      }
    }
  }
};
</script>