﻿
function ModifySave() {
    var tName = $("#tName");
    if (!tName.val()) {
        ShowFailTip("指令操作不能为空");
        return;
    }
    $.ajax({
        url: ($Maticsoft.BasePath + "WeChat/UpdateAction?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: $('#updateAction').serializeArray(),
        success: function (resultData) {
            if (resultData["Result"] == "OK") {
                ShowSuccessTip("操作成功!");
                setTimeout("DelayTime()", 2000);
            } else {
                ShowFailTip("操作失败!");
            }
        }, error: function (xmlHttpRequest, textStatus, errorThrown) {
            ShowFailTip(xmlHttpRequest.responseText);
        }
    });
}
function DelayTime() {
    window.parent.location.reload();
}