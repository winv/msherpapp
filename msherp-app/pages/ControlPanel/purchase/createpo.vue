<template>
	<view>
		<cu-custom bgColor="bg-gradual-pink" :isBack="true">
			<block slot="backText">返回</block>
			<block slot="content">新建采购单</block>
		</cu-custom>
		
		
		<view class="cu-list menu-avatar">
				<view class="cu-form-group">
					<view class="title" >采购单号：</view>
					<input type="text" v-model="pomaster.reqbody.SysNo"  placeholder="自动,无需填写"/>
				</view>
				<view class="cu-form-group">
					<view class="title">供应商：</view>
					<input type="text" v-model="pomaster.reqbody.VendorTextInfo" placeholder="可以直接输入供应系统号"/>
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
					<picker mode="date" :value="date" start="2018-01-01" end="2025-12-31" @change="DateChange">
						<view class="picker">
							{{date}}
						</view>
					</picker>
				</view>
				<view class="cu-form-group">
					<view class="title">对外备注：</view>
					<input type="text" v-model="productInfo.BriefNameSpellCode" />
				</view>
				<view class="cu-form-group">
					<view class="title">对内备注：</view>
					<input type="text" v-model="productInfo.packspec" />
				</view>
				<view class="cu-form-group">
					<view class="title">带票类型：</view>
					<input type="text" v-model="productInfo.DenominatedUnit" />
				</view>
				<view class="cu-form-group">
					<view class="title">支付类型：</view>
					<input type="text" v-model="productInfo.BarCode" />
				</view>
				<view class="cu-form-group">
					<view class="title">截止到货：</view>
					<input type="text" v-model="productInfo.CartonMeasurement" />
				</view>
			</view>
		
		<view class="table">
			<z-table :tableData="baseTableData" :columns="baseColumns"></z-table>
		</view>
		<view class="cu-bar bg-white tabbar border shop fixedbottom">
				<view class="action">
					<view class="cuIcon-cart">
						<view class="cu-tag badge">99</view>
					</view>
					采购篮
				</view>
				<view class="btn-group">
					<button class="cu-btn bg-orange round shadow-blur">保存</button>
					<button class="cu-btn bg-red round shadow-blur">添加商品</button>
				</view>
			</view>
		
	</view>
</template>

<script>
	import purchase from '../../../service/purchase.service.js'
	import zTable from '../../../components/z-table/z-table.vue';
	import dateFormat from '../../../libs/dateformat.js'
	export default {
		components: {
			zTable
		},
		data() {
			return {
				pomaster:{
					reqbody:{
						VendorTextInfo:''
					}
				},
				basketItemList:[],
				ProductInfo: {},
				userInfo: {},
				StockList: [{
					name: '上海总仓',
					value: '6'
				}, ],
				pickindex: 0,
				baseTableData: [
					{
						name: '张三',
						age: 18,
						gender: '男'
					},
					{
						name: '赵四',
						age: 16,
						gender: '女'
					},
					{
						name: '王五',
						age: 20,
						gender: '男'
					},
					{
						name: '李六',
						age: 18,
						gender: '女'
					}
				],
				baseColumns: [
					{
						title: '姓名',
						key: 'name',
						
					},
					{
						title: '性别',
						key: 'gender',
						
					},
					{
						title: '年龄',
						key: 'age',
						
					},
					{
						title: '年龄',
						key: 'age',
						
					},{
						title: '年龄',
						key: 'age',
						
					}
				],
				baseHtml: '<z-table :tableData="baseTableData" :columns="baseColumns"></z-table>',
				date:dateFormat(new Date()+1,'yyyy-MM-dd'),
			};
		},
		onLoad(options) {
			this.userInfo = uni.getStorageSync("MshUserSession");
			if(options.data!==undefined){
				this.basketItemList=JSON.parse(options.data);
				console.log(this.basketItemList);
			}
		},
		onShow(){
			var vinfo=uni.getStorageSync('vendorinfo')
			this.pomaster.reqbody.VendorTextInfo=vinfo.name
			console.log(uni.getStorageSync('vendorinfo'))
			uni.removeStorageSync('vendorinfo')
		},
		mounted(){
			if(this.basketItemList.length>0){
				this.initBasketData()
			}
		},
		methods: {
			initBasketData(){
				purchase.getPoItem(this.basketItemList).then(res=>{
					console.log(res);
				});
			},
			PickerChange(e) {
				this.pickindex = e.detail.value;
				this.searchDataDto.ReqBody.SearchStatus = [this.ProductStatusList[this.pickindex].value];
			},
			ShowVendorIndexList(e){
				uni.navigateTo({
					url:'../vendor/vendorindexlist'
				})
			},
			DateChange(e) {
				this.date = e.detail.value
			},
		}
	};
</script>
<style>
</style>
