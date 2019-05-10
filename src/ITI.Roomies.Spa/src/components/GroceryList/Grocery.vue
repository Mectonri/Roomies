<template>
  <div>
    <div>
      <h1>Liste de Courses</h1>
      <div>
        <router-link :to="`course/create`"> <i> Add </i> </router-link>
      </div>
    </div>
    <table>
      <thead>
        <tr>
          <th>Name</th>
          <th>Date</th>
          <th>Price</th>
          <th>Options</th>
        </tr>
      </thead>

      <tbody>
        <tr v-if="groceryList.legth == 0 ">
          <td>Il n'y as pas de liste de courses</td>
        </tr>

        <tr v-for="g of groceryList" :key="g.groceryId">
          <td> {{ g.courseName }} </td>
          <td> {{ g.courseDate}} </td>
          <td> {{g.coursePrice}} </td>
          <td>
                        <router-link :to="`course/edit/${g.courseId}`"><i>edit</i> </router-link>
                        <router-link :to="`course/info/${g.courseId}`"><i>info</i></router-link>
                        <a href="#" @click="deleteList(g.courseId)">
                            <i class="fa fa-trash"></i>
                        </a>
                    </td>
        </tr>
      </tbody>
    </table>
    <div>
  </div>
</div>
  
</template>

<script>

import { getAllAsync, getGroceryListByIdAsync, deleteAGroceryListAsync} from "../../api/GroceriesApi";


export default {
  data() {
    return {
      
      groceryList:[],
    }
  },

  async mounted() {
    await this.refreshList();
    this.$currColloc.collocId;

  },

  methods: {
    async refreshList() {
      this.groceryList = await getAllAsync(this.$currColloc.collocId);
    },

    async deleteList(courseId) {
      await deleteAGroceryListAsync(courseId);
    },
  },
  
}
</script>
