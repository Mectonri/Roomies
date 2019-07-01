<template>
  <div id="app">
    <header>
      <h2>Listes de Courses</h2>
    </header>
    <main style="padding-top: 2rem;" class="card mainCard">
      <table class="tableTask">
        <!-- <thead>
          <tr>
            <th>Name</th>
            <th>Date</th>
            <th>Price</th>
            <th>Options</th>
            <th></th>
          </tr>
        </thead>-->

        <tbody>
          <tr v-if="groceryList.length == 0 ">
            <td>Il n'y a pas de liste de courses</td>
          </tr>

          <tr v-else v-for="g of groceryList" :key="g.courseId">
            <td>
              <router-link :to="`course/info/${g.courseId}`" onmouseover="style='text-decoration:none'">
              <div class="input-group">
                <label class="form-control formName">{{ g.courseName }}</label>
                <label class="form-control formDate">{{ g.courseDate}}</label>
                <label class="form-control formPrice">{{ g.coursePrice}} €</label>
              </div>
              </router-link>
            </td>
            <!-- <td>
              <router-link :to="`course/info/${g.courseId}`">
                <button class="btn btn-dark">info</button>
              </router-link>
            </td> -->
            <td>
              <router-link :to="`course/edit/${g.courseId}`" onmouseover="style='text-decoration:none'">
                <button class="btn btn-dark">edit</button>
                &nbsp;
              </router-link>
              <button class="btn btn-dark" @click="deleteList(g.courseId)"> X </button>
            </td>
          </tr>
        </tbody>
      </table>
    </main>
    <main class="card mainCard">
      <h3 style="margin: 1.5rem;">Templates</h3>
      <table class="table table-dark">
        <thead>
          <tr>
            <th>Nom</th>
            <th>Price</th>
            <th>Option</th>
          </tr>
        </thead>

        <tbody>
          <tr v-if="templateList.length == 0 ">
            <td>Il n'y a pas de liste de courses</td>
          </tr>

          <tr v-else v-for="t of templateList" :key="t.courseTempId">
            <td>{{ t.courseName }}</td>
            <td>{{t.coursePrice}}</td>
            <td>
              <router-link :to="`course/info/${t.courseTempId}`">
                <button class="btn btn-dark">info</button>
              </router-link>
            </td>
            <td>
              <router-link :to="`course/edit/${t.courseTempId}`">
                <button class="btn btn-dark">edit</button>
                &nbsp;
              </router-link>
              <button class="btn btn-dark" @click="deleteTemp(t.courseTempId)">Supprimer</button>
            </td>
          </tr>
        </tbody>
      </table>
    </main>
    <br>
    <router-link :to="`course/create`">
      <button class="btn btn-dark">Nouvelle liste</button>
    </router-link>
  </div>
</template>

<script>
import {
  getGroceryListByIdAsync,
  getAllListsAsync,
  getTemplateById,
  getAllTemplatesAsync,
  deleteListAsync,
  deleteTemplateAsync
} from "../../api/GroceriesApi";
// import monthFr from "../../components/Utility/month.js";

export default {
  props: [],
  data() {
    return {
      groceryList: [],
      templateList: []
    };
  },

  async mounted() {
    this.monthList = require("../../components/Utility/month.js");
    await this.refreshList();
  },

  methods: {
    async refreshList() {
      try {
        this.groceryList = await getAllListsAsync(this.$currColloc.collocId);
        console.log(this.groceryList);
        if(this.groceryList.length != 0){
          for(var grocery in this.groceryList){
            this.groceryList[grocery].courseDate = this.dateToFrDisplay(this.sqlToJsDate(this.groceryList[grocery].courseDate));
          }
        }
        this.templateList = await getAllTemplatesAsync(this.$currColloc.collocId);
        console.log(this.templateList);
      } catch (e) {
        console.log(e);
      }
    },

    async deleteList(courseId) {
      try {
        await deleteListAsync(courseId);
        await this.refreshList();
      } catch (e) {
        console.log(e);
      }
    },

    async deleteTemp(courseId) {
      try {
        await deleteTemplateAsync(courseId);
        await this.refreshList();
      } catch (e) {
        console.log(e);
      }
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
      let listeDay = ["Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi", "Dimanche"];
      dayToDisplay = listeDay[laDate.getDay()] + " " + dayToDisplay;

      let monthToDisplay = this.monthList.monthFr[laDate.getMonth()];

      return (
        dayToDisplay +
        " " +
        monthToDisplay
       
      );
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
  border-right: 0px !important;
  /* border-left: 1px !important; */
  cursor: pointer;
}

.formBtn {
  max-width: 15rem;
  width: 15rem;
  height: auto;
  border-right: 0px !important;
  border-left: 0px !important;
}
.formDate {
  max-width: 14rem;
  width: 14rem;
  height: auto;
  border-right: 0px !important;
  border-left: 0px !important;
  cursor: pointer;
}
.formPrice {
  max-width: 10rem;
  width: 10rem;
  /* word-break: break-all; */
  height: auto;
  /* border-right: 1px !important; */
  border-left: 0px !important;
  cursor: pointer;
}

tr > td {
  padding-bottom: 1;
}

.tableTask {
  min-width: 40rem;
  max-width: 55rem;
}
.formfalse {
  background-color: #fd4d5f;
}

.formtrue {
  background-color: #82c560;
}
</style>