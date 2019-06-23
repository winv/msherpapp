<template>
	<view>
		<cu-custom bgColor="bg-gradual-pink" :isBack="true">
			<block slot="backText">返回</block>
			<block slot="content">新建采购单</block>
		</cu-custom>

		<view class="cu-list menu-avatar">
			<view class="cu-form-group">
				<view class="title">采购单号：</view>
				<input type="text" v-model="PoMasterInfo.SysNo" placeholder="自动,无需填写" />
			</view>
			<view class="cu-form-group">
				<view class="title">供应商：</view>
				<input type="text" v-model="PoMasterInfo.VendorTextInfo" v-validate:PoMasterInfo.VendorTextInfo="['required']" placeholder="可以直接输入供应系统号" />
				<text class="lg text-red" :class="'cuIcon-filter'" @tap="ShowVendorIndexList"></text>
			</view>
			<view class="cu-form-group">
				<view class="title">收货仓库：</view>
				<picker @change="PickerChange" :value="pickindex" :range="StockList" :range-key="'name'">
					<view class="picker pull-left">{{ StockList[pickindex].name }}</view>
				</picker>
			</view>
			<view class="cu-form-group">
				<view class="title">支付时间：</view>
				<picker mode="date" :value="PoMasterInfo.PayDate" start="2019-01-01" end="2025-12-31" @change="DateChange">
					<view class="picker">{{ PoMasterInfo.PayDate }}</view>
				</picker>
			</view>
			<view class="cu-form-group">
				<view class="title">对外备注：</view>
				<input type="text" v-model="PoMasterInfo.Note" />
			</view>
			<view class="cu-form-group">
				<view class="title">对内备注：</view>
				<input type="text" v-model="PoMasterInfo.Memo" />
			</view>
			<view class="cu-form-group">
				<view class="title">带票类型：</view>
				<picker @change="PickerInvoiceChange" :value="invoiceIndex" :range="POInvoiceTypeList" :range-key="'name'">
					<view class="picker pull-left">{{ POInvoiceTypeList[invoiceIndex].name }}</view>
				</picker>
			</view>
			<view class="cu-form-group">
				<view class="title">支付类型：</view>
				<picker @change="PickerPayTypeChange" :value="pickPayIndex" :range="PayDateTypeList" :range-key="'name'">
					<view class="picker pull-left">{{ PayDateTypeList[pickPayIndex].name }}</view>
				</picker>
			</view>
			<view class="cu-form-group">
				<view class="title">截止到货：</view>
				<picker mode="date" :value="PoMasterInfo.ExpectInTime" start="2018-01-01" end="2025-12-31" @change="arriveDateChange">
					<view class="picker">{{ PoMasterInfo.ExpectInTime }}</view>
				</picker>
			</view>
		</view>
		<view class="table"><z-table :tableData="basePoItemList" :columns="basePoItemColumns"></z-table></view>
		<view class="cu-bar bg-white tabbar border shop fixedbottom">
			<view class="action">
				<view class="cuIcon-cart">
					<view class="cu-tag badge">{{ pobasketcount }}</view>
				</view>
				采购篮
			</view>
			<view class="btn-group">
				<button class="cu-btn bg-orange round shadow-blur" @tap="CreateToPoMaster">创建采购单</button>
				<button class="cu-btn bg-red round shadow-blur">添加商品</button>
			</view>
		</view>
	</view>
</template>
<script>
import purchase from '../../../service/purchase.service.js';
import zTable from '../../../components/z-table/z-table.vue';
import dateFormat from '../../../libs/dateformat.js';
import columns from '../../../config/columns.js';
import vueCommonData from '../../../config/VueCommonConstData.js';

