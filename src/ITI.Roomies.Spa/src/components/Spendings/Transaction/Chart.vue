<template>
  <div>
    <main>
      <div id="chart">
        <charts :chartData="data" :Options="options"></charts>
      </div>

      <!-- <div id="chart">
        <barchart :chartData="data" :Options="options"></barchart>
      </div>-->
      <div>
        <el-radio-group v-model="radio">
          <el-radio-button label="Day"></el-radio-button>
          <el-radio-button label="Week"></el-radio-button>
          <el-radio-button label="Month"></el-radio-button>
        </el-radio-group>
      </div>
     
    </main>
  </div>
</template>

<script>
import VueCharts from "vue-chartjs";
import charts from "../Transaction/Chart.js";


import {
  getCategoryAsync,
  getCategoriesAsync
} from "../../../api/SpendingsApi/CategoryApi";
import {
  getAllBudgetCatAsync,
  getBudgetCatByTimeAsync,
  getDailyBudgetCatAsync
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
      radio: null,
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
    //this.getCategoriesValues();
    //this.getCategoriesNames();
    this.data = await this.getChartData();
  },

  methods: {
    async refresh() {
      //this.getCategoriesNames();
      //this.getCategoriesValues();
      this.data = await this.getChartData();
    },

    async getChartData() {
      return {
        labels: this.getLabels(),
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
            data: this.getData(),
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
    
async getData() {
  switch(this.radio ) {
    case 'Day': 
    console.log("day");
      //return await getDailyBudgetCatAsync(this.collocId);
    break;
    case 'Week': 
    console.log('Week');
      //return await getMonthlyBudgetCatAsync(this.collocId);
    break;
    case 'Month': 
    console.log("Month")
   //return await getYearlyBudgetCatAsync(this.collocId);
    break; 
      default:
        console.log("Default");  
        //return await getMonthlyBudgetCatAsync(this.collocId);

  }
},
    getLabels() {
     this.allBudgetCatData.forEach(e => {
        this.categoriesName.push(e.categoryName);
      });
      return this.categoriesName;
    },

    async getData() {
      //return await this.getCategoriesValues();
    },

  }
};
</script>

<style>
#chart {
  height: 400px;
  width: 400px;
}
</style>
