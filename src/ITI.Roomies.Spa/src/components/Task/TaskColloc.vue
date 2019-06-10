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
        <table class="tableTask">
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
                <label
                  :id="'formFirstName' + task.taskId"
                  class="form-control formFirstName"
                >{{ task.firstName}}</label>
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

    <button
      class="btn btn-dark"
      type="button"
      data-toggle="collapse"
      data-target="#collapseExample"
      aria-expanded="false"
      aria-controls="collapseExample"
      style="
    max-width: 8rem;
"
    >Historique</button>
    <button class="btn btn-dark" style="max-width! 10rem; margin-left: 4rem;" @click="clickRoute('/task/create')">Nouvelle tâche</button>
    
    <br>
    <br>
    <main class="card mainCard" v-if="taskHistoriqueData[0]">
      <br>
      <!-- <h3 style="margin: 1.5rem;">Historique</h3> -->
      <div v-if="taskHistoriqueData !='Nada'" class="collapse" id="collapseExample">
        <table class="tableTask">
          <!-- <thead>
          <th>
            <div class="input-group mb-4">
              <div class="input-group-text formCheckbox"></div>
              <label class="form-control formName">Nom</label>
              <label class="form-control formDate">Date</label>
              <label class="form-control formFirstName">Roomie</label>
              <label class="form-control formDesc">Description</label>
            </div>
          </th>
          <!-- <th>Nom</th>-->
          <!-- <th>Echéance</th> -->
          <!-- <th>Description</th> -->
          <!-- <th style="width: 8rem;"></th> -->
          <!-- </thead> -->
          <tr v-for="task of taskHistoriqueData" :key="task.taskId">
            <th>
              <div class="input-group mb-1">
                <div v-if="task.state" class="input-group-text formCheckbox formtrue">
                  <el-tooltip content="Valider" placement="top">
                    <button class="btn btn-dark" @click="updateState(task.taskId, true)">✓</button>
                  </el-tooltip>
                </div>
                <div v-else class="input-group-text formCheckbox formfalse">
                  <!-- <input type="checkbox" aria-label="Checkbox for following text input"> -->
                  <el-tooltip content="Valider" placement="top">
                    <button class="btn btn-dark" @click="updateState(task.taskId, true)">✓</button>
                  </el-tooltip>
                </div>
                <!-- <label class="form-control formName" :style="myStyle">{{ task.taskName + task.state}}</label> -->
                <label v-if="task.state" class="form-control formName formtrue">{{ task.taskName}}</label>
                <label v-else class="form-control formName formfalse">{{ task.taskName}}</label>
                <!-- <label class="form-control formName">{{ task.taskName + task.state + task.formState}}</label> -->
                <label v-if="task.state" class="form-control formDate formtrue">{{ task.taskDate }}</label>
                <label v-else class="form-control formDate formfalse">{{ task.taskDate }}</label>
                <label
                  v-if="task.state"
                  :id="'formFirstName' + task.taskId"
                  class="form-control formFirstName formtrue"
                >{{ task.firstName}}</label>
                <label
                  v-else
                  :id="'formFirstName' + task.taskId"
                  class="form-control formFirstName formfalse"
                >{{ task.firstName}}</label>
                <label v-if="task.state" class="form-control formDesc formtrue">{{ task.taskDes }}</label>
                <label v-else class="form-control formDesc formfalse">{{ task.taskDes }}</label>
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
  },

  methods: {
    clickRoute(pathToRoute) {
      this.$router.push(pathToRoute);
    },
    sqlToJsDate(sqlDate) {
      sqlDate = sqlDate.replace("T", " ");

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

    dateToFrDisplay(laDate) {
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
          var tempArrayHistorique = [];
          var tempRoomieList = [];
          var currDate = new Date(new Date().getTime());
          currDate.setMilliseconds(0);
          currDate.setSeconds(0);
          currDate.setMinutes(0);
          currDate.setHours(0);

          // Pour chaque ligne
          for (var task in this.futureTaskData) {
            // Si la tâche est différente de la précédente
            if (this.futureTaskData[task].taskId != currTaskDataTaskId) {
              // Ajoute la tâche précédente à un tableau temporaire possédant toutes les tâches de la collocation
              this.futureTaskData[task - 1].firstName = tempRoomieList.join(
                ", "
              );

              // Formatage state
              this.futureTaskData[task - 1].formState =
                "form" + this.futureTaskData[task - 1].state;

              console.log(this.futureTaskData[task - 1].formState);
              // Formatage de la date au format objet Date()
              this.futureTaskData[task - 1].taskDate = this.sqlToJsDate(
                this.futureTaskData[task - 1].taskDate
              );

              // Si la tâche est validée ou que sa date est aujourd'hui ou plus tard, l'ajoute au premier tableau
              if (
                !this.futureTaskData[task - 1].state &&
                this.futureTaskData[task - 1].taskDate > currDate
              ) {
                this.futureTaskData[task - 1].taskDate = this.dateToFrDisplay(
                  this.futureTaskData[task - 1].taskDate
                );
                tempArray.push(this.futureTaskData[task - 1]);
              } else {
                this.futureTaskData[task - 1].taskDate = this.dateToFrDisplay(
                  this.futureTaskData[task - 1].taskDate
                );
                tempArrayHistorique.push(this.futureTaskData[task - 1]);
              }
              tempRoomieList = [];
              // Change la tâche précédente
              currTaskDataTaskId = this.futureTaskData[task].taskId;
            }

            tempRoomieList.push(this.futureTaskData[task].firstName);
          }

          // Formatage de la date au format objet Date()
          this.futureTaskData[task].taskDate = this.sqlToJsDate(
            this.futureTaskData[task].taskDate
          );

          this.futureTaskData[task].firstName = tempRoomieList.join(", ");

          this.futureTaskData[task].formState =
            "form" + this.futureTaskData[task].state;

          // Si la tâche est validée ou que sa date est aujourd'hui ou plus tard, l'ajoute au premier tableau
          if (
            !this.futureTaskData[task].state &&
            this.futureTaskData[task].taskDate > currDate
          ) {
            this.futureTaskData[task].taskDate = this.dateToFrDisplay(
              this.futureTaskData[task].taskDate
            );
            tempArray.push(this.futureTaskData[task]);
          } else {
            this.futureTaskData[task].taskDate = this.dateToFrDisplay(
              this.futureTaskData[task].taskDate
            );
            tempArrayHistorique.push(this.futureTaskData[task]);
          }

          if (tempArray != 0) this.taskData = tempArray;
          else this.taskData = "Nada";

          if (tempArrayHistorique.length != 0)
            this.taskHistoriqueData = tempArrayHistorique;
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
  width: 10rem;
  height: auto;
}
.formCheckbox {
  width: 2.65rem;
  padding: 0.1rem;
  border: 0px;
  border-top-right-radius: 0px;
  border-bottom-right-radius: 0px;
  /* height: auto; */
}
.formDesc {
  max-width: 30rem;
  width: 30rem;
  overflow: hidden;
  word-break: break-all;
  height: auto;
  text-align: left;
}
.formBtn {
  max-width: 15rem;
  width: 15rem;
  height: auto;
}
.formDate {
  max-width: 6rem;
  width: 6rem;
  height: auto;
}
.formFirstName {
  max-width: 10rem;
  width: 10rem;
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

.tableTask {
  min-width: 60rem;
  max-width: 80rem;
}
.formfalse {
  background-color: #fd4d5f;
}

.formtrue {
  background-color: #82c560;
}
/* input[type="checkbox"] {
  transform: scale(1.5);
} */
</style>