export default {
	components: {
		zTable
	},
	data() {
		return {
			pomaster: {
				reqbody: {
					VendorTextInfo: ''
				}
			},
			searchDataDto: {
				reqbody: {
					SysNo: '',
					ProductSysNo: ''
				}
			},
			PoMasterInfo: {
				SysNo: -999999,
				PayDate: dateFormat.formatTimeToStr(dateFormat.AddDays(1), 'yyyy-MM-dd'),
				ExpectInTime: dateFormat.formatTimeToStr(dateFormat.AddDays(3), 'yyyy-MM-dd'),
				POInvoiceType: 0,
				PayDateType: 0,
				StockSysNo: 6,
				VendorTextInfo: '',
				VendorSysNo: 0,
				CreateUserSysNo: 0,
				poItems: []
			},
			basketItemList: [],
			ProductInfo: {},
			userInfo: {},
			StockList: [
				{
					name: '上海总仓',
					value: '6'
				}
			],
			pickindex: 0,
			basePoItemList: [],
			basePoItemColumns: [],
			baseHtml: '<z-table :tableData="baseTableData" :columns="baseColumns"></z-table>'
		};
	},
	mixins: [vueCommonData],
	onLoad(options) {
		this.userInfo = uni.getStorageSync('MshUserSession');
		this.initParam(options);
	},
	onShow() {
		var vinfo = uni.getStorageSync('vendorinfo');
		this.PoMasterInfo.VendorTextInfo = vinfo.name;
		console.log(uni.getStorageSync('vendorinfo'));
		uni.removeStorageSync('vendorinfo');
	},
	mounted() {
		if (this.basketItemList.length > 0) {
			this.initBasketData();
		}
		this.basePoItemColumns = columns.PoItemCol(this);
		this.PoMasterInfo.CreateUserSysNo = this.userSession.User.SysNo;
		this.initData();
	},
	methods: {
		initParam(options) {
			var data = options || '';
			if (Object.keys(data).length > 0) {
				var sysnos = [];
				var productsysnos = [];
				this.basketItemList = JSON.parse(options.data);
				this.basketItemList.forEach(item => {
					sysnos.push(item.SysNo);
					productsysnos.push(item.ProductSysNo);
				});
				this.searchDataDto.reqbody.SysNo = sysnos.join(',');
				this.searchDataDto.reqbody.ProductSysNo = productsysnos.join(',');
				console.log(this.basketItemList);
			}
		},
		initBasketData() {
			var self = this;
			purchase.getPoBasketItem(this.searchDataDto).then(res => {
				console.log(res);
				self.basePoItemList = res.Data.ResBody;
				self.PoMasterInfo.poItems = self.basePoItemList;
				console.log(self.basePoItemList);
			});
			
		},
		initData(){
			var reqdto = this.baseRequestDto;
			reqdto.ReqBody.CreateUserSysNo = this.userSession.User.SysNo;
			reqdto.ReqBody.StockSysNo = 6;
			purchase.GetPobasketCount(reqdto).then(res => {
				console.log(res);
				this.pobasketcount = res.Data;
			});
		},
		CreateToPoMaster() {
			this.baseRequestDto.ReqBodyDTO = this.PoMasterInfo;
			console.log(this.baseRequestDto);
			purchase.CreateToPoMaster(this.baseRequestDto).then(res => {
				console.log(res);
			});
		},
		PickerChange(e) {
			this.pickindex = e.detail.value;
			this.PoMasterInfo.VendorSysNo = this.ProductStatusList[this.pickindex].value;
		},
		ShowVendorIndexList(e) {
			uni.navigateTo({
				url: '../vendor/vendorindexlist'
			});
		},
		DateChange(e) {
			this.PoMasterInfo.PayDate = e.detail.value;
		},
		arriveDateChange(e) {
			this.PoMasterInfo.ExpectInTime = e.detail.value;
		},
		PickerInvoiceChange(e) {
			this.invoiceIndex = e.detail.value;
			this.PoMasterInfo.POInvoiceType = this.POInvoiceTypeList[this.invoiceIndex].value;
		},
		PickerPayTypeChange(e) {
			this.pickPayIndex = e.detail.value;
			this.PoMasterInfo.PayDateType = this.PayDateTypeList[this.pickPayIndex].value;
		}
	}
};
</script>
<style></style>
