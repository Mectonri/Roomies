<template>
  <div>
    <div>
      <h1>Objet dans le liste {{courseName}}</h1>
      <div>
          <button class="btn btn-dark" @click="clickRoute('/course/info/'+ courseId + '/item/create')">Ajouter un objet à la liste</button>
      </div>
    </div>

    <table>
      <div v-if="itemList == 0">
        <tr>
          <td>Il n'y as pas d'item dans la liste {{courseName}}</td>
        </tr>
      </div>

      <thead>
        <tr>
          <th>Name</th>
          <th>Price</th>
          <th>Owner</th>
          <th>Options</th>
        </tr>
      </thead>
      <tbody>
        <div>
          <tr v-for="i of itemList" :key="i.itemId">
            <td>{{i.itemName}}</td>
            <td>{{i.itemPrice}}</td>
            <td>{{i.roomieId}}</td>
            <td>
              <button class="btn btn-dark" @click="clickRoute('/course/info/'+ courseId + '/item/edit/' + i.itemId)"> edit </button>
              <a href="#" @click="deleteItem(i.itemId)">
                <i>delete</i>
              </a>
            </td>
          </tr>
        </div>
      </tbody>
    </table>
  </div>
</template>

<script>
import { getItemListAsync, deleteItemAsync } from "../../api/ItemApi";
import { getGroceryListByIdAsync } from "../../api/GroceriesApi";
import { getRoomieByIdAsync } from "../../api/RoomiesApi.js";

export default {
  data() {
    return {
      itemList: [],
      courseName: "",
      courseId: null,
      item: {},
      roomieName: ""
    };
  },

  async mounted() {
    this.courseId = this.$route.params.id;
    await this.refreshList();
  },

  methods: {
    async refreshList() {
      try {
        this.itemList = await getItemListAsync(this.courseId);
        this.tooMuchData = await getGroceryListByIdAsync(this.courseId);
        this.courseName = this.tooMuchData.courseName;
      } catch (e) {
        console.log(e);
      }
    },

    async deleteItem(itemId) {
      try {
        await deleteItemAsync(itemId);
        await this.refreshList();
      } catch (e) {
        console.log(e);
      } finally {
        await this.refreshList();
      }
    },
    
    clickRoute(pathToRoute) {
      this.$router.push(pathToRoute);
    },

  }
};
</script>
