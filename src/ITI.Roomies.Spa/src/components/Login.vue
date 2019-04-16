<template>

    <div class="main">
        
        <div class="text-center">
            <h1 class="my-4">Bienvenue sur Roomies</h1>
        </div>
        <div>
            <button type="button" @click="login('Google')" class="btn btn-lg btn-block btn-primary">
                <i class="fa fa-google" aria-hidden="true"></i> Se connecter via Google</button>
            
            <button type="button" @click="login('Base')" class="btn btn-lg btn-block btn-default">Se connecter via Roomies</button>
        </div>
    </div>
</template>

<script>
import AuthService from '../services/AuthService'
import Vue from 'vue'
/*import onepagescroll from '../styles/onepagescroll'

onepagescroll(".main", {
     sectionContainer: "section",
     loop: true,
     responsiveFallback: false
   });*/

export default {
    data() {
        return {
            endpoint: null
        }
    },

    mounted() {
        AuthService.registerAuthenticatedCallback(() => this.onAuthenticated());
    },

    beforeDestroy() {
        AuthService.removeAuthenticatedCallback(() => this.onAuthenticated());
    },

    methods: {
        login(provider) {
            AuthService.login(provider);
        },

        onAuthenticated() {
            this.$router.replace('/');
        }
    }
}
</script>

<style lang="scss">
iframe {
  width: 100%;
  height: 600px;
}
</style>