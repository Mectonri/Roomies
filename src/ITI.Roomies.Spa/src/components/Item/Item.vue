<template>
  <div id="app">
    <header>
      <h2>Objet dans {{courseName}}</h2>
    </header>
    <br>
    <table class="mainTable" vertical-align="top">
      <tr>
        <td valign="left">
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
                  <td>{{i.firstName}}</td>
                  <td>
                    <button class="btn btn-dark" @click="deleteItem(i.itemId, i.courseId, i.roomieId, i.itemSaved)">X</button>
                  </td>
                </tr>
              </tbody>
            </table>
          </main>
        </td>
        <td>
          <main class="card mainCard cardItemToAdd">
          <header><h3> Ajouter un objet </h3></header>
          <br>
            <table class="table table-dark">
              <div v-if="savedItemList == 0">
                <tr>
                  <td>Il n'y as pas d'objets enregistrés.</td>
                </tr>
              </div>

              <thead>
                <tr>
                  <th></th>
                  <th>Name</th>
                  <th>Price</th>
                </tr>
              </thead>
              <tbody>
                <tr v-for="i of savedItemList" :key="i.itemId">
                  <td>
                    <button class="btn btn-dark">+</button>
                  </td>
                  <td>{{i.itemName}}</td>
                  <td>{{i.itemPrice / 100}} €</td>
                  <!-- <td> -->
                  <!-- <button class="btn btn-dark" @click="deleteItem(i.itemId)">A modifier</button> -->
                  <!-- </td> -->
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
import {
  getSavedItemListFromCollocAsync,
  deleteItemFromListAsync,
  getItemListAsync
} from "../../api/ItemApi.js";
import { getGroceryListByIdAsync } from "../../api/GroceriesApi";
import { getRoomieByIdAsync } from "../../api/RoomiesApi.js";

export default {
  data() {
    return {
      itemList: [],
      savedItemList: [],
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
        this.savedItemList = await getSavedItemListFromCollocAsync(
          this.$currColloc.collocId
        );
        // console.log(this.savedItemList);
        this.course = await getGroceryListByIdAsync(this.courseId);
        this.courseName = this.course.courseName;

        this.itemList = await getItemListAsync(this.courseId);
        console.log(this.itemList)
      } catch (e) {
        console.log(e);
      }
    },

    async updateBought(itemIdToUpdate, newBoughtState) {
      await UpdateItemBoughtAsync(itemIdToUpdate, newBoughtState);
      this.refreshList();
    },

    async deleteItem(itemId, courseId, roomieId, itemSaved) {
      try {
        await deleteItemFromListAsync(itemId, courseId, roomieId, itemSaved);
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
.cardCurrentItem {
  max-width: 90%;
  min-height: 20rem;
  flex: left;
}
.cardItemToAdd {
  max-width: 90%;
  min-height: 20rem;
}

.mainTable > tr {
  vertical-align: top !important;
}
</style>