<template>
  <div >
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
  getCategoriesAsync,
} from "../../../api/SpendingsApi/CategoryApi";
import {
  getAllBudgetCatAsync,
  getBudgetCatByTimeAsync,
  getDailyBudgetCatAsync,
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

    async refresh() {
      this.getCategoriesNames();
      this.getCategoriesValues();
      this.data = await this.getChartData();
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

      this.allBudgetCatData = await getDailyBudgetCatAsync(1);
      await this.refresh();
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

<style>
#chart {
  height: 400px;
  width: 400px;
}
</style>
