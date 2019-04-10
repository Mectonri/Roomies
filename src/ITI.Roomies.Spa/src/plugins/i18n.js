import Vue from 'vue';
import VueI18n from 'vue-i18n';

Vue.use(VueI18n);

const messages ={
    'fr':{
        welcomeMsg:'Bienvenue sur votre application Vus.js'
    },
    'en':{
        welcomeMsg:'Welcome to your Vue.js app'
    }
};

const i18n = new VueI18n({
    locale:'fr',
    fallbackLocale:'en',
    messages,
});