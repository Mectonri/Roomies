import './main.vendors'
import './main.auth'
import AuthService from './services/AuthService'
import Vue from 'vue'
import App from './components/App.vue'
import router from './main.router'
import i18n from './plugins/i18n'
import ElementUI from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';

Vue.config.productionTip = false
Vue.use(ElementUI);

const main = async() => {
  await AuthService.init();

  new Vue({
    i18n,
    router,
    el:'#app',
    render: h => h(App)
  }).$mount('#app')
};

main();