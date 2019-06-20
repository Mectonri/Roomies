<template>
  <div>
    <div>
      <h1>Ajouter une Transaction</h1>
    </div>

    <div>
      <form @submit="onSubmit($event)">
        <div class="alert alert-alert" v-if="errors.length > 0">
          <b>Les champs suivants semblent invalides</b>
          <ul>
            <li v-for="e of errors" :key="e">{{e}}</li>
          </ul>
        </div>

        <div>
          <label for="Price">Amount</label>
          <br>
          <el-input-number
            type="number"
            name="Price"
            v-model="transaction.price"
            controls-position="right"
            :min="0.1"
            :precision="1" 
            :step="0.1"
            required
          ></el-input-number>
        </div>

        <div>
          <label for="Date">Date</label>
          <br>
          <input type="Date" name="Date" v-model="transaction.date" required>
        </div>

        <div>
          <label>Chosir la category</label>
        </div>
        <div>
          <el-select v-model="transaction.categoryId" clearable placeholder="Categories">
            <el-option
              v-for="category in categories"
              :key="category.categoryId"
              :label="category.categoryName"
              :value="category.categoryId"
            ></el-option>
          </el-select>
          <el-select v-model="transaction.roomieId" placeholder="Roomie">
            <el-option
              v-for="roomie in roomies"
              :key="roomie.roomieId"
              :label="roomie.firstName"
              :value="roomie.roomieId"
            ></el-option>
          </el-select>
        </div>
        <br>
        <button class="btn btn-dark" round @click="onSubmit">Sauvegarder</button>
      </form>
    </div>
  </div>
</template>

<script>
import { GetRoomiesIdNamesByCollocIdAsync } from "../../../api/CollocationApi";
import { getCategoryAsync } from "../../../api/SpendingsApi/CategoryApi";
import {createTransactionAsync } from "../../../api/SpendingsApi/TransactionApi";
export default {
  data() {
    return {
      errors: [],
      categories: [],
      roomies: [],
      collocId: null,
      transaction: {}
    };
  },
  async mounted() {
    this.collocId = this.$currColloc.collocId;
    this.roomies = await GetRoomiesIdNamesByCollocIdAsync(this.collocId);
    this.categories = await getCategoryAsync(this.collocId);
  },
  methods: {
    async onSubmit() {
      event.preventDefault();

      this.transaction.collocId = this.$currColloc.collocId;

      if (!this.transaction.price) this.errors.push("Price");
      if (!this.transaction.date) this.errors.push("Date");
      if (!this.transaction.collocId) this.errors.push("CollocId");
      if (this.transaction.roomieId && this.transaction.categoryId)
        this.errors.push("can't have category and a Roomie");
      if (
        this.transaction.roomieId != null &&
        this.transaction.categoryId != null
      )
        this.errors.push("can't have category and a Roomie");
      await createTransactionAsync(this.transaction);
      this.$root.$emit('update');
      
    }
  }
};
</script>

