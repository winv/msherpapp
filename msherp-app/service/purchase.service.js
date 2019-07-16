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
 * 初始状态采购单 增加商品
 * @param {*} param
 * author lee
 */
purchase.InsertPoItem = function(param) {
	return new Promise(function(resolve, reject) {
		http.$put(urls.getFullERPUrl('Purchase/InsertPoItem'), param).then(res => {
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

/**
 * 查询采购单列表接口
 * */
purchase.QueryPoList = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/QueryPoList'), param).then(res => {
			resolve(res.data)
		})
	})
}

/**
 * 查询采购单详细
 * */
purchase.QueryPoMaster = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/QueryPoMaster'), param).then(res => {
			resolve(res.data)
		})
	})
}

/**
 * 删除采购单子表元素
 * */
purchase.DeletePoItem = function(param) {
	return new Promise(function(resolve, reject) {
		http.$delete(urls.getFullERPUrl('Purchase/DeletePoItem'), param).then(res => {
			resolve(res.data)
		})
	})
}
/**
 * 更新采购单子表信息接口
 */
purchase.UpdatePoitem = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/UpdatePoitem'), param).then(res => {
			resolve(res.data)
		})
	})
}

/**
 * 图像识别接口
 */
purchase.GetImageInfo = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('BaiAI/GetImageInfo'), param).then(res => {
			resolve(res.data)
		})
	})
}

/**
 * 采购单摊销 接口 暂不支持批量摊销
 */
purchase.POApportionCost = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/POApportionCost'), param).then(res => {
			resolve(res.data)
		})
	})
}

/**
 * 采购单取消审核接口
 */
purchase.CancelVerifyPo = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/CancelVerifyPo'), param).then(res => {
			resolve(res.data)
		})
	})
}

/**
 * 采购单作废接口
 */
purchase.AbandonPo = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/AbandonPo'), param).then(res => {
			resolve(res.data)
		})
	})
}

/**
 * 采购单作废接口
 */
purchase.CancelAbandonPo = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/CancelAbandonPo'), param).then(res => {
			resolve(res.data)
		})
	})
}

/**
 * 获取审核内容接口
 */
purchase.GetVerifyContent = function(param) {
	return new Promise(function(resolve, reject) {
		http.post(urls.getFullERPUrl('Purchase/GetVerifyContent'), param).then(res => {
			resolve(res.data)
		})
	})
}
export default purchase
