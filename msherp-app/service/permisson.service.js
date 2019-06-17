import Vue from 'vue'
import App from './App'
import store from './store'
import mshconfig from './config/mshconfig.js'
import httprequest from './utils/httprequest.js'
import md5 from './libs/md5.js'

let permisson={};

/**
 * 登录接口
 * @param {*} param
 *
 */
// permisson.login = function(param){
//   return new Promise(function(resolve,reject){
//     request.loginPost(http.getFullPermissonUrl('/Auth/WXLogin'),param).then(res=>{
//       resolve(res)
//     })
//   })
// }