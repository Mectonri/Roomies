<template>
  <div>
    <div>
      <h1>Liste de Courses</h1>
      <div>
      <router-link :to="`grocery/create`"> <i> Add </i> </router-link>
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
        <tr v-for="g of GroceryList" :key="g.groceryId">
          <td> {{ g.courseName }} </td>
          <td> {{ g.courseDate}} </td>
          <td> {{g.coursePrice}} </td>
          <td>
                        <router-link :to="`grocery/edit/${g.courseId}`"><i class="fa fa-pencil"></i> </router-link>
                        <router-link :to="`grocery/info/${g.courseId}`"><i class="fa fa-info-circle"></i></router-link>
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

import { getAllAsync, getGroceryListByIdAsync, deleteAGroceryListAsync, createGroceryListAsync} from "../../api/GroceriesApi";
import { deleteAsync } from '../../helpers/apiHelper';

export default {
  data() {
    return {
      grocery: {},
      groceryList:[],
    };
  },

  async mounted() {
    await this.refreshList();

  },

  methods: {
    async refreshList() {
      this.groceryList = await getAllAsync();
    },

    async deleteList(courseId) {
      await deleteAGroceryListAsync(courseId);
    },

    async addAList(){

      await createGroceryListAsync();
    }

  },
  
}
</script>
