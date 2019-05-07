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
              <td>Participant(s)</td>
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
              <td>{{task.firstName}}</td>
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
import {
  GetTasksByRoomieIdAsync,
  UpdateTaskStateAsync,
  DeleteTaskByIdAsync
} from "../../api/TaskApi.js";
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
    async updateState(taskIdToUpdate, taskNewState) {
      await UpdateTaskStateAsync(taskIdToUpdate, taskNewState);
      // TO DO : Changer la ligne suivante en actualisation des données affichées.
      document.location.reload(true);
    },

    async deleteTask(taskId) {
      await DeleteTaskByIdAsync(taskId);
      // TO DO : Changer la ligne suivante en actualisation des données affichées.
      document.location.reload(true);
    },

    async getTasks() {
      try {
        this.futureTaskData = await GetTasksByRoomieIdAsync();

        if (this.taskData.length == this.futureTaskData) {
          this.taskData = "Nada";
          console.log(this.taskData);
        } else {
          // TO DO : LE FAIRE EN SQL BORDEL
          // Prend la valeur de la première colloc
          var currTaskDataColloc = this.futureTaskData[0].collocId;
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
            
            // Si la tâche à le même collocId que la tâche précédente
            if (this.futureTaskData[task].collocId != currTaskDataColloc) {
              // Ajoute un objet avec l'id de la collocatione et ses tâches au tableau qui sera affiché
              this.taskData.push({
                collocId: currTaskDataColloc,
                taskArray: tempArray
              });
              tempArray = [];
              // Change la collocation précédente
              currTaskDataColloc = this.futureTaskData[task].collocId;
            }
          }

          // Ajoute la dernière tâche
          this.futureTaskData[task].firstName = tempRoomieList;
          tempArray.push(this.futureTaskData[task]);

          // Ajoute la dernière collocation
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