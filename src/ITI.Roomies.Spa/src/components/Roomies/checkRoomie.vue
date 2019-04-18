<template>
  <div class="container">
    <div>Bonjour</div>
  </div>
</template>

<script>
// import { DateTime } from "luxon";
import AuthService from "../../services/AuthService";
import { state } from "../../state";
import {
  getRoomieByEmailAsync
} from "../../api/RoomiesApi";

export default {
  data() {
    return {};
  },

  async mounted() {
      try{
        var dataToRoute = await getRoomieByEmailAsync();
        console.log(dataToRoute.roomieId);
        this.$router.replace("/roomies/" + dataToRoute.roomieId);
      }
     catch (e) {
        console.error(e);
        this.$router.replace("/roomies/create");

     }
    //this.item.email = AuthService.email;

  },

  methods: {}
};
</script>

<style lang="scss">
</style>

<style lang="scss">
@import "../../styles/global.scss";
</style>