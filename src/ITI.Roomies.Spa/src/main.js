import './main.vendors'
import './main.auth'
import AuthService from './services/AuthService'
import Vue from 'vue'
import App from './components/App.vue'
import router from './main.router'
import i18n from './plugins/i18n'

Vue.config.productionTip = false

const main = async() => {
  await AuthService.init();

  new Vue({
    i18n,
    router,
    render: h => h(App)
  }).$mount('#app')
};

main();