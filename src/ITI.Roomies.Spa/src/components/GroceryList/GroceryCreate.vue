<template>
  <div>
    <div>
      <h1 > Creer une liste  de course</h1>
    </div>

    <div>
      <form @submit="onSubmit($event)">
        <label>Nom</label>
        <input type="text" v-model="course.courseName">

        <label>Date</label>
        <input type="date" v-model="course.courseDate">
        <button type="submit">Sauvegarder</button>
      </form>
      
    </div>

  </div>
</template>

<script>

import {createGroceryListAsync, getGroceryListByIdAsync} from "../../api/GroceriesApi"

export default {
  data() {
    return{
      course: {},
      id: null,
    }
  },

  async mounted() {
    this.id = this.$route.params.id;
    try{
      this.course = await getGroceryListByIdAsync(this.id);
    }catch{

    }finally{

    }

  },

  methods: {
    async onSubmit(event) {
      try{
        await createGroceryListAsync(this.course);
      }
      finally{

      }

    },

  },
}
</script>
