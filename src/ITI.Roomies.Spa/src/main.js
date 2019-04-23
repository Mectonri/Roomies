import './main.vendors'
import './main.auth'
import AuthService from './services/AuthService'
import Vue from 'vue'
import App from './components/App.vue'
import router from './main.router'
import i18n from './plugins/i18n'
import ElementUI from 'element-ui';
import { Button, Select } from 'element-ui';
import 'element-ui/lib/theme-chalk/index.css';
import VCalendar from 'v-calendar';

Vue.config.productionTip = false;

Vue.component(Button.name, Button);
Vue.component(Select.name, Select);
Vue.use(ElementUI);
Vue.use(VCalendar);

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