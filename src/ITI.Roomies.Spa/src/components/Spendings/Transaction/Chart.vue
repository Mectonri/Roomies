<template>
  <div class="createContainer">
    <main>
      <div id="chart">
        <charts :chartData="data" :Options="0" ></charts>
      </div>
    </main>
  </div>
</template>

<script>

import VueCharts from 'vue-chartjs';
import { Pie } from 'vue-chartjs';
import charts from '../Transaction/Chart';

export default {
  components: {
    charts,
  },
  data() {
    return {  
      data: {},
      series: [44, 55, 13, 43, 22],
      chartOptions: {
        labels: ["Team A", "Team B", "Team C", "Team D", "Team E"],
        responsive: [
          {
            breakpoint: 480,
            options: {
              chart: {
                width: 200
              },
              legend: {
                position: "bottom"
              }
            }
          }
        ]
      },
      data: [50, 60 , 20 , 10],
      

    };
  },
  computed: {},
   async mounted() {
     this.getData();
  this.data.label 
  },

  methods: {
    getData() {
      this.data = {
        labels: ["test", "test1"],
        datasets: [{
          label: "Labels" ,
          data : [20,10],
        }]
      }

    //    this.labels = getCategoryNameOfColloc(this.collocId); // get the name of each categoris of the colloc
    //   this.dataset = getDataOfColloc(this.collocId); //get the date of the colloc
     },
     getOption() {
       return{

       }
     }
  }
};
</script>
