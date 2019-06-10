<template>
  <div class="mainContainer">
    <v-calendar is-dark is-expanded :rows="3" :columns="4" title-position="left" :attributes='attrs' year=2019>       
    </v-calendar>
  </div>
</template>


<script>
import Vue from "vue";
import VCalendar from "v-calendar";
import {getTasksByCollocIdAsync} from "../api/TaskApi";
import TaskCollocVue from './Task/TaskColloc.vue';

Vue.use(VCalendar, {
  componentPrefix: "v",
});

export default {
  data() {
    return {
      eventData:{key:'today', bar:true, popover:{label:'',},dates:'',},
      TaskData:[],
      attrs:[]
    };
  },
  methods:{
    CreateCalendarEvents(TaskData){

    },
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
      return new Date(
        sYear,
        sMonth,
        sDay
      );
    }
  },
  async mounted(){
    var TaskData = await getTasksByCollocIdAsync(this.$currColloc.collocId);
    this.TaskData= TaskData;
    var attrs=[];
    var i=0;
    for (i=0; i<this.TaskData.length; i++){
      this.eventData.dates = this.sqlToJsDate(this.TaskData[i].taskDate);
      this.eventData.popover.label = 
        this.TaskData[i].firstName+' '+this.TaskData[i].lastName+' : '+this.TaskData[i].taskDes;
      attrs[i]=this.eventData;
      this.attrs= attrs;
    }
  },
}

</script>