import Vue from 'vue'
import mshconfig from '../config/mshconfig.js'
import http from '../libs/httprequest.js'
import md5 from '../libs/md5.js'
import urls from '../libs/urls.js'
import wxsystem from '../libs/wxsystem.js'

let purchase = {};
purchase.getPoItem = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('/SaleStatistics/GetSalesRankings'), param).then(res => {
			resolve(res.data)
		})
	})
}
/**
 * 获取采购篮列表*/
purchase.QueryPoBaksetList = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/QueryPoBaksetList'), param).then(res => {
			resolve(res.data)
		})
	})
}
/**
 * 获取采购篮商品 列表
 * @param {*} param
 * author lee
 */
purchase.getPoBasketItem = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/QueryPoItemListWithBaskt'), param).then(res => {
			resolve(res.data)
		}).catch(function(res) {
			console.log(res)
			return res
		})
	}).catch(function(res) {
		console.log(res)
		return res
	})
}

/**
 * 写入采购单主表及子表
 * @param {*} param
 * author lee
 */
purchase.CreateToPoMaster = function(param) {
	return new Promise(function(resolve, reject) {
		http.$put(urls.getFullERPUrl('Purchase/InsertPoMaster'), param).then(res => {
			resolve(res.data)
		})
	})
}

/**
 * 写入采购单主表及子表
 * @param {*} param
 * author lee
 */
purchase.GetPobasketCount = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/QueryPoBaksetCount'), param).then(res => {
			resolve(res.data)
		})
	})
}
/**
 * 获取购物篮详情接口
 * */
purchase.QueryPoBaksetDetail = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/QueryPoBaksetDetail'), param).then(res => {
			resolve(res.data)
		})
	})
}

/**
 * 写入购物篮
 * */
purchase.InsertPoBasket = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/InsertPoBasket'), param).then(res => {
			resolve(res.data)
		})
	})
}
export default purchase
