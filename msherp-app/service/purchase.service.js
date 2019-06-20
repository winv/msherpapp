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
            resolve(res)
        })
    })
}

export default purchase
