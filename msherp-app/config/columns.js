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
	})
	arr.push({
		title: '名称',
		key: 'ProductName',
	})
	arr.push({
		title: '所有库存',
		key: 'V_AccountQty',
	})
	arr.push({
		title: '所有可用',
		key: 'V_AvailableQty',
	})
	arr.push({
		title: '日均销',
		key: 'V_DaySaleQty',
	})
	arr.push({
		title: '当日门店需求',
		key: 'V_DemandQty',
	})
	arr.push({
		title: '订购数量',
		key: 'Quantity',
	})
	arr.push({
		title: '订购价格',
		key: 'OrderPrice',
	})
	return arr;
}
export default columns
