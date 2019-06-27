<template>
<div>
  
  <div >
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
            v-model="TDepense.price"
            required>
          </el-input-number>
        </div>

        <div>
          <label for="Date"></label>
          <el-date-picker
            v-model="TDepense.date"
            type="date"
            placeholder="Date de la transaction"
            required
          ></el-date-picker>
        </div>

        <div>
          <el-select v-model="TDepense.rRoomieId" placeholder="Roomie">
            <el-option
              v-for="roomie in roomies"
              :key="roomie.roomieId"
              :label="roomie.firstName"
              :value="roomie.roomieId"
            ></el-option>
          </el-select>
        </div>
        <el-button class="primary" @click="onSubmit">Sauvegarder</el-button>
      </form>
    </div>

</div>
</template>

<script>
import {createTransacDepenseAsync} from "../../../api/SpendingsApi/TransactionApi";
import {GetRoomiesIdNamesByCollocIdAsync} from "../../../api/CollocationApi";
export default {
  data() {
    return {
      errors: [],
      TDepense: {},
      collocId: null,
      roomies: [],
    }
  },

  async mounted() {
    this.collocId = this.$currColloc.collocId;
    this.roomies = await GetRoomiesIdNamesByCollocIdAsync(this.collocId);
  },
  methods: {
    async onSubmit() {

      event.preventDefault();
      if(!this.TDepense.Price || this.TDepense.Price < 0) this.errors.push("Price");
      if (!this.TDepense.date) this.errors.push("Date");
      if(!this.TDepense.rRoomieId) this.errors.push('Roomie');

      try {
        await createTransacDepenseAsync(this.TDepense);
      } catch(e) {
        console.error(e);
      }
      finally {
        this.$root.$emit('update');
      }
    },
  }
  
}
</script>
<style>
.el-input-number .el-input__inner{
  background-color: red
}
</style>


