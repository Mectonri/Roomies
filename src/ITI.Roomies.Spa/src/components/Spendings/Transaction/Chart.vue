<template>
  <div class="createContainer">
    <main>
      <div id="chart">
        <charts :chartData="data" :Options="options"></charts>
      </div>
      
      <div>
        <el-button type="primary" @click="chartByDay" round>Day</el-button>
        <el-button type="primary" @click="chartByWeek" round>Week</el-button>
        <el-button type="primary" @click="chartByMonth" round>Month</el-button>
        <el-button type="primary" @click="chartByYear" round>Year</el-button>
        <el-button type="primary" @click="chartByAll" round>All</el-button>
        <!-- <el-button type="primary" @click="getToday" round>today</el-button> -->
      </div>
    </main>
  </div>
</template>

<script>
import VueCharts from "vue-chartjs";
import { Pie } from "vue-chartjs";
import charts from "../Transaction/Chart";

import {
  getCategoryAsync,
  getCategoriesAsync
} from "../../../api/SpendingsApi/CategoryApi";
import {
  getAllBudgetCatAsync,
  getBudgetCatByTimeAsync
} from "../../../api/SpendingsApi/BudgetApi";

export default {
  components: {
    charts
  },
  data() {
    return {
      collocId: null,
      data: {},
      chartData: {},
      chartOptions: {},
      categories: null,
      categoriesName: [],
      categoriesValues: [],
      allBudgetCatData: [],
      donnee: null,
      options: {
        responsive: true,
        maintainAspectRatio: false,
        title: {
          text: "title",
          display: true,
          position: "top",
          text: "Pie Chart",
          fontSize: 18,
          fontColor: "#111"
        },
        legend: {
          display: true,
          position: "bottom",
          labels: {
            fontColor: "#333",
            fontSize: 16
          }
        }
      }
    };
  },

  async mounted() {
    this.collocId = 1;
    this.allBudgetCatData = await getAllBudgetCatAsync(this.collocId);
    this.getCategoriesValues();
    this.getCategoriesNames();
    this.data = await this.getChartData();
    
  },

  methods: {
    refresh() {
      this.getCategoriesName();
      this.getCategoriesValues();
    },

    async getChartData() {
      return {
        labels: this.categoriesName,
        datasets: [
          {
            label: "Test-Chart",
            backgroundColor: [
              "#3e95cd",
              "#8e5ea2",
              "#3cba9f",
              "#e8c3b9",
              "#c45850",
              "#4CFF33",
              "#140A2B"
            ],
            data: this.categoriesValues
          }
        ]
      };
    },

    getChartDataOption() {
      return {
        responsive: true,
        maintainAspectRatio: false,

        title: {
          text: "title",
          display: true,
          position: "top",
          text: "Pie Chart",
          fontSize: 18,
          fontColor: "#111"
        },
        legend: {
          display: true,
          position: "bottom",
          labels: {
            fontColor: "#333",
            fontSize: 16
          }
        }
      };
    },

    async getLabels() {
      return await this.getCategoriesNames();
    },

    async getData() {
      return await this.getCategoriesValues();
    },

    async chartByDay() {
      console.log("daily budget");
      // var today = new Date();
      // var dd = String(today.getDate()).padStart(2, "0");
      // var mm = String(today.getMonth() + 1).padStart(2, "0"); //January is 0!
      // var yyyy = today.getFullYear();

      // today = mm + "/" + dd + "/" + yyyy;
      // console.log(today.toString());

      // var objet = await getBudgetCatByTimeAsync(1, today.toString());
      // // console.log(objet);

      var date = new Date();
      var day = date.getDate(); // yields date
      var month = date.getMonth() + 1; // yields month (add one as '.getMonth()' is zero indexed)
      var year = date.getFullYear(); // yields year
      var hour = date.getHours(); // yields hours
      var minute = date.getMinutes(); // yields minutes
      var second = date.getSeconds(); // yields seconds

      // After this construct a string with the above results as below
      var time = day + "/" + month + "/" + year;

     var objet = await getBudgetCatByTimeAsync(1, "06-06-2019");
     console.log(objet);
    },
    chartByWeek() {
      console.log("weekly Budget");
    },
    chartByMonth() {
      console.log("monthly budget");
    },

    chartByYear() {
      console.log("yearly budget");
    },

    chartByAll() {
      console.log("All budget");
    },
    getCategoriesNames() {
      this.allBudgetCatData.forEach(e => {
        this.categoriesName.push(e.categoryName);
      });
    },

    getCategoriesValues() {
      this.allBudgetCatData.forEach(e => {
        this.categoriesValues.push(e.amount);
      });
    }
  }
};
</script>