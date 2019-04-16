<template>
  <div id="app">
    <el-menu
      default-active="2"
      class="el-menu-vertical-demo"
      @open="handleOpen"
      @close="handleClose"
      :collapse="isCollapse"
    >
    
      <el-button @click.native="expand_collapse" > <span class="navbar-toggler-icon el-icon-more"></span> </el-button>
      <el-submenu-item index="0"><el-button class="el-icon-star-on" @click="clickRoute('/')">  </el-button>
      </el-submenu-item>
      <el-submenu index="1">
        <template slot="title">
          <i class="el-icon-location"></i>
          <span slot="title">Navigator One</span>
        </template>
        <el-menu-item-group>
          <span slot="title">Group One</span>
          <el-menu-item index="1-1">item one</el-menu-item>
          <el-menu-item index="1-2">item two</el-menu-item>
        </el-menu-item-group>
        <el-menu-item-group title="Group Two">
          <el-menu-item index="1-3">item three</el-menu-item>
        </el-menu-item-group>
        <el-submenu index="1-4">
          <span slot="title">item four</span>
          <el-menu-item index="1-4-1">item one</el-menu-item>
        </el-submenu>
      </el-submenu>
      <el-menu-item index="2">
        <i class="el-icon-menu"></i>
        <span slot="title">Navigator Two</span>
      </el-menu-item>
      <el-menu-item index="3" disabled>
        <i class="el-icon-document"></i>
        <span slot="title">Navigator Three</span>
      </el-menu-item>
      <el-menu-item index="4">
        <i class="el-icon-setting" @click="clickRoute('/Roomies')"></i>
        <span slot="title"><router-link class="nav-link" to="/Roomies">Roomies</router-link></span>
      </el-menu-item>
    </el-menu>
    <main role="main" class="p-3 p-md-4 p-lg-5">
      <router-view class="child"></router-view>
    </main>
  </div>
</template>

<script>
import AuthService from "../services/AuthService";
import "../directives/requiredProviders";
import { state } from "../state";

export default {
  data() {
    return {
      state
    };
  },

  computed: {
    auth: () => AuthService,

    isLoading() {
      return this.state.isLoading;
    }
  }
};
</script>

<style lang="scss" scoped>


.el-menu-vertical-demo:not(.el-menu--collapse) {
  width: 200px;
  float: left;
  min-height: 100vh;
  
}
.el-menu-vertical-demo {
  float: left;
  min-height: 100vh;
}
</style>

<style lang="scss">
@import "../styles/global.scss";
</style>

<script>
export default {
  data() {
    return {
      isCollapse: true
    };
  },
  methods: {
    clickRoute(pathToRoute){
      this.$router.push(pathToRoute);
    },
    handleOpen(key, keyPath) {
      console.log(key, keyPath);
    },
    handleClose(key, keyPath) {
      console.log(key, keyPath);
    },
  expand_collapse(){
    if(this.isCollapse) this.isCollapse = false;
    else this.isCollapse = true;
  }
},
  };
</script>