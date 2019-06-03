<template>
  <div class="">
    <header>
      <h2>Liste de Courses</h2>
    </header>
    <br>
    <table class="table table-dark">
      <thead>
        <tr>
          <th>Name</th>
          <th>Date</th>
          <th>Price</th>
          <th>Options</th>
          <th></th>
        </tr>
      </thead>

      <tbody>
        <tr v-if="groceryList.length == 0 ">
          <td>Il n'y a pas de liste de courses</td>
        </tr>

        <tr v-else v-for="g of groceryList" :key="g.courseId">
          <td>{{ g.courseName }}</td>
          <td>{{ g.courseDate}}</td>
          <td>{{g.coursePrice}}</td>
          <td>
            <router-link :to="`course/info/${g.courseId}`">
              <button class="btn btn-dark">info</button>
            </router-link>
          </td>
          <td>
            <router-link :to="`course/edit/${g.courseId}`">
              <button class="btn btn-dark">edit</button>
              &nbsp;
            </router-link>
            <button class="btn btn-dark" @click="deleteList(g.courseId)">Supprimer</button>
          </td>
        </tr>
      </tbody>
    </table>
    <table class="table table-dark">
      <thead>
        <tr>
          <th>Nom</th>
          <th>Price</th>
          <th>Option</th>
        </tr>
      </thead>

       <tbody>
        <tr v-if="templateList.length == 0 ">
          <!-- <td>Il n'y a pas de liste de courses</td> -->
        </tr>

        <tr v-else v-for="t of templateList" :key="t.courseId">
          <td>{{ t.courseName }}</td>
          <td>{{t.coursePrice}}</td>
          <td>
            <router-link :to="`course/info/${t.courseId}`">
              <button class="btn btn-dark">info</button>
            </router-link>
          </td>
          <td>
            <router-link :to="`course/edit/${t.courseId}`">
              <button class="btn btn-dark">edit</button>
              &nbsp;
            </router-link>
            <button class="btn btn-dark" @click="deleteTemp(t.courseId)">Supprimer</button>
          </td>
        </tr>
         </tbody>

    </table>
    <br>
    <router-link :to="`course/create`">
      <button class="btn btn-dark">Nouvelle liste</button>
    </router-link>
  </div>
</template>

<script>
import {
  getGroceryListByIdAsync,
  getAllListsAsync,
  getTemplateById,
  getAllTemplatesAsync,
  deleteListAsync,
  deleteTemplateAsync,


} from "../../api/GroceriesApi";

export default {
  props: [],
  data() {
    return {
      groceryList: [],
      templateList: [],
      collocId: "",
    };
  },

  async mounted() {
    this.collocId = this.$currColloc.collocId;
    await this.refreshList();
  },

  methods: {
    async refreshList() {
      try {
        this.groceryList = await getAllListsAsync(this.collocId);
        console.log(this.groceryList);
        this.templateList = await getAllTemplatesAsync(this.collocId);
        console.log(this.templateList);
      } catch (e) {
        console.log(e);
      }
    },

    async deleteList(courseId) {
      try {
        await deleteListAsync(courseId);
        await this.refreshList();
      } catch (e) {
        console.log(e);
      }
    }, 

    async deleteTemp(courseId){
      try {
        await deleteTemplateAsync(courseId);
        await this.refreshList();
      }
      catch (e) {
        console.log(e);
      }
    }
  }
};
</script>