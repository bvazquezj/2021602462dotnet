// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Selector del menu, creo que se puede refactorizar pero esto funciona xD

$(document).on("ready", () => {
    console.log("jQuery está funcionando!");
});

const handlerClickItem = (id) => {
    console.log(id);
    $(".menuitem").removeClass("bg-gray-300");
    $(`#menu-item${id}`).addClass("bg-gray-300");
}

$("#menu-item1").on("click", () => handlerClickItem(1));
$("#menu-item2").on("click", () => handlerClickItem(2));
$("#menu-item3").on("click", () => handlerClickItem(3));
$("#menu-item4").on("click", () => handlerClickItem(4));
$("#menu-item5").on("click", () => handlerClickItem(5));
$("#menu-item6").on("click", () => handlerClickItem(6));


//
