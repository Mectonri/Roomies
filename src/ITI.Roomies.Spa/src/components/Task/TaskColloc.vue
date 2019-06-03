<template>
  <!-- <div> -->
  <div>
    <header>
      <h2>Tâches</h2>
    </header>

    <!-- <main v-if="taskData[0]" class="card mainCard"> -->
    <main v-if="taskData[0]" class="card mainCard">
      <h3 style="margin: 1.5rem;">{{$currColloc.collocName}}</h3>
      <!-- 
      <table class="table table-dark" v-if="taskData !='Nada'">
        <tr>
          <td>Nom</td>
          <td>Description</td>
          <td>Echéance</td>
          <td>Etat</td>
          <td>Participant(s)</td>
          <td></td>
          <td></td>
        </tr>
        <tr v-for="task of taskData" :key="task.taskId">
          <td>{{ task.taskName }}</td>
          <td>{{ task.taskDes }}</td>
          <td>{{ task.taskDate }}</td>
          <td v-if="!task.state">
            <button class="btn btn-dark" @click="updateState(task.taskId, true)">{{ task.state }}</button>
          </td>
          <td v-else>
            <button class="btn btn-dark" @click="updateState(task.taskId, false)">{{ task.state }}</button>
          </td>
          <td>{{task.firstName}}</td>
          <td>
            <button class="btn btn-dark" @click="modifierTâche(task.taskId)">Modifier</button>
          </td>
          <td>
            <button class="btn btn-dark" @click="deleteTask(task.taskId)">X</button>
          </td>
        </tr>
      </table>-->
      <div v-if="taskData !='Nada'">
        <table>
          <thead>
            <th>
              <div class="input-group mb-4">
                <div class="input-group-text formCheckbox"></div>
                <label class="form-control formName">Nom</label>
                <label class="form-control formDate">Date</label>
                <label class="form-control formFirstName">Roomie</label>
                <label class="form-control formDesc">Description</label>
              </div>
            </th>
            <!-- <th>Nom</th> -->
            <!-- <th>Echéance</th> -->
            <!-- <th>Description</th> -->
            <th style="width: 8rem;"></th>
          </thead>
          <!-- <div v-for="task of taskData" :key="task.taskId"> -->
          <tr v-for="task of taskData" :key="task.taskId">
            <!-- <tr> -->
            <th>
              <div class="input-group mb-1">
                <div class="input-group-text formCheckbox">
                  <!-- <input type="checkbox" aria-label="Checkbox for following text input"> -->
                  <el-tooltip content="Valider" placement="top">
                  <button class="btn btn-dark" @click="updateState(task.taskId, true)">✓</button>
                  </el-tooltip>
                </div>
                <label class="form-control formName">{{ task.taskName }}</label>
                <label class="form-control formDate">{{ task.taskDate }}</label>
                <label :id="'formFirstName' + task.taskId" class="form-control formFirstName">
                  {{ task.firstName}}
                  <br>
                  {{ task.firstName}}
                </label>
                <label class="form-control formDesc">{{ task.taskDes }}</label>
              </div>
            </th>
            <th style="padding-left: 1rem;">
              <!-- <label class="form-control formBtn"> -->
              <el-tooltip content="Modifier" placement="top">
                <button class="btn btn-dark" @click="modifierTâche(task.taskId)">⚙</button>
              </el-tooltip>&nbsp;
              <el-tooltip content="Supprimer" placement="top">
                <button class="btn btn-dark" @click="deleteTask(task.taskId)">X</button>
              </el-tooltip>
              <!-- </label> -->
            </th>
            <!-- </div> -->
          </tr>
        </table>
      </div>
      <div v-else>Aucune tâche à afficher</div>
    </main>
    <main v-else>
      <loading/>
    </main>

    <br>
    <main class="card mainCard" v-if="taskHistoriqueData[0]">
      <h3>Historique</h3>

      <table class="table table-dark" v-if="taskHistoriqueData !='Nada'">
        <tr>
          <td>Nom</td>
          <td>Description</td>
          <td>Echéance</td>
          <td>Etat</td>
          <td>Participant(s)</td>
          <td></td>
          <td></td>
        </tr>
        <tr v-for="task of taskHistoriqueData" :key="task.taskId">
          <td>{{ task.taskName }}</td>
          <td>{{ task.taskDes }}</td>
          <td>{{ task.taskDate }}</td>
          <td v-if="!task.state">
            <button class="btn btn-dark" @click="updateState(task.taskId, true)">{{ task.state }}</button>
          </td>
          <td v-else>
            <button class="btn btn-dark" @click="updateState(task.taskId, false)">{{ task.state }}</button>
          </td>
          <td>{{task.firstName}}</td>
          <td>
            <button class="btn btn-dark" @click="modifierTâche(task.taskId)">Modifier</button>
          </td>
          <td>
            <button class="btn btn-dark" @click="deleteTask(task.taskId)">X</button>
          </td>
        </tr>
      </table>
      <div v-else>Aucune tâche à afficher</div>
    </main>
    <main v-else>
      <loading/>
    </main>
  </div>
  <!-- </div> -->
