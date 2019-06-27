<template>
  <div>
    <div>
      <h1 v-if="mode == 'create'">Ajouter une Category</h1>
      <h1 v-else>Editer la category {{category.categoryName}}</h1>
    </div>
    <form @submit="onSubmit($event)">
      <div class="alert alert-danger" v-if="errors.length > 0">
        <b>Les champs suivants semblent invalides :</b>
        <ul>
          <li v-for="e of errors" :key="e">{{e}}</li>
        </ul>
      </div>

      <label class="required">Nom</label>
      <br>
      <el-input
        placeholder="Nom de la category" 
        v-model="category.categoryName"
        clearable required>
      </el-input>
      <label>Icon</label>
      <br>
        <div><p>
        <button
          class="btn btn-primary"
          type="button"
          data-toggle="collapse"
          data-target="#collapseExample"
          aria-expanded="false"
          aria-controls="collapseExample"
        >Choisissez votre icon</button>
      </p>
      <div class="collapse" id="collapseExample">
        <div class="card card-body">
          <div>
            <div class="row">
              <div
                class="col"
                v-for="(icon, index) in icons"
                :key="index"
                :label="icon.iconName"
                :value="icon.iconName"
              >
                <el-button @click="setIcon(icon.iconName)">
                  <img :src="iconPath + '/' + icon.iconName + '.png'" width="50" height="50">
                  <div class="w-100"></div>
                </el-button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
      

      <button class="btn btn-dark" @click="onSubmit">Sauvegarder</button>
    </form>

  
  </div>
</template>

<script>
import {
  getDefaultIconsAsync,
  createCategoryAsync,
  getCategoryAsync,
  updateCategoryAsync
} from "../../../api/SpendingsApi/CategoryApi";

import Budget from "../../Spendings/Budget.vue";

export default {
  components: {
   
    Budget,
  },

  data() {
    return {
      errors: [],
      state: true,
      category: {},
      idIsUndefined: true,
      icons: [],
      iconPath: "http://localhost:5000/Pictures/Icons",
      categoriesList: [],
      categoryId: null,
      mode: null,
      collocId: null,
    };
  },

  computed: {
    auth: () => AuthService,

    isLoading() {
      return this.state.isLoading;
    }
  },

  async mounted() {
    this.mode = this.$route.params.mode;
    console.log(this.mode);
    this.collocId = this.$currColloc.collocId;
    console.log(this.collocId);

    this.icons = await getDefaultIconsAsync();
    this.categoryId = this.$route.params.id;

    if (this.mode == "edit") {
      try {

        const category = await getCategoryAsync(this.categoryId);
   
        this.category = category;
        this.errors.push(this.category.errorMessage);
      } catch (errors) {
        console.error(errors);
      } finally {
        await this.refreshList();
      }
    }
  },

  methods: {
    async refreshList() {
      try {
        this.categoriesList = await getCategoriesAsync(this.collocId);
      } catch (errors) {
        console.error(errors);
      }
    },

    async setIcon(iconName) {
      this.category.iconName = iconName;
    },

    async onSubmit() {
      event.preventDefault();
       this.category.collocId = this.collocId;
      console.log(this.category);

      var errors = [];

      if (!this.category.categoryName) errors.push("Category Name");
      if (!this.category.iconName) errors.push("Icon");
      if(!this.category.collocId) errors.push("collocId");

      this.errors = errors;
      console.log(this.category);

      
        try {
          if (this.mode == "create") {
           
            await createCategoryAsync(this.category);
          } 
          if( this.route == 'edit') {
            await updateCategoryAsync(this.category);
          }
        } catch (errors) {
          console.error(errors);
        } finally {
          this.$root.$emit('updateCategoryList');
        }
    }
  }
};
</script>