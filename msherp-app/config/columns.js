let columns = {};
/**
 * 采购单 商品table列配置
 * @obj vue对象this
*/
columns.PoItemCol = function(obj) {
	var arr = [];
	arr.push({
		title: '商品号',
		key: 'ProductSysNo',
		width:100
	})
	arr.push({
		title: '名称',
		key: 'ProductName',
	})
	arr.push({
		title: '数量',
		key: 'Quantity',
	})
	arr.push({
		title: '价格',
		key: 'OrderPrice',
	})
	arr.push({
		title: '库存',
		key: 'V_AccountQty',
	})
	arr.push({
		title: '可用',
		key: 'V_AvailableQty',
	})
	arr.push({
		title: '日均销',
		key: 'V_DaySaleQty',
	})
	arr.push({
		title: '门店需求',
		key: 'V_DemandQty',
	})
	return arr;
}
columns.PoItemMainCol = function(obj) {
	var arr = [];
	arr.push({
		title: '商品号',
		key: 'ProductSysNo',
		width:100
	})
	arr.push({
		title: '名称',
		key: 'ProductName',
	})
	arr.push({
		title: '数量',
		key: 'Quantity',
	})
	arr.push({
		title: '价格',
		key: 'OrderPrice',
	})
	arr.push({
		title: '库存',
		key: 'V_AccountQty',
	})
	arr.push({
		title: '可用',
		key: 'V_AvailableQty',
	})
	arr.push({
		title: '日均销',
		key: 'V_DaySaleQty',
	})
	arr.push({
		title: '门店需求',
		key: 'V_DemandQty',
	})
	return arr;
}
export default columns