</template>

<script>
import { DateTime } from "luxon";
import AuthService from "../../services/AuthService";
import {
  UpdateTaskStateAsync,
  getTasksByCollocIdAsync,
  DeleteTaskByIdAsync
} from "../../api/TaskApi.js";
import Loading from "../../components/Utility/Loading.vue";
import monthFr from "../../components/Utility/month.js";

export default {
  components: {
    Loading
  },
  data() {
    return {
      errors: [],
      taskData: [],
      taskHistoriqueData: [],
      monthList: null
    };
  },
  computed: {
    auth: () => AuthService,

    isLoading() {
      return this.state.isLoading;
    }
  },
  async mounted() {
    this.monthList = require("../../components/Utility/month.js");
    this.refreshList();
    // console.log(monthFr);
    // console.log(month.monthFr);
  },

  methods: {
    sqlToJsDate(sqlDate) {
      console.log(sqlDate);
      sqlDate = sqlDate.replace("T", " ");
      console.log(sqlDate);
      //sqlDate in SQL DATETIME format ("yyyy-mm-dd hh:mm:ss.ms")
      var sqlDateArr1 = sqlDate.split("-");
      //format of sqlDateArr1[] = ['yyyy','mm','dd hh:mm:ms']
      var sYear = sqlDateArr1[0];
      var sMonth = (Number(sqlDateArr1[1]) - 1).toString();
      var sqlDateArr2 = sqlDateArr1[2].split(" ");
      //format of sqlDateArr2[] = ['dd', 'hh:mm:ss.ms']
      var sDay = sqlDateArr2[0];
      var sqlDateArr3 = sqlDateArr2[1].split(":");
      //format of sqlDateArr3[] = ['hh','mm','ss.ms']
      var sHour = sqlDateArr3[0];
      var sMinute = sqlDateArr3[1];
      // var sqlDateArr4 = sqlDateArr3[2].split(".");
      //format of sqlDateArr4[] = ['ss','ms']
      var sSecond = 0;
      var sMillisecond = 0;

      return new Date(
        sYear,
        sMonth,
        sDay,
        sHour,
        sMinute,
        sSecond,
        sMillisecond
      );
    },

    dateToFrDisplay(dateToFormat) {
      let laDate = dateToFormat;

      let dayToDisplay =
        laDate.getDate().toString().length == 1
          ? "0" + laDate.getDate().toString()
          : laDate.getDate();
      // let monthToDisplay = laDate.getMonth() + 1;
      // monthToDisplay =
      //   monthToDisplay.toString().length == 1
      //     ? "0" + monthToDisplay.toString()
      //     : monthToDisplay;
      let monthToDisplay = this.monthList.monthFr[laDate.getMonth()];
      let hourToDisplay =
        laDate.getHours().toString().length == 1
          ? "0" + laDate.getHours().toString()
          : laDate.getHours();
      let minutesToDisplay =
        laDate.getMinutes().toString().length == 1
          ? "0" + laDate.getMinutes().toString()
          : laDate.getMinutes();
      return (
        dayToDisplay +
        " " +
        monthToDisplay +
        " " +
        hourToDisplay +
        "h" +
        minutesToDisplay
      );
    },

    async updateState(taskIdToUpdate, taskNewState) {
      await UpdateTaskStateAsync(taskIdToUpdate, taskNewState);
      this.refreshList();
    },

    async deleteTask(taskId) {
      await DeleteTaskByIdAsync(taskId);
      this.refreshList();
    },

    async modifierTâche(taskId) {
      this.$router.push("/task/edit/" + taskId);
    },
    async refreshList() {
      try {
        this.futureTaskData = await getTasksByCollocIdAsync(
          this.$currColloc.collocId
        );
        this.taskData = [];
        this.taskHistoriqueData = [];
        if (this.taskData.length == this.futureTaskData) {
          this.taskData = "Nada";
          this.taskHistoriqueData = "Nada";
        } else {
          // TO DO : LE FAIRE EN SQL BORDEL
          // Prend la valeur de la première tâche
          var currTaskDataTaskId = this.futureTaskData[0].taskId;
          var tempArray = [];
          var tempArray2 = [];
          var tempRoomieList = [];
          // Pour chaque ligne
          for (var task in this.futureTaskData) {
            // Si la tâche est différente de la précédente
            if (this.futureTaskData[task].taskId != currTaskDataTaskId) {
              // Ajoute la tâche précédente à un tableau temporaire possédant toutes les tâches de la collocation
              this.futureTaskData[task - 1].firstName = tempRoomieList.join(
                ", "
              );

              // Formatage de la date
              this.futureTaskData[task - 1].taskDate = this.dateToFrDisplay(
                this.sqlToJsDate(this.futureTaskData[task - 1].taskDate)
              );

              if (!this.futureTaskData[task - 1].state)
                tempArray.push(this.futureTaskData[task - 1]);
              else tempArray2.push(this.futureTaskData[task - 1]);

              tempRoomieList = [];
              // Change la tâche précédente
              currTaskDataTaskId = this.futureTaskData[task].taskId;
            }

            tempRoomieList.push(this.futureTaskData[task].firstName);
          }

          // Formatage de la date
          this.futureTaskData[task].taskDate = this.dateToFrDisplay(
            this.sqlToJsDate(this.futureTaskData[task].taskDate)
          );

          this.futureTaskData[task].firstName = tempRoomieList.join(", ");
          if (!this.futureTaskData[task].state)
            tempArray.push(this.futureTaskData[task]);
          else tempArray2.push(this.futureTaskData[task]);

          if (tempArray != 0) this.taskData = tempArray;
          else this.taskData = "Nada";

          if (tempArray2.length != 0) this.taskHistoriqueData = tempArray2;
          else this.taskHistoriqueData = "Nada";
        }
      } catch (e) {
        console.log(e);
      }
    }
  }
};
</script>

<style scoped>
.input-group {
  text-align: center;
}
.formName {
  max-width: 10rem;
  height: auto;
}
.formCheckbox {
  width: 2.65rem;
  padding: 0.1rem;
  /* height: auto; */
}
.formDesc {
  max-width: 30rem;
  overflow: hidden;
  word-break: break-all;
  height: auto;
  text-align: left;
}
.formBtn {
  max-width: 15rem;
  height: auto;
}
.formDate {
  max-width: 6rem;
  height: auto;
}
.formFirstName {
  max-width: 10rem;
  /* word-break: break-all; */
  height: auto;
}

.formDate,
.formDesc,
.formFirstName,
.formName {
  /* border: 1px solid rgb(50,50,50) !important; */
  border-top: 0px !important;
  border-bottom: 0px !important;
  border-right: 0px !important;
}
tr > td {
  padding-bottom: 1;
}
/* input[type="checkbox"] {
  transform: scale(1.5);
} */
</style>