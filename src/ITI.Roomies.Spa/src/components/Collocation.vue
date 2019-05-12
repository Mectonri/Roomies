<template>
  <el-container>
    <div class="container">

      <div class="button">
        <el-button @click="changeCreate()" >création</el-button>
        <el-button @click="changeInvite()">inviter</el-button>
        <el-button @click="changeJoin()" >joindre</el-button>
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

        <el-button native-type="submit" v-if="this.collocName==''">Sauvegarder</el-button>
        <p v-if="this.collocName!='' ">Vous avez déjà une collocation.</p>
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

        <el-button native-type="submit" v-if="this.collocName==''">Rejoindre</el-button>
        <p v-if="this.collocName!=''">Vous avez déjà une collocation.</p>
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

      <el-button native-type="submit" v-if="this.collocName!=''">Envoyer</el-button>
      <p v-if="this.collocName==''">Veuillez d'abords créer une collocation avant de chercher à inviter des personnes.</p>
      </form>
    </div>

    <div v-if='this.collocName!="" && show4'>
      <br>
      <el-button @click="onSubmitQuit($event)" native-type="submit">Quitter la collocation</el-button>
    </div>


  </div>
  </el-container>

</template>


<script>
import {createCollocAsync, quitCollocAsync} from "../api/CollocationApi";
import { state } from "../state";
export default {
  data() {
    return {
      item: {},
      UserId: null,
      errors: [],
      show1: false,
      show2: false,
      show3: false, 
      show4:false,
      collocName:'',
      collocid:''
    };
  },
  

  async mounted() {
    this.idColloc = this.$currColloc.collocId;
    this.collocName= this.$currColloc.collocName;
  },

  methods: {
    changeCreate(){
      this.show1 = true;
      this.show2 = false;
      this.show3 = false;
      this.show4 = true;
    },
    changeInvite(){
      this.show1 = false;
      this.show2 = true;
      this.show3 = false;
      this.show4 = true;
    },
    changeJoin(){
      this.show1 = false;
      this.show2 = false;
      this.show3 = true;
      this.show4 = true;
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

    async onSubmitQuit(event){
      try {
        await quitCollocAsync(this.idColloc);
        this.$router.replace("/roomies");
      }catch(e) {
        console.error(e);
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