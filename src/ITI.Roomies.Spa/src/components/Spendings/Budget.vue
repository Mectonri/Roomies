<template>
  <div class="createContainer">
    <H1>Attribuer un budget à la categorie</H1>
    <div>
      <label class="required">Montant</label>
      <br>
      <input type="text" v-model="budget.amount">

      <label class="required">Debut</label>
      <br>
      <input type="text" v-model="budget.date1">

      <label class="required">fin</label>
      <br>
      <input type="text" v-model="budget.date2">
      <!-- Ajouter une liste des diffeerentres categoy creer -->

      <el-form-item label="Category">
        <el-select
          v-for="category in categories"
          :key="category"
          placeholder="please select your category"
        >
          <el-option>{{category}}</el-option>
        </el-select>
        <el-button @click="createBudget">Create</el-button>
      </el-form-item>
    </div>
  </div>
</template>

<script>

import {
  getAllBudgetAsync,
  getBudgetAsync,
  createBudgetAsync
} from "../../api/SpendingsApi/BudgetApi";
import { getCategoriesAsync } from "../../api/SpendingsApi/CategoryApi";
import AuthService from "../../services/AuthService";
import { state } from "../../state";
import Loading from "../../components/Utility/Loading.vue";

export default {
  data() {
    return {
      errors: [],
      state: true,
      idIsUndefined: true,
      bugets: null,
      budget: null,
      categories: null
    };
  },

  computed: {
    auth: () => AuthService,

    isLoading() {
      return this.state.isLoading;
    }
  },

  async mounted() {
    this.collocId = this.$currColloc.collocId;
    this.categories = await getCategoriesAsync(this.collocId);
    this.budgets = await getAllBudgetAsync(this.collocId);
  },

  methods: {
    async createBudget() {
      event.preventDefault();

      var errors = [];
      if (!this.budget.amount) errors.push("Amount");
      if (!this.budget.date1) errors.push("date1");
      if (!this.budget.date2) errors.push("date2");

      this.errors = errors;
      this.budget.collocId = this.collocId;
      await createBudgetAsync(this.budget);
    }
  }
};
</script>

