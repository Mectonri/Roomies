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
          <td>Description</td>
          <td>Echéance</td>
          <td>Etat</td>
          <td>Participant(s)</td>
          <td></td>
        </tr>
        <tr v-for="task of taskData" :key="task.taskId">
          <td>{{ task.taskName }}</td>
          <td>{{ task.taskDes }}</td>
          <td>{{ task.taskDate }}</td>
          <td v-if="!task.state">
            <el-button @click="updateState(task.taskId, true)">{{ task.state }}</el-button>
          </td>
          <td v-else>
            <el-button @click="updateState(task.taskId, false)">{{ task.state }}</el-button>
          </td>
          <td>{{task.firstName}}</td>
          <td>
            <el-button @click="deleteTask(task.taskId)">X</el-button>
          </td>
        </tr>
      </table>
      <div v-else>Aune tâche à afficher</div>
    </el-main>
    <el-main v-else>Chargement en cours</el-main>
  </el-container>
</template>

<script>
import { DateTime } from "luxon";
import AuthService from "../../services/AuthService";
import {
  UpdateTaskStateAsync,
  getTasksByCollocIdAsync,
  DeleteTaskByIdAsync
} from "../../api/TaskApi.js";
// import { state } from "../../state";

export default {
  data() {
    return {
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
    try {
      // console.log("avant : " + this.taskData);
      // console.log(this.taskData.length);
      // this.taskData2 = this.taskData;
      this.futureTaskData = await getTasksByCollocIdAsync(
        this.$currColloc.collocId
      );
      if (this.taskData.length == this.futureTaskData) {
        this.taskData = "Nada";
      } else { 
          // TO DO : LE FAIRE EN SQL BORDEL
          // Prend la valeur de la première tâche
          var currTaskDataTaskId = this.futureTaskData[0].taskId;
          var tempArray = [];
          var tempRoomieList = [];
          // Pour chaque ligne
          for (var task in this.futureTaskData) {
            
            // Si la tâche est différente de la précédente
            if (this.futureTaskData[task].taskId != currTaskDataTaskId) {
              // Ajoute la tâche précédente à un tableau temporaire possédant toutes les tâches de la collocation
              this.futureTaskData[task-1].firstName = tempRoomieList;
              tempArray.push(this.futureTaskData[task-1]);

              tempRoomieList = [];
              // Change la tâche précédente
              currTaskDataTaskId = this.futureTaskData[task].taskId;
            }

            tempRoomieList.push(this.futureTaskData[task].firstName);

          }

          // Ajoute la dernière tâche
          this.futureTaskData[task].firstName = tempRoomieList;
          tempArray.push(this.futureTaskData[task]);

          this.taskData = tempArray;
      }
      // console.log(this.taskData);
      // console.log(this.taskData.length);
      // console.log(this.taskData == this.taskData2);
    } catch (e) {
      console.log(e);
    }
  },

  methods: {
    async updateState(taskIdToUpdate, taskNewState) {
      await UpdateTaskStateAsync(taskIdToUpdate, taskNewState);
      // TO DO : Changer la ligne suivante en actualisation des données affichées.
      document.location.reload(true);
    },

    async deleteTask(taskId) {
      await DeleteTaskByIdAsync(taskId);
      // TO DO : Changer la ligne suivante en actualisation des données affichées.
      document.location.reload(true);
    }
  }
};
</script>