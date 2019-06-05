<template>
  <div class="createContainer">
    <main>
      <div id="chart">
        <charts :chartData="getChartData()" :Options="getOption()"></charts>
      </div>
      <div>
        <el-button type="primary" @click="chartByDay" round>Day</el-button>
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
    };
  },

  async mounted() {
    this.refresh();
   
    console.log(this.categories);
    console.log(this.categories.length);
  },

  methods: {

  refresh(){
       this.collocId = this.$currColloc.collocId;
       this.getCategoriesName();
  
    },
    async getChartData() {
      return (this.chartData = {
        labels: this.getLabels(),
        datasets: [
          {
            label: "Test-Chart",
            backgroundColor: [
              "#3e95cd",
              "#8e5ea2",
              "#3cba9f",
              "#e8c3b9",
              "#c45850"
            ],
            data: this.getData()
          }
        ]
      });

      //    this.labels = getCategoryNameOfColloc(this.collocId); // get the name of each categoris of the colloc
      //   this.dataset = getDataOfColloc(this.collocId); //get the date of the colloc
    },
    getOption() {
      return (this.chartOption = {
        responsive: true,
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
      });
    },

    async getLabels() {
      this.refresh();
      //return ["test", "test1", "test2"];
      
    },

    async getData() {
      
      return [50,20,50,100,30];
      //return await getCategories(this.collocId);
    },

    chartByDay() {
      console.log("daily budget");
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
    getCategoriesName() {
      var i;
      for (i = 0; i < this.categories.length; i++) {
        this.categoriesName.push(i.categoryName);
        console.log(this.categoryName);
      }
      return this.categoriesName;

    }
  }
};
</script>
