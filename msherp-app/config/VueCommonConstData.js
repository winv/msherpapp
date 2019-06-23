/**
 * 常用数据通用接口
 * 混入后可以直接调用
 * @Author：lee
 * */
let VueCommonConstData = {
	data() {
		return {
			/*采购单模块*/
			CurrencyList: [],
			invoiceIndex:0,
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
			pickPayIndex:0,
			PayDateTypeList:[
				{
					name: '正常支付',
					value: 0
				},{
					name: '预付',
					value: 1
				},{
					name: '低于帐期支付',
					value: 2
				},
			],
			/*基础请求DTO Body内容根据业务构造传入*/
			baseRequestDto:{
				Token: uni.getStorageSync(this.MshSessionID),
				TimeSpan: uni.getStorageSync(this.mshconfig.mshdata_expirationName),
				ReqBody:{},
				ReqBodyDTO:{}
			},
			pobasketcount:0,
			userSession:{}
		}
	},
	created: function() {
		this.userSession=uni.getStorageSync("MshUserSession")
	},
	methods: {
		showAbout: function() {
			console.log("这是一个混入，可以测试调用是否正确");
		}
	}
}
export default VueCommonConstData
