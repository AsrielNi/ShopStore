/*import { jQuery } from "./jquery-3.7.1";*/

// 獲得多個商品資訊的方法
function getMenu() {
    var request = new XMLHttpRequest();
    request.open("get", "/api/Product/GetMenu", false);
    request.send();
    var dataCluster = JSON.parse(request.responseText);
    for (var i = 0; i < dataCluster.length; i++) {
        var data = dataCluster[i]
        data["link"] = `/Product/ProductInfo?productID=${data.productID}`
    }
    return dataCluster
}

// 獲得單一商品資訊的方法
function getProductInfo(id) {
    var request = new XMLHttpRequest();
    request.open("get", `/api/Product/GetProductInfo/${id}`, false);
    request.send();
    var data = JSON.parse(request.responseText);
    return data;
}