/**
 * 常用数据通用接口
 * 混入后可以直接调用
 * @Author：lee
 * */

import {
	mapState
} from 'vuex'
import dateFormat from '../libs/dateformat.js'

let VueCommonConstData = {
	data() {
		return {
			/*采购单模块*/
			CurrencyList: [],
			invoiceIndex: 0,
			POInvoiceTypeList: [{
				name: '--All--',
				value: -999999
			}, {
				name: '====',
				value: 1
			}, {
				name: '带增票',
				value: 2
			}, {
				name: '带普票',
				value: 3
			}],
			pickPayIndex: 0,
			PayDateTypeList: [{
				name: '正常支付',
				value: 0
			}, {
				name: '预付',
				value: 1
			}, {
				name: '低于帐期支付',
				value: 2
			}, ],
			/*基础请求DTO Body内容根据业务构造传入*/
			baseRequestDto: {
				Token: uni.getStorageSync(this.MshSessionID),
				TimeSpan: uni.getStorageSync(this.mshconfig.mshdata_expirationName),
				UserSysNo: 0,
				ReqBody: {},
				ReqBodyDTO: {}
			},
			pobasketcount: 0,
			userSession: {},
			modalName: null
		}
	},
	computed: mapState(['forcedLogin', 'hasLogin', 'userName']),
	created: function() {
		this.userSession = uni.getStorageSync("MshUserSession")
		this.baseRequestDto.UserSysNo = this.userSession.User.SysNo
	},
	methods: {
		showAbout: function() {
			console.log("这是一个混入，可以测试调用是否正确");
		},
		hideModal(e) {
			this.modalName = null;
		},
	},
	mounted: function() {},
	filters: {
		formatFullDate: function(time) {
			if (time != null && time != "") {
				var date = new Date(time);
				return dateFormat.formatTimeToStr(date, "yyyy-MM-dd HH:mm:ss");
			} else {
				return "";
			}
		},
		formatShortDate: function(time) {
			if (time != null && time != "") {
				var date = new Date(time);
				return dateFormat.formatTimeToStr(date, "yy-MM-dd hh:mm");
			} else {
				return "";
			}
		},
		formatDate: function(time) {
			if (time != null && time != "") {
				var date = new Date(time);
				return dateFormat.formatTimeToStr(date, "yyyy-MM-dd");
			} else {
				return "";
			}
		},
		percent: function(value) {
			if (value != null && value != "") {
				let str = Number(value * 100).toFixed(2);
				str += "%";
				return str
			} else {
				return "";
			}
		}
	}
}
export default VueCommonConstData
