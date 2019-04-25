<template></template>


<script>
// import { DateTime } from "luxon";
import AuthService from "../../services/AuthService";
import { state } from "../../state";
import { FindByEmail } from "../../api/RoomiesApi";
import {getCollocByRoomieIdAsync} from "../../api/CollocationApi";

export default {
  data() {
    return {};
  },

  async mounted() {
    try {
      var dataToRoute = await FindByEmail();
      try {
        this.$currColloc.setCollocId(await getCollocByRoomieIdAsync())
      } catch (e) {
        console.log(e);
      }
      // Affiche le menu de navigation
      document.getElementById("navMenu").style.display = "block";
      this.$router.replace("/roomies/" + dataToRoute.roomieId);
    } catch (e) {
      console.error(e);
      this.$router.replace("/roomies/create");
    }
  },

  methods: {}
};
</script>

<style lang="scss">
</style>

<style lang="scss">
@import "../../styles/global.scss";
</style>