import Vue from 'vue'
import mshconfig from '../config/mshconfig.js'
import http from '../libs/httprequest.js'
import md5 from '../libs/md5.js'
import urls from '../libs/urls.js'
import wxsystem from '../libs/wxsystem.js'

/**
 *供应服务接口 
 * @author：lee
 * */
 var vendor={}
 vendor.GetVendorIndexList=function(param){
	 return new Promise(function(resolve, reject) {
	     http.post(urls.getFullERPUrl('vendor/QueryVendorList'), param).then(res => {
	         resolve(res.data)
	     })
	 })
 }
 export default vendor