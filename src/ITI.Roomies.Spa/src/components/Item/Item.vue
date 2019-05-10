<template>

<div>
  <div>
    <h1> Item dans le liste {{courseName}}({{courseId}})</h1>
    <div>
      <router-link :to="`item/create`"><i>Add</i></router-link>
    </div>
  </div>
  
  <table>
    <thead>
      <tr>
        <th>Name</th>
        <th>Price</th>
        <th>Owner</th>
      </tr>
    </thead>
    <tbody>
      <tr>
        <td>Il n'y as pas d'item dans la liste {{courseName}}</td>
      </tr>
      <tr v-for="i of itemList" :key="i.itemId">
        <td>{{i.itemName}}</td>
        <td>{{i.itemPrice}}</td>
        <td>{{i.roomieId}}</td>
        <td>
          <router-link :to="`item/edit/${i.itemId}`"><i>edit</i></router-link>
          <!-- <router-link :to="`item/delete/${i.itemId}`"><i>delete</i></router-link> -->
          <a href="#" @click="deleteItem(i.itemId)">
            <i>delete</i>
          </a>
          
        </td>
      </tr>
    </tbody>
  </table>
 
</div>
</template>

<script>

import { getItemListAsync, deleteItemAsync} from "../../api/ItemApi";
import {getGroceryListByIdAsync} from "../../api/GroceriesApi";


export default {
  data() {
    return { 
      itemList: [],
      courseName: '', 
      courseId: null,
      item: {},

    }
  },

  async mounted(){
    this.courseId = this.$route.params.id;
    console.log(this.courseId);
    await this.refreshList();
    
  },

  methods:{
    async refreshList(){
      console.log(this.courseId);
      this.itemList = await getItemListAsync(this.courseId);
      console.log(this.item);
      this.courseName = await getGroceryListByIdAsync(this.courseId).courseName;
      //console.log(await getGroceryListByIdAsync(this.courseId));
      //console.log(await getGroceryListByIdAsync(this.courseId).courseName);
    },

    async deleteItem(itemId){
      try{
      await deleteItemAsync(itemId);
      await refreshList();
      }catch(e){
        console.log(e);
      }finally{
        await refreshList();
      }
    },
    
  },
}
</script>
