<template>
  <div id="app">
    <header>
      <h2>Objet dans {{courseName}}</h2>
    </header>
    <br>
    <table>
      <tr>
        <td align="left">
    <main class="card mainCard cardCurrentItem">
      <table class="table table-dark">
        <div v-if="itemList == 0">
          <tr>
            <td>Il n'y as pas d'item dans {{courseName}}</td>
          </tr>
        </div>

        <thead>
          <tr>
            <th>Name</th>
            <th>Price</th>
            <th>Quantite</th>
            <th>Owner</th>
            <th>Options</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="i of itemList" :key="i.itemId">
            <td>{{i.itemName}}</td>
            <td>{{i.itemPrice}}</td>
            <td>{{i.itemQuantite}}</td>
            <td>{{i.roomieId}}</td>
            <td>
              <button
                class="btn btn-dark"
                @click="clickRoute('/course/info/'+ courseId + '/item/edit/' + i.itemId)"
              >edit</button>
            </td>
            <td>
              <button class="btn btn-dark" @click="deleteItem(i.itemId)">Supprimer</button>
            </td>
          </tr>
        </tbody>
      </table>
    </main>
    </td>
    <td align="right">
    <main class="card mainCard cardItemToAdd">
      <table class="table table-dark">
        <div v-if="itemList == 0">
          <tr>
            <td>Il n'y as pas d'item dans la liste {{courseName}}</td>
          </tr>
        </div>

        <thead>
          <tr>
            <th>Acheté ?</th>
            <th>Name</th>
            <th>Price</th>
            <th>Nb</th>
            <th>Owner</th>
            <th>Options</th>
            <th></th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="i of itemList" :key="i.itemId">
            <td><button class="btn btn-dark" @click="updateBought(i.itemId, !i.itemBought)">✓</button></td>
            <td>{{i.itemName}}</td>
            <td>{{i.itemPrice}}</td>
            <td>{{i.itemQuantite}}</td>
            <td>{{i.roomieId}}</td>
            <td>
              <button
                class="btn btn-dark"
                @click="clickRoute('/course/info/'+ courseId + '/item/edit/' + i.itemId)"
              >edit</button>
            </td>
            <td>
              <button class="btn btn-dark" @click="deleteItem(i.itemId)">Supprimer</button>
            </td>
          </tr>
        </tbody>
      </table>
    </main>
    </td>
      </tr>
    </table>
    <br>
    <div>
      <button
        class="btn btn-dark"
        @click="clickRoute('/course/info/'+ courseId + '/item/create')"
      >Ajouter un objet à la liste</button>
    </div>
  </div>
</template>

<script>
import { getItemListAsync, deleteItemAsync, UpdateItemBoughtAsync } from "../../api/ItemApi";
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
        console.log(this.itemList);
        this.tooMuchData = await getGroceryListByIdAsync(this.courseId);
        this.courseName = this.tooMuchData.courseName;
      } catch (e) {
        console.log(e);
      }
    },

    async updateBought(itemIdToUpdate, newBoughtState) {
      await UpdateItemBoughtAsync(itemIdToUpdate, newBoughtState);
      this.refreshList();
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
    }
  }
};
</script>

<style scoped>
.cardCurrentItem{
max-width: 90%;
min-height: 20rem;
flex: left;
}
.cardItemToAdd{
max-width: 90%;
min-height: 20rem;
}
</style>