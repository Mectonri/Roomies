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
      <input type="text" v-model="category.categoryName" placeholder="Nom de la category" required>

      <label>Icon</label>
      <br>
      <el-select v-model="category.iconName" placeholder="Choisiser une Icon">
        <el-option
          v-for="(icon, index) in icons"
          :key="index"
          :label="icon.iconName"
          :value="icon.iconName"
        >
          <img :src="iconPath + '/' + icon.iconName + '.png'" width="50" height="50">
          <!-- @click="test(icon.iconName)" -->
        </el-option>
      </el-select>

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
} from "../../api/SpendingsApi/CategoryApi";
import AuthService from "../../services/AuthService";
import Loading from "../../components/Utility/Loading.vue";
import Budget from "../Spendings/Budget.vue";
export default {
  components: {
    Loading,
    Budget
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
    console.log(this.mode)
    this.collocId = this.$currColloc.collocId;
    this.icons = await getDefaultIconsAsync();
    this.budget.collocId = this.collocId;
    this.categoryId = this.$route.params.id;

  

    if (this.mode == 'edit') {
      try {
            
            console.log(this.categoryId);
        const category = await getCategoryAsync(this.categoryId);
        console.log(category);
        this.category = category;
        this.errors.push(this.category.errorMessage);
      } catch (errors) {
        console.errors(errors)
      }  finally {
    await this.refreshList();
  }
    }

   
  },

  methods: {
    async refreshList() {
      try {
        this.categoriesList = await getCategoriesAsync(this.collocId);
      } catch (errors) {
        console.errors(errors);
      }
    },
    async onSubmit() {
      event.preventDefault();

      var errors = [];

      if (!this.category.categoryName) errors.push("Category Name");
      if (!this.category.iconName) errors.push("Icon");

      this.errors = errors;

      if (errors.length == 0) {
        try {
          if (this.mode == "create") {
            this.category.collocId = this.collocId;
            await createCategoryAsync(this.category);
          }else {
            debugger;
            await updateCategoryAsync(this.category);
          }

        } catch (errors) {
          console.error(errors);
        } finally {
          await this.refreshList();
        }
      } else {
        for (var j = 0; j < errors.length; j++) {
          console.log(errors[j]);
        }
      }
    }
  }
};
</script>