
/// <reference path="../lib/ts/require.d.ts" />
/// <reference path="../lib/ts/jquery.d.ts" />
  
class TsTest{
    constructor(public firstName: string, public lastName: string) { }

    fullName()
    {
        return this.firstName + this.lastName;
    }

    static fullName0(): void {
       console.log("this.firstName + this.lastName");
    }

}

function getName()
{
    let tst = new TsTest("David ", "Huang ");
    document.getElementById("result").innerText = tst.fullName();
}

//$("#clickints").click(TsTest.fullName0); //not worked, because not the static class
//very important, class is not static, but method is, so we still can call it strightly
//simply function use jquery

class TsTeststaic {
    static fullName0(): void {
        console.log("this.firstName + this.lastName");
    }
}
$("#clickints").click(TsTeststaic.fullName0); //not worked, because not the static class

 
function getDate() {

    let date: Date = new Date(2016, 2, 11);
    $("#dater").text(date.toString());

}
//only module uses exports

 module loginM {
    export interface IPerson {
        firstName: string,
        lastName: string,
        email: string,
        mobile: string,
        fullName();
    }
    export class login implements IPerson {
        public firstName: string;
        public lastName: string;
        public email: string;
        public mobile: string;
        public fullName() {
            return this.firstName + "" + this.lastName;
        }
    }
    //oo programming language
    export class sublogin extends login {

        constructor() {  //declare module no constructor
            super();
        }

        //public usegetName() {
        //    return this.firstName + this.lastName+" in extends"; //call login parameters
        //}
    }
}

function getmodulelogin()
{
    let login = new loginM.login();
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

    let subl = new loginM.sublogin();

    subl.firstName = "first in sub";
    subl.lastName = "last in sub";

    let nameresult = subl.fullName();//.usegetName();

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
 module TeaItemLoal { //namespace
    export interface ItItem {
        id: number;
        name: string;
        desc: string;
    }

    export class Titem implements ItItem {
        id: number;
        name: string;
        desc: string;
    }
}
 class IteMCRUD {
   // constructor() { }
  static SaveItem()
    {
      let itemlist: TeaItemLoal.Titem[] = [];
      let ltem = new TeaItemLoal.Titem();
        ltem.desc = "item 22 for tea green";
        ltem.name = "item22";
        ltem.id = 22;
        itemlist.push(ltem);
        ltem.desc = "item 2 for tea green";
        ltem.name = "item2";
        ltem.id = 2;
        itemlist.push(ltem);
        return itemlist.length;
     }
}
function gtsaveitem() {
    alert(IteMCRUD.SaveItem());
    $("#result").text(IteMCRUD.SaveItem());
 }


module slotlocal
{
    interface Islot {
        id: number;
        name: string;
        desc: string;
        SelectSlot(callback: any);
        InsertSlot(id:number, name:string, desc:string);
    }
    export class Slot implements Islot {

        public id: number;
        public name: string;
        public desc: string;

        public SelectSlot(callback: any): void {
            $.ajax({
               method: "GET",
                url: "AllSlots"
            }).then(callback, function (err) {
                    console.log(err);
            });
        }
        public InsertSlot(id:number, name:string, desc:string) {

        }

    }
}

function searchslot() {
    let slotall = new slotlocal.Slot();
    slotall.SelectSlot(callbacka);
}

function callbacka(data)
{
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

