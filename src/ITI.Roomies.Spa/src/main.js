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
import VueGlobalVariable from 'vue-global-var';

Vue.config.productionTip = false;

Vue.component(Button.name, Button);
Vue.component(Select.name, Select);
Vue.use(ElementUI);
Vue.use(VCalendar);

class currentColloc {

  constructor(collocId, collocName) {
    this.collocId = collocId;
    this.collocName = collocName;
  }

  setCollocId(newId){
    this.collocId = newId;
  }

  setCollocName(newName){
    this.collocName = newName;
  }
}

Vue.use(VueGlobalVariable, {
  globals: {
  $collocName: '',
  $currColloc: new currentColloc(0,'')
  },
  });

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