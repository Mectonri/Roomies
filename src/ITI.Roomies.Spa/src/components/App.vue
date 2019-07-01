<template>
  <div id="app">
    <!-- Menu de navigation -->
    <el-menu
    id="navMenu"
      default-active="2"
      class="el-menu-vertical-demo"
      :collapse="isCollapse"
      v-bind:style="actualTheme.style"
    >
      <!-- TO DO : Remplacer par l'image de la Colloc -->
      Name : {{$currColloc.collocName}}
      <br>
      <el-menu-item @click="clickRoute('/roomies')">
        <i class="el-icon-star-on">
          <span slot="title">Accueil</span>
        </i>
      </el-menu-item>
      <br>
      <br>
      <el-button @click.native="expand_collapse">
        <span class="navbar-toggler-icon el-icon-more"></span>
      </el-button>
      <el-menu-item @click="clickRoute('/roomies/collocation')">
        <i class="el-icon-menu"></i>
        <span slot="title">Gestion collocation</span>
      </el-menu-item>
      <el-menu-item @click="clickRoute('/roomies/profile')">
        <i class="el-icon-s-custom"></i>
        <span slot="title">Profil</span>
      </el-menu-item>
      <el-menu-item
        @click="clickRoute('/roomies/calendar')"
        :disabled="$setMenuItemDisabled.disableState"
      >
        <i class="el-icon-menu"></i>
        <span slot="title">Calendrier</span>
      </el-menu-item>


      <el-submenu index="1" :disabled="$setMenuItemDisabled.disableState">
        <template slot="title">
          <i class="el-icon-document"/>
          <span>Tâches</span>
        </template>

        <el-menu-item
          class="el-submenu-item"
          index="1-1"
          @click="clickRoute('/task/colloc')"
          :disabled="$setMenuItemDisabled.disableState"
        >Tâche de la Collocation</el-menu-item>
        <el-menu-item
          class="el-submenu-item"
          index="1-2"
          @click="clickRoute('/task/roomie')"
          :disabled="true"
        >Vos tâches</el-menu-item>
        <el-menu-item
          class="el-submenu-item"
          index="1-3"
          @click="clickRoute('/task/create')"
          :disabled="$setMenuItemDisabled.disableState"
        >Ajouter une tâche</el-menu-item>
      </el-submenu>

      <el-menu-item @click="clickRoute('/course')" :disabled="$setMenuItemDisabled.disableState">
        <i class="el-icon-location"></i>
        <span slot="title">Listes de courses</span>
      </el-menu-item>

      
      <el-submenu index="2" :disabled="$setMenuItemDisabled.disableState">
        <template slot="title">
          <i class="el-icon-pie-chart"/>
          <span>Dépenses</span>
        </template>

        <el-menu-item
          class="el-submenu-item"
          index="2-1"
          @click="clickRoute('/chart')"
          :disabled="$setMenuItemDisabled.disableState"
        >Graphique</el-menu-item>
        <el-menu-item
          class="el-submenu-item"
          index="2-2"
          @click="clickRoute('/budget/create')"
          :disabled="$setMenuItemDisabled.disableState"
        >Budget</el-menu-item>
        <el-menu-item
          class="el-submenu-item"
          index="2-3"
          @click="clickRoute('/transaction/create')"
          :disabled="$setMenuItemDisabled.disableState"
        >Transaction</el-menu-item>
        <el-menu-item
          class="el-submenu-item"
          index="2-4"
          @click="clickRoute('/Category/create')"
          :disabled="$setMenuItemDisabled.disableState"
        >Catégorie</el-menu-item>
      </el-submenu>


      <el-menu-item @click="clickRoute('/')" disabled>
        <i class="el-icon-setting"></i>
        <span slot="title">Paramètres</span>
      </el-menu-item>
      <br>
      <br>
      <br>
      <el-menu-item @click="clickRoute('/logout')">
        <i class="el-icon-circle-close"></i>
        <span slot="title">Se déconnecter</span>
      </el-menu-item>
    </el-menu>

    <template>
      <div id="globalContainer">
      <main v-if="state == true " role="main">
        <loading/>
      </main>
      <main v-else class="card containerCard">
        <router-view id="pageContent" class="child"></router-view>
      </main>
      </div>
    </template>


    <!-- Le footer -->
    <footer id="footer" class="font-small mdb-color lighten-3">
      <div class="container">
        <br>
        <language/>
      </div>

    </footer>
  </div>
