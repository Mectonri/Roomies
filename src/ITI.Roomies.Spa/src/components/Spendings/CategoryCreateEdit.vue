<template>
  <div>

  </div>
</template>

<script>

import {
  getIcon, createCategoryAsync, getCategoryAsync, 
  getCategoriesAsync, deleteCategoryAsync, 
  updateCategoryAsync } from "../../api/SpendingsApi/CategoryApi.js";
import AuthService from "../../services/AuthService";
import { state } from "../../state";
export default {
  
  data(){
    return {
      errors: [],
      route = null,
      state = true,

    }
  },

  async mounted() {

  },

  methods: {
    async onSumit(){
      event.preventDefault();
      
      var errors = [];
      
      
    }

  }
}
</script>
