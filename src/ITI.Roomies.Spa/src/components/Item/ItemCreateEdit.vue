<template>
  <el-container v-if="state == false">
    <el-main v-if="idIsUndefined == false">
      <el-header v-if="route == 'create'">
        <h2>Ajouter un Item a la liste de course {{item.courseId}}</h2>
      </el-header>
      <el-header v-if="route == 'edit'">
        <h2>Modifier l'item {{itemNameToShow}}</h2>
      </el-header>

      <el-form>
        <div>
          <label class="required">Nom</label>
          <br>
          <input class="input_border" type="text" v-model="item.itemName" required>
        </div>

        <div>
          <label class="required">Prix</label>
          <input class="input_border" type="number" v-model="item.itemPrice" required>
        </div>

        <!-- /!\ TO DO : faire tune liste deffilantes avex le nom des liste/!\-->
        <!-- <div v-if="route =='edit'">
        <label class="required">Liste</label>
        <input class="input_border" type="text" v-model="item.courseId" required>
        </div>-->
        <!-- /!\ TO DO : faire une liste defilante avec le nom des roomies /!\ -->
        <!-- <div>
        <label class="required">Roomie</label>
        <input class="input_border" type="text" v-model="item.roomieId" required>
        </div>-->

        <button class="btn btn-dark" @click="onSubmit">Sauvegarder</button>
      </el-form>
    </el-main>
    <el-main v-else>Erreur</el-main>
  </el-container>
  <el-container v-else>Chargement en cours</el-container>
</template>

<script>
import AuthService from "../../services/AuthService";
import { state } from "../../state";
import {
  createItemAsync,
  getItemByItemIdAsync,
updateItemAsync
} from "../../api/ItemApi.js";

export default {
  // props: ,
  data() {
    return {
      errors: [],
      item: {},
      idIsUndefined: true,
      state: true,
      itemNameToShow: "",
      route: null
    };
  },
  computed: {
    auth: () => AuthService,

    isLoading() {
      return this.state.isLoading;
    }
  },

  async mounted() {
    this.item.courseId = this.$route.params.id;

    if (
      this.$route.fullPath.replace(
        "/course/info/" + this.item.courseId + "/item/",
        ""
      ) == "create"
    ) {
      this.route = "create";
      this.idIsUndefined = false;
    } else {
      this.route = "edit";

      if (this.$route.params.itemId == undefined) {
        this.idIsUndefined = true;
      } else {
        try {
          this.item = (await getItemByItemIdAsync(
            this.$route.params.itemId
          )).content;
          this.itemNameToShow = this.item.itemName;
          this.idIsUndefined = false;
        } catch (e) {
          console.log(e);
          this.idIsUndefined = true;
        }
      }
    }
    this.state = false;
  },

  methods: {
    async onSubmit() {
      event.preventDefault();

      var errors = [];

      if (!this.item.itemName) errors.push("Nom");
      if (!this.item.itemPrice) errors.push("Prix");

      if (errors.length == 0) {
        try {
          if (this.route == "create") {
            await createItemAsync(this.item);
          }
          if (this.route == "edit") {
            await updateItemAsync(this.item);
            this.$router.push('/course/info/'+ courseId);
          }
        } catch (e) {
          console.error(e);
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

<style lang="scss" scoped>
.input_border {
  border-width: 1px;
}
</style>
