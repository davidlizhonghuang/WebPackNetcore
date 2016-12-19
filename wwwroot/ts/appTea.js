/// <reference path="../lib/ts/require.d.ts" />
/// <reference path="../lib/ts/jquery.d.ts" />
var __extends = (this && this.__extends) || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
};
var TsTest = (function () {
    function TsTest(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
    }
    TsTest.prototype.fullName = function () {
        return this.firstName + this.lastName;
    };
    TsTest.fullName0 = function () {
        console.log("this.firstName + this.lastName");
    };
    return TsTest;
}());
function getName() {
    var tst = new TsTest("David ", "Huang ");
    document.getElementById("result").innerText = tst.fullName();
}
//$("#clickints").click(TsTest.fullName0); //not worked, because not the static class
//very important, class is not static, but method is, so we still can call it strightly
//simply function use jquery
var TsTeststaic = (function () {
    function TsTeststaic() {
    }
    TsTeststaic.fullName0 = function () {
        console.log("this.firstName + this.lastName");
    };
    return TsTeststaic;
}());
$("#clickints").click(TsTeststaic.fullName0); //not worked, because not the static class
function getDate() {
    var date = new Date(2016, 2, 11);
    $("#dater").text(date.toString());
}
//only module uses exports
var loginM;
(function (loginM) {
    var login = (function () {
        function login() {
        }
        login.prototype.fullName = function () {
            return this.firstName + "" + this.lastName;
        };
        return login;
    }());
    loginM.login = login;
    //oo programming language
    var sublogin = (function (_super) {
        __extends(sublogin, _super);
        function sublogin() {
            _super.call(this);
        }
        return sublogin;
    }(login));
    loginM.sublogin = sublogin;
})(loginM || (loginM = {}));
function getmodulelogin() {
    var login = new loginM.login();
    login.firstName = "jim";
    login.lastName = "zhang";
    login.email = "jim@cgh.com";
    login.mobile = "0895343222";
    document.getElementById("fname").innerText = login.firstName;
    document.getElementById("lname").innerText = login.lastName;
    document.getElementById("email").innerText = login.email;
    document.getElementById("mobile").innerText = login.mobile;
}
function sublogin() {
    var subl = new loginM.sublogin();
    subl.firstName = "first in sub";
    subl.lastName = "last in sub";
    var nameresult = subl.fullName(); //.usegetName();
    // it means we extends sublogin from login(), we inherit all its property and methods, we set constructor
    //  constructor() {
    // super();
    //}
    //means we init constructor from login super class
    //then we can simply call super class method and property.
    //we know sublogin from login, but we do not have code here. 
    //this is allow us to hide the important super class 
    //we does not know login class
    //we simply use sublogin to extends it
    //we then can use super class method, 
    //this is dangeous.
    document.getElementById("subresult").innerText = nameresult;
}
//so far so good typescript basics are working
//import module programming tip
//import * as m from "TeaItem";  //need requirejs
//http://www.codeguru.com/csharp/.net/using-typescript-to-perform-crud-operations.htm
var TeaItemLoal;
(function (TeaItemLoal) {
    var Titem = (function () {
        function Titem() {
        }
        return Titem;
    }());
    TeaItemLoal.Titem = Titem;
})(TeaItemLoal || (TeaItemLoal = {}));
var IteMCRUD = (function () {
    function IteMCRUD() {
    }
    // constructor() { }
    IteMCRUD.SaveItem = function () {
        var itemlist = [];
        var ltem = new TeaItemLoal.Titem();
        ltem.desc = "item 22 for tea green";
        ltem.name = "item22";
        ltem.id = 22;
        itemlist.push(ltem);
        ltem.desc = "item 2 for tea green";
        ltem.name = "item2";
        ltem.id = 2;
        itemlist.push(ltem);
        return itemlist.length;
    };
    return IteMCRUD;
}());
function gtsaveitem() {
    alert(IteMCRUD.SaveItem());
    $("#result").text(IteMCRUD.SaveItem());
}
var slotlocal;
(function (slotlocal) {
    var Slot = (function () {
        function Slot() {
        }
        Slot.prototype.SelectSlot = function (callback) {
            $.ajax({
                method: "GET",
                url: "AllSlots"
            }).then(callback, function (err) {
                console.log(err);
            });
        };
        Slot.prototype.InsertSlot = function (id, name, desc) {
        };
        return Slot;
    }());
    slotlocal.Slot = Slot;
})(slotlocal || (slotlocal = {}));
function searchslot() {
    var slotall = new slotlocal.Slot();
    slotall.SelectSlot(callbacka);
}
function callbacka(data) {
    $.each(data, function (key, val) {
        var tableRow = '<tr>' +
            '<td>' + val.slotNo + '</td>' +
            '<td><input type="text" value="' + val.slotName + '"/></td>' +
            '<td><input type="text" value="' + val.description + '"/></td>' +
            '<td><input type="button" name="btnUpdate" value="Update" /> <input type="button" name="btnDelete" value="Delete" /></td>' +
            '</tr>';
        $('#customerTable').append(tableRow);
    });
}
//$("input[name='btnInsert']").click(function () {
//    var customerId = $("#txtCustomerId").val();
//    var companyName = $("#txtCompanyName").val();
//    var contactName = $("#txtContactName").val();
//    var country = $("#txtCountry").val();
//    var customer = new CustomerModule.Customer();
//    customer.CustomerID = customerId;
//    customer.CompanyName = companyName;
//    customer.ContactName = contactName;
//    customer.Country = country;
//    customer.Insert(function () {
//        $("#txtCustomerId").val('');
//        $("#txtCompanyName").val('');
//        $("#txtContactName").val('');
//        $("#txtCountry").val('');
//        alert('Customer Added !');
//    });
//});
//$("input[name='btnUpdate']").click(function () {
//    var cell;
//    var customerId = $(this).parent().parent().children().get(0).innerHTML;
//    cell = $(this).parent().parent().children().get(1);
//    var companyName = $(cell).find('input').val();
//    cell = $(this).parent().parent().children().get(2);
//    var contactName = $(cell).find('input').val();
//    cell = $(this).parent().parent().children().get(3);
//    var country = $(cell).find('input').val();
//    var customer = new CustomerModule.Customer();
//    customer.CustomerID = customerId;
//    customer.CompanyName = companyName;
//    customer.ContactName = contactName;
//    customer.Country = country;
//    customer.Update(function () {
//        alert('Customer Updated !');
//    });
//});
//$("input[name='btnDelete']").click(function () {
//    var customerId = $(this).parent().parent().children().get(0).innerHTML;
//    var data = '{"id":"' + customerId + '"}';
//    var row = $(this).parent().parent();
//    var customer = new CustomerModule.Customer();
//    customer.CustomerID = customerId;
//    customer.Delete(function () {
//        alert('Customer Deleted !');
//    });
//});