</template>

<script>
import AuthService from "../services/AuthService";
import "../directives/requiredProviders";
import { inviteRoomieAsync } from "../api/RoomiesApi.js";
import { getCollocNameIdByRoomieIdAsync } from "../api/CollocationApi";
import Language from "../components/Utility/Language.vue";
import Loading from "../components/Utility/Loading.vue";

export default {
  components: {
    Language,
    Loading
  },

  data() {
    return {
      styles: [
        {
          name: "Style1",
          style: "background-color: white;"
        },
        {
          name: "Style2",
          style: "background-color: rgb(142, 142, 142);"
        }
      ],
      themeIdx: 0,
      message: "",
      state: true,
      isCollapse: true
    };
  },
  computed: {
    auth: () => AuthService,
    actualTheme() {
      return this.styles[this.themeIdx];
    },
    isLoading() {
      return this.state.isLoading;
    }
  },
  async mounted() {
    this.state = true;
    //Cache le menu de navigation si l'utilisateur n'est pas connecté
    try {
      if (!AuthService.isConnected) {
        document.getElementById("navMenu").style.display = "none";
      } else {
        // Récupère la premère collocation du Roomie
        var collocData = await getCollocNameIdByRoomieIdAsync();
        if (collocData != undefined) {
          this.$currColloc.setCollocId(collocData.collocId);
          this.$currColloc.setCollocName(collocData.collocName);
          this.$setMenuItemDisabled.setDisableState(false);
        }

      }
    } catch (e) {
      console.log(e);
    } finally {
      this.state = false;
    }

    if (this.$cookies.get("themeIdx") != undefined) {
      this.themeIdx = this.$cookies.get("themeIdx");
    } else {
      this.setTheme(0);
      this.themeIdx = this.$cookies.get("themeIdx");
    }
  },

  methods: {
    setTheme(themeIdx) {
      this.$cookies.set("themeIdx", themeIdx);
      this.themeIdx = themeIdx;
    },
    async invite() {
      await inviteRoomieAsync(this.message);
    },
    clickRoute(pathToRoute) {
      if (!this.isCollapse) this.expand_collapse();
      this.$router.push(pathToRoute);
    },
    expand_collapse() {
      if (this.isCollapse) {
        this.isCollapse = false;
      } else {
        this.isCollapse = true;
      }
    }
  }
};
</script>

<style lang="scss">
@import "../styles/global.scss";
</style>

<style lang="scss" scoped>
.el-menu-vertical-demo:not(.el-menu--collapse) {
  width: 12rem;
  max-width: 12rem;
  border-right: 1px solid #000000;
  float: left;
  margin-top: 0;
}
.el-menu-vertical-demo {
  float: left;
  min-height: 100vh;
  max-height: 100vh;
  border-right: 1px solid #000000;
  position: fixed;
  margin-top: 0;
}

.el-menu-item i,
.el-submenu i,
.el-menu-item.is-active,
.el-submenu-item.is-active {
  color: black;
}
.el-submenu-item,
.el-submenu-item.is-active {
  min-width: 12rem !important;
}
.el-button {
  background: rgb(102, 102, 102);
  border: 1px solid #000000;
  color: #000000;
}

#footer {
  bottom: 0;
  width: 100%;
  height: 8.5rem; /* Footer height */

  top: 2.5rem;
  padding-left: 12rem;
}
</style>
