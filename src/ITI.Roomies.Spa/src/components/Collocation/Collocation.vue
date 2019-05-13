<template>
  <el-container>
    <div class="container">

      <div class="button">
        <el-button @click="changeCreate()">création</el-button>
        <el-button @click="changeInvite()">inviter</el-button>
        <el-button @click="changeJoin()">joindre</el-button>
      </div>
    <div v-if="show1">
    <form @submit="onSubmit($event)">
      <div class="alert alert-danger" v-if="errors.length > 0">
        <b>Les champs suivants semblent invalides :</b>

        <ul>
          <li v-for="e of errors">{{e}}</li>
        </ul>
      </div>

      <div class="form-group">
        <label class="required">Nom de collocation</label>
        <el-input type="text" v-model="item.CollocName" required />
      </div>

      <el-button native-type="submit">Sauvegarder</el-button>
    </form>
    </div>
    <div v-if="show3">
      <form @submit="onSubmitInvite($event)">
      <div class="alert alert-danger" v-if="errors.length > 0">
        <b>Les champs suivants semblent invalides :</b>

        <ul>
          <li v-for="e of errors">{{e}}</li>
        </ul>
      </div>

      <div class="form-group">
        <label class="required">Clé : </label>
        <el-input type="text" v-model="item.InviteKey" required />
      </div>

      <el-button native-type="submit">Rejoindre</el-button>
    </form>
    </div>

    <div v-if="show2">
      <form @submit="onSubmitInvite($event)">
      <div class="alert alert-danger" v-if="errors.length > 0">
        <b>Les champs suivants semblent invalides :</b>

        <ul>
          <li v-for="e of errors">{{e}}</li>
        </ul>
      </div>

      <div class="form-group">
        <label class="required">Mail </label>
        <el-input type="text" v-model="item.mail" required />
      </div>

      <el-button native-type="submit">Envoyer</el-button>
    </form>
    </div>
  </div>
  </el-container>

</template>


<script>
import {createCollocAsync} from "../../api/CollocationApi";
import { state } from "../../state";
export default {
  data() {
    return {
      item: {},
      UserId: null,
      errors: [],
      idColloc : 0,
      show1: true,
      show2: false,
      show3: false
    };
  },
  

  async mounted() {
    this.idColloc = this.$route.params.id;
  },

  methods: {
    changeCreate(){
      this.show1 = true;
      this.show2 = false;
      this.show3 = false;
    },
    changeInvite(){
      this.show1 = false;
      this.show2 = true;
      this.show3 = false;
    },
    changeJoin(){
      this.show1 = false;
      this.show2 = false;
      this.show3 = true;
    },
    async onSubmit(event) {
      event.preventDefault();

      var errors = [];

      var idColloc = null;
      if (!this.item.CollocName) errors.push("CollocName");

      this.errors = errors;
      if (errors.length == 0) {
        try {
          idColloc = await createCollocAsync(this.item);

          // Ajout à la colloc en utilisation
          this.$currColloc.setCollocId(idColloc);
          this.$currColloc.setCollocName(this.item.CollocName);
     
          this.$router.replace("/roomies/collocation/" + idColloc);
        } catch (e) {
          console.error(e);
        }
      }
    },
    async onSubmitInvite(event){
      event.preventDefault();

      var errors = [];
      if(!this.item.mail) error.push("mail")
      if(errors.length==0){
        try{

        }catch(e){
          console.error(e);
        }
      }
    }
  }
};
</script>
