<template>
  <div class="container">
    <div>
      <h1>Tableau de bord</h1>
    </div>
    <div>Bonjour</div>
  </div>
</template>

<script>
// import { DateTime } from "luxon";
import AuthService from "../../services/AuthService";
import { state } from "../../state";
import {
  getRoomieByIdAsync
} from "../../api/RoomiesApi";

export default {
  data() {
    return {};
  },

  async mounted() {
    console.log(this.$route.params.id);
    console.log(await getRoomieByIdAsync(this.$route.params.id));
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