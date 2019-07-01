<template>
  <div class="mainContainer">
    <FullCalendar defaultView="dayGridWeek"
      :header="{
        left: 'prev,next today',
        center: 'title',
        right: 'dayGridMonth,timeGridWeek,timeGridDay,listWeek'
      }" 
      :events="eventData" :plugins="calendarPlugins" :editable="true"  @dateClick="handleDateClick" />
  </div>
</template>


<script>
import Vue from "vue";
import {getTasksByCollocIdAsync} from "../api/TaskApi";
import TaskCollocVue from './Task/TaskColloc.vue';

import FullCalendar from "@fullcalendar/vue";
import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from '@fullcalendar/timegrid'
import interactionPlugin from "@fullcalendar/interaction";
import bootstrapPlugin from '@fullcalendar/bootstrap';

Vue.component('FullCalendar', FullCalendar)


export default {
  data() {
    return {
      eventData:[{title: "All Day Event",start: "2019-06-30"}],
      calendarPlugins: [ // plugins must be defined in the JS
        dayGridPlugin,
        timeGridPlugin,
        interactionPlugin // needed for dateClick
      ],
      
    };
  },    
  methods:{

    sqlToJsDate(sqlDate) {
      sqlDate = sqlDate.replace("T", " ");
      //sqlDate in SQL DATETIME format ("yyyy-mm-dd hh:mm:ss.ms")
      /*var sqlDateArr1 = sqlDate.split("-");
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
      );*/
      return sqlDate;
    },
    handleDateClick(arg) {
      if (confirm('Would you like to add an event to ' + arg.dateStr + ' ?')) {
        this.eventData.push({ // add new event data
          title: 'New Event',
          start: arg.date,
          allDay: arg.allDay
        })
      }
    }
  },
  async mounted(){
    var Task = await getTasksByCollocIdAsync(this.$currColloc.collocId);
    var eventData=[];
    var taskData={title:"",start:""};
    
    for (var i=0; i<Task.length; i++){
      //window['eventData'+i] ={ bar:true, popover:{label:'',},dates:'',}
      taskData.start =this.sqlToJsDate( Task[i].taskDate);
      taskData.title = 
        Task[i].firstName+' '+Task[i].lastName+' : '+Task[i].taskDes;
      eventData[i] = taskData ;
      console.log(taskData);
    }
    this.eventData= eventData;
  },
}

</script>
<style lang="scss">
@import"~@fullcalendar/core/main.css";
@import"~@fullcalendar/daygrid/main.css";
@import "~@fullcalendar/timegrid/main.css";
</style>