<template>
  <div>
    
    <header v-if="route=='create'">
      <h2>Ajouter une transaction</h2>
    </header>
    <header>
      <H2>Modifier la transaction</H2>
    </header>

    <div>
      <form @submit="onSubmit($event)">
        <div class="alert alert-alert" v-if="errors.length > 0">
          <b>Les champs suivants semblent invalides</b>
          <ul>
            <li v-for="e of errors" :key="e">{{e}}</li>
          </ul>
        </div>
        <div>
          <label for="Price"></label>
         <el-input-number
            type="number"
            name="Price"
            v-model="TBudget.price"
            required>
          </el-input-number>
        </div>
        <div>
          <label for="Date"></label>
          <el-date-picker
            v-model="TBudget.date"
            type="date"
            placeholder="Date de la transaction"
            required
            
          ></el-date-picker>
        </div>
        <div>
          <el-select v-model="TBudget.categoryId" clearable placeholder="Categories">
            <el-option
              v-for="category in categories"
              :key="category.categoryId"
              :label="category.categoryName"
              :value="category.categoryId"
            ></el-option>
          </el-select>
        </div> 
       <el-button class="primary" @click="onSubmit">Sauvegarder</el-button>
      </form>
    </div>

  </div>
</template>

<script>
import { createTransacBugetAsync, updateTransacBudgetAsync } from '../../../api/SpendingsApi/TransactionApi';
import { getCategoriesAsync } from "../../../api/SpendingsApi/CategoryApi";

export default {
  data(){
    return {
      errors: [],
      TBudget: {},
      collocId: null,
      categories: [],
      route: null,
    }
  },

  async mounted() {
    this.collocId = this.$currColloc.collocId;
    this.categories = await getCategoriesAsync(this.collocId);
    if( this.$route.fullPath.replace() == 'create' ){
      this.routee = "create";

   }else {
     this.route = 'edit';

   }
  },
  methods: {
    
    async onSubmit(){
      event.preventDefault();

      if(!this.TBudget.price ||this.TBudget.price < 0) this.errors.push("Price");
      if(!this.TBudget.date) this.errors.push("Date");
      if(!this.TBudget.categoryId) this.errors.push("Category");

      
      try {
        if (this.route == 'create' ){
          await createTransacBugetAsync(this.TBudget);
        }
        if( this.route== 'edit') {
          await updateTransacBudgetAsync(this.TBudget);
        }
      } catch(e) {
        console.error(e);
      } finally {
        this.$root.$emit('update');
      }
    },
  }
}
</script>
