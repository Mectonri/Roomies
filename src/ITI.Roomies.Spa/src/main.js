import Vue from 'vue'
import App from './App.vue'
import router from './router'
import i18n from './plugins/i18n'

Vue.config.productionTip = false

new Vue({
  router,
  render: h => h(App)
}).$mount('#app')
