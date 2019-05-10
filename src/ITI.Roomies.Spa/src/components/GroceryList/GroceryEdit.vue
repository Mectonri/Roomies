<template>
  <div>
    <div>
      <h1>MODIFIER LA LISTE</h1>
    </div>

    <form @submit="onSubmit($event)">
      <label>Nom</label>
      <input type="text" v-model="course.CpurseName" required>

      <label>Date</label>
      <input type="date" v-model="course.CourseDate" required>

      <button type="submit" class="btn btn-primary">Sauvegarder</button>
    </form>

  </div>
</template>

<script>

import { updateAgroceryListAsync } from "../../api/GroceriesApi"
export default {
  data(){
    return {
      id: null,
      course: {},

    }
  },
  async mounted(){
    this.id = this.$route.params.id;
  },
   methods: {
     async onSubmit(event){
       event.preventDefault();

       try{
         
        await updateAgroceryListAsync(this.course);

       }finally{

       }
     }
   }
  
}
</script>

