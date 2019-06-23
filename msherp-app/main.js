import Vue from 'vue'
import VueValidator,{ Validator } from 'vue-validator'
import App from './App'
import store from './store'
import mshconfig from './config/mshconfig.js'
import httprequest from './libs/httprequest.js'
import md5 from './libs/md5.js'
import cuCustom from './colorui/components/cu-custom.vue'
import faIcon from './components/kilvn-fa-icon/fa-icon.vue'
import wxsystem from './libs/wxsystem.js'
import veeconf from './config/validate.js'

Vue.component('cu-custom',cuCustom)
Vue.component('fa-icon',faIcon)
Vue.use(VueValidator,veeconf.veeconfig)
//Validator.updateDictionary(veeconf.dictionary)

App.mpType = 'app'
Vue.config.productionTip = false
Vue.prototype.$store = store
Vue.prototype.SERVER_URL = mshconfig.env === "production" ? mshconfig.productionUrl : mshconfig.devUrl
Vue.prototype.http = httprequest;
Vue.prototype.Md5 = md5;
Vue.prototype.MshSessionID = mshconfig.mshsessionid;
Vue.prototype.mshconfig = mshconfig;
console.log(httprequest)
const app = new Vue({
	store,
	...App
})
app.$mount()
