<template>
  <div class="mainContainer">
    <FullCalendar  :plugins="DayPlugin"/>
  </div>
</template>


<script>
import Vue from "vue";
import {getTasksByCollocIdAsync} from "../api/TaskApi";
import TaskCollocVue from './Task/TaskColloc.vue';

import VFullcalendar from 'v-fullcalendar';
import FullCalendar from "@fullcalendar/vue"
import dayGridPlugin from "@fullcalendar/daygrid";
import interactionPlugin from "@fullcalendar/interaction";
import bootstrapPlugin from '@fullcalendar/bootstrap';

Vue.component('FullCalendar', FullCalendar)


export default {
  
  data() {
    return {
      eventData:{ bar:true, popover:{label:'',},dates:'',},
      TaskData:[],
      attrs:[],
      DayPlugin: [dayGridPlugin],
      config: {
      plugins: [interactionPlugin, dayGridPlugin, bootstrapPlugin ],
      themeSystem: 'bootstrap',
      defaultDate: "2019-06-18",
      editable: true,
      eventLimit: true // allow "more" link when too many events
    },
    events: []
    };
  },    
  methods:{
    
    CreateCalendarEvents(TaskData){

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
      var sDay = (Number(sqlDateArr2[0]) + 1).toString();
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
    
    for (var i=0; i<this.TaskData.length; i++){
      window['eventData'+i] ={ bar:true, popover:{label:'',},dates:'',}
      window['eventData'+i].dates = this.sqlToJsDate(this.TaskData[i].taskDate);
      window['eventData'+i] .popover.label = 
        this.TaskData[i].firstName+' '+this.TaskData[i].lastName+' : '+this.TaskData[i].taskDes;
      attrs[i]=window['eventData'+i] ;
    }
    this.attrs= attrs;
  },
}

</script>
<style lang="scss">
@import"~@fullcalendar/core/main.css";
@import"~@fullcalendar/daygrid/main.css";
  </style>