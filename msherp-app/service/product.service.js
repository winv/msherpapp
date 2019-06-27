import Vue from 'vue'
import mshconfig from '../config/mshconfig.js'
import http from '../libs/httprequest.js'
import md5 from '../libs/md5.js'
import urls from '../libs/urls.js'
import wxsystem from '../libs/wxsystem.js'

let product = {}
/**
 * 查询商品列表接口
 * @param {}
 * @author:lee
 */
product.QueryProductList = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Product/QueryProductList'), param).then(res => {
			resolve(res.data)
		}).catch(res => {
			console.log(res)
			reject(res)
		})
	})
}
export default product
